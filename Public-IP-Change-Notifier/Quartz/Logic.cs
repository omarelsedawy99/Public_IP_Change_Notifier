using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Tulpep.NotificationWindow;
using System.Windows.Forms;

namespace Public_IP_Change_Notifier.Quartz
{
    class Logic
    {
        public void Process()
        {
            String oldIP;
            try
            {
                StreamReader sr = new StreamReader("IP.txt");
                oldIP = sr.ReadLine();
                sr.Close();
                while (oldIP != null)
                {
                    Command command = new Command("nslookup myip.opendns.com resolver1.opendns.com");
                    Runspace runspace = RunspaceFactory.CreateRunspace();
                    runspace.Open();
                    Pipeline pipeline = runspace.CreatePipeline();
                    pipeline.Commands.AddScript("nslookup myip.opendns.com resolver1.opendns.com");
                    Collection<PSObject> results = pipeline.Invoke();
                    runspace.Close();

                    string newIP = results[4].ToString().Substring(10);
                    if(newIP.Equals(oldIP))
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            //StreamWriter sw = new StreamWriter("IP.txt");
                            //sw.WriteLine(newIP);
                            //sw.Close();
                            NotifyIcon notifyIcon = new NotifyIcon(new System.ComponentModel.Container());
                            notifyIcon.Visible = true;
                            notifyIcon.ShowBalloonTip(1000, "Public IP Notifier", newIP, ToolTipIcon.Info);
                            //PopupNotifier popupNotifier = new PopupNotifier();
                            //popupNotifier.Image = Properties.Resources.info;
                            //popupNotifier.TitleText = "Public IP Changed";
                            //popupNotifier.ContentText = newIP;
                            //popupNotifier.Popup();

                            return;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
