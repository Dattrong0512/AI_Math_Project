using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AIMathProject.API.Handler
{
    public class CustomAuthenticationSchemeProvider : AuthenticationSchemeProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationSchemeProvider(
            IOptions<AuthenticationOptions> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Ghi đè GetDefaultAuthenticateSchemeAsync để chọn scheme dựa trên tuyến đường
        public override async Task<AuthenticationScheme> GetDefaultAuthenticateSchemeAsync()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                throw new ArgumentNullException("The HTTP request cannot be retrieved.");
            }

            if (request.Path.StartsWithSegments("/api"))
            {
                return await GetSchemeAsync("Bearer");
            }
            else
            {
                return await GetSchemeAsync("Cookies");
            }
        }

        // Ghi đè GetDefaultChallengeSchemeAsync để chọn scheme khi cần thử thách (challenge)
        public override async Task<AuthenticationScheme> GetDefaultChallengeSchemeAsync()
        {
            return await GetDefaultAuthenticateSchemeAsync();
        }

        // Ghi đè GetDefaultSignInSchemeAsync để chọn scheme khi đăng nhập
        public override async Task<AuthenticationScheme> GetDefaultSignInSchemeAsync()
        {
            return await GetDefaultAuthenticateSchemeAsync();
        }
    }
}