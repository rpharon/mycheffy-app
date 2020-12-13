using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace mycheffy.Services
{
    public class AuthenticatedHandler : DelegatingHandler
    {
        private readonly Func<Task<string>> getToken;

        public AuthenticatedHandler(Func<Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException(nameof(getToken));
            this.getToken = getToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await getToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
