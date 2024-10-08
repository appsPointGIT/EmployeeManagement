using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EmployeeManagement.API.Authentication
{
    public class BearerTokenAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly string _apiToken;

        public BearerTokenAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) 
            : base(options, logger, encoder, clock)
        {
            _apiToken = configuration["BearerToken"];
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                !authHeader.ToString().StartsWith("Bearer "))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();

            if (token != _apiToken)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }
            
            var claims = new[] { new Claim(ClaimTypes.Name, "kpmg-dev-api") };
            var identity = new ClaimsIdentity(claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Bearer");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
