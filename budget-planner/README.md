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

## What does not build yet

This is the honest part, and it is why `--allow-incomplete` is not optional here. The build reports
**seventy-three gaps** and writes every one of them into the generated README and into
`cordango.build.json`:

| | |
| --- | --- |
| **48 × CORD2305** | computed fields. 31 are rollups over other records, 17 read something outside their own row. The columns exist and stay empty. |
| **18 × CORD2301** | screen blocks the generator does not draw yet — `create`, `repeat`, `chip`, `tiles`, `settings`, `action`. Each leaves a card on the page saying so. |
| **5 × CORD2303** | `createRecord` and `updateRecord` effects on commands. The command runs and moves the record; the effect does not fire. |
| **2 × CORD2102** | `history` blocks. Record history is a platform feature — a standalone application keeps no business audit trail, so this one will not arrive in a later release. |

So what you get today is the data model, the API, the permissions, the commands and most of the
screens. **What you do not get is the arithmetic** — which, in a budget planner, is the application.
It compiles, it runs, and every figure that depends on a rollup or on another record is blank.

That is the point of building it now: the gaps are a list somebody can read rather than a wall, and
this application is the acceptance case for closing them.
