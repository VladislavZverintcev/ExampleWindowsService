using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;

namespace ExampleWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DoSomeWork();
        }
        async void DoSomeWork()
        {
            await Task.Run(() =>
            {
                string username = String.Empty;
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
                    string fullname = (string)searcher.Get().Cast<ManagementBaseObject>().First()["UserName"];
                    username = fullname.Substring(fullname.IndexOf(@"\") + 1);
                }
                catch
                {
                    Stop();
                }

                while (true)
                {
                    try
                    {
                        if (File.Exists(@"C:\Users\" + username + @"\Desktop\☻.txt"))
                        {
                            File.WriteAllText(@"C:\Users\" + username + @"\Desktop\☺.txt", "File ☻.txt is Exist");
                            Task.Delay(5000);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            );
        }

        protected override void OnStop()
        {
        }
    }
}
