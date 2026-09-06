# People

Three apps about the people who work here. They connect through two things neither of them owns: the
People core app, which holds the personnel file, and the shared calendar.

```
cordango check
cordango discover
```

| App | What it owns | Entities |
| --- | --- | --- |
| [`time-off`](apps/time-off) | Leave requests, approval, yearly allowances and the balance left | 4 |
| [`assets`](apps/assets) | What the company owns, who holds it, what it has cost, when to replace it | 5 |
| [`feedback`](apps/feedback) | Review cycles: self, manager and peer feedback, and what it averaged to | 6 |

## Connecting without copying

None of these three declares an employee. The personnel file lives in `core_people`, and each app
holds only what it needs on top:

```yaml
employee:
  type: reference
  targetApp: core_people
  targetEntity: employee
```

A leave allowance links to it for the contract facts. A planning profile in `operations/` links to
the same row. Nobody re-types a job title, and nobody has two spellings of the same person.

**The calendar is the other shared surface.** Four entities across this suite carry `calendar: true`
— an approved absence, a blackout period, an asset's replacement date, a feedback deadline. Each
lands in the calendar of whoever is responsible for it, beside their meetings and, if `operations/`
is installed, their project milestones. No app was told about any of the others.

An entity on the calendar has to name a person. `public_holiday` deliberately does not carry
`calendar: true` for exactly that reason: a public holiday is nobody's, and a calendar entry with
nobody responsible for it cannot land anywhere.

## What reacts to what

| When this happens | This app reacts | Mechanism |
| --- | --- | --- |
| hardware or equipment is bought (in `finance/`) | Asset Register puts it on the register with its cost, supplier and warranty | subscription |
| a project finishes (in `operations/`) | 360 Feedback asks the sponsor how it went | subscription |
| leave is approved | Resource Planning (in `operations/`) takes the person out of the plan | subscription, declared there |

## Three things worth reading the source for

**Anonymity is a grant, not a label.** `review_cycle.anonymous_peers` says peer feedback is
anonymous, and the field is not what makes it true. The `employee` role has no `read` on
`feedback_request` or `feedback_score` at all — only on the `participant` averages. A flag saying
"anonymous" over data the subject can read is a promise the app does not keep.

**A balance is summed, never typed.** `leave_allowance.taken_days` rolls up the approved absences
that name it. It cannot roll up through the platform directory — a sibling rollup needs both sides to
point at a local entity — so the absence carries an explicit `allowance` link, filled in by a `pick`
when the request is made. The constraint is real and the model is better for it: a day off is taken
*from* a year's entitlement.

**Working days are not expressible, and the field says so.** `day_count` is
`days_between(start_date, end_date) + 1` — calendar days. There is no weekday-aware count in an
expression, and a number that quietly skipped weekends in one country and not another would be worse
than an honest one.
