//ref http://www.cnblogs.com/yanweidie/p/4605212.html
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;
namespace Appointed2_GUI.Runtime
{
    /// <summary>
    /// 指定Appointed2的应用程序管理器
    /// </summary>
    public sealed class AppointedManager
    {
        private static Modules.AppointedModulesManager m_single_ModulesInfo;
        private AppointedManager() { }
        public static Modules.AppointedModulesManager GetModulesManager()
        {
            if (m_single_ModulesInfo == null)
                m_single_ModulesInfo = new Modules.AppointedModulesManager();
            return m_single_ModulesInfo;
        }
    }
}
