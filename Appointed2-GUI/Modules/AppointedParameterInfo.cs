using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointed2_GUI.Modules
{
    public abstract class AppointedParameterInfo
    {

        /// <summary>
        /// 格式化为命令行格式
        /// </summary>
        /// <param name="value">值类型</param>
        /// <returns>返回字符串</returns>
        public abstract string FormatString(object value = null);
        /// <summary>
        /// 是否是标志参数
        /// </summary>
        /// <returns></returns>
        public abstract bool IsFlagParameter();
        /// <summary>
        /// 指定的默认值
        /// </summary>
        public object DefaultValue { get; private set; }
        /// <summary>
        /// 参数值的类型
        /// </summary>
        public Types.AppointedTypesInfo ValueType { get; private set; }
        /// <summary>
        /// 抽象类的构造函数
        /// </summary>
        /// <param name="type">参数值类型</param>
        /// <param name="defaultvalue">默认值</param>
        public AppointedParameterInfo(Types.AppointedTypesInfo type, object defaultvalue=null)
        {
            this.DefaultValue = defaultvalue;
            this.ValueType = type;
        }
       
    }
    /// <summary>
    /// 键-值对参数类型，也可以是标志类型的参数（当值类型为NONE时）
    /// </summary>
    public sealed class AppointedKeyWordParameterInfo:AppointedParameterInfo, IEquatable<AppointedKeyWordParameterInfo>
    {
        /// <summary>
        /// 短名称字符
        /// </summary>
        public char ShortName { get; private set; }
        /// <summary>
        /// 长名称
        /// </summary>
        public string LongName { get; private set; }
        public override string FormatString(object value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}", this.LongName);
            if(this.IsFlagParameter() == false)
            {
                //如果传入的参数以及默认的参数为null，那么认为这个参数是没有参数的类型。那么完成否则继续添加数据
                if (!(value == null && this.DefaultValue == null))
                {
                    sb.AppendFormat("={0}", value != null ? value : (this.DefaultValue != null ? this.DefaultValue : "null"));
                }
            }
            return sb.ToString();

        }

        /// <summary>
        /// 是否是标志参数
        /// </summary>
        /// <returns>返回值</returns>
        public override bool IsFlagParameter()
        {
            return this.ValueType == Types.AppointedTypesInfo.NONE;
        }
        /// <summary>
        /// 比较两个参数是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AppointedKeyWordParameterInfo other)
        {
            if (other != null)
                return this.ShortName.Equals(other.ShortName) && this.LongName.Equals(other.LongName);
            else
                return false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shortname">短参数名</param>
        /// <param name="longname">长参数名</param>
        /// <param name="valuetype">值类型（如果值类型为None，则参数时标志参数）</param>
        /// <param name="defaultvalue">默认值</param>
        public AppointedKeyWordParameterInfo(char shortname, string longname, Types.AppointedTypesInfo valuetype, string defaultvalue =null):
            base(valuetype, defaultvalue)
        {
            this.ShortName = shortname;this.LongName = longname;
        }
        public override int GetHashCode()
        {
            return this.LongName.GetHashCode() + this.ShortName.GetHashCode();
        }
    }
    public sealed class AppointedListParameterInfo : AppointedParameterInfo
    {
        public AppointedListParameterInfo(string defaultvalue = null):
            base(Types.AppointedTypesInfo.LIST, defaultvalue)
        {

        }
        public override string FormatString(object value)
        {
            if(this.ValueType == Types.AppointedTypesInfo.LIST)
            {
                var list = value as IEnumerable<object>;
                if(list !=null)
                {
                    var result = string.Join(" ", list);
                    return result;
                }
                else
                {
                    throw new ArgumentException("无效的传入参数");
                }
            }
            //非可枚举的类型
            return value.ToString();
        }

        public override bool IsFlagParameter()
        {
            return false;
        }
    }
}
