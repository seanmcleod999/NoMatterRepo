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

namespace Temboo.Library.Zoho.Sheet
{
    /// <summary>
    /// ListPublicSpreadsheets
    /// Lists all the spreadsheets that have been made "public" from a user's Zoho Sheet  Account.
    /// </summary>
    public class ListPublicSpreadsheets : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ListPublicSpreadsheets Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ListPublicSpreadsheets(TembooSession session) : base(session, "/Library/Zoho/Sheet/ListPublicSpreadsheets")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Zoho
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (optional, integer) Sets the number of documents to be listed.
         /// </summary>
         /// <param name="value">Value of the Limit input for this Choreo.</param>
         public void setLimit(String value) {
             base.addInput ("Limit", value);
         }
         /// <summary>
         /// (required, string) Your Zoho username (or login id)
         /// </summary>
         /// <param name="value">Value of the LoginID input for this Choreo.</param>
         public void setLoginID(String value) {
             base.addInput ("LoginID", value);
         }
         /// <summary>
         /// (optional, string) Order documents by createdTime, lastModifiedTime or name.
         /// </summary>
         /// <param name="value">Value of the OrderBy input for this Choreo.</param>
         public void setOrderBy(String value) {
             base.addInput ("OrderBy", value);
         }
         /// <summary>
         /// (required, password) Your Zoho password
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (optional, string) The format that response should be in. Can be set to xml or json. Defaults to xml.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Sorting order: asc or desc. Default sort order is set to ascending.
         /// </summary>
         /// <param name="value">Value of the SortOrder input for this Choreo.</param>
         public void setSortOrder(String value) {
             base.addInput ("SortOrder", value);
         }
         /// <summary>
         /// (optional, integer) Sets the initial document number from which the documents will be listed.
         /// </summary>
         /// <param name="value">Value of the StartFrom input for this Choreo.</param>
         public void setStartFrom(String value) {
             base.addInput ("StartFrom", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ListPublicSpreadsheetsResultSet containing execution metadata and results.</returns>
        new public ListPublicSpreadsheetsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ListPublicSpreadsheetsResultSet results = new JavaScriptSerializer().Deserialize<ListPublicSpreadsheetsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ListPublicSpreadsheets Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ListPublicSpreadsheetsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Zoho. Corresponds to the ResponseFormat input. Defaults to XML.</returns>
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
