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

namespace Temboo.Library.Twitter.Users
{
    /// <summary>
    /// UpdateProfile
    /// Updates values that users are able to set under "Account" tab of their settings page.
    /// </summary>
    public class UpdateProfile : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateProfile Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateProfile(TembooSession session) : base(session, "/Library/Twitter/Users/UpdateProfile")
        {
        }

         /// <summary>
         /// (required, string) The Access Token Secret provided by Twitter or retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessTokenSecret input for this Choreo.</param>
         public void setAccessTokenSecret(String value) {
             base.addInput ("AccessTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The Access Token provided by Twitter or retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The API Key (or Consumer Key) provided by Twitter.
         /// </summary>
         /// <param name="value">Value of the ConsumerKey input for this Choreo.</param>
         public void setConsumerKey(String value) {
             base.addInput ("ConsumerKey", value);
         }
         /// <summary>
         /// (required, string) The API Secret (or Consumer Secret) provided by Twitter.
         /// </summary>
         /// <param name="value">Value of the ConsumerSecret input for this Choreo.</param>
         public void setConsumerSecret(String value) {
             base.addInput ("ConsumerSecret", value);
         }
         /// <summary>
         /// (optional, string) A description of the user owning the account. Maximum of 160 characters.
         /// </summary>
         /// <param name="value">Value of the Description input for this Choreo.</param>
         public void setDescription(String value) {
             base.addInput ("Description", value);
         }
         /// <summary>
         /// (optional, boolean) The user "entities" node containing extra metadata will not be included when set to false.
         /// </summary>
         /// <param name="value">Value of the IncludeUserEntities input for this Choreo.</param>
         public void setIncludeUserEntities(String value) {
             base.addInput ("IncludeUserEntities", value);
         }
         /// <summary>
         /// (optional, string) The city or country describing where the user of the account is located.
         /// </summary>
         /// <param name="value">Value of the Location input for this Choreo.</param>
         public void setLocation(String value) {
             base.addInput ("Location", value);
         }
         /// <summary>
         /// (optional, string) The full name associated with the profile. Maximum of 20 characters.
         /// </summary>
         /// <param name="value">Value of the Name input for this Choreo.</param>
         public void setName(String value) {
             base.addInput ("Name", value);
         }
         /// <summary>
         /// (optional, boolean) When set to true, statuses will not be included in the returned user objects.
         /// </summary>
         /// <param name="value">Value of the SkipStatus input for this Choreo.</param>
         public void setSkipStatus(String value) {
             base.addInput ("SkipStatus", value);
         }
         /// <summary>
         /// (optional, string) URL associated with the profile.
         /// </summary>
         /// <param name="value">Value of the URL input for this Choreo.</param>
         public void setURL(String value) {
             base.addInput ("URL", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A UpdateProfileResultSet containing execution metadata and results.</returns>
        new public UpdateProfileResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateProfileResultSet results = new JavaScriptSerializer().Deserialize<UpdateProfileResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateProfile Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateProfileResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Twitter.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Limit" output from this Choreo execution
        /// <returns>String - (integer) The rate limit ceiling for this particular request.</returns>
        /// </summary>
        public String Limit
        {
            get
            {
                return (String) base.Output["Limit"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Remaining" output from this Choreo execution
        /// <returns>String - (integer) The number of requests left for the 15 minute window.</returns>
        /// </summary>
        public String Remaining
        {
            get
            {
                return (String) base.Output["Remaining"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Reset" output from this Choreo execution
        /// <returns>String - (date) The remaining window before the rate limit resets in UTC epoch seconds.</returns>
        /// </summary>
        public String Reset
        {
            get
            {
                return (String) base.Output["Reset"];
            }
        }
    }
}
