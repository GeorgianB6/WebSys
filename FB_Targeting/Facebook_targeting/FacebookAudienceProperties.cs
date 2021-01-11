using System.Collections.Generic;

namespace Facebook_targeting
{
    public class Audience
    {
        public string[] schema { get; set; }
        public List<List<string>> data { get; set; }
        public static string ExternId = string.Empty;
        public static string HashedFirstName = string.Empty;
        public static string HashedLastName = string.Empty;
        public static string HashedEmail = string.Empty;
        public static string HashedPhone = string.Empty;

    }

    public class RemoveAudienceDetails
    {
        public static Audience Data(List<FacebookDal.FacebookRenewalProposers> audienceDataToRemove, string externalId)
        {
            var audienceDetails = new Audience();
            audienceDetails.schema = new[] { "EXTERN_ID" };
            var data = new List<List<string>>();
            foreach (var proposerData in audienceDataToRemove)
            {
                Audience.ExternId = proposerData.ExternalId.ToString();               
                data.Add(new List<string>(new[] { Audience.ExternId }));
            }
            audienceDetails.data = data;

            return audienceDetails;
        }
    }

    public class AddAudienceDetails
    {
        public static Audience Data(List<FacebookDal.FacebookRenewalProposers> audienceDataToSend, string externalId)
        {

            var audienceDetails = new Audience();
            audienceDetails.schema = new[] {"EXTERN_ID", "FN", "LN", "EMAIL", "PHONE" };
            var data = new List<List<string>>();
            foreach (var proposerData in audienceDataToSend)
            {
                Audience.ExternId = proposerData.ExternalId.ToString();
                Audience.HashedFirstName = FacebookUtils.sha256Hash(proposerData.FirstName);
                Audience.HashedLastName = FacebookUtils.sha256Hash(proposerData.LastName);
                Audience.HashedEmail = FacebookUtils.sha256Hash(proposerData.Email);
                Audience.HashedPhone = FacebookUtils.sha256Hash(proposerData.MobilePhone);
                data.Add(new List<string>(new[] { Audience.ExternId, Audience.HashedFirstName, Audience.HashedLastName, Audience.HashedEmail, Audience.HashedPhone }));
            }
            audienceDetails.data = data;

            return audienceDetails;
        }
    }
}
