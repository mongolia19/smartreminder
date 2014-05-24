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
       static const string Chat = "Chat";
       static const string Learn = "Learn";
       static const string BroadCast = "BroadCast";
       static const string Sing = "Sing";
       static const string Reminder = "Reminder";
       static const string GetAddress = "GetAddress";
       static const string GetInfor = "GetInfor";
       static const string SendMail = "SendMail";
       public Event(String evt)
        {
            evtType = evt;
            TriggerTime = 10;
            nextEvt = null;
        }
    }
}
