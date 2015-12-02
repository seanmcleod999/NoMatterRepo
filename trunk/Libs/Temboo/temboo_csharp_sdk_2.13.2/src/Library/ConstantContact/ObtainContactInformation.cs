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

namespace Temboo.Library.ConstantContact
{
    /// <summary>
    /// ObtainContactInformation
    /// Retrieves contact information from Constant Contact by specifying the contact ID.
    /// </summary>
    public class ObtainContactInformation : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ObtainContactInformation Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ObtainContactInformation(TembooSession session) : base(session, "/Library/ConstantContact/ObtainContactInformation")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Constant Contact.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, integer) The ID for the contact you want to retrieve information for.
         /// </summary>
         /// <param name="value">Value of the ContactId input for this Choreo.</param>
         public void setContactId(String value) {
             base.addInput ("ContactId", value);
         }
         /// <summary>
         /// (required, password) Your Constant Contact password.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (required, string) Your Constant Contact username.
         /// </summary>
         /// <param name="value">Value of the UserName input for this Choreo.</param>
         public void setUserName(String value) {
             base.addInput ("UserName", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ObtainContactInformationResultSet containing execution metadata and results.</returns>
        new public ObtainContactInformationResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ObtainContactInformationResultSet results = new JavaScriptSerializer().Deserialize<ObtainContactInformationResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ObtainContactInformation Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ObtainContactInformationResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from Constant Contact.</returns>
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