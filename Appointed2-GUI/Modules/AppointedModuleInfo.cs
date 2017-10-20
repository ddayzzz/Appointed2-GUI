using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace Appointed2_GUI.Modules
{
    /// <summary>
    /// 定义模块的信息
    /// </summary>
    public class AppointedModuleInfo:IEquatable<AppointedModuleInfo>
    {
        //[JsonProperty]
        /// <summary>
        /// 模块的唯一的标识符
        /// </summary>
        public string Guid { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 模块名称, 例如查询单词
        /// </summary>
        public string Name { get; private set; }
        [JsonProperty]
        /// <summary>
        /// 关联的模块命令。一个命令可以有多个不同的FriendlyName
        /// </summary>
        public HashSet<AppointedCmdInfo> AssociatedCmdsInfo { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="guid">模块的唯一的标识符</param>
        public AppointedModuleInfo(string name, string guid)
        {
            this.Name = name;
            this.Guid = guid;
            this.AssociatedCmdsInfo = new HashSet<AppointedCmdInfo>();
        }
        /// <summary>
        /// 添加一条新的命令
        /// </summary>
        /// <param name="newCmd">新的命令，具有重复的Guid的命令将无法添加</param>
        /// <returns>是否成功</returns>
        public bool AddCmd(AppointedCmdInfo newCmd)
        {
            if(newCmd !=null)
            {
                return this.AssociatedCmdsInfo.Add(newCmd);
            }
            return false;
        }
        /// <summary>
        /// 移除具有给定FriendlyName的命令
        /// </summary>
        /// <param name="guid">运行信息的唯一的标识符</param>
        /// <returns>移除的命令个数</returns>
        public int RemoveCmd(string guid)
        {
            if(this.AssociatedCmdsInfo != null)
            {
                int removedNum = this.AssociatedCmdsInfo.RemoveWhere((AppointedCmdInfo info) =>
                {
                    return info.FriendlyName.Equals(guid);
                });
                return removedNum;
            }
            return 0;
        }
        public Modules.AppointedCmdInfo FindCmd(string guid)
        {
            var fnd = this.AssociatedCmdsInfo.Where((Modules.AppointedCmdInfo cmd)=>{
                return cmd.Guid.Equals(guid);
            });
            return fnd.FirstOrDefault();
        }
        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }
        public bool Equals(AppointedModuleInfo other)
        {
            if (other != null)
                return this.Guid.Equals(other.Name);
            else
                return false;
        }
    }
}
