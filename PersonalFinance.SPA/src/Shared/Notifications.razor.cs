using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Shared
{
    public partial class Notifications : ComponentBase
    {
        [Inject]
        private ReadNotificationService _ReadNotificationService { get; set; }

        public string Mensagem { get; set; }

        protected override void OnInitialized()
        {
            _ReadNotificationService.OnChange += OnReceiveNotification;
        }

        private void OnReceiveNotification(string mensagem)
        {
            Mensagem = mensagem;
        }

        public void Dispose()
        {
            _ReadNotificationService.OnChange -= OnReceiveNotification;
        }
    }
}
