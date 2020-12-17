using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace RecipeFinder.WebAPI
{
    /**
     * 
     */
    public class AuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            string[] TokenAndUser = null;

            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization Header.", request);
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authorization Schema.", request);
                return;
            }

            TokenAndUser = authorization.Parameter.Split(':');

            string Token;
            string userName;

            try
            {
                Token = TokenAndUser[0];
                userName = TokenAndUser[1];
            }
            catch (Exception)
            {
                context.ErrorResult = new AuthenticationFailureResult("Something Went Wrong Or Incomplete Token.", request);
                return;
            }

            if (String.IsNullOrEmpty(Token))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Token.", request);
                return;
            }

            string ValidUserName = TokenManager.ValidateToken(Token);

            if (userName != ValidUserName)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Token for User.", request);
                return;
            }

            context.Principal = TokenManager.GetPrincipal(Token);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhase;
        public HttpRequestMessage Request { get; set; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhase = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhase;

            return responseMessage;
        }
    }
}