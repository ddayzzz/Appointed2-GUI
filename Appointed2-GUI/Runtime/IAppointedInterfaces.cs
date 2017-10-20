using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointed2_GUI.Runtime
{
    public interface IAppointedSender
    {
        void AddSendEventHandler(EventHandler<Events.AppointedRequestEventArgs> handler);
        void RemoveEventHandler(EventHandler<Events.AppointedRequestEventArgs> handler);
        EventHandler<Events.AppointedRequestEventArgs> GetReceiveEventHandler();
    }
}
