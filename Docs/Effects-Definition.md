## Effects – Definition

In this architecture, an **Effect** is any operation that cannot be expressed as an immediate, deterministic state transition:

nextState = Reduce(oldState, action)

A **pure reducer**:
- depends only on current state and action  
- produces next state immediately  
- has no interaction with external systems  
- is deterministic (same input → same output)  

An **Effect**:
- performs work outside that pure computation  
- may depend on time, IO, or external systems  
- does not complete immediately  
- produces a result later  
- re-enters the system through another action  

---

## Why “Effect” (the name)

The term comes from **side effect** in programming theory.

A side effect is:
> something a function does beyond returning a value

Examples:
- network calls  
- timers  
- file/database access  
- external services  
- UI interactions  

So:

Pure → computation only  
Effect → computation + interaction with the outside world  

If the word feels unclear, think:

Effect = external work  
Effect = non-pure operation  
Effect = async/IO/time-based work  

---

## Pure vs Effectful (practical)

### Pure example

Action:
Increment

Reducer:
- reads current state  
- computes next state immediately  

Example:
- count = 5 → 6  

No waiting, no external dependency, no uncertainty.

---

### Effectful example

Action:
LoadMessage

Reducer:
- sets loading state  
- cannot produce final message  

Then:
- async work starts  
- time passes  
- external result arrives  

New action:
LoadMessageCompleted(message)

Reducer:
- updates state with final result  

---

## Why `LoadMessage` is an Effect

Because:
- result is not available immediately  
- depends on external service  
- requires time  
- reducer alone cannot compute final state  

So `LoadMessage` is:
> an intent to start effectful work  

---

## Why `LoadMessageCompleted` is pure

Once the result exists:
- reducer can compute next state immediately  
- no waiting  
- no uncertainty  

So:
- effect has already happened  
- now we return to pure state transition  

---

## Naming pattern

- `Increment` → pure, immediate  
- `LoadMessage` → starts effect  
- `LoadMessageCompleted` → result of effect  

This separates:
- start of work  
- completion of work  

---

## Core flow

1. Action enters  
2. Reducer computes immediate state  
3. Effect runs outside reducer  
4. Effect completes later  
5. New action is dispatched  
6. Reducer applies result  

---

## Key rule

> Pure logic computes state immediately.  
> Effects perform external or delayed work and return later through another action.

---

## Simplified mental model

Pure → instant, predictable  
Effect → delayed, external, uncertain

## TCA
Some operations like orchestrations -> passing to new action or
merging and combining multiple actions (sync or async) are considered
effects in the sense that Reduce is not hit immediately (set new state)
but it's delayed. 

Inside of this and possible async steps surely can be some real Effect.

## Final notion

Action → Reduce → State   (always happens)
Effect → produces Action → Reduce → State   (later)

Reducer is the only place where state changes.

Effects never change state.
Effects only produce actions.

Actions always go through reducer.

State is always the result of reduction.


## Top level notion (Unidirectional Data Flow, UDF)
> STATE IS IN CONSTANT TRANSITION