using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
namespace Appointed2_GUI.Modules
{
    public class AppointedModulesManager
    {
        private HashSet<Modules.AppointedModuleInfo> modules;
        public AppointedModulesManager()
        {
            this.modules = new HashSet<Modules.AppointedModuleInfo>();
        }
        public void Add(Modules.AppointedModuleInfo module)
        {
            this.modules.Add(module);
        }
        public Modules.AppointedModuleInfo Find(string guid)
        {
            return this.modules.Where((Modules.AppointedModuleInfo module) =>
            {
                return module.Guid.Equals(guid);
            }).FirstOrDefault();
        }
        public bool SaveToFile(string filepath)
        {
            try
            {
                String deserialized = JsonConvert.SerializeObject(this.modules, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.Write(deserialized);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public bool LoadFromFile(string filepath)
        {
            try
            {
                if (this.modules.Count > 0)
                    this.modules.Clear();
                String deserialized = JsonConvert.SerializeObject(this.modules, Formatting.Indented);
                using (StreamReader sr = new StreamReader(filepath))
                {
                    var str = sr.ReadToEnd();
                    var lists = JsonConvert.DeserializeObject<List<Modules.AppointedModuleInfo>>(str, new Modules.AppointedParameterInfoConverter());
                    foreach (var item in lists)
                        this.modules.Add(item);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}