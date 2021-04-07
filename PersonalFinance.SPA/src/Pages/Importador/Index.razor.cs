using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalFinance.Pages.Importador
{
    [Authorize]
    public partial class Index : ComponentBase
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }
        [Inject]
        private IHttpClientFactory _ClientFactory { get; set; }

        public string ImportacaoTipo { get; set; }

        public Stream Arquivo { get; set; }
        public string ArquivoNome { get; set; }

        public string Mensagem { get; set; }
        public bool IsError { get; set; }

        public IDictionary<string, string> ImportacaoTipos { get; set; }

        protected override void OnInitialized()
        {
            ImportacaoTipos = new Dictionary<string, string>();
            ImportacaoTipos.Add("itaucsv", "Itaú (*.csv)");
            ImportacaoTipos.Add("itaucardexcel", "Itaucard (*.xls)");
            ImportacaoTipos.Add("nubankcsv", "Nubank (*.csv)");
        }

        private async Task OnInputFileChangeAsync(InputFileChangeEventArgs e)
        {
            long maxFileSize = 1024 * 1024 * 1;

            if (e.File.Size < maxFileSize)
            {
                Arquivo = e.File.OpenReadStream();
                ArquivoNome = e.File.Name;
            }
            else
            {
                Arquivo = null;
                ArquivoNome = null;

                //logger.LogInformation("{FileName} not uploaded", file.Name);
            }
        }

        private async Task OnSubmitAsync()
        {
            if ((Arquivo == null) || (Arquivo.Position > 0))
            {
                Mensagem = "Arquivo não selecionado ou inválido";
                IsError = true;

                return;
            }

            var ObjectKey = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss.fff") + "-" + ArquivoNome;

            ImportacaoTipo = ImportacaoTipo.ToLower();

            var PresignedUrl = await _HttpClient
                .GetStringAsync($"GetPresignedUrl?bucketName={ImportacaoTipo}&objectKey={ObjectKey}");

            if (!string.IsNullOrEmpty(PresignedUrl))
            {
                var S3UploadHttpClient = _ClientFactory.CreateClient("S3UploadHttpClient");

                S3UploadHttpClient.DefaultRequestHeaders.Add("Origin", "Origin");

                var Response = await S3UploadHttpClient.PutAsync(PresignedUrl,
                    new StreamContent(Arquivo));

                if (!Response.IsSuccessStatusCode)
                {
                    var Erro = await Response.Content.ReadAsStringAsync();

                    Mensagem = "Erro";
                    IsError = true;

                    return;
                }

                Mensagem = "Importado com sucesso";
                IsError = false;

                Arquivo = null;
            }
        }
    }
}
