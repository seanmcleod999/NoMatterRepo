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

namespace Temboo.Library.Twitter.Timelines
{
    /// <summary>
    /// LatestMention
    /// Retrieves the latest mention for the authenticating user.
    /// </summary>
    public class LatestMention : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the LatestMention Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public LatestMention(TembooSession session) : base(session, "/Library/Twitter/Timelines/LatestMention")
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
         /// (optional, boolean) Set to true to include the screen_name of the contributor. By default only the user_id of the contributor is included.
         /// </summary>
         /// <param name="value">Value of the ContributorDetails input for this Choreo.</param>
         public void setContributorDetails(String value) {
             base.addInput ("ContributorDetails", value);
         }
         /// <summary>
         /// (optional, boolean) The "entities" node containing extra metadata will not be included when set to false.
         /// </summary>
         /// <param name="value">Value of the IncludeEntities input for this Choreo.</param>
         public void setIncludeEntities(String value) {
             base.addInput ("IncludeEntities", value);
         }
         /// <summary>
         /// (optional, string) Returns results with an ID less than (older than) or equal to the specified ID.
         /// </summary>
         /// <param name="value">Value of the MaxId input for this Choreo.</param>
         public void setMaxId(String value) {
             base.addInput ("MaxId", value);
         }
         /// <summary>
         /// (optional, string) Returns results with an ID greater than (more recent than) the specified ID.
         /// </summary>
         /// <param name="value">Value of the SinceId input for this Choreo.</param>
         public void setSinceId(String value) {
             base.addInput ("SinceId", value);
         }
         /// <summary>
         /// (optional, boolean) When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Defaults to false, which returns the complete user object.
         /// </summary>
         /// <param name="value">Value of the TrimUser input for this Choreo.</param>
         public void setTrimUser(String value) {
             base.addInput ("TrimUser", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A LatestMentionResultSet containing execution metadata and results.</returns>
        new public LatestMentionResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            LatestMentionResultSet results = new JavaScriptSerializer().Deserialize<LatestMentionResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the LatestMention Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class LatestMentionResultSet : Temboo.Core.ResultSet
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
        /// Retrieve the value for the "ID" output from this Choreo execution
        /// <returns>String - (string) The Tweet ID.</returns>
        /// </summary>
        public String ID
        {
            get
            {
                return (String) base.Output["ID"];
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
        /// <summary> 
        /// Retrieve the value for the "ScreenName" output from this Choreo execution
        /// <returns>String - (string) The screen name of the user who has mentioned the authenticated user.</returns>
        /// </summary>
        public String ScreenName
        {
            get
            {
                return (String) base.Output["ScreenName"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Text" output from this Choreo execution
        /// <returns>String - (string) The Tweet text.</returns>
        /// </summary>
        public String Text
        {
            get
            {
                return (String) base.Output["Text"];
            }
        }
    }
}
