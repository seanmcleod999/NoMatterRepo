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

namespace Temboo.Library.Tumblr.OAuth
{
    /// <summary>
    /// InitializeOAuth
    /// Generates an authorization URL that an application can use to complete the first step in the OAuth process.
    /// </summary>
    public class InitializeOAuth : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the InitializeOAuth Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public InitializeOAuth(TembooSession session) : base(session, "/Library/Tumblr/OAuth/InitializeOAuth")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Tumblr (AKA the OAuth Consumer Key).
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (optional, string) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the AccountName input for this Choreo.</param>
         public void setAccountName(String value) {
             base.addInput ("AccountName", value);
         }
         /// <summary>
         /// (optional, string) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the AppKeyName input for this Choreo.</param>
         public void setAppKeyName(String value) {
             base.addInput ("AppKeyName", value);
         }
         /// <summary>
         /// (optional, string) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the AppKeyValue input for this Choreo.</param>
         public void setAppKeyValue(String value) {
             base.addInput ("AppKeyValue", value);
         }
         /// <summary>
         /// (optional, string) The URL that Temboo will redirect your users to after they grant access to your application. This should include the "https://" or "http://" prefix and be a fully qualified URL.
         /// </summary>
         /// <param name="value">Value of the ForwardingURL input for this Choreo.</param>
         public void setForwardingURL(String value) {
             base.addInput ("ForwardingURL", value);
         }
         /// <summary>
         /// (required, string) The Secret Key provided by Tumblr (AKA the OAuth Consumer Secret).
         /// </summary>
         /// <param name="value">Value of the SecretKey input for this Choreo.</param>
         public void setSecretKey(String value) {
             base.addInput ("SecretKey", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A InitializeOAuthResultSet containing execution metadata and results.</returns>
        new public InitializeOAuthResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            InitializeOAuthResultSet results = new JavaScriptSerializer().Deserialize<InitializeOAuthResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the InitializeOAuth Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class InitializeOAuthResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "AuthorizationURL" output from this Choreo execution
        /// <returns>String - (string) The authorization URL that the application's user needs to go to in order to grant access to your application.</returns>
        /// </summary>
        public String AuthorizationURL
        {
            get
            {
                return (String) base.Output["AuthorizationURL"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "CallbackID" output from this Choreo execution
        /// <returns>String - (string) An ID used to retrieve the callback data that Temboo stores once your application's user authorizes.</returns>
        /// </summary>
        public String CallbackID
        {
            get
            {
                return (String) base.Output["CallbackID"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "OAuthTokenSecret" output from this Choreo execution
        /// <returns>String - (string) The temporary OAuth Token Secret that can be exchanged for permanent tokens using the FinalizeOAuth Choreo.</returns>
        /// </summary>
        public String OAuthTokenSecret
        {
            get
            {
                return (String) base.Output["OAuthTokenSecret"];
            }
        }
    }
}
