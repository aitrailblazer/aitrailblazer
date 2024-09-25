
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

using static ApiKeyAuthenticationProvider;
public class ApiKeyAuthenticationProvider : IAuthenticationProvider
{
    private readonly KeyLocation location;
    private readonly string keyName;
    private readonly  string keyValue;

    public enum KeyLocation {
        QueryParameter,
        Header
    }
    public ApiKeyAuthenticationProvider(string keyName, string keyValue, KeyLocation location)
    {
        this.keyName = keyName;
        this.keyValue = keyValue;
        this.location = location;
    }
    
    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        switch (location)
        {
            case KeyLocation.QueryParameter:
                var uriString = request.URI.OriginalString + (request.URI.Query != string.Empty?"&":"?") + $"{keyName}={keyValue}";
                request.URI = new Uri(uriString);
                break;
            case KeyLocation.Header:
                request.Headers.Add(keyName, keyValue);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(location), location, null);
        }
    }
}

public class NullAuthenticationProvider : IAuthenticationProvider
{
    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        // do nothing
    }
}


