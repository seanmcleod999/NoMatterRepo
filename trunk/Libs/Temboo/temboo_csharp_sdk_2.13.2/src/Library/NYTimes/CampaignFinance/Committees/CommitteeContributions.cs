using System;
using Temboo.Core;
using System.Web.Script.Serialization;

/*
Copyright 2014 Temboo, Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

namespace Temboo.Library.NYTimes.CampaignFinance.Committees
{
    /// <summary>
    /// CommitteeContributions
    /// Obtain contributions made by a Political Action Committee (PAC) in an election cycle.
    /// </summary>
    public class CommitteeContributions : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the CommitteeContributions Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public CommitteeContributions(TembooSession session) : base(session, "/Library/NYTimes/CampaignFinance/Committees/CommitteeContributions")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by NY Times.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, integer) Enter the campaign cycle year in YYYY format.  This must be an even year. 
         /// </summary>
         /// <param name="value">Value of the CampaignCycle input for this Choreo.</param>
         public void setCampaignCycle(String value) {
             base.addInput ("CampaignCycle", value);
         }
         /// <summary>
         /// (conditional, string) Enter a political action committee's FEC ID.  Either CommitteeFECID, OR Name must be provided.
         /// </summary>
         /// <param name="value">Value of the CommitteeFECID input for this Choreo.</param>
         public void setCommitteeFECID(String value) {
             base.addInput ("CommitteeFECID", value);
         }
         /// <summary>
         /// (conditional, string) Enter the name of a political action committee (PAC) whose contributions will be obtained. Either Name or CommitteeFECID must be provided.
         /// </summary>
         /// <param name="value">Value of the Name input for this Choreo.</param>
         public void setName(String value) {
             base.addInput ("Name", value);
         }
         /// <summary>
         /// (optional, integer) The first 20 results are shown by default. To page through the results, set Offset to the appropriate value (e.g., Offset=40 displays results 41–60).
         /// </summary>
         /// <param name="value">Value of the Offset input for this Choreo.</param>
         public void setOffset(String value) {
             base.addInput ("Offset", value);
         }
         /// <summary>
         /// (optional, string) Enter json or xml.  Default is json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A CommitteeContributionsResultSet containing execution metadata and results.</returns>
        new public CommitteeContributionsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            CommitteeContributionsResultSet results = new JavaScriptSerializer().Deserialize<CommitteeContributionsResultSet>(json);

            // Note that we may actually have run into an exception while trying to execute
            // this request; if so, then throw an appropriate exception
            if (results.Execution.LastError != null)
            {
                throw new TembooException(results.Execution.LastError);
            }
            return results;
        }

    }

    /// <summary>
    /// A ResultSet with methods tailored to the values returned by the CommitteeContributions Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class CommitteeContributionsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from the NY Times API corresponds to the setting (json, or xml) entered in the ResponseFormat variable.  Default is set to json.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
    }
}
