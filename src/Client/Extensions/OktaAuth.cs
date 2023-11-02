using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Client.Extensions;
public static class OktaAuth
{
    public static WebAssemblyHostBuilder AddAuth(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient("OktaWASM.ServerAPI", client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("OktaWASM.ServerAPI"));

        builder.Services.AddOidcAuthentication(options =>
        {
            options.ProviderOptions.Authority = builder.Configuration["Okta:Authority"];
            options.ProviderOptions.ClientId = builder.Configuration["Okta:ClientId"];
            options.ProviderOptions.ResponseType = "code";
        });
        var authority = builder.Configuration["Okta:ClientId"];
        builder.Services.AddApiAuthorization();
        return builder;
    }

}