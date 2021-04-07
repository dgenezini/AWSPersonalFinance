using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalFinance
{
    public class TokenHttpClientHandler : HttpClientHandler
    {
        private readonly IAccessTokenProvider _AccessTokenProvider;

        public TokenHttpClientHandler(IAccessTokenProvider accessTokenProvider)
        {
            _AccessTokenProvider = accessTokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if ((await _AccessTokenProvider.RequestAccessToken()).TryGetToken(out var Token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token.Value);

            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
