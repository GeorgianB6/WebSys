using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Facebook_targeting
{
    public partial class FacebookService
    {
        public static IConfigurationRoot Configuration;

        public FacebookService()
        {
            //Console.WriteLine("---ConfigureServices---");
            //ConfigureServices();

            ////Facebook settings
            //var accessToken = Configuration["AccessToken"];
            //var adAccountId = Configuration["AdAccountID"];
            //var daysToKeep = Configuration["DaysToKeep"];
            //var facebookClient = new FacebookClient(Configuration["FacebookEndpoint"]);
            //var facebookService = new FacebookService(facebookClient);

            ////Unsubscriber settings
            //var unsubscriberSettings = new UnsubscriberSettings
            //{
            //    ApplicationName = Configuration["UnsubscriberSettings:ApplicationName"],
            //    Product = Configuration["UnsubscriberSettings:Product"],
            //    UserName = Configuration["UnsubscriberSettings:UserName"],
            //    Password = Configuration["UnsubscriberSettings:Password"],
            //    UnsubscriberEndpoint = Configuration["UnsubscriberSettings:UnsubscriberEndpoint"],
            //    UnsubscriptionType = Configuration["UnsubscriberSettings:UnsubscriptionType"]
            //};

            ////Renewal settings
            //var maximumProposersBatch = int.Parse(Configuration["maximumProposers"]);

            //var sitesRenewalSettings = Configuration.GetSection("SitesRenewalSettings").GetChildren();
            //foreach (var siteSetting in sitesRenewalSettings)
            //{
            //    var renewalSettings = Configuration.GetSection("SitesRenewalSettings:" + siteSetting.Key).Get<List<string>>();
            //    var skinName = renewalSettings[0];
            //    var deltaDays = renewalSettings[1];
            //    var externalId = renewalSettings[2];
            //    var connectionString = renewalSettings[3];
            //    var audienceId = renewalSettings[4];
            //    var endpoint = string.Concat(audienceId, "/users");

            //    //Get audience to remove from facebook


            //    var audienceDataToRemove = FacebookDal.GetRenewalProposersToDelete(daysToKeep, maximumProposersBatch, externalId, DateTime.Now, connectionString);

            //    if (audienceDataToRemove.Count > 0)
            //    {
            //        //Log data on our db
            //        FacebookDal.Update_Facebook_Renewal_Status(audienceDataToRemove, connectionString);
            //        Console.WriteLine("---AudienceDataToRemoveCount---" + audienceDataToRemove.Count + " settings: " + skinName + externalId + externalId);

            //        try
            //        {
            //            var payload = RemoveAudienceDetails.Data(audienceDataToRemove, externalId);
            //            var deleteAudienceTask =  facebookService.DeleteAudienceAsync(accessToken, endpoint, payload);
            //            if (!deleteAudienceTask.IsCompletedSuccessfully)
            //                ExceptionLogger.Log(deleteAudienceTask.Exception, "DeleteAudienceAsync - error");
            //        }
            //        catch (Exception ex)
            //        {
            //            ExceptionLogger.Log(ex, "facebookService.DeleteAudienceAsync - error");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No audience data to remove " + externalId);
            //    }



            //    ////Get audience to send to facebook
            //    var audienceDataToSend = FacebookDal.GetRenewalProposers(deltaDays, maximumProposersBatch, skinName, DateTime.MinValue, connectionString);
            //    ////Check audience for unsubscriptions
            //    var audienceDataToSendFiltered = FacebookUtils.FilterAudienceDataToSend(audienceDataToSend, unsubscriberSettings, skinName);


            //    if (audienceDataToSendFiltered.Count > 0)
            //    {
            //    //        //Log data on our db
            //        FacebookDal.Insert_Facebook_Renewals(audienceDataToSendFiltered, externalId, connectionString);
            //            Console.WriteLine(siteSetting.Key + "---AudienceDataToAddCount---" + audienceDataToSendFiltered.Count + " settings: " + skinName + deltaDays + externalId);

            //           try
            //            {

            //                var payload = AddAudienceDetails.Data(audienceDataToSendFiltered, externalId);
            //                var addAudienceTask = facebookService.AddAudienceAsync(accessToken, endpoint, payload);
            //                if (!addAudienceTask.IsCompletedSuccessfully)
            //                    ExceptionLogger.Log(addAudienceTask.Exception, "AddAudienceAsync - error");
            //            }
            //            catch (Exception ex)
            //            {
            //                ExceptionLogger.Log(ex, "facebookService.AddAudienceAsync - error");
            //            }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No audience data to add " + externalId);
            //    }
            //}
            Console.WriteLine("Hi from the service");
            Console.ReadLine();
        }

        public static void ConfigureServices()
        {
            // Build configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("ApiConfig.json", false)
                .Build();
        }
    }
}
