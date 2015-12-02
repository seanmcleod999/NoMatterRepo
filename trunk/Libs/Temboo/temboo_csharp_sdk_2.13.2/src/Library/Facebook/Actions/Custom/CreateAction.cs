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

namespace Temboo.Library.Facebook.Actions.Custom
{
    /// <summary>
    /// CreateAction
    /// Creates an Open Graph action that represents a user performing the specified action while using your app.
    /// </summary>
    public class CreateAction : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the CreateAction Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public CreateAction(TembooSession session) : base(session, "/Library/Facebook/Actions/Custom/CreateAction")
        {
        }

         /// <summary>
         /// (required, string) The access token retrieved from the final step of the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The type of action that a user is performing in your app (e.g. runs, walks, bikes).
         /// </summary>
         /// <param name="value">Value of the ActionType input for this Choreo.</param>
         public void setActionType(String value) {
             base.addInput ("ActionType", value);
         }
         /// <summary>
         /// (required, string) The namespace that you chose for you app. This can be found in the Settings section of your App page.
         /// </summary>
         /// <param name="value">Value of the AppNamespace input for this Choreo.</param>
         public void setAppNamespace(String value) {
             base.addInput ("AppNamespace", value);
         }
         /// <summary>
         /// (optional, date) The time that the action was created (e.g. 2013-06-24T18:53:35+0000).
         /// </summary>
         /// <param name="value">Value of the CreatedTime input for this Choreo.</param>
         public void setCreatedTime(String value) {
             base.addInput ("CreatedTime", value);
         }
         /// <summary>
         /// (optional, date) The time that the user ended the action (e.g. 2013-06-24T18:53:35+0000).
         /// </summary>
         /// <param name="value">Value of the EndTime input for this Choreo.</param>
         public void setEndTime(String value) {
             base.addInput ("EndTime", value);
         }
         /// <summary>
         /// (optional, integer) The amount of time (in milliseconds) from the publish_time that the action will expire.
         /// </summary>
         /// <param name="value">Value of the ExpiresIn input for this Choreo.</param>
         public void setExpiresIn(String value) {
             base.addInput ("ExpiresIn", value);
         }
         /// <summary>
         /// (optional, boolean) Indicates that the user is explicitly sharing this action. Requires the explicitly_shared capability to be enabled.
         /// </summary>
         /// <param name="value">Value of the ExplicitlyShared input for this Choreo.</param>
         public void setExplicitlyShared(String value) {
             base.addInput ("ExplicitlyShared", value);
         }
         /// <summary>
         /// (optional, boolean) Deprecated (retained for backward compatibility only).
         /// </summary>
         /// <param name="value">Value of the ExplicityShared input for this Choreo.</param>
         public void setExplicityShared(String value) {
             base.addInput ("ExplicityShared", value);
         }
         /// <summary>
         /// (optional, string) A message attached to this action. Setting this parameter requires enabling of message capabilities.
         /// </summary>
         /// <param name="value">Value of the Message input for this Choreo.</param>
         public void setMessage(String value) {
             base.addInput ("Message", value);
         }
         /// <summary>
         /// (optional, boolean) Whether or not this action should be posted to the users feed.
         /// </summary>
         /// <param name="value">Value of the NoFeedStory input for this Choreo.</param>
         public void setNoFeedStory(String value) {
             base.addInput ("NoFeedStory", value);
         }
         /// <summary>
         /// (optional, string) The URL or ID for an Open Graph object representing the location associated with this action.
         /// </summary>
         /// <param name="value">Value of the Place input for this Choreo.</param>
         public void setPlace(String value) {
             base.addInput ("Place", value);
         }
         /// <summary>
         /// (optional, string) The id of the user's profile. Defaults to "me" indicating the authenticated user.
         /// </summary>
         /// <param name="value">Value of the ProfileID input for this Choreo.</param>
         public void setProfileID(String value) {
             base.addInput ("ProfileID", value);
         }
         /// <summary>
         /// (required, string) The name of a property that you've defined for this Open Graph story. This will be an object type (e.g. album, song, book). Multiple property names can be separated by commas.
         /// </summary>
         /// <param name="value">Value of the PropertyName input for this Choreo.</param>
         public void setPropertyName(String value) {
             base.addInput ("PropertyName", value);
         }
         /// <summary>
         /// (required, string) The URL or ID for an Open Graph object representing the object specified as the PropertyName. Multiple property values can be separated by commas.
         /// </summary>
         /// <param name="value">Value of the PropertyValue input for this Choreo.</param>
         public void setPropertyValue(String value) {
             base.addInput ("PropertyValue", value);
         }
         /// <summary>
         /// (optional, string) A string identifier up to 50 characters used for tracking and insights.
         /// </summary>
         /// <param name="value">Value of the Reference input for this Choreo.</param>
         public void setReference(String value) {
             base.addInput ("Reference", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, date) The time that the user started the action (e.g. 2013-06-24T18:53:35+0000).
         /// </summary>
         /// <param name="value">Value of the StartTime input for this Choreo.</param>
         public void setStartTime(String value) {
             base.addInput ("StartTime", value);
         }
         /// <summary>
         /// (optional, string) A comma separated list of other profile IDs that also performed this action.
         /// </summary>
         /// <param name="value">Value of the Tags input for this Choreo.</param>
         public void setTags(String value) {
             base.addInput ("Tags", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A CreateActionResultSet containing execution metadata and results.</returns>
        new public CreateActionResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            CreateActionResultSet results = new JavaScriptSerializer().Deserialize<CreateActionResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the CreateAction Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class CreateActionResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Facebook.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ActivityURL" output from this Choreo execution
        /// <returns>String - (string) The URL for the newly created action.</returns>
        /// </summary>
        public String ActivityURL
        {
            get
            {
                return (String) base.Output["ActivityURL"];
            }
        }
    }
}
