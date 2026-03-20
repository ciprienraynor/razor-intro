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