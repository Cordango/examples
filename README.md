# Cordango examples

Four complete Cordango applications, as source you can clone, read, change and build.

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
```

## The examples

| Example | Entities | What it shows |
| --- | --- | --- |
| [`expenses/`](expenses) | 1 | The smallest complete application, and the one to read first. An approval lifecycle, and three roles that see genuinely different things. |
| [`time-off/`](time-off) | 1 | Absence requests with a manager approval step, a team schedule, and a duration the application works out for itself. |
| [`task-manager/`](task-manager) | 5 | Projects, tasks, subtasks and milestones, with rollups that let a parent count its children. Read this one for computed fields. |
| [`room-booking/`](room-booking) | 4 | Rooms, bookings, attendees, and a policy entity that holds the rules instead of hard-coding them. |

## Reading one

Start with `apps/<name>/app.cordango.yaml`, which names the application and fixes the order things
appear in. Then:

```
entities/           what the application stores, one file per entity
workflows/          lifecycles (a record's states and the moves between them) and automations
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
