using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Twilio;

namespace DogTrainingWindowsService
{
    public sealed partial class DogTrainingWindowsService : ServiceBase
    {
        public const string DogLogSource = "DogTrainingBarkSender";
        public const string DogLog = "DogTrainingBarkLog";
        public const string GetLatestBarkApiPath = "api/DogBarkApi/GetLatest";

        #region AppSettings

        private readonly int BarkCheckIntervalMs = int.Parse(ConfigurationManager.AppSettings["BarkCheckIntervalSeconds"]) * 1000;
        private readonly string BarkCloudServiceUrl = ConfigurationManager.AppSettings["BarkCloudServiceUrl"];
        private readonly string TwilioAccountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
        private readonly string TwilioAuthToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string TwilioSenderNumber = ConfigurationManager.AppSettings["TwilioSenderNumber"];
        private readonly string TwilioRecipentNumber = ConfigurationManager.AppSettings["TwilioRecipentNumber"];

        #endregion

        private Timer BarkCheckTimer;
        private DogBarkModel LatestBark;
        private TwilioRestClient TwilioClient;

        public DogTrainingWindowsService()
        {
            EventLog.Source = DogLogSource;
            EventLog.Log = DogLog;

            if (!EventLog.SourceExists(DogLogSource)) { EventLog.CreateEventSource(DogLogSource, DogLog); }

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LatestBark = GetLatestBark().GetAwaiter().GetResult();

                BarkCheckTimer = new Timer { Interval = BarkCheckIntervalMs };
                BarkCheckTimer.Elapsed += OnTimer;
                BarkCheckTimer.Start();
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry(exception.Message, EventLogEntryType.Error);
                throw;
            }
        }

        protected override void OnStop()
        {
            BarkCheckTimer.Stop();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                EventLog.WriteEntry("Checking for new bark", EventLogEntryType.Information);
                var newLatestBark = GetLatestBark().GetAwaiter().GetResult();
                if (newLatestBark != null && (LatestBark == null || newLatestBark.Id != LatestBark.Id))
                {
                    EventLog.WriteEntry("Sending new bark: " + newLatestBark.Bark, EventLogEntryType.Information);

                    var twilioClient = new TwilioRestClient(TwilioAccountSid, TwilioAuthToken);
                    twilioClient.SendMessage(TwilioSenderNumber, TwilioRecipentNumber, "🐶 " + newLatestBark.Bark);

                    LatestBark = newLatestBark;
                }
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry(exception.Message, EventLogEntryType.Error);
            }
        }

        private async Task<DogBarkModel> GetLatestBark()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BarkCloudServiceUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(GetLatestBarkApiPath);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<DogBarkModel>() : null;
            }
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