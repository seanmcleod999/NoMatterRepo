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

namespace Temboo.Library.Dropbox.Datastore
{
    /// <summary>
    /// InsertRecord
    /// Inserts a record into a datastore table.
    /// </summary>
    public class InsertRecord : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the InsertRecord Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public InsertRecord(TembooSession session) : base(session, "/Library/Dropbox/Datastore/InsertRecord")
        {
        }

         /// <summary>
         /// (required, string) The Access Token Secret retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessTokenSecret input for this Choreo.</param>
         public void setAccessTokenSecret(String value) {
             base.addInput ("AccessTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The Access Token retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The App Key provided by Dropbox (AKA the OAuth Consumer Key).
         /// </summary>
         /// <param name="value">Value of the AppKey input for this Choreo.</param>
         public void setAppKey(String value) {
             base.addInput ("AppKey", value);
         }
         /// <summary>
         /// (required, string) The App Secret provided by Dropbox (AKA the OAuth Consumer Secret).
         /// </summary>
         /// <param name="value">Value of the AppSecret input for this Choreo.</param>
         public void setAppSecret(String value) {
             base.addInput ("AppSecret", value);
         }
         /// <summary>
         /// (required, json) A JSON-encoded list of name/value pairs to insert into the table.
         /// </summary>
         /// <param name="value">Value of the Data input for this Choreo.</param>
         public void setData(String value) {
             base.addInput ("Data", value);
         }
         /// <summary>
         /// (required, string) The handle of an existing datastore.
         /// </summary>
         /// <param name="value">Value of the Handle input for this Choreo.</param>
         public void setHandle(String value) {
             base.addInput ("Handle", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, string) The revision to which to apply the delta. If not provided, the Choreo will perform a lookup for the latest revision number.
         /// </summary>
         /// <param name="value">Value of the Revision input for this Choreo.</param>
         public void setRevision(String value) {
             base.addInput ("Revision", value);
         }
         /// <summary>
         /// (conditional, string) The row identifier. If not provided, a randomly generated GUID will be inserted for this value.
         /// </summary>
         /// <param name="value">Value of the RowID input for this Choreo.</param>
         public void setRowID(String value) {
             base.addInput ("RowID", value);
         }
         /// <summary>
         /// (required, string) The name of the datastore table.
         /// </summary>
         /// <param name="value">Value of the Table input for this Choreo.</param>
         public void setTable(String value) {
             base.addInput ("Table", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A InsertRecordResultSet containing execution metadata and results.</returns>
        new public InsertRecordResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            InsertRecordResultSet results = new JavaScriptSerializer().Deserialize<InsertRecordResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the InsertRecord Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class InsertRecordResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Dropbox. Corresponds to the ResponseFormat input. Defaults to json.</returns>
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
