using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointed2_GUI.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Appointed2_GUI.Modules
{
    /// <summary>
    /// 定义命令的信息
    /// </summary>
    public sealed class AppointedCmdInfo:IEquatable<AppointedCmdInfo>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 定义命令的类型
        /// </summary>
        public enum CmdTypeInfo
        {
            /// <summary>
            /// 上传到服务端处理
            /// </summary>
            Online,
            /// <summary>
            /// 客户端直接处理
            /// </summary>
            Offline
        }
        [JsonProperty]
        /// <summary>
        /// 定义的命令关联的命令名称
        /// </summary>
        public string AssociatedCmd { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 命令唯一标识符，例如{2729005D-ADC9-4F5F-B475-4BB2DA722935}等
        /// </summary>
        public string Guid { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 友好的名称
        /// </summary>
        public string FriendlyName { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 命令的类型
        /// </summary>
        public CmdTypeInfo CmdType { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 标志控制按键（用于删选FunctionKey）
        /// </summary>
        public Appointed2_GUI.Events.AppointedHotKeyMonitor.KeyModifiers FlagKey { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 功能按键
        /// </summary>
        public System.Windows.Forms.Keys FunctionKey { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 指定的参数，这些参数需要有默认值，否则会用null作为代替
        /// </summary>
        private HashSet<AppointedParameterInfo> parameters;
        [JsonProperty]
        /// <summary>
        /// 程序默认接收的参数值，用于接收由事件返回的唯一参数值。可以为空
        /// </summary>
        private AppointedParameterInfo defaultParameter;



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="guid">命令的唯一标识符，例如{2729005D-ADC9-4F5F-B475-4BB2DA722935}</param>
        /// <param name="associatedCmd">这个运行信息关联的唯一的命令</param>
        /// <param name="friendlyName">友好的名称，例如查询单词，这个由用户决定</param>
        /// <param name="type">命令类型，指定命令是在本机还是远程服务端运行</param>
        /// <param name="parameters">参数</param>
        /// <param name="defaultParameter">默认的参数，用于接收由事件返回的唯一参数值</param>
        /// <param name="flagKey">标志控制按键（用于删选FunctionKey）</param>
        /// <param name="functionKey">功能按键</param>
        public AppointedCmdInfo(string guid, string associatedCmd,string friendlyName, CmdTypeInfo type,
            IEnumerable<AppointedParameterInfo> parameters, AppointedParameterInfo defaultParameter=null,
            Appointed2_GUI.Events.AppointedHotKeyMonitor.KeyModifiers flagKey= Appointed2_GUI.Events.AppointedHotKeyMonitor.KeyModifiers.None, System.Windows.Forms.Keys functionKey=System.Windows.Forms.Keys.None)
        {
            this.Guid = guid;
            this.AssociatedCmd = associatedCmd;
            this.FriendlyName = friendlyName;
            this.CmdType = type;
            this.FlagKey = flagKey;
            this.FunctionKey = functionKey;
            this.defaultParameter = defaultParameter;
            //添加指定的参数
            if(parameters !=null)
            {
                this.parameters = new HashSet<AppointedParameterInfo>();
                foreach (var para in parameters)
                {
                    this.parameters.Add(para);
                }
            }
        }
        /// <summary>
        /// 输出命令行格式的命令字符串
        /// </summary>
        /// <param name="defaultvalue">事件返回的唯一参数值</param>
        /// <returns>命令行字符串</returns>
        public string FormatString(object defaultvalue=null)
        {
            StringBuilder result = new StringBuilder(this.AssociatedCmd);
            //所有的非默认参数进行输出
            foreach (var item in this.parameters)
            {
                result.AppendFormat(" {0}", item.FormatString());
            }
            //计算默认的参数的信息
            if (defaultParameter !=null)
            {
                result.AppendFormat(" {0}", defaultParameter.FormatString(defaultvalue));
            }
            //返回值
            return result.ToString();
        }


        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }

        public bool Equals(AppointedCmdInfo other)
        {
            if(other !=null)//HashSet还比较了Equals https://stackoverflow.com/questions/8952003/how-does-hashset-compare-elements-for-equality
            {
                return this.Guid.Equals(other.Guid);
            }
            return false;
        }

       
    }
}
