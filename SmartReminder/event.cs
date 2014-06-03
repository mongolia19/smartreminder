using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartReminder
{


   public class Event//a event to handle
    {
       String evtType;
       int TriggerTime;

       Event nextEvt;
        const string Chat = "Chat";
        const string Learn = "Learn";
        const string BroadCast = "BroadCast";
        const string Sing = "Sing";
        const string Reminder = "Reminder";
        const string GetAddress = "GetAddress";
        const string GetInfor = "GetInfor";
        const string SendMail = "SendMail";
       public Event(String evt)
        {
            evtType = evt;
            TriggerTime = 10;
            nextEvt = null;
        }
    }
}
