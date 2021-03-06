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

namespace Temboo.Library.Google.Calendar
{
    /// <summary>
    /// UpdateCalendar
    /// Updates the metadata for a calendar.
    /// </summary>
    public class UpdateCalendar : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateCalendar Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateCalendar(TembooSession session) : base(session, "/Library/Google/Calendar/UpdateCalendar")
        {
        }

         /// <summary>
         /// (optional, string) A valid access token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new access token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The unique ID for the calendar to update. Note that calendar IDs can be retrieved by running GetAllCalendars or SearchCalendarsByName.
         /// </summary>
         /// <param name="value">Value of the CalendarID input for this Choreo.</param>
         public void setCalendarID(String value) {
             base.addInput ("CalendarID", value);
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
         /// (optional, string) The new description for the calendar to update.
         /// </summary>
         /// <param name="value">Value of the NewDescription input for this Choreo.</param>
         public void setNewDescription(String value) {
             base.addInput ("NewDescription", value);
         }
         /// <summary>
         /// (optional, string) The new location for the calendar to update.
         /// </summary>
         /// <param name="value">Value of the NewLocation input for this Choreo.</param>
         public void setNewLocation(String value) {
             base.addInput ("NewLocation", value);
         }
         /// <summary>
         /// (required, string) The new summary for the calendar to update.
         /// </summary>
         /// <param name="value">Value of the NewSummary input for this Choreo.</param>
         public void setNewSummary(String value) {
             base.addInput ("NewSummary", value);
         }
         /// <summary>
         /// (optional, string) The new timezone for the calendar to update.
         /// </summary>
         /// <param name="value">Value of the NewTimezone input for this Choreo.</param>
         public void setNewTimezone(String value) {
             base.addInput ("NewTimezone", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new access token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (optional, string) The format that response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A UpdateCalendarResultSet containing execution metadata and results.</returns>
        new public UpdateCalendarResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateCalendarResultSet results = new JavaScriptSerializer().Deserialize<UpdateCalendarResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateCalendar Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateCalendarResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Google. Corresponds to the ResponseFormat input. Defaults to JSON.</returns>
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
