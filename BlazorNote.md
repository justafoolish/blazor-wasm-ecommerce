1. Get access to authentication state in client

```csharp
[CascadingParameter] Task<AuthenticationState> _authState {get; set;}
```
or
```csharp
@inject AuthenticationStateProvider _authState

// => authentication state : _authState.GetAuthenticationStateAsync()
```

2. Convert JSON response object to T (Root Object)
```csharp
// convert JSON to <T>
T dataConverted = JsonSerializer.Deserialize<T>(
      data, // JSON data 
      new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
    );

```

3. Generate TailwindCSS

```
mkdir Tailwindcss && cd Tailwindcss
```

```
npm i tailwindcss

npx tailwindcss init
```

```js
module.exports = {
  content: [
    "../BlazorAppEC/Client/Shared/**/*.razor",
    "../BlazorAppEC/Client/Shared/Components/**/*.razor",
    "../BlazorAppEC/Client/Shared/Components/Admin/**/*.razor",
    "../BlazorAppEC/Client/Shared/Components/Client/**/*.razor",
    "../BlazorAppEC/Client/Pages/**/*.razor",
    "../BlazorAppEC/Client/Pages/Admin/**/*.razor",
    "../BlazorAppEC/Client/Pages/Client/**/*.razor",

  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

```css
//app.css file
@tailwind base;
@tailwind components;
@tailwind utilities;
```

- Export tailwind css file
```
npx tailwindcss -i ./app.css -o ../BlazorAppEC/Client/wwwroot/css/tailwind.css --minify
```