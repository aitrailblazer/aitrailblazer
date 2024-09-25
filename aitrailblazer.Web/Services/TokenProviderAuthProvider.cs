using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class TokenProviderAuthProvider : IAccessTokenProvider
{
    private readonly ITokenAcquisition _tokenAcquisition;

    public TokenProviderAuthProvider(ITokenAcquisition tokenAcquisition)
    {
        _tokenAcquisition = tokenAcquisition;
    }

    public async Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" }; // Or your specific scopes
        return await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
    }

    public AllowedHostsValidator AllowedHostsValidator { get; } = new AllowedHostsValidator();
}
