[![Nuget](https://img.shields.io/nuget/dt/Pinja.NetCore.WebApi.Util.svg)](https://www.nuget.org/packages/Pinja.NetCore.WebApi.Util/)

# Pinja.NetCore.WebApi.Util

Library to share commonly used web api utilities and tools in Pinja projects.

## Pinja.NetCore.WebApi.Util.RequestLogging

To add very verbose logging about incoming request (body, query etc...) add following to startup and set loggging level as 'Debug'.

```cs
app.UseMiddleware<RequestLoggerMiddleware>();
```

## Pinja.NetCore.WebApi.Util.Paged

Pagination utility that create nice paged response from given query:

```cs
  [HttpGet("foo")]
  public ActionResult<Paged<FooResponse>> Get([FromQuery] int page)
  {
      return Ok(Paged<FooResponse>.FromQuery(_context.Foos.Select(...), page));
  }
```

## Pinja.NetCore.WebApi.Util.OrderBy

Utility to implement common "orderBy" queries at web api based on response type properties:

```cs
  [HttpGet("foo")]
  public ActionResult<Paged<FooResponse>> Get(
    [FromQuery] OrderByQueryString<FooResponse> orderBy)
  {
      return Ok(_context.Select(FooResponse.Projection).OrderBy(orderBy));
  }
```

And documentation support `c.OperationFilter<OrderByOperationFilter>();` to swagger gen configuration.

## Pinja.NetCore.WebApi.Util.Query

Simple helper methods to implement common search, filtering etc operations at web api:

```cs
  [HttpGet("foo")]
  public ActionResult<Paged<FooResponse>> Get(
    [FromQuery] OrderByQueryString<FooResponse> orderBy,
    [FromQuery] FooFilterObject? filterObject,
    [FromQuery] string? search = null)
  {
    var eventQuery = _context.Events
      .AsNoTracking()
      .SearchWhen(search,
          e => e.Foo.BarName.Contains(searchString),
          e => e.Foo.BarAddress.Contains(searchString))
      .FilterWhen(() => FooFilterObject != null, e => e.State == filterObject);
  }
```
