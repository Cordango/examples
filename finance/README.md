# Finance

Three apps that are each worth having on their own, and worth more together. This is the suite to
read first if you want to see what "born connected" means in files rather than in a diagram.

```
cordango check        # parse, lower and validate all three
cordango discover     # what each one announces, and what it builds on
cordango inspect      # what is in here
```

| App | What it owns | Entities |
| --- | --- | --- |
| [`budget-tracker`](apps/budget-tracker) | Budgets per cost centre and period; committed, actual and forecast money against their lines; adjustments somebody has to approve | 6 |
| [`purchase-requests`](apps/purchase-requests) | The request to buy something, routed by amount, and what was finally bought | 4 |
| [`vendor-flow`](apps/vendor-flow) | Vendor intake with a security and finance review, and the register of live contracts, seats and renewal deadlines | 5 |

## What connects them, and how

Nothing here is an integration. No app names another app's screens, and no app was changed to make
another one work. Two mechanisms do all of it.

**A reference, when both apps are installed together.** A purchase request points at the budget line
it should come out of:

```yaml
budget_line:
  type: reference
  targetApp: budget_tracker
  targetEntity: budget_line
```

The picker offers real lines, the chip opens the line in Budget Tracker, and the budget line's
related-apps panel lists the requests pointing at it — none of which Budget Tracker was told about.

**A subscription, when something happens.** Budget Tracker reacts to a purchase being approved:

```yaml
automation: commit_on_purchase_approved
trigger: state.entered
app: purchase_requests
event: purchase_request.approved
effects:
  - type: createRecord
    entity: spend_entry
    set:
      budget_line: '{{record.budget_line}}'
      amount: '{{record.amount}}'
      kind: commitment
```

Purchase Requests does not know this exists. It announces that a request reached `approved`, and
stops there. Adding a second listener changes nothing in it.

### The whole table

| When this happens | This app reacts | Mechanism |
| --- | --- | --- |
| a purchase request is approved | Budget Tracker commits the money against the line it named | subscription + reference |
| software is bought | Vendor & SaaS opens a contract for it | subscription |
| hardware is bought | Asset Register (in `people/`) puts it on the register | subscription |
| a new vendor is approved | Purchase Requests opens a draft request to buy it | subscription |
| a subscription is renewed | Budget Tracker commits next year's money | subscription |
| a project is approved (in `operations/`) | Budget Tracker commits the estimate | subscription |

## Three things worth reading the source for

**The approval tier is a snapshot, not a calculation.** `purchase_request.approval_tier` is set from
the amount by an `initial` rule when the request is created, and then left alone. A live calculation
would reshuffle a board grouped by it every time somebody edited a figure mid-approval.

**Three transitions reach one state, so none of them owns the event.** `manager_approve`,
`finance_approve` and `management_approve` all end in `approved`. An emitted name may be announced by
one command only — a name that identifies three different things identifies nothing — so the
subscription listens for the STATE instead, which the runtime publishes however the record got there.

**Separation of duties lives on the transition, not on the role.** `approve_adjustment` carries
`when: requested_by neq {{actor.id}}`. A role grant cannot express it: somebody who is both a budget
owner and a controller passes every role check on their own row.

## What is not here yet

A purchase request cannot show "this would take Marketing / Software over its plan", even though the
budget line it points at knows. Reading another app's field at display time is a cross-app query, and
that is slice Q of
[`plan-connected-runtime-2026-09.md`](../../../_docs/10-platform/plan-connected-runtime-2026-09.md).
The reference and the reaction both work today; the warning does not.
