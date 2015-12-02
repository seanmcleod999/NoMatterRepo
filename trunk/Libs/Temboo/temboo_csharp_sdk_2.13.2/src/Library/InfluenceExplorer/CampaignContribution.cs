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

namespace Temboo.Library.InfluenceExplorer
{
    /// <summary>
    /// CampaignContribution
    /// Retrieve detailed information on political campaign contributions, filtered by date, contributor, state, employer, campaign, etc.
    /// </summary>
    public class CampaignContribution : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the CampaignContribution Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public CampaignContribution(TembooSession session) : base(session, "/Library/InfluenceExplorer/CampaignContribution")
        {
        }

         /// <summary>
         /// (required, string) The API key provided by Sunlight Data Services.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (conditional, string) Enter the amount of dollars spent on lobbying.  Valid formats include: 500 (exactly $500); >|500 (greater than, or equal to 500); <|500 (less than or equal to 500).
         /// </summary>
         /// <param name="value">Value of the Amount input for this Choreo.</param>
         public void setAmount(String value) {
             base.addInput ("Amount", value);
         }
         /// <summary>
         /// (conditional, string) Specfiy the name of an individual, PAC, organization, or employer for which a full-text search will be performed.
         /// </summary>
         /// <param name="value">Value of the ContributorName input for this Choreo.</param>
         public void setContributorName(String value) {
             base.addInput ("ContributorName", value);
         }
         /// <summary>
         /// (conditional, string) Enter a two-letter state designation from which the contribution is made.
         /// </summary>
         /// <param name="value">Value of the ContributorsByState input for this Choreo.</param>
         public void setContributorsByState(String value) {
             base.addInput ("ContributorsByState", value);
         }
         /// <summary>
         /// (conditional, string) Specify a yyyy-formatted election cycle. Example: 2012, or 2008|2012 to limit results between 2008 and 2012.
         /// </summary>
         /// <param name="value">Value of the Cycle input for this Choreo.</param>
         public void setCycle(String value) {
             base.addInput ("Cycle", value);
         }
         /// <summary>
         /// (conditional, string) Specify a date of the contribution in ISO date format.  For example: 2006-08-06.  Or, ><|2006-08-06|2006-09-12 to limit results between specific dates.
         /// </summary>
         /// <param name="value">Value of the Date input for this Choreo.</param>
         public void setDate(String value) {
             base.addInput ("Date", value);
         }
         /// <summary>
         /// (conditional, string) Specify a full-text search on employer, organization, and parent organization.
         /// </summary>
         /// <param name="value">Value of the OrganizationName input for this Choreo.</param>
         public void setOrganizationName(String value) {
             base.addInput ("OrganizationName", value);
         }
         /// <summary>
         /// (conditional, string) Enter the full-text search on name of PAC or candidate receiving the contribution.
         /// </summary>
         /// <param name="value">Value of the RecipientName input for this Choreo.</param>
         public void setRecipientName(String value) {
             base.addInput ("RecipientName", value);
         }
         /// <summary>
         /// (conditional, string) Specify a two-letter state abbreviation for the state in which the recipient of contributions is running a campaign.
         /// </summary>
         /// <param name="value">Value of the RecipientState input for this Choreo.</param>
         public void setRecipientState(String value) {
             base.addInput ("RecipientState", value);
         }
         /// <summary>
         /// (optional, string) Indicates the desired format for the response. Accepted values are: json (the default), csv, and xls. Note when specifying xls, restults are returned as Base64 encoded data.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, string) Specify the type of political office being sought.  Examples: federal:senate (US Senate), federal:president (US President), state:governor.  For more info see documentation.
         /// </summary>
         /// <param name="value">Value of the Seat input for this Choreo.</param>
         public void setSeat(String value) {
             base.addInput ("Seat", value);
         }
         /// <summary>
         /// (optional, string) Filters on federal or state contributions. Valid namespaces are: urn:fec:transaction (for federal) or urn:nimsp:transaction (for state).
         /// </summary>
         /// <param name="value">Value of the TransactionNamespace input for this Choreo.</param>
         public void setTransactionNamespace(String value) {
             base.addInput ("TransactionNamespace", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A CampaignContributionResultSet containing execution metadata and results.</returns>
        new public CampaignContributionResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            CampaignContributionResultSet results = new JavaScriptSerializer().Deserialize<CampaignContributionResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the CampaignContribution Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class CampaignContributionResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Influence Explorer. Corresponds to the ResponseFormat input. Defaults to json.</returns>
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
