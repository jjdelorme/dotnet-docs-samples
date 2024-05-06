/* This code is equivalent to the following JavaScript code:

    import {GoogleAuth} from 'google-auth-library';
    const auth = new GoogleAuth();
    const url = 'https://helloworld-xxxxx-uc.a.run.app';
    const client = await auth.getIdTokenClient(url);

* The documentation at cloud.google.com/docs/authentication/get-id-token should be updated to include C# sample.
*/

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Iam.Credentials.V1;

if (args.Length == 0)
{
    Console.WriteLine("Error: No URL argument provided.");
    Console.WriteLine("Usage: dotnet run <URL>");
    return;
}

string url = args[0];

// Get an HTTP client with the authorization header set to an ID token
var client = await GetIdTokenClientAsync(url);

// Call the cloud run service to get a response
var response = await client.GetStringAsync(url);

Console.WriteLine($"Reponse: {response}");


/// <summary>
/// Gets an HTTP client with an ID token set.
/// </summary>
async Task<HttpClient> GetIdTokenClientAsync(string url)
{
    string accessToken = await GetIdTokenFromApplicationDefaultAsync(url);

    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
    
    return client;
}

/// <summary>
/// Gets an ID token from Application Default Credentials.
/// </summary>
async Task<string> GetIdTokenFromApplicationDefaultAsync(string url)
{
    // Get default Google credential
    var credential = await GoogleCredential.GetApplicationDefaultAsync()
        .ConfigureAwait(false);

    var token = await credential.GetOidcTokenAsync(OidcTokenOptions.FromTargetAudience(url));

    var idToken = await token.GetAccessTokenAsync();

    return idToken;
}

