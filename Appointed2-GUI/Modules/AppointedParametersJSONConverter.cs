using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
//参考 http://www.cnblogs.com/qixue/p/5292374.html
namespace Appointed2_GUI.Modules
{
    public abstract class AppointedParametersJSONConverter<T>:JsonConverter
    {
        protected abstract T Create(Type objType, JObject jsonObject);
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonobj = JObject.Load(reader);
            var target = Create(objectType, jsonobj);
            serializer.Populate(jsonobj.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class AppointedParameterInfoConverter : AppointedParametersJSONConverter<Modules.AppointedParameterInfo>
    {
        protected override AppointedParameterInfo Create(Type objType, JObject jsonObject)
        {
            var type = jsonObject["ShortName"];
            try
            {
                if(type == null)
                {
                    return (Modules.AppointedListParameterInfo)jsonObject.ToObject(typeof(Modules.AppointedListParameterInfo));
                    //是一个列表参数
                    //var valueType = (Types.AppointedTypesInfo)jsonObject["ValueType"].ToObject(typeof(Types.AppointedTypesInfo));
                    //var defaultValue = (string)jsonObject["DefaultValue"].ToObject(typeof(string));
                    //return new Modules.AppointedListParameterInfo(valueType, defaultValue);
                }
                else
                {
                    return (Modules.AppointedKeyWordParameterInfo)jsonObject.ToObject(typeof(Modules.AppointedKeyWordParameterInfo));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}
