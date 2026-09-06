# Operations

A Cordango workspace. One repository is one installation: the apps under `apps/` share
People, Organizations and Calendar, and they are built, reviewed and deployed together.

```
cordango check        # parse, lower and validate every app
cordango inspect      # what is in here
cordango configure    # where these apps run. Asked once, committed
cordango build        # do what configure said
```

A generated application lands in `generated/<app>/` — an api, a web front end and a
Dockerfile, yours to run. It is build output and gitignored: everything in it comes from the
source under `apps/`. Move the directory out when it is ready to have a life of its own.

The first app is in [`apps/project-intake`](apps/project-intake). Change it or delete it.

**One workspace holds many apps.** `cordango add app <name>` adds another under `apps/`; they
share People, Organizations and Calendar, reference each other's records, and are checked,
built and deployed together. `cordango new` is only for creating the workspace itself.

**People, Organizations and Calendar are already there.** They are core apps the platform
provides — `core_people`, `core_organizations`, `core_calendar`. Point at them instead of
declaring your own company or contact entity:

```
cordango inspect                          # lists them, with their entity keys
cordango vocabulary core organizations    # what one of them holds
```

A field pointing at one is `type: reference` with `targetApp: core_organizations` and
`target: organization`. Nothing about this needs an instance or a login.

Semantic source lives in `.cordango.yaml` files, one per aggregate — an entity, a lifecycle, a role, a
screen. They are the source of truth. `.cordango/` (with the leading dot) is build output and is
not committed.
