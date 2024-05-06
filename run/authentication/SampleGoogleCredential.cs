using Google.Apis.Auth.OAuth2;

public class SampleGoogleCredential
{
    /// <summary>
    /// Gets an HTTP client with an ID token set.
    /// </summary>
    /// <param name="audience">
    /// The audience to use for the ID token (i.e. Iap Client Id or the Cloud Run url)
    /// </param>
    public static async Task<HttpClient> GetIdTokenClientAsync(string audience, CancellationToken cancellationToken = default)
    {
        string idToken = await GetIdTokenFromApplicationDefaultAsync(audience, cancellationToken)
            .ConfigureAwait(false);


        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {idToken}");
        
        return client;
    }

    /// <summary>
    /// Gets an ID token from Application Default Credentials.
    /// </summary>
    private static async Task<string> GetIdTokenFromApplicationDefaultAsync(string url, CancellationToken cancellationToken)
    {
        // Get default Google credential
        var credential = await GoogleCredential.GetApplicationDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        var token = await credential.GetOidcTokenAsync(OidcTokenOptions.FromTargetAudience(url), cancellationToken)
            .ConfigureAwait(false);

        // Despite the method being called AccessToken this is an IdToken
        var idToken = await token.GetAccessTokenAsync(cancellationToken)
            .ConfigureAwait(false);

        return idToken;
    }
}
