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

namespace Temboo.Library.Bugzilla
{
    /// <summary>
    /// RetrieveUser
    /// Retrieves information for a specified user.
    /// </summary>
    public class RetrieveUser : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the RetrieveUser Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public RetrieveUser(TembooSession session) : base(session, "/Library/Bugzilla/RetrieveUser")
        {
        }

         /// <summary>
         /// (optional, password) Your Bugzilla password.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (required, string) The ID of the user to retrieve information for. Valid input formats include: email address, or numeric user ID. If searching by numeric ID, authentication is requred.
         /// </summary>
         /// <param name="value">Value of the QueryUserID input for this Choreo.</param>
         public void setQueryUserID(String value) {
             base.addInput ("QueryUserID", value);
         }
         /// <summary>
         /// (optional, string) The base URL for the Bugzilla server to access. Defaults to https://api-dev.bugzilla.mozilla.org/latest. To access the test server, set to https://api-dev.bugzilla.mozilla.org/test/latest.
         /// </summary>
         /// <param name="value">Value of the Server input for this Choreo.</param>
         public void setServer(String value) {
             base.addInput ("Server", value);
         }
         /// <summary>
         /// (optional, string) Your Bugzilla username.
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RetrieveUserResultSet containing execution metadata and results.</returns>
        new public RetrieveUserResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RetrieveUserResultSet results = new JavaScriptSerializer().Deserialize<RetrieveUserResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the RetrieveUser Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RetrieveUserResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Bugzilla.</returns>
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
