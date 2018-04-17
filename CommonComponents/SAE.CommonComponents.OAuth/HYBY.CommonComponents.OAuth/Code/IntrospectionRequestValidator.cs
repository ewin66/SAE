using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using SAE.CommonComponents.OAuth.Application;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace SAE.CommonComponents.OAuth.Code
{
    public class IntrospectionRequestValidator : IdentityServer4.Validation.IntrospectionRequestValidator, IIntrospectionRequestValidator
    {
        /// <summary>
        /// http请求上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccess;
        /// <summary>
        /// 资源服务
        /// </summary>
        private readonly IResourceServer _resourceServer;
        public IntrospectionRequestValidator(ITokenValidator tokenValidator, 
            ILogger<IdentityServer4.Validation.IntrospectionRequestValidator> logger,
            IHttpContextAccessor httpContextAccess,
            IResourceServer resourceServer) : base(tokenValidator, logger)
        {
            _httpContextAccess = httpContextAccess;
            _resourceServer = resourceServer;
        }

        async Task<IntrospectionRequestValidationResult> IIntrospectionRequestValidator.ValidateAsync(NameValueCollection parameters, ApiResource apiResource)
        {
            StringValues requestUrl;
            if (!_httpContextAccess.HttpContext.Request.Headers.TryGetValue("Referer", out requestUrl))
            {
                return new IntrospectionRequestValidationResult
                {
                    Error = "The Referrer is invalid",
                    IsError = true,
                    FailureReason = IntrospectionRequestValidationFailureReason.InvalidScope,
                    IsActive = false,
                };
            }
            var result=await base.ValidateAsync(parameters, apiResource);

            if (result.IsActive)
            {
                var resource = _resourceServer.GetByUrl(requestUrl.First());
                if (resource == null)
                {
                    result.IsActive = false;
                    result.Error = "The Referrer is invalid";
                    result.IsError = true;
                    result.FailureReason = IntrospectionRequestValidationFailureReason.InvalidScope;
                }
            }

            return result;
        }
    }
}
