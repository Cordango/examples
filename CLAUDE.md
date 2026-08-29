# CordangoTesting

This is a Cordango workspace. The apps under `apps/` are defined by **semantic source** —
`.cordango.yaml` files describing entities, workflows, roles and screens. `cordango` turns them into a
running application.

## Before you change anything

```
cordango inspect                        # the apps in this workspace
cordango inspect --app <key>            # one app
cordango inspect entities/claim --app <key>   # one aggregate
```

Inspect the aggregate you are about to touch. Do not read every file in the repository — the
point of this layout is that you never need to.

## Finding out what may be written

```
cordango vocabulary                     the words, and what exists
cordango vocabulary field               a field's properties and its types
cordango vocabulary block calendar      what a calendar block accepts
cordango vocabulary core organizations  what a core app holds
```

**Never read the `cordango` binary, and never go looking for the App Definition schema.** If
`cordango vocabulary` cannot answer your question, that is a missing feature in cordango — say so
rather than working around it.

## Some things already exist — link to them

People, Organizations and Calendar are **core apps** the platform provides to every
workspace. They are not in this repository and you will not find them by reading files, so
`cordango inspect` lists them for you along with their entity keys.

Before declaring an entity, check whether it is one of these. A company, a customer, a
supplier, an employee, a calendar event: these already have a canonical record, and a second
copy is the one mistake this layout cannot undo for you later. To point at one:

```
type: reference   targetApp: core_organizations   target: organization
```

The entity key is not always the label — Organizations calls `organization` a "Company".
`cordango vocabulary core organizations` gives you both, plus every field it already carries.

This workspace has two halves and it is worth knowing which one you are in:

- `entities/`, `workflows/`, `roles/` are **semantic**. Small vocabulary, listed under
  `cordango vocabulary` with no arguments. You will rarely need more than that.
- `views/` are still **App Definition block trees** — screens are not modelled semantically
  yet. That is where `cordango vocabulary block <kind>` earns its place. Ask for one kind at a
  time; asking for everything defeats the point of the layer.

## Changing an app

Two ways, both supported:

**Semantic operations** (preferred for structural change):

```
cordango apply ops.json --app <key> --scope domain
cordango apply - --app <key> --scope screen:claims     # ops on stdin
```

`--scope` names the one aggregate the change may touch. Kinds: `domain`, `behaviour`,
`access`, `screen:<key>`, `tab:<screen>/<tab>`. An operation naming anything else is refused
and **no file is written** — so a rejected attempt costs nothing and needs no cleanup.
Add `--dry-run` to see which files a change would rewrite.

**Editing `.cordango.yaml` files directly** is fine too. They describe what a thing IS, not
the steps that built it, and both paths assemble into the same model.

## After you change anything

```
cordango check --app <key>
```

Two outcomes that are not the same thing:

- **not coherent** — the app does not hold together. This is an error. Fix it.
- **coherent, incomplete** — it holds together but is not a finished application yet (no
  screen to land on, nothing to create). Normal mid-build. Keep going.

`cordango check` never calls a model and never touches a database. Run it freely.

## Rules

- **Never edit `.cordango/`** (the dotted directory). It is build output; your changes vanish on
  the next `cordango build`.
- **Use stable keys.** Renaming an entity or field key is not supported yet, because nothing
  rewrites the references to it. Add and remove instead, deliberately.
- **One aggregate per change.** It keeps diffs reviewable and is what `--scope` enforces.
- **You do not accept your own work.** `cordango apply` writes to the working tree; a person
  reads `git diff` and stages it. Do not commit unless you are asked to.
- Run `cordango fmt` if you hand-edited a file and the formatting drifted.
