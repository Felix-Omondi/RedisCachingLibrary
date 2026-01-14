# Redis Caching Library

A lightweight, clean wrapper around `Microsoft.Extensions.Caching.Distributed` for Redis-based caching in .NET applications.

## Features

- Generic async `Get`, `Set`, `Delete` methods
- Automatic JSON serialization/deserialization using `System.Text.Json`
- Configurable cache expiration (default: 5 minutes absolute expiration)
- Simple dependency injection friendly interface

## Installation

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

## Usage

1. Register services in `Program.cs`

```bash

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "MyApp_";
});

builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
```

1. Inject and use

```bash
public class ProductService
{
    private readonly IRedisCacheService _cache;

    public ProductService(IRedisCacheService cache)
    {
        _cache = cache;
    }

    public async Task RemoveProductCacheAsync(int id)
    {
        await _cache.DeleteDataAsync<Product>($"product:{id}");
    }
}
```
