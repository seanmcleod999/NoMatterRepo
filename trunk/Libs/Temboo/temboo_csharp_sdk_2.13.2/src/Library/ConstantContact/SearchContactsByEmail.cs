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
    /// SearchContactsByEmail
    /// Allows you to search Constant Contact by email to retrieve a contact's information.
    /// </summary>
    public class SearchContactsByEmail : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the SearchContactsByEmail Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public SearchContactsByEmail(TembooSession session) : base(session, "/Library/ConstantContact/SearchContactsByEmail")
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
         /// (required, string) The email address to use in your search.
         /// </summary>
         /// <param name="value">Value of the EmailAddress input for this Choreo.</param>
         public void setEmailAddress(String value) {
             base.addInput ("EmailAddress", value);
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
        /// <returns>A SearchContactsByEmailResultSet containing execution metadata and results.</returns>
        new public SearchContactsByEmailResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            SearchContactsByEmailResultSet results = new JavaScriptSerializer().Deserialize<SearchContactsByEmailResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the SearchContactsByEmail Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class SearchContactsByEmailResultSet : Temboo.Core.ResultSet
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
