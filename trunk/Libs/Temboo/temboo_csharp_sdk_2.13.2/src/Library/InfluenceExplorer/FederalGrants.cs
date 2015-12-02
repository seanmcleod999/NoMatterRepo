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
    /// FederalGrants
    /// Returns information about federal grants awarded.
    /// </summary>
    public class FederalGrants : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the FederalGrants Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public FederalGrants(TembooSession session) : base(session, "/Library/InfluenceExplorer/FederalGrants")
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
         /// (optional, string) Full-text search on the reported name of the federal agency awarding the grant.
         /// </summary>
         /// <param name="value">Value of the AgencyName input for this Choreo.</param>
         public void setAgencyName(String value) {
             base.addInput ("AgencyName", value);
         }
         /// <summary>
         /// (optional, string) The grant amount. Valid formats include: 500 (exactly $500); >|500 (greater than, or equal to 500); <|500 (less than or equal to 500).
         /// </summary>
         /// <param name="value">Value of the Amount input for this Choreo.</param>
         public void setAmount(String value) {
             base.addInput ("Amount", value);
         }
         /// <summary>
         /// (optional, integer) A numeric code for the type of grant awarded. See documentation for more details for this parameter.
         /// </summary>
         /// <param name="value">Value of the AssistanceType input for this Choreo.</param>
         public void setAssistanceType(String value) {
             base.addInput ("AssistanceType", value);
         }
         /// <summary>
         /// (optional, date) The year in which the grant was awarded. A YYYY formatted year. You can also specify a range by separating years with a pipe (i.e. 2008|2012).
         /// </summary>
         /// <param name="value">Value of the FiscalYear input for this Choreo.</param>
         public void setFiscalYear(String value) {
             base.addInput ("FiscalYear", value);
         }
         /// <summary>
         /// (optional, string) Full-text search on the reported name of the grant recipient.
         /// </summary>
         /// <param name="value">Value of the RecipientName input for this Choreo.</param>
         public void setRecipientName(String value) {
             base.addInput ("RecipientName", value);
         }
         /// <summary>
         /// (optional, string) Two-letter abbreviation of the state in which the grant was awarded.
         /// </summary>
         /// <param name="value">Value of the RecipientState input for this Choreo.</param>
         public void setRecipientState(String value) {
             base.addInput ("RecipientState", value);
         }
         /// <summary>
         /// (optional, integer) The numeric code representing the type of entity that received the grant. See documentation for more details about this parameter.
         /// </summary>
         /// <param name="value">Value of the RecipientType input for this Choreo.</param>
         public void setRecipientType(String value) {
             base.addInput ("RecipientType", value);
         }
         /// <summary>
         /// (optional, string) Indicates the desired format for the response. Accepted values are: json (the default), csv, and xls. Note when specifying xls, restults are returned as Base64 encoded data.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A FederalGrantsResultSet containing execution metadata and results.</returns>
        new public FederalGrantsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            FederalGrantsResultSet results = new JavaScriptSerializer().Deserialize<FederalGrantsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the FederalGrants Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class FederalGrantsResultSet : Temboo.Core.ResultSet
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
