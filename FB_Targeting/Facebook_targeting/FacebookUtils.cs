using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Facebook_targeting
{
    public class FacebookUtils
    {
        public static FormUrlEncodedContent GetPayload(object data, string accessToken)
        {
            var json = JsonConvert.SerializeObject(data);
            var postContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", accessToken),
                new KeyValuePair<string, string>("payload", json)
            });

            return postContent;
        }

        public static string sha256Hash(string data)
        {
            var sha256 = new SHA256Managed();
            var hash = new StringBuilder();

            byte[] computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            foreach (var theByte in computedHash)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        public static List<FacebookDal.FacebookRenewalProposers> FilterAudienceDataToSend(List<FacebookDal.FacebookRenewalProposers> audienceDataToSend,UnsubscriberSettings unsubscriberSettings, string skinName)
        {
            try
            {
                foreach (var audience in audienceDataToSend.ToList())
                {
                    var unsubscriberResponse = Unsubscriber.Send(unsubscriberSettings, audience, skinName);

                    if (unsubscriberResponse.Result.ToLower().Contains("true"))
                    {
                        audienceDataToSend.Remove(audience);
                    }
                }
                return audienceDataToSend;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "FilterAudienceDataToSend - error");
            }
            return audienceDataToSend;
        }
    }
}