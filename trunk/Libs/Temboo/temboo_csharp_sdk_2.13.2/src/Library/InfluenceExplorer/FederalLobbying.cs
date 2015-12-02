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
    /// FederalLobbying
    /// Obtain detailed lobbying information.
    /// </summary>
    public class FederalLobbying : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the FederalLobbying Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public FederalLobbying(TembooSession session) : base(session, "/Library/InfluenceExplorer/FederalLobbying")
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
         /// (optional, string) Enter the amount of dollars spent on lobbying.  Valid formats include: 500 (exactly $500); >|500 (greater than, or equal to 500); <|500 (less than or equal to 500).
         /// </summary>
         /// <param name="value">Value of the Amount input for this Choreo.</param>
         public void setAmount(String value) {
             base.addInput ("Amount", value);
         }
         /// <summary>
         /// (optional, string) Specify a full-text search of a client's parent organizationfor.
         /// </summary>
         /// <param name="value">Value of the ClientParentOrganization input for this Choreo.</param>
         public void setClientParentOrganization(String value) {
             base.addInput ("ClientParentOrganization", value);
         }
         /// <summary>
         /// (optional, string) Enter the name of the client for whom this lobbyist is working. This parameter executes a full-text search.
         /// </summary>
         /// <param name="value">Value of the ClientSearch input for this Choreo.</param>
         public void setClientSearch(String value) {
             base.addInput ("ClientSearch", value);
         }
         /// <summary>
         /// (optional, string) Specify the type of filing as identified by CRP.  Example: n, for non-self filer parent.  For more info, go here: http://data.influenceexplorer.com/api/lobbying/
         /// </summary>
         /// <param name="value">Value of the FilingType input for this Choreo.</param>
         public void setFilingType(String value) {
             base.addInput ("FilingType", value);
         }
         /// <summary>
         /// (optional, string) Specify a full-text search of a lobbyist's name.
         /// </summary>
         /// <param name="value">Value of the LobbyistSearch input for this Choreo.</param>
         public void setLobbyistSearch(String value) {
             base.addInput ("LobbyistSearch", value);
         }
         /// <summary>
         /// (optional, string) Specify a full-text search of an organization or a person, who is fling the lobbyist registration.
         /// </summary>
         /// <param name="value">Value of the RegistrantSearch input for this Choreo.</param>
         public void setRegistrantSearch(String value) {
             base.addInput ("RegistrantSearch", value);
         }
         /// <summary>
         /// (optional, string) Indicates the desired format for the response. Accepted values are: json (the default), csv, and xls. Note when specifying xls, restults are returned as Base64 encoded data.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Enter the report ID given by the Senate Office of Public Records.
         /// </summary>
         /// <param name="value">Value of the TransactionID input for this Choreo.</param>
         public void setTransactionID(String value) {
             base.addInput ("TransactionID", value);
         }
         /// <summary>
         /// (optional, string) Enter the type of filing as reported by the Senate Office of Public Records. See here for additional info: http://assets.transparencydata.org.s3.amazonaws.com/docs/transaction_types-20100402.csv
         /// </summary>
         /// <param name="value">Value of the TransactionType input for this Choreo.</param>
         public void setTransactionType(String value) {
             base.addInput ("TransactionType", value);
         }
         /// <summary>
         /// (optional, string) Specify the year in which a registration was filed. Use the following format: yyyy. Example: 2011. Logical OR is also possible by using the | (pipe) symbol.  Example: 2008|2012.
         /// </summary>
         /// <param name="value">Value of the YearFiled input for this Choreo.</param>
         public void setYearFiled(String value) {
             base.addInput ("YearFiled", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A FederalLobbyingResultSet containing execution metadata and results.</returns>
        new public FederalLobbyingResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            FederalLobbyingResultSet results = new JavaScriptSerializer().Deserialize<FederalLobbyingResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the FederalLobbying Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class FederalLobbyingResultSet : Temboo.Core.ResultSet
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
