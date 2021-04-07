using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalFinance
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = builder.Configuration["Cognito:Authority"];
                options.ProviderOptions.ResponseType = builder.Configuration["Cognito:ResponseType"];
                options.ProviderOptions.MetadataUrl = builder.Configuration["Cognito:MetadataUrl"];
                options.ProviderOptions.ClientId = builder.Configuration["Cognito:ClientId"];
            });

            builder.Services.AddScoped<TokenHttpClientHandler>();

            builder.Services.AddScoped(sp =>
            {
                var Configuration = sp.GetService<IConfiguration>();
                var TokenHttpClientHandler = sp.GetService<TokenHttpClientHandler>();

                return new HttpClient(TokenHttpClientHandler)
                {
                    BaseAddress = new Uri(Configuration["API:URL"])
                };
            });

            builder.Services.AddHttpClient("S3UploadHttpClient", (sp, client) =>
            {
                var Configuration = sp.GetService<IConfiguration>();

                client.BaseAddress = new Uri(Configuration["API:URL"]);
            });

            builder.Services.AddScoped<AmazonS3Client>(a =>
            {
                return new AmazonS3Client(RegionEndpoint.SAEast1);
            });

            builder.Services.AddSingleton<ReadNotificationService>();

            await builder.Build().RunAsync();
        }
    }
}
