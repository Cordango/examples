# Operations

Two apps about work before it starts: deciding whether to do it, and finding the people to do it.

```
cordango check
cordango discover
```

| App | What it owns | Entities |
| --- | --- | --- |
| [`project-intake`](apps/project-intake) | Proposals, their priority score, the committee decision, and the milestones after it | 4 |
| [`resource-planning`](apps/resource-planning) | Plannable capacity, skills, allocations, and where a project is short of people | 7 |

## The loop between them

```
project.planned          →  a project to staff appears in Resource Planning
                            (name, manager, dates and priority carried over)
project.staffed          →  Project Intake can start it
leave.approved           →  capacity drops, without a planner hearing about it
project.completed        →  360 Feedback asks the sponsor how it went
project.approved         →  Budget Tracker commits the estimate
```

Two of those cross into other workspaces, and the difference is worth understanding.

## References and subscriptions are not interchangeable

`resource_planning.planned_project` mirrors an intake project by NAME rather than pointing at it:

```yaml
intake_ref:
  type: text
  help: The proposal this came from, by title.
```

That is not a modelling preference. A reference has to resolve to a real table, so the two apps must
be installed together for one to exist. A subscription only needs the other app to be reachable *when
it fires* — an app can subscribe to something that is not installed yet, and the runtime simply never
delivers it. Since these suites are separate workspaces, cross-suite links are subscriptions plus a
text reference, and `cordango check` says so:

```
note: 'time_off' is not in this workspace, so nothing it announces arrives here yet
```

That note is the honest state of affairs, not a warning to silence. Install the suites into one
tenant and the subscription starts firing with nothing changed. Slice Q of the runtime plan is what
eventually retires the mirror.

## Three things worth reading the source for

**A priority score is arithmetic over claims, and does not pretend otherwise.** Impact, urgency and
alignment are scored one to five by the proposer; cost and effort are put into five bands by an
`initial` rule. The score subtracts one from the other. That does not make it objective — it makes
two proposals comparable, and moves the argument to the inputs, which is where a committee is useful.

There is no High/Medium/Low band on it, deliberately. A label would hide the disagreement the
committee exists to have, and there is no `if/then` in an expression anyway.

**The person who asks for an allocation cannot confirm it.** `confirm_allocation` carries
`when: requested_by neq {{actor.id}}`. Without it a plan is written by whoever asked loudest.

**Over-allocation is caught afterwards, not prevented.** A guard cannot compare two values on the
same record, so "these hours exceed their free hours" is unwritable. What exists instead is an
automation that fires when an allocation lands and the person's utilisation has gone over 100. That
is the honest shape of the constraint, and the field comments say so rather than implying a check
that is not there.
