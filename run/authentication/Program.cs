
/* This code is equivalent to the following JavaScript code:

    import {GoogleAuth} from 'google-auth-library';
    const auth = new GoogleAuth();
    const url = 'https://helloworld-xxxxx-uc.a.run.app';
    const client = await auth.getIdTokenClient(url);

* The documentation at cloud.google.com/docs/authentication/get-id-token should be updated to include C# sample.
*/
// Aliasing for demonstration purposes.
using GoogleCredential = SampleGoogleCredential;

if (args.Length == 0)
{
    Console.WriteLine("Error: No URL argument provided.");
    Console.WriteLine("Usage: dotnet run <URL>");
    return;
}

string url = args[0];

// Get an HTTP client with the authorization header set to an ID token
var client = await GoogleCredential.GetIdTokenClientAsync(url);

// Call the cloud run service to get a response
var response = await client.GetStringAsync(url);

// Print out the response from the Cloud Run service
Console.WriteLine($"Reponse: {response}");
