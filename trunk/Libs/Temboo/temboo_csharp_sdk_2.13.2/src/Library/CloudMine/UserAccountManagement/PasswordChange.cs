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

namespace Temboo.Library.CloudMine.UserAccountManagement
{
    /// <summary>
    /// PasswordChange
    /// Updates a user's password.
    /// </summary>
    public class PasswordChange : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the PasswordChange Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public PasswordChange(TembooSession session) : base(session, "/Library/CloudMine/UserAccountManagement/PasswordChange")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by CloudMine after registering your app.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, string) The application identifier provided by CloudMine after registering your app.
         /// </summary>
         /// <param name="value">Value of the ApplicationIdentifier input for this Choreo.</param>
         public void setApplicationIdentifier(String value) {
             base.addInput ("ApplicationIdentifier", value);
         }
         /// <summary>
         /// (required, string) The user's new password.
         /// </summary>
         /// <param name="value">Value of the NewPassword input for this Choreo.</param>
         public void setNewPassword(String value) {
             base.addInput ("NewPassword", value);
         }
         /// <summary>
         /// (required, string) The password for the user that needs to authenticate.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (required, string) The username for the user that needs to authenticate.
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A PasswordChangeResultSet containing execution metadata and results.</returns>
        new public PasswordChangeResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            PasswordChangeResultSet results = new JavaScriptSerializer().Deserialize<PasswordChangeResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the PasswordChange Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class PasswordChangeResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from CloudMine.</returns>
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
