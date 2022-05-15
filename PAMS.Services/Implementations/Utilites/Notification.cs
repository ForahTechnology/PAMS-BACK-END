using Microsoft.Extensions.Configuration;
using Nancy.Json;
using PAMS.Application.Interfaces.Utilities;
using PAMS.DTOs.Request;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations.Utilities
{
    public class Notification : INotification
    {
        private readonly IConfiguration configuration;

        public Notification(
            IConfiguration configuration
            )
        {
            this.configuration = configuration;
        }

        public Task<bool> Receive(NotificationDTO data, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Send(NotificationDTO data)
        {
            var serverKey = configuration.GetSection("Notification").GetSection("Server_Key").Value;
            var senderId = configuration.GetSection("Notification").GetSection("Sender_Id").Value;
            try
            {

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (StreamReader tReader = new StreamReader(dataStream))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
