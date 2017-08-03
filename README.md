[![Build status](https://ci.appveyor.com/api/projects/status/4k9jfvn49u7geb9u?svg=true)](https://ci.appveyor.com/project/savpek/protacon-netcore-webapi-util)
[![Nuget](https://img.shields.io/nuget/dt/Protacon.NetCore.WebApi.Util.svg)](https://www.nuget.org/packages/Protacon.NetCore.WebApi.Util/)

# Protacon.NetCore.WebApi.Util.ModelValidation namespace
Validates MVC models with self explanatory error messages what went wrong.

Returns bad request (400) and meaningfull message what should be fixed.

## Setup

Enable globally on startup:
```cs
services.AddMvc(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});
```

## Example
```cs
public class Foo
{
    [Required]
    public string Value { get; set; }
}
```

When requested with empty value returns 400 with content:
```javascript
[
  {
    "Code": "INVALID_MODEL",
    "Message": "Value: The Value field is required."
  }
]
```
