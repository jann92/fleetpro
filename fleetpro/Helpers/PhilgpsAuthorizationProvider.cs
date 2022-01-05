using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using FleetPro2019.Models;
using FleetPro2019.Helpers.SERVERDB;
using System.Configuration;

namespace FleetPro2019.Helpers
{
    public class PhilgpsAuthorizationProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var data = await context.Request.ReadFormAsync();
            string ConfigurationName = data.Where(x => x.Key == "db").FirstOrDefault().Value.FirstOrDefault().ToString();
            string IPAddress = data.Where(x => x.Key == "IPAdd").FirstOrDefault().Value.FirstOrDefault().ToString();

            var WebConfigurationName = ConfigurationName.GetWebConfigurationName("TFDB");
            var DownloadsURL = ConfigurationName.GetDownloadsHistoryURL("TFDB");

            PhilgpsUserRepository _repo = new PhilgpsUserRepository(WebConfigurationName);

            Account result = _repo.FindUser(context.UserName, context.Password, IPAddress);

            if( result == null)
            {
                context.SetError("invalid grant", "The username or password is incorrect");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Username", result.Username));
            identity.AddClaim(new Claim("AccountID", result.AccountID.ToString()));
            identity.AddClaim(new Claim("Groupcodes", "0"));
            identity.AddClaim(new Claim("UserEmail", result.Email ?? "No Email"));
            identity.AddClaim(new Claim("ParentID", result.ParentID.ToString()));
            identity.AddClaim(new Claim("ConfigurationName", ConfigurationName));
            identity.AddClaim(new Claim("DownloadsURL", DownloadsURL));
            identity.AddClaim(new Claim("ReadRoleValue", result.ReadRoleValue.ToString()));

            //for FleetAdmin
            identity.AddClaim(new Claim("UserID", result.AccountID.ToString()));
            identity.AddClaim(new Claim("ParentUserID", result.ParentID.ToString()));
            context.Validated(identity);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            context.AdditionalResponseParameters.Add("GoogleClientID", ConfigurationManager.AppSettings["GoogleClientID"]);
            context.AdditionalResponseParameters.Add("GoogleAPIKey", ConfigurationManager.AppSettings["GoogleAPIKey"]);
            return base.TokenEndpointResponse(context);
        }



    }
}