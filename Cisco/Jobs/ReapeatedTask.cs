using OpenAlmaty;
using OpenAlmaty.Helpers.OAOffice.Events;
using OpenAlmaty.Hubs;
using Quartz;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static OpenAlmaty.Hubs.CiscoUser;

namespace CiscoService.Jobs {
    public class ReapetedTask : IJob {
        Task IJob.Execute(IJobExecutionContext context)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            Caller caller =  Caller.Instance();
        
            // WebClient AUTH data
            string m_strAuth = Convert.ToBase64String(Encoding.ASCII.GetBytes(caller.CallerNumber + ":" + caller.CallerPassword));

            // setup WebClient
            System.Net.WebClient m_wc = new WebClient();
            m_wc.Headers.Add("Authorization", string.Format("Basic {0}", m_strAuth));
            var getStr = m_wc.DownloadString("#URL" + caller.CallerId);

            //Сереализация объекта
            Serializer ser = new Serializer();
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;

            xmlInputData = getStr;

            User customer2 = ser.Deserialize<User>(xmlInputData);
            xmlOutputData = ser.Serialize(customer2);
            if (customer2.State == "TALKING")
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:23332/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = client.GetAsync("api/Test").Result;



            }
            return Task.CompletedTask;
        }


    }
}