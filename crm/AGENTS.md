# Crm

This is a Cordango workspace. The apps under `apps/` are defined by **semantic source** —
`.cordango.yaml` files describing entities, workflows, roles and screens. `cordango` turns them into a
running application.

## Start with questions, not with an entity

The person asking for an app knows their work and does not know this language. You know the
language and nothing about their work. Almost every bad generated app comes from closing that
gap by guessing.

So before you write a single entity, ask. Not a questionnaire — two or three questions, then
say back what you heard and ask the next two. Keep going until you can answer these yourself:

- **What is the record?** The thing there are many of, that somebody counts at the end of the
  month. A claim, a shift, a deal, a machine. Most apps have one, plus the things that hang
  off it.
- **Who touches it, and what may each of them do?** "Everyone" is an answer that produces an
  app with no roles and no reason to sign in. Ask for the job titles.
- **What states does it move through, and what moves it?** Draft, submitted, approved is a
  workflow. Who presses the button, and what has to be true before they may?
- **What does somebody open first thing in the morning?** That is the home screen. If nobody
  can name it, the app does not yet have a landing page worth building.
- **What must never happen?** An approval by the person who filed it. Two bookings on one
  room. These become guards, not a paragraph in a README.
- **What already exists?** Before modelling a company, a person or a calendar event, read
  "Some things already exist" below — and then ask which of theirs is one of those.
- **What gets counted?** Anything totalled, averaged, aged or ranked is a computed field or a
  rollup, and both are far easier to model at the start than to add later over a shape that
  assumed nobody would ask.

Ask about the WORK, never about the schema. "Does a claim ever get reopened after it is
paid?" is a question they can answer. "Should status be a select or a reference?" is not, and
it is your job anyway.

Two more rules about asking:

- **Ask when the answer changes what you build.** Do not ask permission for the obvious.
- **Say what you assumed.** When something is unclear and small, choose, build it, and name
  the assumption in the same message. Being corrected is cheap. Being blocked is not.

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

## Where this workspace builds

`cordango.yaml` carries a `build:` block saying where these apps are meant to run. It is the
reason `cordango build` needs no flags.

```
cordango configure --show                 what is set, if anything
cordango configure --target standalone
cordango build
```

Two targets:

- **standalone** — a repository of its own: ASP.NET Core, Vue, PostgreSQL, a Dockerfile,
  written to `generated/<app-key>/`. That path is not configurable and there is no `--out`;
  the directory is gitignored build output, and moving it out of the repository is how an
  application leaves. It belongs to them: delete cordango afterwards and it still builds.
- **platform** — published to a Cordango instance with `cordango publish`. It needs a
  connection, and `configure` and `build` both refuse without one — on the platform an app
  can see the other apps in the workspace and the core apps, and none of that is knowable
  offline. `cordango login <token>` connects it.

Publishing has a way back. `cordango import` with no argument lists the apps on the connected
instance and asks which one; `cordango import --list` just looks, and
`cordango import <handle>` brings one in as source under `apps/`. That is how an app built in
Studio, or published from somebody else's checkout, becomes files you can read and edit. A
file works with no connection at all: `cordango import <app.definition.json>`.

If there is no `build:` block, **ask which one they want before the first build**, the same
way you asked about the domain. It is one question and the answer is committed to the
repository, so nobody has to answer it twice.

`cordango configure` with no flags asks interactively — but **you cannot answer that prompt**,
and it knows: run from a script or an agent it does not ask at all. Put the question in the
conversation, then run the flag form above.

## Rules

- **Ask before you model.** An entity invented to fill a silence is the most expensive thing
  in this repository, because everything else references it and nothing renames it.
- **Never edit `.cordango/`** (the dotted directory). It is build output; your changes vanish on
  the next `cordango build`.
- **Use stable keys.** Renaming an entity or field key is not supported yet, because nothing
  rewrites the references to it. Add and remove instead, deliberately.
- **One aggregate per change.** It keeps diffs reviewable and is what `--scope` enforces.
- **You do not accept your own work.** `cordango apply` writes to the working tree; a person
  reads `git diff` and stages it. Do not commit unless you are asked to.
- Run `cordango fmt` if you hand-edited a file and the formatting drifted.
