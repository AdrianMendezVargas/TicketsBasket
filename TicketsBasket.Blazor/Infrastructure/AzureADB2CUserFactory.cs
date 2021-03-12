using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Blazor.Infrastructure {

    //Esta clase se encargara de agregar claims al ClaimsPrincipal, en esta caso, el role.
    public class AzureADB2CUserFactory :  AccountClaimsPrincipalFactory<RemoteUserAccount>{

        private readonly IHttpClientFactory _httpClientFactory;
        public AzureADB2CUserFactory(IAccessTokenProviderAccessor tokenProviderAccessor,
                                     IHttpClientFactory httpClientFactory) : base(tokenProviderAccessor) {

            _httpClientFactory = httpClientFactory;
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account , RemoteAuthenticationUserOptions options) {
            var initalUser = await base.CreateUserAsync(account , options);

            if (initalUser.Identity.IsAuthenticated) {

                var userIdentity = (ClaimsIdentity)initalUser.Identity;

                using (var httpClient = _httpClientFactory.CreateClient("TicketsBasket.Api")) {
                    var response = await httpClient.GetFromJsonAsync<OperationResponse<UserProfileDetail>>("api/userprofiles");
                    if (response.IsSuccess) {
                        var role = response.Record.IsOrganizer ? "Organizer" : "User";
                        userIdentity.AddClaim(new Claim(ClaimTypes.Role , role));
                    }
                }
            }

            return initalUser;
        }

    }
}
