# Showcase

This is a Cordango workspace, and it is **not one to build in**. It exists to be read.

Every other workspace here is an honest application that uses the handful of surfaces its own job
needs. This one contains a real, working instance of every block kind the runtime can draw — 42 of
the 44 that exist — so it is the place to look when you know what you want and not how it is
written.

## Use it to answer "how is this built"

```
cordango inspect --app showcase          what the app holds
cordango example <kind>                  the same examples, extracted from here
```

`cordango vocabulary block gantt` tells you what a gantt ACCEPTS. This workspace tells you what a
correct one looks like in place, with the rest of a page around it. Reach for it whenever a
construct is new to you, and whenever `cordango check` rejects one.

`README.md` has an index: which file to open for a roadmap, a composed grid, a computed field, an
automation, a public link.

## Which half you are reading

Cord models the domain. It does **not** model screens — its screen vocabulary is deliberately
narrow, because the alternative was putting the block catalogue's forty-odd kinds into the
authoring prefix.

- `entities/`, `roles/`, `workflows/` — **Cord**. Read these to learn the language.
- `views/` — **preserved screens**: App Definition block trees written as YAML and handed back
  unchanged at build time. Read these to learn the block language underneath Cord.

Copying a block out of `views/` into your own app works, and is the point. Copying the *shape* of
`entities/` into your own app is how you learn to write Cord.

## Do not edit it to try something out

This source is the origin of two published things: `catalog/definitions/showcase.appdef.json`, the
template a workspace installs, and the worked examples `cordango example` serves. The round trip is
exact and a drift check enforces it, so an edit here changes what other people are shown.

Make a new app for experiments:

```
cordango add app scratch
```
