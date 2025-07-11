using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;

namespace Order.Service.Proxies.Config
{
    public static class HttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient client, IHttpContextAccessor context) 
        {
            if (context.HttpContext.User.Identity.IsAuthenticated) 
            {
                //var token = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("access_token"))?.Value;
                string? token = context.HttpContext.Request.Headers.FirstOrDefault(x=>x.Key.Equals("Authorization")).Value;
                if (!string.IsNullOrEmpty(token)) 
                {
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                }
            }
        }
    }
}
