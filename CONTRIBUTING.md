# Contributing to the examples

These are real applications, and an improvement here reaches everyone who reads them — in both
environments they run in.

By taking part you agree to the [Code of Conduct](CODE_OF_CONDUCT.md).

## What is welcome

Clearer labels. More realistic permissions. A state a lifecycle is missing. A screen that reads
better. A comment explaining why something is modelled the way it is.

New examples are welcome too, but open an issue first — an example earns its place by showing
something the others do not.

## What to check before opening a pull request

```sh
cordango check --app <name>
cordango check --app <name> --target standalone
```

The first asks whether it is a valid Cordango application. The second asks the narrower question of
whether the standalone generator can also build it — a standalone build is one application, so
anything depending on other installed applications is reported rather than silently dropped.

Then actually build it:

```sh
cordango build --target standalone --out generated --allow-incomplete
cd generated && docker compose up --build
```

## What is not edited here

`generated/` is output. It is gitignored, and a change belongs in `apps/<name>/` — the source it was
generated from.

If the *generator* produced something wrong, that is a bug in
[cordango/cordango](https://github.com/cordango/cordango), not here.

## Where these came from

Each was imported from the conformance corpus in the compiler repository using `cordango import`.
That corpus stays where it is, because the test suite is pinned to it and it must not drift. These
copies are the ones meant for people to read, and they are allowed to grow.

## Getting help

- [Issues](https://github.com/cordango/examples/issues)
- [hello@cordango.com](mailto:hello@cordango.com)
