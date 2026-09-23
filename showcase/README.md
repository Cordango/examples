# Showcase

One app whose subject is the platform it runs on. Every block kind the runtime can draw has a
record in its catalogue and a real instance on one of its pages, so the list of what is possible
and the demonstration of it are the same document.

```
cordango check
cordango inspect --app showcase
```

| | |
| --- | --- |
| Entities | 24 |
| Pages | 16 |
| Commands | 10 · Processes 2 · Automations 7 · Roles 3 |
| Block kinds drawn | 42 of the 44 that exist |

## Why it is in this repository

**It is the only workspace here that contains every block kind, in readable YAML.** The other
examples are honest applications that use the handful of surfaces their own job needs. This one
exists to be read: if you need to know how a gantt, a composed grid, an org chart, a master-detail
queue or a public share link is actually written, it is in here as working source rather than as a
snippet somebody typed into a document.

It is also where `cordango example <kind>` gets its answers — the examples that command serves are
extracted from this app, so a block that stops being correct here stops being served there.

## Read it in this order

| Want to know how to… | Look at |
| --- | --- |
| lay a page out at all | `views/screens/overview.cordango.yaml` |
| build a record's detail page | `views/entities/capability/detail.cordango.yaml` and its `tabs/` |
| draw a roadmap or a schedule | `views/screens/roadmap.cordango.yaml`, `sessions.cordango.yaml` |
| build a grid whose cells are records | `views/screens/readiness.cordango.yaml` |
| make something colourful | `views/screens/styling.cordango.yaml` |
| wire up buttons, dialogs and inline edit | `views/screens/interactivity.cordango.yaml` |
| write a computed field or a rollup | `entities/capability.cordango.yaml`, `entities/adoption.cordango.yaml` |
| react to something happening | `workflows/automations/` |
| move a record through states | `workflows/lifecycles/capability_lifecycle.cordango.yaml` |

## A word about the screens

Cord models the domain. It does **not** model screens: its screen vocabulary is deliberately narrow
(`list`, `metric`, `chart`, `text`, `split` over four view types), because the alternative was
putting the block catalogue's forty-odd kinds into the authoring prefix.

So the files under `views/` carry **preserved screens** — App Definition block trees, written out
as YAML and handed back unchanged at build time. That is why this app can exist here at all, and it
is worth knowing which half you are reading: `entities/`, `roles/` and `workflows/` are Cord, and
`views/` is the block language underneath it.

The round trip is exact. This source rebuilds the shipped definition byte for byte, which is what
lets the platform generate `catalog/definitions/showcase.appdef.json` from here rather than keeping
a second copy by hand.

## What it deliberately does not draw

Two of the 42 kinds, both for the same sort of reason:

- **`externalEmbed`** loads a third party's code into the browser of whoever opens the record. It
  needs a provider a platform admin has approved and a viewer who has consented in their own
  browser, so an example app that demonstrated it would be shipping the approval.
- **`widgets`** is deprecated at authoring: still rendered so older definitions keep working,
  absent from the vocabulary so nothing new emits one.
