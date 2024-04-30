# .NET Get ID Token Sample

This sample demonstrates how to invoke a Cloud Run service using an ID token based on the Application Default Credentials using C# and the .NET client libraries.

The sample requires [.NET Core 6.0][net-core] or later.

## Setup

1.  Set up a [.NET development environment](https://cloud.google.com/dotnet/docs/setup).

1.  [Enable the Cloud Run API][enable-api] in your Google Cloud project.

1. Deploy a simple hello world container to cloud run:

```bash
gcloud run deploy helloworld --image testcontainers/helloworld --region us-central1

Allow unauthenticated invocations to [helloworld] (y/N)?  N
```

1. Run the application with the new URL from cloud run

```bash
dotnet run https://helloworld-XXXXX-uc.a.run.app
```

## Contributing changes

* See [CONTRIBUTING.md](../../../CONTRIBUTING.md)


## Licensing

* See [LICENSE](../../../LICENSE)


## Testing

* See [TESTING.md](../../../TESTING.md)


[kms]: https://cloud.google.com/kms
[enable-api]: https://console.cloud.google.com/flows/enableapi?apiid=run.googleapis.com
[net-core]: https://www.microsoft.com/net/core
