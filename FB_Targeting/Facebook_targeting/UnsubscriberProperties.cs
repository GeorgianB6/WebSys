using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_targeting
{
    public class UnsubscriberSettings
    {
        public string Product { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UnsubscriberEndpoint { get; set; }
        public string ApplicationName { get; set; }
        public string UnsubscriptionType { get; set; }
    }

    public class Unsubscriber
    {
        public static Task<string> Send(UnsubscriberSettings unsubscriberSettings,FacebookDal.FacebookRenewalProposers audience, string skinName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var request = CreateXmlRequest(unsubscriberSettings, audience, skinName);
                    var content = new StringContent(request, Encoding.UTF8, "text/xml");
                    content.Headers.Add("SOAPAction", "http://tempuri.org/GdprIsUnsubscribed");
                    var result = httpClient.PostAsync(unsubscriberSettings.UnsubscriberEndpoint, content);
                    var resultContent = result.Result.Content.ReadAsStringAsync();
                    return resultContent;
                }
            }
            catch (Exception exception)
            {
                ExceptionLogger.Log(exception, "Unsubscriber send error");
            }

            return null;
        }

        private static string CreateXmlRequest(UnsubscriberSettings unsubscriberSettings,FacebookDal.FacebookRenewalProposers audience, string skinName)
        {
            var xml = new StringBuilder();
            xml.AppendFormat(
                "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><UserCredentials xmlns=\"http://tempuri.org/\"><UserName>{0}</UserName><Password>{1}</Password></UserCredentials></soap:Header><soap:Body><GdprIsUnsubscribed xmlns=\"http://tempuri.org/\"><applicationName>{2}</applicationName><product>{3}</product><skinName>{4}</skinName><contactPhone>{5}</contactPhone><email>{6}</email><unsubscriptionType>{7}</unsubscriptionType></GdprIsUnsubscribed></soap:Body></soap:Envelope>",
                unsubscriberSettings.UserName,
                unsubscriberSettings.Password,
                unsubscriberSettings.ApplicationName,
                unsubscriberSettings.Product,
                skinName,
                audience.MobilePhone,
                audience.Email,
                unsubscriberSettings.UnsubscriptionType
            );
            return xml.ToString();
        }
    }
}
