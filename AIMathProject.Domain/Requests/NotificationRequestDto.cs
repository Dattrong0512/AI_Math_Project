using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Requests
{
    public class NotificationRequestDto
    {


        public string? NotificationType { get; set; }

        public string? NotificationTitle { get; set; }

        public string? NotificationMessage { get; set; }
       
    }
}
