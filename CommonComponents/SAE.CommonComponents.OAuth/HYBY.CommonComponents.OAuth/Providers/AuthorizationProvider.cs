using SAE.CommonComponents.OAuth.Application;
using SAE.CommonComponents.OAuth.Application.Dtos;
using SAE.CommonLibrary.Cache;
using SAE.CommonLibrary.OAuth;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth.Providers
{
    public  class Application
    {
        public string ApplicationID { get; set; }
        public string DisplayName { get; set; }
        public string RedirectUri { get; set; }
        public string LogoutRedirectUri { get; set; }
        public string Secret { get; set; }
    }
    public sealed class AuthorizationProvider : OpenIdConnectServerProvider
    {
        
        private readonly IOAuthServer _oauthServer;
        private readonly ICache _cache;
        public AuthorizationProvider(IOAuthServer oAuthServer, ICache cache)
        {
            this._oauthServer = oAuthServer;
            this._cache = cache;
        }

        public override async Task ValidateAuthorizationRequest(ValidateAuthorizationRequestContext context)
        {
            // Note: the OpenID Connect server middleware supports the authorization code, implicit and hybrid flows
            // but this authorization provider only accepts response_type=code authorization/authentication requests.
            // You may consider relaxing it to support the implicit or hybrid flows. In this case, consider adding
            // checks rejecting implicit/hybrid authorization requests when the client is a confidential application.
            if (!context.Request.IsAuthorizationCodeFlow())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.UnsupportedResponseType,
                    description: "Only the authorization code flow is supported by this authorization server.");

                return;
            }

            // Note: to support custom response modes, the OpenID Connect server middleware doesn't
            // reject unknown modes before the ApplyAuthorizationResponse event is invoked.
            // To ensure invalid modes are rejected early enough, a check is made here.
            if (!string.IsNullOrEmpty(context.Request.ResponseMode) && !context.Request.IsFormPostResponseMode() &&
                                                                       !context.Request.IsFragmentResponseMode() &&
                                                                       !context.Request.IsQueryResponseMode())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidRequest,
                    description: "The specified 'response_mode' is unsupported.");

                return;
            }

            // Retrieve the application details corresponding to the requested client_id.
            var application = this._oauthServer.GetByClientId(context.ClientId);

            if (application == null)
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified client identifier is invalid.");

                return;
            }

            if (!string.IsNullOrEmpty(context.RedirectUri) &&
                !string.Equals(context.RedirectUri, application.Callback, StringComparison.Ordinal))
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified 'redirect_uri' is invalid.");

                return;
            }

            context.Validate(application.Callback);
        }

        public override async Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            // Note: the OpenID Connect server middleware supports authorization code, refresh token, client credentials
            // and resource owner password credentials grant types but this authorization provider uses a safer policy
            // rejecting the last two ones. You may consider relaxing it to support the ROPC or client credentials grant types.
            if (!context.Request.IsAuthorizationCodeGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    description: "Only authorization code and refresh token grant types " +
                                 "are accepted by this authorization server.");

                return;
            }

            // Note: client authentication is not mandatory for non-confidential client applications like mobile apps
            // (except when using the client credentials grant type) but this authorization server uses a safer policy
            // that makes client authentication mandatory and returns an error if client_id or client_secret is missing.
            // You may consider relaxing it to support the resource owner password credentials grant type
            // with JavaScript or desktop applications, where client credentials cannot be safely stored.
            // In this case, call context.Skip() to inform the server middleware the client is not trusted.
            if (string.IsNullOrEmpty(context.ClientId) || string.IsNullOrEmpty(context.ClientSecret))
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidRequest,
                    description: "The mandatory 'client_id'/'client_secret' parameters are missing.");

                return;
            }

            // Retrieve the application details corresponding to the requested client_id.
            var application = this._oauthServer.GetByClientId(context.ClientId);

            if (application == null)
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified client identifier is invalid.");

                return;
            }


            if (!string.Equals(context.ClientSecret, application.Client.Secret, StringComparison.Ordinal))
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified client credentials are invalid.");

                return;
            }

            context.Validate();
        }

        public override async Task ValidateLogoutRequest(ValidateLogoutRequestContext context)
        {
            //// When provided, post_logout_redirect_uri must exactly
            //// match the address registered by the client application.
            
            //if (!string.IsNullOrEmpty(context.PostLogoutRedirectUri) &&
            //    ! _database.Any(application => application.LogoutRedirectUri == context.PostLogoutRedirectUri))
            //{
            //    context.Reject(
            //        error: OpenIdConnectConstants.Errors.InvalidRequest,
            //        description: "The specified 'post_logout_redirect_uri' is invalid.");

            //    return;
            //}
            context.Validate();
        }

        public override Task SerializeAccessToken(SerializeAccessTokenContext context)
        {
            OwnerDto owner;
            if (context.Scopes.Any(s => s.ToLower().CompareTo(OAuthDefault.ApiScope) == 0))
            {
                owner = new OwnerDto
                {
                    Id = context.Request.ClientId,
                    Type = OwnerType.App,
                };
            }
            else
            {
               var sub = context.Ticket.Principal.FindFirst(OpenIdConnectConstants.Claims.Subject);
                if (sub == null)
                {
                    context.HandleSerialization();
                    context.Response.Error = "This request is missing Subject";
                    return Task.FromResult<object>(null);
                }
               owner= new OwnerDto
               {
                   Id = sub.Value,
                   Type = OwnerType.User
               };
            }
            var o = this._oauthServer.CreateToken(owner, context.Request.ClientId);
            context.AccessToken = o.Ticket;
            return base.SerializeAccessToken(context);
        }

        public override Task DeserializeAccessToken(DeserializeAccessTokenContext context)
        {
            var token = this._oauthServer.GetByTicket(context.AccessToken);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                            {
                                new Claim(ClaimTypes.Sid,token.Owner.Id),
                                new Claim(OpenIdConnectConstants.Claims.Subject,token.Owner.Id),
                                new Claim(OpenIdConnectConstants.Claims.Name,token.Owner.Name),
                                new Claim(OpenIdConnectConstants.Claims.Scope,token.Owner.Type== OwnerType.App?OAuthDefault.ApiScope:OAuthDefault.Scope)
                            }, OAuthDefault.AuthenticationScheme));

            var propertues = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = token.CreateTime.Add(token.Expires)
            };

            context.Ticket = new AuthenticationTicket(principal,propertues,OAuthDefault.AuthenticationScheme);
            
            return base.DeserializeAccessToken(context);
        }
        public override Task HandleUserinfoRequest(HandleUserinfoRequestContext context)
        {
            foreach(var itm in context.Ticket.Principal.Claims)
            {
                context.Claims.Add(itm.Type, itm.Value);
            }
            
            return base.HandleUserinfoRequest(context);
        }
    }
}
