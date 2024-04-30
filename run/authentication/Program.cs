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

    // Get the underlying ServiceAccountCredential
    var serviceAccount = credential.UnderlyingCredential as ServiceAccountCredential;

    if(serviceAccount == null)
        throw new Exception("Could not get ServiceAccountCredential from ApplicationDefaultCredentials");
 
    var idToken = await GetIdTokenAsync(url, serviceAccount.Id);

    return idToken;
}

/// <summary>
/// Gets the ID Token scoped to a url with a service account ID (usually the email address).
/// </summary>
async Task<string> GetIdTokenAsync(string url, string serviceAccountId)
{
    // Create IAMCredentialsClient
    var client = IAMCredentialsClient.Create();

    // Generate ID token
    var tokenResponse = await client.GenerateIdTokenAsync(serviceAccountId, null, url, true)
            .ConfigureAwait(false);

    return tokenResponse.Token;
}
