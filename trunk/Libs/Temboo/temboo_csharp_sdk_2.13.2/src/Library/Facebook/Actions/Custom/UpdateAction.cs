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
    /// UpdateAction
    /// Updates an existing custom action.
    /// </summary>
    public class UpdateAction : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateAction Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateAction(TembooSession session) : base(session, "/Library/Facebook/Actions/Custom/UpdateAction")
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
         /// (required, string) The id of the action to update.
         /// </summary>
         /// <param name="value">Value of the ActionID input for this Choreo.</param>
         public void setActionID(String value) {
             base.addInput ("ActionID", value);
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
         /// (optional, string) A message attached to this action. Setting this parameter requires enabling of message capabilities.
         /// </summary>
         /// <param name="value">Value of the Message input for this Choreo.</param>
         public void setMessage(String value) {
             base.addInput ("Message", value);
         }
         /// <summary>
         /// (optional, string) The URL or ID for an Open Graph object representing the location associated with this action.
         /// </summary>
         /// <param name="value">Value of the Place input for this Choreo.</param>
         public void setPlace(String value) {
             base.addInput ("Place", value);
         }
         /// <summary>
         /// (optional, string) The name of a property that you've defined for this Open Graph story. This will be an object type (e.g. album, song, book). Multiple property names can be separated by commas.
         /// </summary>
         /// <param name="value">Value of the PropertyName input for this Choreo.</param>
         public void setPropertyName(String value) {
             base.addInput ("PropertyName", value);
         }
         /// <summary>
         /// (optional, string) The URL or ID for an Open Graph object representing the object specified as the PropertyName. Multiple property values can be separated by commas.
         /// </summary>
         /// <param name="value">Value of the PropertyValue input for this Choreo.</param>
         public void setPropertyValue(String value) {
             base.addInput ("PropertyValue", value);
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
        /// <returns>A UpdateActionResultSet containing execution metadata and results.</returns>
        new public UpdateActionResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateActionResultSet results = new JavaScriptSerializer().Deserialize<UpdateActionResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateAction Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateActionResultSet : Temboo.Core.ResultSet
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
    }
}
