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

namespace Temboo.Library.Fitbit.Activities
{
    /// <summary>
    /// GetLatestActivity
    /// Gets a user's latest activity.
    /// </summary>
    public class GetLatestActivity : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetLatestActivity Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetLatestActivity(TembooSession session) : base(session, "/Library/Fitbit/Activities/GetLatestActivity")
        {
        }

         /// <summary>
         /// (required, string) The Access Token Secret retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessTokenSecret input for this Choreo.</param>
         public void setAccessTokenSecret(String value) {
             base.addInput ("AccessTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The Access Token retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The Consumer Key provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerKey input for this Choreo.</param>
         public void setConsumerKey(String value) {
             base.addInput ("ConsumerKey", value);
         }
         /// <summary>
         /// (required, string) The Consumer Secret provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerSecret input for this Choreo.</param>
         public void setConsumerSecret(String value) {
             base.addInput ("ConsumerSecret", value);
         }
         /// <summary>
         /// (optional, string) The format that you want the response to be in: xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) The user's encoded id. Defaults to "-" (dash) which will return data for the user associated with the token credentials provided.
         /// </summary>
         /// <param name="value">Value of the UserID input for this Choreo.</param>
         public void setUserID(String value) {
             base.addInput ("UserID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetLatestActivityResultSet containing execution metadata and results.</returns>
        new public GetLatestActivityResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetLatestActivityResultSet results = new JavaScriptSerializer().Deserialize<GetLatestActivityResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetLatestActivity Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetLatestActivityResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Fitbit.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ActivityID" output from this Choreo execution
        /// <returns>String - (string) The ID of the activity.</returns>
        /// </summary>
        public String ActivityID
        {
            get
            {
                return (String) base.Output["ActivityID"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Calories" output from this Choreo execution
        /// <returns>String - (integer) Calories burned during this activity.</returns>
        /// </summary>
        public String Calories
        {
            get
            {
                return (String) base.Output["Calories"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Description" output from this Choreo execution
        /// <returns>String - (string) The description of the activity.</returns>
        /// </summary>
        public String Description
        {
            get
            {
                return (String) base.Output["Description"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Distance" output from this Choreo execution
        /// <returns>String - (decimal) The distance traveled during the activity.</returns>
        /// </summary>
        public String Distance
        {
            get
            {
                return (String) base.Output["Distance"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Duration" output from this Choreo execution
        /// <returns>String - (integer) The duration of the activity (in milliseconds).</returns>
        /// </summary>
        public String Duration
        {
            get
            {
                return (String) base.Output["Duration"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Name" output from this Choreo execution
        /// <returns>String - (string) The name of the activity.</returns>
        /// </summary>
        public String Name
        {
            get
            {
                return (String) base.Output["Name"];
            }
        }
    }
}
