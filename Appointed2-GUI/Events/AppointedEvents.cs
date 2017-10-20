using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointed2_GUI.Events
{
    public class AppointedHotKeyPressEventArgs : EventArgs
    {
        public readonly string[] Data;
        public readonly UInt32 KeyID;
        public AppointedHotKeyPressEventArgs(UInt32 id, string[] data):base()
        {
            this.Data = data;
            this.KeyID = id;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RegistriedHotKeyID:" + this.KeyID);
            sb.Append("\nData:\n");
            if (Data != null)
                for (int i = 0; i < Data.Length; ++i)
                    sb.AppendLine(Data[i]);
            return sb.ToString();
        }
    }
    public class AppointedRequestEventArgs:EventArgs
    {
        public String FormatString { get; private set; }
        public AppointedRequestEventArgs(String formatStr)
        {
            this.FormatString = formatStr;
        }
    }

}
