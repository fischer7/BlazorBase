using Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace Client;
public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder
                      .CreateDefault(args)
                      .AddRootComponents()
                      //.AddAuth()
                      .AddClientServices();

        await builder.Build().RunAsync();
    }
}