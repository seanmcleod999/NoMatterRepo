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

namespace Temboo.Library.Google.Gmailv2.Threads
{
    /// <summary>
    /// ModifyThread
    /// Modifies the labels for a specific thread.
    /// </summary>
    public class ModifyThread : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ModifyThread Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ModifyThread(TembooSession session) : base(session, "/Library/Google/Gmailv2/Threads/ModifyThread")
        {
        }

         /// <summary>
         /// (optional, string) A valid Access Token retrieved during the OAuth2 process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new Access Token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (conditional, json) An array of label IDs to apply to the thread.
         /// </summary>
         /// <param name="value">Value of the AddLabelIDs input for this Choreo.</param>
         public void setAddLabelIDs(String value) {
             base.addInput ("AddLabelIDs", value);
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
         /// (optional, string) Used to specify fields to include in a partial response. This can be used to reduce the amount of data returned. See Choreo notes for syntax rules.
         /// </summary>
         /// <param name="value">Value of the Fields input for this Choreo.</param>
         public void setFields(String value) {
             base.addInput ("Fields", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new Access Token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (conditional, json) An array of label IDs to remove from the thread.
         /// </summary>
         /// <param name="value">Value of the RemoveLabelIDs input for this Choreo.</param>
         public void setRemoveLabelIDs(String value) {
             base.addInput ("RemoveLabelIDs", value);
         }
         /// <summary>
         /// (required, string) The ID of the thread to modify.
         /// </summary>
         /// <param name="value">Value of the ThreadID input for this Choreo.</param>
         public void setThreadID(String value) {
             base.addInput ("ThreadID", value);
         }
         /// <summary>
         /// (optional, string) The ID of the acting user. Defaults to "me" indicating the user associated with the Access Token or Refresh Token provided.
         /// </summary>
         /// <param name="value">Value of the UserID input for this Choreo.</param>
         public void setUserID(String value) {
             base.addInput ("UserID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ModifyThreadResultSet containing execution metadata and results.</returns>
        new public ModifyThreadResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ModifyThreadResultSet results = new JavaScriptSerializer().Deserialize<ModifyThreadResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ModifyThread Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ModifyThreadResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Google.</returns>
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
