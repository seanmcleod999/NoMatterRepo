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

namespace Temboo.Library.KhanAcademy.Users
{
    /// <summary>
    /// GetExerciseFollowUp
    /// Retrieves user data about all excercises which have the specified excercise as a prerequisite.
    /// </summary>
    public class GetExerciseFollowUp : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetExerciseFollowUp Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetExerciseFollowUp(TembooSession session) : base(session, "/Library/KhanAcademy/Users/GetExerciseFollowUp")
        {
        }

         /// <summary>
         /// (required, string) The Consumer Key provided by Khan Academy.
         /// </summary>
         /// <param name="value">Value of the ConsumerKey input for this Choreo.</param>
         public void setConsumerKey(String value) {
             base.addInput ("ConsumerKey", value);
         }
         /// <summary>
         /// (required, string) The OAuth Consumer Secret provided by Khan Academy.
         /// </summary>
         /// <param name="value">Value of the ConsumerSecret input for this Choreo.</param>
         public void setConsumerSecret(String value) {
             base.addInput ("ConsumerSecret", value);
         }
         /// <summary>
         /// (optional, string) The email address (coach or student ID) of user. If not provided, defaults to currently logged in user.
         /// </summary>
         /// <param name="value">Value of the Email input for this Choreo.</param>
         public void setEmail(String value) {
             base.addInput ("Email", value);
         }
         /// <summary>
         /// (required, string) The exercise for which you want to retrieve follwow up exercises (e.g. "simplifying_fractions").
         /// </summary>
         /// <param name="value">Value of the ExerciseName input for this Choreo.</param>
         public void setExerciseName(String value) {
             base.addInput ("ExerciseName", value);
         }
         /// <summary>
         /// (required, string) The OAuth Token Secret retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the OAuthTokenSecret input for this Choreo.</param>
         public void setOAuthTokenSecret(String value) {
             base.addInput ("OAuthTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The OAuth Token retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the OAuthToken input for this Choreo.</param>
         public void setOAuthToken(String value) {
             base.addInput ("OAuthToken", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetExerciseFollowUpResultSet containing execution metadata and results.</returns>
        new public GetExerciseFollowUpResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetExerciseFollowUpResultSet results = new JavaScriptSerializer().Deserialize<GetExerciseFollowUpResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetExerciseFollowUp Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetExerciseFollowUpResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Khan Academy.</returns>
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
