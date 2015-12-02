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

namespace Temboo.Library.AuthorizeNet.TransactionDetailsAPI
{
    /// <summary>
    /// GetTransactionDetails
    /// Returns transaction details for a specified transaction ID.
    /// </summary>
    public class GetTransactionDetails : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetTransactionDetails Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetTransactionDetails(TembooSession session) : base(session, "/Library/AuthorizeNet/TransactionDetailsAPI/GetTransactionDetails")
        {
        }

         /// <summary>
         /// (required, string) The API Login Id provided by Authorize.net when signing up for a developer account.
         /// </summary>
         /// <param name="value">Value of the APILoginId input for this Choreo.</param>
         public void setAPILoginId(String value) {
             base.addInput ("APILoginId", value);
         }
         /// <summary>
         /// (optional, string) Set to api.authorize.net when running in production. Defaults to apitest.authorize.net for sandbox testing.
         /// </summary>
         /// <param name="value">Value of the Endpoint input for this Choreo.</param>
         public void setEndpoint(String value) {
             base.addInput ("Endpoint", value);
         }
         /// <summary>
         /// (required, integer) The id of the transaction that you want to retrieve information for.
         /// </summary>
         /// <param name="value">Value of the TransactionId input for this Choreo.</param>
         public void setTransactionId(String value) {
             base.addInput ("TransactionId", value);
         }
         /// <summary>
         /// (required, string) The TransactionKey provided by Authorize.net when signing up for a developer account.
         /// </summary>
         /// <param name="value">Value of the TransactionKey input for this Choreo.</param>
         public void setTransactionKey(String value) {
             base.addInput ("TransactionKey", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetTransactionDetailsResultSet containing execution metadata and results.</returns>
        new public GetTransactionDetailsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetTransactionDetailsResultSet results = new JavaScriptSerializer().Deserialize<GetTransactionDetailsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetTransactionDetails Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetTransactionDetailsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from Authorize.net.</returns>
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
