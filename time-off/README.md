# Time off

Somebody requests time off. Their manager approves or declines it. A calendar shows who is away.

One entity, three roles, three screens. Read it after `expenses` to see how differently the same
shape can be presented.

## What to look at

**`views/screens/schedule.cordango.yaml`** is the interesting file, and the reason to read this one.
It is a calendar, and a calendar needs to know which month it is showing:

```yaml
screen: schedule
label: Who's off
state:
  - key: cursor
    type: date
    default: '{{today}}'
```

That `state` block is screen state. The cursor belongs to the page rather than to any record, it
starts at today, and moving it is what pages the calendar back and forth. It is the mechanism behind
every screen that has a position of its own, and this is the smallest example of it.

**`workflows/lifecycles/approval.cordango.yaml`** is deliberately shorter than the expenses one.
Approval is a decision with two outcomes, and modelling it with six states would describe the
software rather than the business.

**`roles/`** shows the same three-way split as `expenses` over completely different subject matter,
which is worth comparing: an employee sees their own requests, a manager sees the queue, an admin
sees everything.

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
cordango.yaml              the workspace: which applications are installed
apps/time-off/
  app.cordango.yaml        the application's name, and the order things appear in
  entities/                what it stores
  workflows/               lifecycles and automations
  roles/                   who may do what
  views/                   collections, screens, and one record's detail
```
