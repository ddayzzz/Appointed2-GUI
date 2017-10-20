using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Appointed2_GUI.Events
{
    /// <summary>
    /// 监控用户按下的热键信息
    /// 参考 http://blog.csdn.net/YanMang/article/details/4246595
    /// 参考 Quick C#版本
    /// </summary>
    public class AppointedHotKeyMonitor:System.Windows.Forms.IMessageFilter, IDisposable
    {
        //如果函数执行成功，返回值不为0。  
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。  
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄  
            UInt32 id,                     //定义热键ID（不能与其它ID重复）             
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效  
            Keys vk                     //定义热键的内容  
            );

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄  
            UInt32 id                      //要取消热键的ID  
            );
        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）  
        [Flags()]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        /// <summary>
        /// 向全局原子表添加一个字符串，并返回这个字符串的唯一标识符（原子ATOM）。
        /// </summary>
        /// <param name="lpString">字符串</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern UInt32 GlobalAddAtom(String lpString);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);
        //定义参考 https://msdn.microsoft.com/en-us/library/system.windows.forms.imessagefilter.prefiltermessage(v=vs.110).aspx
        /// <summary>
        /// 筛选消息
        /// </summary>
        /// <param name="m">传入的消息</param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            //处理即传递给窗口句柄的消息
            if (m.Msg == 0x312)
            {
                if (OnHotkeyPress != null)
                {
                    foreach(var item in this.registriedKeys)
                    {
                        if((UInt32)m.WParam == item.Value)
                        {
                            OnHotkeyPress(this.Hwnd, new AppointedHotKeyPressEventArgs(item.Value, new string[] { "Hello", "apple" }));
                            return true;//消息被成功的处理
                        }
                    }
                }
            }
            return false;//向其他的处理程序传递
        }

        
        /// <summary>
        /// 构造一个热键管理器
        /// </summary>
        /// <param name="hWnd">绑定的窗口的句柄</param>
        public AppointedHotKeyMonitor(IntPtr hWnd)
        {
            this.Hwnd = Hwnd;
            this.registriedKeys = new Dictionary<KeyValuePair<KeyModifiers, Keys>, uint>();
        }
        /// <summary>
        /// 注册一个热键
        /// </summary>
        /// <param name="flagKey">控制键Ctrl、Alt和Win，可以用管道命令制定多个筛选</param>
        /// <param name="functionKey">字母，表示绑定的按键</param>
        /// <returns>返回注册的事件全局唯一的ID(UINT32类型)</returns>
        public UInt32 RegistryHotKey(KeyModifiers flagKey, Keys functionKey)
        {

            try
            {
                //是否有存在的按键？
                var keyCombine = new KeyValuePair<KeyModifiers, Keys>(flagKey, functionKey);
                if (!this.registriedKeys.ContainsKey(keyCombine))
                {
                    //添加注册的程序
                    UInt32 hotkeyid = GlobalAddAtom(global::System.Guid.NewGuid().ToString());
                    RegisterHotKey(this.Hwnd, hotkeyid, flagKey, functionKey);
                    //注册成功，看看是否是第一个
                    if (this.registriedKeys.Count() == 0)
                        System.Windows.Forms.Application.AddMessageFilter(this);//添加事件注册程序
                    registriedKeys.Add(keyCombine, hotkeyid);
                    return hotkeyid;
                }
                return registriedKeys[keyCombine];
            }
            catch(Exception e)
            {
                MessageBox.Show(caption: "错误", text: e.Message);
                return 0U;
            }
            
        }
        /// <summary>
        /// 解除注册一个热键
        /// </summary>
        /// <param name="flagKey">控制键Ctrl、Alt和Win，可以用管道命令制定多个筛选</param>
        /// <param name="functionKey">字母，表示绑定的按键</param>
        /// <returns>返回是否成功呢解除按键</returns>
        public bool UnregistryHotKey(KeyModifiers flagKey, Keys functionKey)
        {
            try
            {
                if (this.registriedKeys.Count == 0)
                    return false;
                var keyCombine = new KeyValuePair<KeyModifiers, Keys>(flagKey, functionKey);
                if (this.registriedKeys.ContainsKey(keyCombine))
                {
                    UnregisterHotKey(this.Hwnd, this.registriedKeys[keyCombine]);
                    GlobalDeleteAtom(this.registriedKeys[keyCombine]);
                    this.registriedKeys.Remove(keyCombine);
                    if (this.registriedKeys.Count == 0)
                        System.Windows.Forms.Application.RemoveMessageFilter(this);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 卸载所有注册时热键
        /// </summary>
        /// <returns>是否成功</returns>
        public bool UnregistryAllHotKeys()
        {
            try
            {
                if (this.registriedKeys.Count() > 0)
                {
                    foreach (var item in this.registriedKeys)
                    {
                        UnregisterHotKey(this.Hwnd, item.Value);
                        GlobalDeleteAtom(item.Value);
                    }
                    this.registriedKeys.Clear();
                    System.Windows.Forms.Application.RemoveMessageFilter(this);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            this.UnregistryAllHotKeys();
        }

        /// <summary>
        /// 绑定的句柄
        /// </summary>
        internal IntPtr Hwnd { get; private set; }
        /// <summary>
        /// 事件处理的委托
        /// </summary>
        /// <param name="sender">事件的发送者</param>
        /// <param name="eventArgs">事件的参数</param>
        public delegate void HotkeyEventHandler(object sender, AppointedHotKeyPressEventArgs eventArgs);
        /// <summary>
        /// 热键触发的事件
        /// </summary>
        public event HotkeyEventHandler OnHotkeyPress;
        /// <summary>
        /// 绑定x+x+x的已经注册的按键的信息
        /// </summary>
        private Dictionary<KeyValuePair<KeyModifiers, Keys>, UInt32> registriedKeys;
    }
}
