using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Utilities
{
    /// <summary>
    /// This handles push notification to both web and mobile application.
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// This sends push notification to a user in real time.
        /// </summary>
        /// <returns></returns>
        Task<bool> Send(NotificationDTO data);
        Task<bool> Receive(NotificationDTO data, Guid userId);
    }
}
