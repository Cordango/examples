# Cordango examples

Fifteen complete Cordango applications, as source you can clone, read, change and build. Eight of
them come as three connected suites: apps that are useful on their own and do more together.

> Status: pre-alpha. These track the App Definition schema in
> [cordango/cordango](https://github.com/cordango/cordango) and will move while it settles.

Each directory is a real workspace, not a snippet. What you see is what an author writes: one file
per aggregate, an entity here, a lifecycle there, a role, a screen. No compiled App Definition is
checked in, because that is the output of the toolchain rather than the thing a person edits.

```
cd expenses
cordango check                        # parse, lower and validate. No model, no database.
cordango inspect                      # what is in here
cordango check --target standalone    # can this be generated as an application you own?

cordango build --target standalone --out generated --allow-incomplete
cd generated && docker compose up --build
```

`--allow-incomplete` is needed today and says so out loud. The standalone generator does not yet
emit workflows, computed fields or command guards, and rather than shipping an application that
looks finished it refuses the build until you say you know. Every gap is listed in the generated
README and recorded in `cordango.build.json`, so a partial build can never pass for a complete one
later.

Then open <http://localhost:8080>: the first screen asks you to create the administrator account.
There is no default password and none is generated for you.

Anything marked **Platform only** is the exception: it references a core app, or another app in its
workspace, and a reference between applications is something only Cordango Platform can resolve.
`cordango check --target standalone` refuses those outright rather than degrading, so they are read,
checked and published rather than built and run locally. Everything in the three connected suites is
in this category by construction.

## Documentation

**[docs.cordango.com](https://docs.cordango.com)** — the [quickstart](https://docs.cordango.com/quickstart),
[authoring](https://docs.cordango.com/guides/authoring), and the
[concepts](https://docs.cordango.com/concepts) these applications are written in.

## The connected starter pack

Eight apps in three workspaces. Each is worth having on its own; installed together they do things
none of them could do alone, and **not one of them was changed to make another one work**.

| Suite | Apps | What the suite is for |
| --- | --- | --- |
| [`finance/`](finance) | Budget & Spend Tracker, Purchase Requests, Vendor & SaaS Management | Deciding to spend money, agreeing to it, and knowing afterwards where it went |
| [`people/`](people) | PTO & Leave, Asset Register, 360 Feedback | Absence, equipment and reviews, all hanging off one personnel file and one calendar |
| [`operations/`](operations) | Project Intake & Portfolio, Resource Planning | Deciding what to do, then finding the people to do it |

Start with [`finance/`](finance): it is the shortest path to seeing what connects them.

### What "connected" means here

Three mechanisms, all of them ordinary authoring:

**A shared record.** Every app that deals with a company points at the same
`core_organizations.organization` row. A supplier's purchase requests, its contracts, its invoiced
spend and the laptops bought from it are four apps' answers about one record, and none of them keeps
its own spelling of the name.

**A reference into another app.** A purchase request names the budget line it comes out of. The
picker offers real lines; the chip opens the line where it lives; the line's related-apps panel lists
the requests pointing at it. Budget Tracker was not told any of this would happen.

**A subscription to what another app announces.** Budget Tracker reacts to a purchase reaching
`approved` by committing the money. Purchase Requests announces and stops there — it does not know
Budget Tracker exists, and adding a second listener changes nothing in it.

```
purchase approved  →  money committed against the line          (finance)
software bought    →  a contract opens with its renewal date    (finance)
hardware bought    →  it appears on the asset register          (finance → people)
leave approved     →  capacity drops in the plan                (people → operations)
project planned    →  something to staff appears                (operations)
project finished   →  the sponsor is asked how it went          (operations → people)
```

Each suite README has the full table and the source to read for it.

### One honest limitation

A purchase request cannot yet display "this would take Marketing over its plan", even though the
budget line it points at knows the number. Reading another app's field at display time is a cross-app
query, and it is planned rather than built — see
[`plan-connected-runtime-2026-09.md`](../../_docs/10-platform/plan-connected-runtime-2026-09.md).
References and reactions work today. Cross-app warnings do not, and nothing here pretends they do.

## The single-app examples

Read these for one idea each, without a suite around it.

| Example | Entities | What it shows |
| --- | --- | --- |
| [`expenses/`](expenses) | 1 | The smallest complete application, and the one to read first. An approval lifecycle, and three roles that see genuinely different things. |
| [`task-manager/`](task-manager) | 5 | Projects, tasks, subtasks and milestones, with rollups that let a parent count its children. Read this one for computed fields. |
| [`room-booking/`](room-booking) | 4 | Rooms, bookings, attendees, and a policy entity that holds the rules instead of hard-coding them. |
| [`budget-planner/`](budget-planner) | 16 | The largest by a distance, and the one that pushes the language hardest: scenarios, cohort growth, rollups across a window, and a running cash balance that reads the row before it. Read it for the calculation plane. |
| [`time-records/`](time-records) | 10 | Working time under German law — the recording every employer has owed since the Bundesarbeitsgericht decided it in 2022. Rules that are given from outside and cannot be argued with: a step function built without a branch, an eleven-hour rest read from the previous day, and a works-council role that the entity shapes make possible. **Platform only.** |
| [`time-and-leave/`](time-and-leave) | 16 | The same application plus holiday, sickness and a Gleitzeit account. Contracted hours joined by weekday through a one-value window, and a balance that carries itself from month to month. **Platform only.** |
| [`crm/`](crm) | 8 | Leads, a deal pipeline and activities, plus intake forms somebody builds inside the application — the questions are records, and the form declares what a submission FILES and which lead field each answer lands in. Read it for the forms archetype, and for linking a company to Organizations instead of keeping a second copy. **Platform only.** |

Every app in the three suites is **Platform only**: they reference core apps and each other, and a
reference between applications is something only Cordango Platform can resolve.
`cordango check --target standalone` refuses them outright rather than degrading.

## Reading one

Start with `apps/<name>/app.cordango.yaml`, which names the application, states its `purpose` and the
apps it `uses`, and fixes the order things appear in. Then:

```
entities/           what the application stores, one file per entity
workflows/          lifecycles (a record's states and the moves between them), and automations —
                    including the ones that subscribe to another app's events
roles/              who may read, write and run what
views/collections/  a table, board or calendar over one entity
views/screens/      a page, assembled from views and blocks
views/entities/     one record's detail, peek and form
```

## Contributing

An improvement here reaches everyone using the example, in either environment. Clearer labels, more
realistic permissions, a state a lifecycle is missing, a screen that reads better: all welcome.

Two things to check before opening a pull request:

```
cordango check --app <name>
cordango check --app <name> --target standalone
```

The first asks whether it is a valid Cordango application. The second asks the narrower question of
whether the standalone generator can also build it. A standalone build is one application, so
anything that depends on other installed applications is reported rather than silently dropped.

## Where these came from

Each was imported from the conformance corpus in the compiler repository using `cordango import`,
which turns an App Definition back into source. That corpus stays where it is, because the test
suite is pinned to it and it must not drift. These copies are the ones meant for people to read,
and they are allowed to grow.

## License

Apache-2.0. See [LICENSE](LICENSE).
