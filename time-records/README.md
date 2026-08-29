# Zeiterfassung

Record working time the way German law requires it, and prove the limits were kept.

Since the Bundesarbeitsgericht decided on 13 September 2022 (1 ABR 22/21, following the European
Court of Justice in *CCOO*, C-55/18) every German employer must operate a system that records the
**whole** working time of every employee — not only the hours beyond eight that § 16 Abs. 2 ArbZG
has always required. This application is that system: a clock for Kommen and Gehen, a way to enter
a forgotten day, and, on top of the record, the arithmetic of §§ 3 to 6 and 9 to 11 ArbZG plus the
recording and retention duties of § 17 MiLoG.

It is the other kind of application from the rest of this repository. Everywhere else the rules are
somebody's preference and can be argued with. Here they are given from outside, they are not
negotiable, and the app's whole job is to be right about them.

## What to look at

**`entities/work_day.cordango.yaml`** is where the law lives. Every rule in §§ 3 to 6 has the same
two reference points — this day and the day before it — so the entity is declared a `series`
partitioned by worker and ordered by date, and the eleven-hour rest of § 5 becomes one subtraction:

```yaml
rest_minutes:
  computed:
    expr: first_start_minute + 1440 - prev(last_end_minute, -9999)
```

That reads as arithmetic only because of the convention in `time_entry`: minutes are counted from
midnight **of the day the booking belongs to**, so a shift ending at 02:00 the next morning is 1560
rather than 120, and the subtraction is still right.

Exactly one of those numbers is ever typed. `start_minute` is entered — the Gehen button asks for
it — and everything else is measured off the `datetime` pair the law wants recorded anyway:

```yaml
gross_minutes:  { computed: { expr: minutes_between(started_at, ended_at) } }
end_minute:     { computed: { expr: start_minute + gross_minutes } }
```

So the duration can never disagree with the timestamps, and the past-midnight convention falls out
of the addition instead of having to be maintained by hand.

**The break rule of § 4 has no `if`.** Thirty minutes are due after six hours and forty-five after
nine, and an expression cannot branch, so a multiplier turns the ramp into a step:

```yaml
required_break_minutes:
  computed:
    expr: min(30, max(0, worked_minutes - 360) * 30) + min(15, max(0, worked_minutes - 540) * 15)
```

360 gives 0, 361 gives 30, 540 gives 30, 541 gives 45. It is exact because the minutes are integers.
The same trick in `time_entry.qualifying_gap_minutes` discards interruptions under fifteen minutes,
which is what makes a gap between two bookings a Ruhepause in the sense of § 4 Satz 2 rather than
just a gap.

**Verstöße are raised the moment a limit is crossed.** One automation per paragraph, each keyed on
`field.changed` over a *computed* boolean — `rest_ok`, `daily_ok`, `break_ok`, `stretch_ok`,
`sunday_or_holiday_work` — writing through `createForEach` with `key: [work_day, rule]`, so a rule
that stays broken never files a second row. There is no per-effect `when:` in the language, which is
why it is five files rather than one with five guarded effects.

**Who wrote a row is never asked for.** `recorded_by` on a Zeitbuchung and `punched_by` on a
Stempelung are filled by the runtime, because of their names: a person reference called `owner` or
`requested_by`, or ending in `_by`, gets a before-create hook that sets it to the signed-in person.
Trying to say the same thing with `default: '{{actor.id}}'` is refused, and the refusal names the
convention. Nothing stamps these fields in `workflows/` — there is nothing to stamp.

It only reaches *person* references, though, which is why a Zeitbuchung still asks which
Mitarbeiterprofil it belongs to. The profile is not a person; it points at one. Filling that from
the signed-in user would mean resolving "the profile whose `account` is the actor", and the language
has no way to say it. Worth knowing before you copy the pattern: the convention fires on the field
NAME without checking the target, so calling a non-person reference `owner` makes the generator
write a person id into it — it compiles, it passes `cordango check`, and it is wrong.

**`entities/violation.cordango.yaml` is the reason the model is shaped the way it is.** Grants are
per entity and there is no row-level rule, so the only way to give the Betriebsrat what § 80 Abs. 1
Nr. 1 BetrVG entitles it to — the means to check that the law is kept — without handing it a
surveillance tool is to put the aggregates on entities of their own. `work_week`,
`averaging_window` and `violation` exist so that `roles/works_council.cordango.yaml` can grant read
on those and name `time_entry`, `time_punch` and `work_day` nowhere at all.

**The weekday is stamped, not derived, and that shapes the setup.** `cordango vocabulary` offers
exactly three date functions — `minutes_between`, `hours_between`, `days_between` — all of which
return numbers. There is no weekday function, no date part, no modulo and no way to build a date, so
nothing can work out that a given date is a Sunday. The only tool that can put a number on a day is
`createForEach` over a `range`, whose `{{source.index}}` counts 1, 2, 3…

So the count has to start on a Monday. `worker_profile.recording_start` is that Monday: creating a
profile lays out `recording_weeks` weeks from it, and each week lays out seven days where
`{{source.index}}` is 1 to 7 by construction. **A Mitarbeiterprofil is the only thing anybody
creates** — the weeks and days follow, and `key:` makes both generators safe to run again.

The cost is stated plainly on the field: if `recording_start` is not a Monday, that person's whole
weekday numbering is shifted and every Sunday and Werktag check is quietly wrong. An earlier draft
avoided this with a Kalenderjahr entity holding one ISO anchor for everyone, but it made a human
create a year, then a week, then a work week before a single day existed — three levels of
scaffolding in the way of the thing they came to do. One field with a clear label is the better
trade.

Public holidays are Landesrecht, so they join on *(date, Bundesland)*. Rollup filters take literal
values only, so the Bundesland cannot be a filter — it is the `match:` key, and `work_day` carries a
denormalised `bundesland` reference for it to match on.

## Running it

**This application runs on Cordango Platform, not standalone.** It references the People core app
for the personnel file, and cross-application references are a Platform feature — `--target
standalone` refuses with CORD2100 rather than degrading, so there is no `generated/` directory here.

```
cordango check --app time_records
cordango login <token>
cordango publish --app time_records
```

Then, in the running app: add the Bundesländer and their holidays under Konfiguration, and create
one Mitarbeiterprofil against an existing employee with a **Monday** as its Aufzeichnungsbeginn. The
weeks and the days appear on their own; nobody creates either by hand.

## Why Platform and not standalone

`worker_profile.employee` points at `core_people.employee`. A person's job title, department,
manager and start date already have a canonical record, and the one mistake this layout cannot undo
later is a second copy of them — so the profile carries only what the time law needs: the
Bundesland of the place of work, the § 3 and § 5 thresholds, the MiLoG sector, the night-time
boundaries, and the basis of any deviation under § 7 ArbZG.

`account` is a separate reference to the platform's own `person`, because `{{actor.id}}` means the
signed-in person rather than their personnel file. The "my days" views and the reminders hang off
that one; everything to do with employment hangs off the other.

Choosing this costs the standalone build outright. Two other Platform features the application uses
and would have lost anyway:

- **The audit trail.** `kind: history` on the Arbeitstag and the Zeitbuchung. Alongside it,
  `time_punch` keeps the raw Kommen/Gehen log, never corrected, so a change to a booking cannot
  quietly rewrite what was actually pressed — an organisational answer, not a technical guarantee,
  because nothing in the language makes a row immutable.
- **`min`, `max` and `avg` rollups**, which the dotnet-vue generator does not write. They carry
  `first_start_minute` and `last_end_minute` and hence the § 5 rest check, `longest_stretch_minutes`
  and hence § 4 Satz 2, and `avg_daily_minutes`. The § 3 averaging test is shipped twice regardless
  — the literal average *and* a `budget_minutes` / `overrun_minutes` pair built from `sum` and
  `count`, which is what the alerts use and the more readable figure anyway.

## What the language still cannot do

- **Deletion.** There is no `deleteRecord` effect, so the two-year retention of § 16 Abs. 2 ArbZG
  and § 17 Abs. 1 MiLoG can be *flagged* but not carried out. `flag_retention_due` marks the days
  and `anonymise_worker` overwrites the free-text fields of a profile. That is pseudonymisation, not
  erasure, and it belongs in the Verzeichnis von Verarbeitungstätigkeiten described as such.
- **Overlapping bookings.** `unique` is a column constraint and `overlaps` exists only in
  conditions, so two bookings that overlap on the same day can be flagged afterwards but not
  refused at write time.

One thing worth reporting rather than working around: `minutes_between(<date>, <datetime>)` passes
`cordango check` but the dotnet-vue generator emits C# that will not compile, because it hands a
`DateOnly` to a parameter typed `DateTimeOffset?`. Deriving the minute of day straight from the
booking's date is the obvious way to write this entity and it is the reason `start_minute` is typed
rather than computed. Both arguments being `datetime` is fine, which is what `gross_minutes` uses.

## Deliberately not modelled

Jugendarbeitsschutzgesetz, MuSchG §§ 4 and 5, and § 7 ArbZG deviations as a rule engine. The
exceptions of § 14 (außergewöhnliche Fälle) and § 7 (Tarifvertrag) appear instead as
`accept_violation`, which will not close a Verstoß without a written justification, alongside
`worker_profile.deviation_basis`. The exception is documented rather than defined away.

## Structure

```
apps/time-records/
  app.cordango.yaml
  entities/         10 — work_day and time_entry carry the arithmetic
  workflows/
    lifecycles/     time_entry_flow, work_day_flow, violation_flow
    actions/        start_clock, assign_replacement_rest, anonymise_worker
    automations/    the week and day generators, one rule-detector per paragraph, the MiLoG
                    and retention flags
  roles/            employee, manager, hr_admin, payroll, works_council, auditor
  views/
    collections/    tables and the Verstöße board
    screens/        Stempeluhr, Meine Zeiten, Wochennachweis, Teamzeiten, Verstöße,
                    Arbeitszeitgesetz, Nachweise, Betriebsrat, and three configuration pages
                    (Mitarbeiterprofile, Feiertage, Einstellungen)
    entities/       detail, form and the Arbeitstag's four tabs
```

Labels are German because the law is; keys, this README and the notes for agents are English,
like the rest of the repository.

`time-and-leave/` next door is this application plus absences, leave and a Gleitzeit account.
