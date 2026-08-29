# Zeiterfassung und Abwesenheiten

Everything `time-records/` does, plus the other half of the answer: what somebody is owed when they
are not working.

The statutory recording next door is complete on its own — it will tell you that Tuesday ran to
nine and a half hours and that the rest before Wednesday was too short. What it cannot tell you is
whether Tuesday was supposed to be a working day at all, or what the month comes to against the
contract. That needs a Sollzeit, and once there is a Sollzeit there has to be somewhere for holiday,
sickness and Freizeitausgleich to go. This workspace adds six entities and leaves the other ten
exactly as they were.

Read `time-records/README.md` first — the arithmetic of §§ 3 to 6 and 9 to 11 ArbZG is explained
there and is unchanged here.

## What this one adds

**`entities/schedule_day.cordango.yaml` and the weekday join.** Contracted hours are seven rows per
employee, one per weekday, and an Arbeitstag finds its own by a rollup window one number wide:

```yaml
target_minutes:
  computed:
    rollup:
      entity: schedule_day
      via: worker
      match: worker
      op: sum
      field: target_minutes
      window:
        at: weekday_number
        within: {from: weekday_number, to: weekday_number}
```

Window bounds may be numbers as readily as dates, so a degenerate one-value window is an equality
join — which is what picks the single row whose weekday matches. No branch, no seven columns.

The `weeklyHours` control that `json` fields offer would be a nicer thing to fill in, but an
expression cannot read JSON, so the rows are the machine-readable truth and the mask is not used.

**`entities/time_account.cordango.yaml` is a Gleitzeitkonto, and it is four lines.** The entity is a
`series` partitioned by worker and ordered by month, so the balance carries itself forward:

```yaml
closing_balance:
  computed:
    expr: prev(closing_balance, opening_balance) + movement_minutes
```

Only the first month of a series needs an opening balance. Every later month finds its own by
looking at the one before it, and editing a month halfway up the series recomputes the rest.

**`entities/absence.cordango.yaml` shows what `initial:` is for.** Whether an absence eats into the
holiday entitlement is a property of its *type*, but a rollup filter compares literals and cannot
follow a reference. So the flag is copied down at creation, by a guard that hops one relation:

```yaml
leave_relevant:
  type: boolean
  default: false
  initial:
    - when: {path: absence_type.deducts_leave, operator: eq, value: true}
      value: true
```

`leave_entitlement.taken_days` can then filter on a plain boolean of its own, and the § 3 BUrlG
arithmetic works. The same trick sets the eleven-hour rest exception in `worker_profile` next door.

**`absence.requested_by` is not on the form at all.** The name is the whole mechanism — a person
reference called `owner` or `requested_by`, or ending in `_by`, is filled with the signed-in person
before the record is written. An Abwesenheit is requested by whoever is looking at the screen, so
asking would be a question with one possible answer.

`absence.day_count` is `days_between(start_date, end_date) + 1`, and `expand_absence_days` uses it
as the row count of a `createForEach` range, resolving each day's Arbeitstag with a `pick`. Give the
effect a `key` of `[absence, absence_date]` and re-running it is harmless.

## Running it

Cordango Platform, for the same reason as `time-records` — `worker_profile.employee` points at
`core_people.employee`, and a cross-application reference is a Platform feature.

```
cordango check --app time_and_leave
cordango login <token>
cordango publish --app time_and_leave
```

Set up as for `time-records` — the Bundesländer and their holidays, then a Mitarbeiterprofil
against an existing employee whose Aufzeichnungsbeginn is a Monday — then add the seven Sollzeit rows on the profile,
the Abwesenheitsarten, and a Urlaubsanspruch for the year.

## What the language still cannot do

The same list as `time-records/`, unchanged: deletion and overlap prevention. Nothing added here
makes it longer — the Sollzeit join, the Gleitzeit series and the absence expansion are all `sum`,
`count`, `prev()` and `createForEach`.

One thing this application deliberately does *not* take from the core app: `core_people.employee`
carries a `weekly_hours` figure, and it is not used. A single weekly number cannot say which days
those hours fall on, and § 3 ArbZG counts werktäglich — so the Sollzeit is seven `schedule_day`
rows instead, and the core field is left to mean what it means in the personnel file.

## Structure

```
apps/time-and-leave/
  entities/         16 — the ten of time-records plus:
                    schedule_day, absence_type, absence, absence_day,
                    leave_entitlement, time_account
  workflows/
    lifecycles/     + absence_flow
    automations/    + expand_absence_days, require_au_certificate, warn_leave_carryover
  views/
    screens/        + Abwesenheiten, Abwesenheitskalender, Urlaubskonto, Arbeitszeitkonto
```

`work_day` gains five fields — `absence_minutes`, `absence_count`, `is_absent`, `target_minutes`
and `balance_minutes`. Everything else it carries is identical to the smaller application, on
purpose: the two are meant to be read side by side.
