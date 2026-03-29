# _Imports.razor needs to be in the root project folder, but we want to keep it out of the way 
  of the main code. This file is just a placeholder 
  to make sure it gets included in the project and is available for use in our documentation pages.
  
### Otherwise it's scoped to the folder it's in and won't be available globally. 
    By including it in the project, we ensure that all our documentation pages can access the 
    components and services defined in _Imports.razor without any issues.