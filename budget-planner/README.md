# Budget Planner

Build an investor-ready financial plan: named scenarios, a usage-based price list, customer growth
and churn, hiring by team, cost lines, funding rounds, and a cash balance that runs month by month
until it tells you how much runway is left.

It is by a distance the largest application here — sixteen entities and seventy-three computed
fields — and it exists because it is the one that could not be built at all until the calculation
plane was. Read the others first. Read this one when you want to know how far the language goes.

## What to look at

**`entities/period.cordango.yaml` and `entities/scenario.cordango.yaml`** are where the arithmetic
lives. A period's figures are computed from its own row, from its parent scenario, and from the
period *before it* — a running cash balance is `prev(cash_at_end, scenario.starting_cash) +
net_cash_movement`, which is the recurrence a spreadsheet writes as `=B26+C24-C25` and which no
expression over a single record can say. That one primitive is why this application is expressible.

**`entities/cohort_month.cordango.yaml`** is the other half. Customers arrive in a month and decay
over the months after it, so revenue in any period is a sum over every cohort still alive. The grid
is built by a workflow rather than modelled by hand, because the number of rows depends on the plan
length.

**`entities/revenue_plan.cordango.yaml`** carries the pricing. Every per-plan difference is a NUMBER
on the plan row rather than a branch, because an expression cannot read a select — so a base fee, a
cap multiplier and a price per app-user are three columns, and the same formula serves every plan.

**`views/screens/projection.cordango.yaml`** is the payoff: recognised revenue and cash collected
side by side, which are not the same number and are the two an investor asks about.

**`roles/`** has three, and the interesting one is `viewer` — an investor or a board member who may
read a scenario and change nothing in it.

## Running it

```
cordango check                        # is this a valid Cordango application?
cordango check --target standalone    # can the standalone generator build it?

cordango build --target standalone --out generated --allow-incomplete
cd generated && docker compose up --build
```

## What does not build

Two blocks, and they are not coming: `history` on the scenario and on the funding round. Record
history is a Cordango Platform feature — the platform keeps a field-level audit trail automatically
and that block is the screen over it. A standalone application keeps technical logs and no business
audit trail, so there is nothing behind the block to draw. Each one leaves a card on the page saying
so, and `--allow-incomplete` is how you accept that.

**Everything else builds**, which on this application is the whole of the interesting part:

- **73 computed fields.** Expressions over the record's own columns, figures read across a reference
  (`segment.mature_active_users`), 31 rollups over other records — including the windowed ones, where
  a hiring line counts towards every period its own start and end months span — and the two that
  carry down the series, `prev(cash_end, scenario.starting_cash) + net_cash_movement`.
- **The chain that keeps them right.** Edit a hiring line and every period of that scenario is worked
  out again, then the series folded down them in order, then the scenario over the lot.
  `api/Computed/AppRollups.cs` is that chain written out — the order came from the definition when
  this was generated, so there is nothing at run time deciding what depends on what.
- Eight workflows, eighteen commands with their guards and effects, three roles, and every screen.

Read `api/Computed/PeriodRollups.cs` first. Each rollup is one LINQ query you can run by hand against
the database, which is the point: a total that looks wrong is a query you can read rather than an
expression string somewhere inside an engine.
