using Public_IP_Change_Notifier.Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Public_IP_Change_Notifier
{
    public partial class Notifier : Form
    {
        SchedulerManager _objSchedulerManager;
        public Notifier()
        {
            InitializeComponent();
            StopButton.Enabled = false;
        }

        private void StartWorking(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            StopButton.Enabled = true;
            _objSchedulerManager = new SchedulerManager();
            _objSchedulerManager.GetTriggerCron(_objSchedulerManager.GetJobs(), "* * * ? * *");
        }

        private void StopWorking(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
            StopButton.Enabled = false;
            _objSchedulerManager.Stop();
        }

        private void Notifier_Load(object sender, EventArgs e)
        {

        }
    }
}
