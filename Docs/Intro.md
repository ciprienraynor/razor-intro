Blazor is not just a UI replacement.
It changes where state, navigation, debugging, and interaction logic live.

Blazor is feasible, but it is not a drag-and-drop/data-connector replacement.
It requires decisions about:
- component boundaries
- state flow
- navigation
- server/client execution model
- debugging approach

before migration, we need to choose the application model:
- simple component-based
- state-container-based
- store/reducer-based

because that affects maintainability and debugging much more than the control library does
same UI
three architectures
different consequences

Fast performance is just slow mastery compressed in time.

Classic desktop frameworks (WinForms/WPF) are **event-driven and imperative**: 
you handle an event and directly mutate the UI, so the UI itself becomes a source of truth.

Example (imperative):
button.Text = "Done"
panel.Visible = false

Modern frameworks like Blazor, React, or Jetpack Compose are **state-driven and declarative**: 
you update state, and the UI is re-rendered as a projection of that state.

Example (declarative):
state.IsDone = true

UI:
button text = state.IsDone ? "Done" : "Start"
panel visible = !state.IsDone

In short:
Desktop → tell the UI what to do  
Modern UI → describe what the UI should be based on state