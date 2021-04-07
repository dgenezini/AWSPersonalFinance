using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalFinance.Application.Interactors;
using PersonalFinance.Application.Repositories;
using PersonalFinance.Application.Services;
using PersonalFinance.Application.Services.Itau;
using PersonalFinance.Application.Services.Nubank;

namespace PersonalFinance.LambdaAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AmazonDynamoDBClient>(a =>
            {
                var clientConfig = new AmazonDynamoDBConfig();
                clientConfig.RegionEndpoint = RegionEndpoint.SAEast1;

                return new AmazonDynamoDBClient(clientConfig);
            });

            services.AddScoped<AmazonS3Client>(a =>
            {
                return new AmazonS3Client(RegionEndpoint.SAEast1);
            });

            services.AddScoped<TransacaoRepository>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<ContaRepository>();
            services.AddScoped<FormaPagamentoRepository>();
            services.AddScoped<PrevistoRepository>();
            services.AddScoped<InvestimentoRepository>();
            services.AddScoped<TransacaoLocalRepository>();

            services.AddScoped<ImportarInteractor>();
            services.AddScoped<DeleteCategoriasInteractor>();
            services.AddScoped<GetCategoriasInteractor>();
            services.AddScoped<PostCategoriasInteractor>();
            services.AddScoped<PutCategoriasInteractor>();
            services.AddScoped<DeleteContasInteractor>();
            services.AddScoped<GetContasInteractor>();
            services.AddScoped<PostContasInteractor>();
            services.AddScoped<PutContasInteractor>();
            services.AddScoped<DeleteFormasPagtoInteractor>();
            services.AddScoped<GetFormasPagtoInteractor>();
            services.AddScoped<PostFormasPagtoInteractor>();
            services.AddScoped<PutFormasPagtoInteractor>();
            services.AddScoped<DeletePrevistosInteractor>();
            services.AddScoped<GetPrevistosInteractor>();
            services.AddScoped<PostPrevistosInteractor>();
            services.AddScoped<PutPrevistosInteractor>();
            services.AddScoped<DeleteTransacoesInteractor>();
            services.AddScoped<GetTransacoesInteractor>();
            services.AddScoped<PostTransacoesInteractor>();
            services.AddScoped<PutTransacoesInteractor>();
            services.AddScoped<GetResumoInteractor>();
            
            services.AddScoped<ImportadorServices>();

            services.AddScoped<ItauCSVServices>();
            services.AddScoped<ItaucardExcelServices>();
            services.AddScoped<NubankCSVServices>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
