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

namespace Temboo.Library.Fitbit.OAuth
{
    /// <summary>
    /// FinalizeOAuth
    /// Completes the OAuth process by retrieving a Fitbit access token, token secret and encoded user id (UserID) for a user, after they have visited the authorization URL returned by the InitializeOAuth choreo and clicked "allow."
    /// </summary>
    public class FinalizeOAuth : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the FinalizeOAuth Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public FinalizeOAuth(TembooSession session) : base(session, "/Library/Fitbit/OAuth/FinalizeOAuth")
        {
        }

         /// <summary>
         /// (required, string) The callback token returned by the InitializeOAuth Choreo. Used to retrieve the callback data after the user authorizes.
         /// </summary>
         /// <param name="value">Value of the CallbackID input for this Choreo.</param>
         public void setCallbackID(String value) {
             base.addInput ("CallbackID", value);
         }
         /// <summary>
         /// (optional, string) The OAuth 2.0 Client ID provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (optional, string) The OAuth 2.0 Client Secret provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (required, string) The OAuth 1.0 Consumer Key provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerKey input for this Choreo.</param>
         public void setConsumerKey(String value) {
             base.addInput ("ConsumerKey", value);
         }
         /// <summary>
         /// (required, string) The OAuth 1.0 Consumer Secret provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerSecret input for this Choreo.</param>
         public void setConsumerSecret(String value) {
             base.addInput ("ConsumerSecret", value);
         }
         /// <summary>
         /// (required, string) The OAuthTokeSecret retrieved during the OAuth process. This is returned by the InitializeOAuth Choreo when going through OAuth 1.0 process.
         /// </summary>
         /// <param name="value">Value of the OAuthTokenSecret input for this Choreo.</param>
         public void setOAuthTokenSecret(String value) {
             base.addInput ("OAuthTokenSecret", value);
         }
         /// <summary>
         /// (optional, boolean) When set to true, errors received during the OAuth redirect process will be suppressed and returned in the ErrorMessage output.
         /// </summary>
         /// <param name="value">Value of the SuppressErrors input for this Choreo.</param>
         public void setSuppressErrors(String value) {
             base.addInput ("SuppressErrors", value);
         }
         /// <summary>
         /// (optional, integer) The amount of time (in seconds) to poll your Temboo callback URL to see if your app's user has allowed or denied the request for access. Defaults to 20. Max is 60.
         /// </summary>
         /// <param name="value">Value of the Timeout input for this Choreo.</param>
         public void setTimeout(String value) {
             base.addInput ("Timeout", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A FinalizeOAuthResultSet containing execution metadata and results.</returns>
        new public FinalizeOAuthResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            FinalizeOAuthResultSet results = new JavaScriptSerializer().Deserialize<FinalizeOAuthResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the FinalizeOAuth Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class FinalizeOAuthResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "AccessTokenSecret" output from this Choreo execution
        /// <returns>String - (string) The OAuth 1.0 Access Token Secret retrieved during the OAuth process.</returns>
        /// </summary>
        public String AccessTokenSecret
        {
            get
            {
                return (String) base.Output["AccessTokenSecret"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "AccessToken" output from this Choreo execution
        /// <returns>String - (string) The access token for the user that has granted access to your application.</returns>
        /// </summary>
        public String AccessToken
        {
            get
            {
                return (String) base.Output["AccessToken"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ErrorMessage" output from this Choreo execution
        /// <returns>String - (string) Contains an error message if an error occurs during the OAuth redirect process and if SuppressErrors is set to true.</returns>
        /// </summary>
        public String ErrorMessage
        {
            get
            {
                return (String) base.Output["ErrorMessage"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Expires" output from this Choreo execution
        /// <returns>String - (integer) The remaining lifetime of the short-lived OAuth 2.0 access token.</returns>
        /// </summary>
        public String Expires
        {
            get
            {
                return (String) base.Output["Expires"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "RefreshToken" output from this Choreo execution
        /// <returns>String - (string) The OAuth 2.0 refresh token that may be used to obtain a new access token when the short-lived access token expires.</returns>
        /// </summary>
        public String RefreshToken
        {
            get
            {
                return (String) base.Output["RefreshToken"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "UserID" output from this Choreo execution
        /// <returns>String - (string) The user's encoded id associated with the access token that is being retrieved. (Only returned when using the OAuth 1.0 flow).</returns>
        /// </summary>
        public String UserID
        {
            get
            {
                return (String) base.Output["UserID"];
            }
        }
    }
}
