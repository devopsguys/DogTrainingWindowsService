namespace DogTrainingWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DogTrainingProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.dogTrainingInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DogTrainingProcessInstaller
            // 
            this.DogTrainingProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.DogTrainingProcessInstaller.Password = null;
            this.DogTrainingProcessInstaller.Username = null;
            // 
            // dogTrainingInstaller
            // 
            this.dogTrainingInstaller.ServiceName = "DogTrainingWindowsService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DogTrainingProcessInstaller,
            this.dogTrainingInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DogTrainingProcessInstaller;
        private System.ServiceProcess.ServiceInstaller dogTrainingInstaller;
    }
}