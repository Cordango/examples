# Room booking

Rooms get booked. Bookings have attendees. A policy record holds the rules.

## What to look at

**`entities/booking_policy.cordango.yaml`** is the reason to read this one. How far ahead a room may
be booked, whether weekends are allowed, whether approval is needed: those are things a company
changes its mind about, so they live in a record rather than in the source.

```yaml
entity: booking_policy
kind: settings
fields:
  max_advance_days:
    label: Max advance (days)
    type: integer
    default: 30
```

`kind: settings` is what makes it a single record rather than a table of them. It renders as a
grouped form on a settings page instead of a list nobody would ever add a second row to.

**`entities/booking_attendee.cordango.yaml`** is the join between a booking and the people on it.
This is what a many-to-many relationship looks like once it has fields of its own.

**`views/screens/availability.cordango.yaml`** is a calendar over rooms rather than over people, and
it is the largest screen here.

## Running it

```
cordango check
cordango check --target standalone
```

## Structure

```
cordango.yaml                the workspace: which applications are installed
apps/room-booking/
  app.cordango.yaml          the application's name, and the order things appear in
  entities/                  what it stores
  workflows/                 lifecycles and automations
  roles/                     who may do what
  views/                     collections, screens, and one record's detail
```
