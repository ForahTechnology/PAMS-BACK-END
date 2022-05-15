using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class NotificationDTO
    {
        public string To { get; set; }
        public object Notification { get; set; }
        public object Data { get; set; }
    }
}
