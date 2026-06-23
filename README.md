# WebApiCatchAll

A minimal ASP.NET Core 8 utility that registers a single catch-all route across all HTTP methods (`GET`, `POST`, `PUT`, `DELETE`, `PATCH`, `HEAD`, `OPTIONS`). Useful for local development, webhook inspection, and end-to-end testing — any request path and body is echoed back as a readable response.

## Features

- Matches any URL path via `/{*slug}`
- Accepts any of the standard HTTP verbs
- If a JSON body is provided it is pretty-echoed alongside the path
- Structured logging via [Serilog](https://serilog.net/) with console and file sinks
- Full HTTP request/response logging via `Microsoft.AspNetCore.HttpLogging`

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run locally

```bash
git clone https://github.com/JerrettDavis/WebApiCatchAll.git
cd WebApiCatchAll
dotnet run --project WebApiCatchAll
```

The app starts on `http://localhost:5000` (or the port shown in the console). Send any request:

```bash
curl http://localhost:5000/hello/world
# Hello hello/world!

curl -X POST http://localhost:5000/api/test \
     -H "Content-Type: application/json" \
     -d '{"key":"value"}'
# Hello api/test! Here's your body:
# {"key":"value"}
```

## Configuration

Logging behaviour is controlled via `appsettings.json` / `appsettings.Development.json` using the standard Serilog configuration extension.

## License

[MIT](LICENSE)
