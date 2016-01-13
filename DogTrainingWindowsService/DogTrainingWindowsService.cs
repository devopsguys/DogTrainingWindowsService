using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace DogTrainingWindowsService
{
    public sealed partial class DogTrainingWindowsService : ServiceBase
    {
        public static readonly string DogLogSource = "DogTrainingBarkSender";
        public static readonly string DogLog = "DogTrainingBarkLog";

        public static readonly int BarkCheckInterval = 60000; // 60 seconds

        private Timer _timer;

        public DogTrainingWindowsService()
        {
            EventLog.Source = DogLogSource;
            EventLog.Log = DogLog;

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer { Interval = BarkCheckInterval };
            _timer.Elapsed += OnTimer;
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry("Woof woof", EventLogEntryType.Information);
        }

        public void UserInteractiveStartAndStop()
        {
            Console.WriteLine("Running in interactive mode - press Enter to finish");
            OnStart(null);
            Console.ReadLine();
            OnStop();
        }
    }
}
