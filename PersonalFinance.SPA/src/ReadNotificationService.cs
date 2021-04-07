using System;
using System.Timers;

namespace PersonalFinance
{
    public class ReadNotificationService
    {
        private bool _Running = true;
        private readonly Timer _Timer;

        public event Action<string> OnChange;

        private void NotifyStateChanged(string mensagem) => OnChange?.Invoke(mensagem);

        public ReadNotificationService()
        {
            //_Timer = new Timer(20000);

            //_Timer.Elapsed += async (s, e) =>
            //{
            //    await ReadNotificationsAsync();
            //};

            //_Timer.Enabled = true;
        }
    }
}
