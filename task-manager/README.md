# Task manager

Projects hold tasks. Tasks hold sections and comments. Milestones group work by date.

Five entities, which makes this the one to read for how records relate to each other and how a
parent learns about its children.

## What to look at

**`entities/milestone.cordango.yaml`** is the reason to start here. A milestone does not store how
many tasks it has, or how many are still open, or what they cost. It declares five rollups over the
tasks that point at it: a count, the same count filtered to unfinished work, the same filtered the
other way, and sums of effort and cost.

```yaml
open_tasks:
  label: Open tasks
  type: integer
  computed:
    rollup:
      entity: task
      via: milestone
      op: count
      filters:
        - field: status
          operator: neq
          value: done
```

Nothing recalculates that by hand and nothing can forget to. Move a task to another milestone and
both milestones settle, because the relationship is declared rather than maintained.

**`entities/task_section.cordango.yaml`** is how a checklist inside a task works without inventing
a second concept for it.

**`views/screens/projects.cordango.yaml`** is the largest screen in these examples. Read it after
the smaller ones.

## Running it

```
cordango check                        # is this a valid Cordango application?
cordango check --target standalone    # can the standalone generator build it?

cordango build --target standalone --out generated --allow-incomplete
cd generated && docker compose up --build
```

Then <http://localhost:8080>. The first screen asks you to create the administrator account.

`--allow-incomplete` is needed while the standalone generator is still missing workflows, computed
fields and command guards. It lists what it left out rather than shipping an application that looks
finished.

## Structure

```
cordango.yaml                the workspace: which applications are installed
apps/task-manager/
  app.cordango.yaml          the application's name, and the order things appear in
  entities/                  what it stores
  workflows/                 lifecycles and automations
  roles/                     who may do what
  views/                     collections, screens, and one record's detail
```
