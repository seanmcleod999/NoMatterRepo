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

namespace Temboo.Library.Google.Spreadsheets
{
    /// <summary>
    /// UpdateRow
    /// Allows you to update a row by providing the row number and comma-separated data.
    /// </summary>
    public class UpdateRow : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateRow Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateRow(TembooSession session) : base(session, "/Library/Google/Spreadsheets/UpdateRow")
        {
        }

         /// <summary>
         /// (required, string) The updated row data formatted as a comma-separated string.
         /// </summary>
         /// <param name="value">Value of the RowData input for this Choreo.</param>
         public void setRowData(String value) {
             base.addInput ("RowData", value);
         }
         /// <summary>
         /// (optional, string) A valid Access Token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new Access Token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (optional, password) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new Access Token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: xml (the default) and json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, integer) The number of the row to update. Note that row 1 (the column header row) can not be updated. Also, the row that is being updated must exist already. To add new rows, see AppendRow.
         /// </summary>
         /// <param name="value">Value of the Row input for this Choreo.</param>
         public void setRow(String value) {
             base.addInput ("Row", value);
         }
         /// <summary>
         /// (required, string) The unique key of the spreadsheet associated with the row you want to update. This can be found in the URL when viewing the spreadsheet. Required unless SpreadsheetName and WorksheetName are supplied.
         /// </summary>
         /// <param name="value">Value of the SpreadsheetKey input for this Choreo.</param>
         public void setSpreadsheetKey(String value) {
             base.addInput ("SpreadsheetKey", value);
         }
         /// <summary>
         /// (optional, string) The name of the spreadsheet to update. This and WorksheetName can be used as an alternative to SpreadsheetKey and WorksheetId to lookup spreadsheet details by name.
         /// </summary>
         /// <param name="value">Value of the SpreadsheetName input for this Choreo.</param>
         public void setSpreadsheetName(String value) {
             base.addInput ("SpreadsheetName", value);
         }
         /// <summary>
         /// (optional, string) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }
         /// <summary>
         /// (required, string) The unique ID of the worksheet that you want to udpate. Typically, Sheet1 has the id of "od6". Required unless SpreadsheetName and WorksheetName are supplied.
         /// </summary>
         /// <param name="value">Value of the WorksheetId input for this Choreo.</param>
         public void setWorksheetId(String value) {
             base.addInput ("WorksheetId", value);
         }
         /// <summary>
         /// (optional, string) The name of the worksheet to update. This and SpreadsheetName can be used as an alternative to SpreadsheetKey and WorksheetId to lookup spreadsheet details by name.
         /// </summary>
         /// <param name="value">Value of the WorksheetName input for this Choreo.</param>
         public void setWorksheetName(String value) {
             base.addInput ("WorksheetName", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A UpdateRowResultSet containing execution metadata and results.</returns>
        new public UpdateRowResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateRowResultSet results = new JavaScriptSerializer().Deserialize<UpdateRowResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateRow Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateRowResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Google.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "NewAccessToken" output from this Choreo execution
        /// <returns>String - (string) Contains a new AccessToken when the RefreshToken is provided.</returns>
        /// </summary>
        public String NewAccessToken
        {
            get
            {
                return (String) base.Output["NewAccessToken"];
            }
        }
    }
}
