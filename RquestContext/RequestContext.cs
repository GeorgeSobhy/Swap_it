using Microsoft.AspNetCore.Http;
namespace RquestContext
{
    public class RequestContext : IRequestContext
    {
        private HttpContext _httpContext;

        public RequestContext(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext!;

        }

        public string UserId => _httpContext.Items["UserId"] != null ? _httpContext.Items["UserId"].ToString() : string.Empty;

        public int? OrganizationId
        {
            get
            {
                return _httpContext.Session.GetInt32("OrganizationId");
            }
        }
        public string RequestScheme => _httpContext.Request.Scheme;
        public string RequestHost => _httpContext.Request.Host.Value;
        public string RequestPath => _httpContext.Request.Path;
        public string RequestQueryString => _httpContext.Request.QueryString.Value;
    }

    public interface IRequestContext
    {
        string? UserId { get; }

        int? OrganizationId { get; }
        string RequestScheme { get; }
        string RequestHost { get; }
        string RequestPath { get; }
        string RequestQueryString { get; }
    }
}