using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Facebook_targeting
{
    public class FacebookDal
    {
        public class FacebookRenewalProposers : IEnumerable
        {
            public int ExternalId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string MobilePhone { get; set; }
            public DateTime DateToCommence { get; set; }
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public static List<FacebookRenewalProposers> GetRenewalProposers(string deltaDays, int maxProposers,string enabledThemes, DateTime timestamp, string specificConnectionString)
        {
            IEnumerable<FacebookRenewalProposers> returnList = null;
            try
            {
                if (timestamp < SqlDateTime.MinValue.Value)
                {
                    timestamp = SqlDateTime.MinValue.Value;
                }


                using (IDbConnection con = new SqlConnection(specificConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int intDeltaDays = Convert.ToInt32(deltaDays);
                    var parameter = new DynamicParameters();
                    parameter.Add("@DeltaDays", intDeltaDays);
                    parameter.Add("@MaxProposers", maxProposers);
                    parameter.Add("@EnabledThemesString", enabledThemes);
                    parameter.Add("@Timestamp", timestamp);

                    returnList = con.Query<FacebookRenewalProposers>("FacebookRenewal_GetProposerDetails", parameter,commandType: CommandType.StoredProcedure);

                    con.Close();
                }

                return (List<FacebookRenewalProposers>) returnList;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "FacebookRenewal_GetProposerDetails - error");
                return (List<FacebookRenewalProposers>) returnList;
            }
        }

        public static void Insert_Facebook_Renewals(List<FacebookRenewalProposers> audienceDataToSend, string externalId, string specificConnectionString)
        {
            try
            {
                if (audienceDataToSend.Count != 0)
                {
                    var proposersObject = new FacebookRenewalProposers();
                    var parameter = new DynamicParameters();
                    var parameterLists = new List<DynamicParameters>();

                    using (IDbConnection con = new SqlConnection(specificConnectionString))
                    {
                        con.Open();
                        var trans = con.BeginTransaction();
                        foreach (var variable in audienceDataToSend)
                        {
                            parameterLists.Clear();
                            proposersObject.FirstName = variable.FirstName;
                            proposersObject.LastName = variable.LastName;
                            proposersObject.Email = variable.Email;
                            proposersObject.MobilePhone = variable.MobilePhone;
                            proposersObject.DateToCommence = variable.DateToCommence;
                            
                            parameter.Add("@ExternalId", 0, DbType.Int32 ,ParameterDirection.Output);
                            parameter.Add("@FirstName", proposersObject.FirstName);
                            parameter.Add("@LastName", proposersObject.LastName);
                            parameter.Add("@Email", proposersObject.Email);
                            parameter.Add("@MobilePhone", proposersObject.MobilePhone);
                            parameter.Add("@DestinationList", externalId);
                            parameter.Add("@InitialDateToCommence", proposersObject.DateToCommence);
                            parameter.Add("@IsOnFacebookAudienceList", 1);
                            parameter.Add("@LastTimeProcessed", DateTime.Now);
                            parameterLists.Add(parameter);                           

                            con.Execute("FacebookRenewal_Insert_Renewals", parameterLists, trans, commandType: CommandType.StoredProcedure);
                            variable.ExternalId = parameter.Get<int>("@ExternalId");
                        }
                        trans.Commit();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "FacebookRenewal_Insert_Renewals - error");
            }

        }

        public static void Update_Facebook_Renewal_Status(List<FacebookRenewalProposers> audienceDataToRemove, string specificConnectionString)
        {
            try
            {              
                var parameter = new DynamicParameters();
                using (IDbConnection con = new SqlConnection(specificConnectionString))
                {
                    con.Open();
                    var trans = con.BeginTransaction();
                    foreach (var variable in audienceDataToRemove)
                    {

                        parameter.Add("@ExternalId", variable.ExternalId);
                        parameter.Add("@LastTimeProcessed", DateTime.Now);
                        con.Execute("FacebookRenewal_Update_Renewals", parameter, trans, commandType: CommandType.StoredProcedure);
                    }
                    trans.Commit();
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "FacebookRenewal_Update_Renewal_Status - error");
            }
        }

        public static List<FacebookRenewalProposers> GetRenewalProposersToDelete(string daysToKeep, int maxProposers, string listName, DateTime timestamp, string specificConnectionString)
        {
            IEnumerable<FacebookRenewalProposers> returnList = null;
            try
            {
                if (timestamp < SqlDateTime.MinValue.Value)
                {
                    timestamp = SqlDateTime.MinValue.Value;
                }


                using (IDbConnection con = new SqlConnection(specificConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int intDaysToKeep = Convert.ToInt32(daysToKeep);
                    var parameter = new DynamicParameters();
                    parameter.Add("@DaysToKeep", intDaysToKeep);
                    parameter.Add("@MaxProposers", maxProposers);
                    parameter.Add("@ListName", listName);
                    parameter.Add("@Timestamp", timestamp);

                    returnList = con.Query<FacebookRenewalProposers>("FacebookRenewal_GetProposersToDelete", parameter, commandType: CommandType.StoredProcedure);

                    con.Close();
                }

                return (List<FacebookRenewalProposers>)returnList;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "FacebookRenewal_GetProposerDetails - error");
                return (List<FacebookRenewalProposers>)returnList;
            }
        }
    }
}
