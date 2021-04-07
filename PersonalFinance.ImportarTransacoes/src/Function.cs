using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Application.Interactors;
using PersonalFinance.Application.Repositories;
using PersonalFinance.Application.Services;
using PersonalFinance.Application.Services.Itau;
using PersonalFinance.Application.Services.Nubank;
using PersonalFinance.Domain.Exceptions;
using PersonalFinance.Domain.Values;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PersonalFinance.ImportarTransacoes
{
    public class Function
    {
        private readonly ServiceCollection _ServiceCollection;

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Function()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            _ServiceCollection = new ServiceCollection();

            _ServiceCollection.AddScoped<AmazonDynamoDBClient>(a =>
            {
                var clientConfig = new AmazonDynamoDBConfig();

                clientConfig.RegionEndpoint = RegionEndpoint.SAEast1;

                return new AmazonDynamoDBClient(clientConfig);
            });

            _ServiceCollection.AddScoped<AmazonS3Client>(a =>
            {
                return new AmazonS3Client(RegionEndpoint.SAEast1);
            });

            _ServiceCollection.AddScoped<ImportarInteractor>();

            _ServiceCollection.AddScoped<ImportadorServices>();
            _ServiceCollection.AddScoped<ItauCSVServices>();
            _ServiceCollection.AddScoped<ItaucardExcelServices>();
            _ServiceCollection.AddScoped<NubankCSVServices>();

            _ServiceCollection.AddScoped<TransacaoRepository>();
            _ServiceCollection.AddScoped<CategoriaRepository>();
            _ServiceCollection.AddScoped<ContaRepository>();
            _ServiceCollection.AddScoped<TransacaoLocalRepository>();

            _ServiceCollection.AddScoped<PostTransacoesInteractor>();
        }

        public async Task<string> FunctionHandlerAsync(S3Event evnt, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine($"Lambda started.");

                using (ServiceProvider ServiceProvider = _ServiceCollection.BuildServiceProvider())
                {
                    var AmazonS3Client = ServiceProvider.GetService<AmazonS3Client>();

                    var ImportarInteractor = ServiceProvider.GetService<ImportarInteractor>();

                    foreach (var record in evnt.Records)
                    {
                        var s3 = record.S3;

                        var S3ObjectKey = HttpUtility.UrlDecode(s3.Object.Key);

                        context.Logger.LogLine($"{S3ObjectKey} started.");

                        ImportacaoTipo ImportacaoTipo;

                        if (s3.Bucket.Name == ImportacaoTipo.ItauCardExcel.ToString().ToLower())
                        {
                            ImportacaoTipo = ImportacaoTipo.ItauCardExcel;
                        }
                        else if (s3.Bucket.Name == ImportacaoTipo.ItauCSV.ToString().ToLower())
                        {
                            ImportacaoTipo = ImportacaoTipo.ItauCSV;
                        }
                        else if (s3.Bucket.Name == ImportacaoTipo.NubankCSV.ToString().ToLower())
                        {
                            ImportacaoTipo = ImportacaoTipo.NubankCSV;
                        }
                        else
                        {
                            throw new PersonalFinanceControlException("Tipo de importação inválido");
                        }

                        var ObjectStream = await GetS3ObjectAsync(s3.Bucket.Name, S3ObjectKey, AmazonS3Client);

                        context.Logger.LogLine($"{S3ObjectKey} read.");

                        await ImportarInteractor.ImportarAsync(ImportacaoTipo, ObjectStream);

                        context.Logger.LogLine($"{S3ObjectKey} imported.");

                        await DeleteS3ObjectAsync(s3.Bucket.Name, S3ObjectKey, AmazonS3Client);

                        context.Logger.LogLine($"{S3ObjectKey} deleted.");
                    }
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                context.Logger.LogLine(ex.Message);

                throw;
            }
        }

        private static async Task<Stream> GetS3ObjectAsync(string bucketName, string objectKey,
            AmazonS3Client amazonS3Client)
        {
            var response = await amazonS3Client
                .GetObjectAsync(bucketName, objectKey);

            var MemoryStream = new MemoryStream();
            response.ResponseStream.CopyTo(MemoryStream);

            return MemoryStream;
        }

        private static async Task<bool> DeleteS3ObjectAsync(string bucketName, string objectKey,
            AmazonS3Client amazonS3Client)
        {
            var response = await amazonS3Client
                .DeleteObjectAsync(bucketName, objectKey);

            return true;
        }
    }
}
