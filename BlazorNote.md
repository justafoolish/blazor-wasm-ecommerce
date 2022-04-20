1. Get access to authentication state in client

```csharp
[CascadingParameter] Task<AuthenticationState> _authState {get; set;}
```
or
```csharp
@inject AuthenticationStateProvider _authState

// => authentication state : _authState.GetAuthenticationStateAsync()
```