# Expenses

An employee files an expense claim. An approver approves or declines it. Once approved, somebody
marks it reimbursed.

It is the smallest application here that is still complete, which makes it the one to read first.

## What to look at

**`workflows/lifecycles/claim_flow.cordango.yaml`** is the heart of it. Four states, the moves
between them, and which moves are allowed from where. A claim starts at `submitted`; `reimbursed`
and `declined` are terminal and there is no path back out of either. States carry colours because
the renderer uses them, so a declined claim looks declined without anyone writing a condition for
it.

**`roles/`** holds three files, and they are not variations on one theme. An employee sees their own
claims. An approver sees the queue. An admin sees everything and maintains the categories. Read them
together: the difference between them is the security model, and the server enforces it rather than
the screen you happen to be on.

**`views/screens/`** puts three pages over a single entity, which is the point. The same claims are
"my expenses" to the person who filed them and an inbox to the person approving them.

## Running it

```
cordango check
cordango check --target standalone
```

## Structure

```
cordango.yaml            the workspace: which applications are installed
apps/expenses/
  app.cordango.yaml      the application's name, and the order things appear in
  entities/              what it stores
  workflows/             lifecycles and automations
  roles/                 who may do what
  views/                 collections, screens, and one record's detail
```
