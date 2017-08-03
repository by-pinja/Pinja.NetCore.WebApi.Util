# Protacon.NetCore.WebApi.Util

## Protacon.NetCore.WebApi.Util.ModelValidation namespace
Validates MVC models with self explanatory error messages what went wrong.

Returns bad request (400) and meaningfull message what should be fixed.

### Setup
```cs
services.AddMvc(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});
```

### Example
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
