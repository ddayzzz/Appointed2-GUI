using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appointed2_GUI.Controls;
using Appointed2_GUI.Events;
namespace Appointed2_GUI
{
    public partial class Form1 : Form, Runtime.IAppointedSender
    {

        private bool once_loaded = false;//是否已经显示过一次调用结果
        public Form1()
        {
            InitializeComponent();
            this.webview1.WebLoadedFinishedEvent += new Webview.WebLoadFinishedEventHandler(Webview_FinishedLoad);
            AppointedHotKeyMonitor keyMonitor = new AppointedHotKeyMonitor(this.Handle);
            keyMonitor.OnHotkeyPress += new AppointedHotKeyMonitor.HotkeyEventHandler(TestForHotKey);
            keyMonitor.RegistryHotKey(AppointedHotKeyMonitor.KeyModifiers.Ctrl, Keys.B);
            keyMonitor.RegistryHotKey(AppointedHotKeyMonitor.KeyModifiers.Ctrl, Keys.T);
            //测试
            //测试消息发送
            this.webview1.AddSendEventHandler(new EventHandler<AppointedRequestEventArgs>(this.SendResquetHandler));
            this.ReceiveRequestHandler += this.webview1.GetReceiveEventHandler();
            // this.webview1.ShowCommandBar = false;
        }
        public event EventHandler<Events.AppointedRequestEventArgs> ReceiveRequestHandler;

        public void AddSendEventHandler(EventHandler<AppointedRequestEventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public EventHandler<AppointedRequestEventArgs> GetReceiveEventHandler()
        {
            throw new NotImplementedException();
        }

        public void RemoveEventHandler(EventHandler<AppointedRequestEventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void Webview_FinishedLoad(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(!once_loaded)
            {
                this.Size = new Size(700, 500);
                once_loaded = true;
            }
            
        }
        internal void TestForHotKey(object sender, AppointedHotKeyPressEventArgs e)
        {
            //var info = Appointed2_GUI.Runtime.AppointedManager.GetModulesManager();
            //var p1 = new Modules.AppointedListParameterInfo(null);
            //var p2 = new Modules.AppointedKeyWordParameterInfo('f', "force", Types.AppointedTypesInfo.STRING,"ox");
            //var p3 = new Modules.AppointedKeyWordParameterInfo('h', "help", Types.AppointedTypesInfo.NONE);
            //var info1 = new Modules.AppointedCmdInfo("{A9945D72-E80E-41D0-953C-BB63F6C91850}", "word", "查询单词", Modules.AppointedCmdInfo.CmdTypeInfo.Offline, new[]{ p2,p3 }, p1);
            //var m1 = new Modules.AppointedModuleInfo("查询单词", "{F7FFB5A3-A784-4B16-A51D-577A2B9FE6E6}");
            //m1.AddCmd(info1);
            //info.Add(m1);
            ////MessageBox.Show((info.Find("{F7FFB5A3-A784-4B16-A51D-577A2B9FE6E6}").FindCmd("{A9945D72-E80E-41D0-953C-BB63F6C91850}").Guid.Equals("{A9945D72-E80E-41D0-953C-BB63F6C91850}")).ToString());
            //this.ReceiveRequestHandler(this, new AppointedRequestEventArgs(info1.FormatString(new string[] {"apple","bear","juice" })));
            //info.SaveToFile("2.json");
        }
        private void SendResquetHandler(object sender, Events.AppointedRequestEventArgs e)
        {
            //Process
            MessageBox.Show(e.FormatString);
        }
    }
}
