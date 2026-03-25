## Note on Effects (Important)

The entire **Effects** section exists primarily to handle one critical real-world problem:

> Effects may complete after the original scope is no longer valid.

Examples:
- component disposed
- navigation happened
- user changed context
- newer request replaced older one

This leads to:
- stale updates
- incorrect state mutations
- race conditions
- UI inconsistencies

---

## Why this matters

Effects are:
- asynchronous
- time-based
- external

So they can:
- finish late
- finish out of order
- finish when they should no longer apply

---

## What we are preparing for

All concepts in this section (shapes, separation, chaining, cancellation, etc.) build toward:

- controlling effect lifetime
- preventing invalid state updates
- handling concurrency safely
- ensuring only relevant results affect state

---

## Core idea

> Not every completed effect should be allowed to update state.

---

## Practical outcome

Later we will introduce:
- cancellation strategies
- scoping rules
- ownership of effects
- guarding re-entry into reducer

---

## One sentence

Effects are easy to start —  
the real challenge is deciding **whether their result is still valid when they finish**.