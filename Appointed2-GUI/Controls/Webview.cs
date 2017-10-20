using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appointed2_GUI.Events;

namespace Appointed2_GUI.Controls
{
    public partial class Webview : UserControl, Runtime.IAppointedSender
    {
        private EventHandler<Events.AppointedRequestEventArgs> receiveHandler_single;
        //私有成员
        /// <summary>
        /// 是否显示命令条
        /// </summary>
        private bool showCommandBar=true;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Webview()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 是否显示命令条
        /// </summary>
        public bool ShowCommandBar
        {
            get
            {
                return this.showCommandBar;
            }
            set
            {
                this.showCommandBar = value;
                this.textBox1.Visible = value;
            }
        }
        /// <summary>
        /// 定位到URL
        /// </summary>
        /// <param name="url">url</param>
        public void Navigate(string url)
        {
            //this.webBrowser1.Navigate(url, false);
        }
        //自定义的事件的处理器
        /// <summary>
        /// 完成页面加载的事件处理器
        /// </summary>
        /// <param name="sender">事件的发送方</param>
        /// <param name="e">事件参数</param>
        public delegate void WebLoadFinishedEventHandler(object sender, WebBrowserDocumentCompletedEventArgs e);
        //声明事件
        /// <summary>
        /// 网页加载完成事件
        /// </summary>
        public event WebLoadFinishedEventHandler WebLoadedFinishedEvent;
        //当前网页的触发事件，定义为私有的
        private void webBrowser_DocumentLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(WebLoadedFinishedEvent !=null)
            {
                WebLoadedFinishedEvent(this.webBrowserEx1, e);
            }
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r')
            {
                //string location = "http://localhost:9000/api/Runcmd/" + textBox1.Text;
                //webBrowser1.Navigate(location);
                if(textBox1.Text.Length > 0)
                {
                    int space = textBox1.Text.IndexOf(" ");
                    if (space > 0)
                    {
                        string cmdname = textBox1.Text.Substring(0, space);
                        string location = "http://localhost:9000/api/Runcmd?cmdname=" + cmdname + "&cmdline=\"" + textBox1.Text + "\"&redirectType=normal";
                        this.webBrowserEx1.Navigate(location);
                    }
                    else
                    {
                        if(textBox1.Text == "/")
                        {
                            this.webBrowserEx1.Navigate("http://localhost:9000/");
                        }
                        else
                        {
                            string location = "http://localhost:9000/" + textBox1.Text;
                            this.webBrowserEx1.Navigate(location);
                        }
                    }
                }
            }
        }

        private void webBrowser_NewWindow(object sender, Controls.WebBrowserNewWindowEventArgs e)
        {
            WebBrowserEx webBrowser = (WebBrowserEx)sender;
            Uri ur = new Uri(e.Url);
            if (this.SendRequestEvent !=null)
            {
                this.SendRequestEvent(this, new Appointed2_GUI.Events.AppointedRequestEventArgs(e.Url));
            }
            e.Cancel = true;
        }
        /// <summary>
        /// 接收请求事件的处理程序
        /// </summary>
        /// <param name="sender">发送方</param>
        /// <param name="e">事件参数</param>
        private void receiveRequestHandler(object sender, Events.AppointedRequestEventArgs e)
        {
            
            
            MessageBox.Show(e.FormatString);
            this.textBox1.Text = e.FormatString;
            this.textBox1_KeyPress(this.textBox1, new KeyPressEventArgs('\r'));
        }

        public void AddSendEventHandler(EventHandler<Events.AppointedRequestEventArgs> handler)
        {
            this.SendRequestEvent += handler;
        }

        public void RemoveEventHandler(EventHandler<AppointedRequestEventArgs> handler)
        {
            this.SendRequestEvent -= handler;
        }

        public EventHandler<Events.AppointedRequestEventArgs> GetReceiveEventHandler()
        {
            if (this.receiveHandler_single == null)
                this.receiveHandler_single = new EventHandler<AppointedRequestEventArgs>(this.receiveRequestHandler);
            return this.receiveHandler_single;
        }

        /// <summary>
        /// 定义的发出请求的事件
        /// </summary>
        private event EventHandler<Events.AppointedRequestEventArgs> SendRequestEvent;
    }
}
