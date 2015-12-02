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
    /// SearchEvents
    /// Allows you to search for events using a variety of search parameters.
    /// </summary>
    public class SearchEvents : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the SearchEvents Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public SearchEvents(TembooSession session) : base(session, "/Library/Google/Calendar/SearchEvents")
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
         /// (required, string) The unique ID for the calendar with the events to search. Note that calendar IDs can be retrieved by running GetAllCalendars or SearchCalendarsByName.
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
         /// (optional, date) An event's last modification time (as a RFC 3339 timestamp) to filter by.
         /// </summary>
         /// <param name="value">Value of the LastModified input for this Choreo.</param>
         public void setLastModified(String value) {
             base.addInput ("LastModified", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of attendees to include in the response. If there are more than the specified number of attendees, only the participant is returned.
         /// </summary>
         /// <param name="value">Value of the MaxAttendees input for this Choreo.</param>
         public void setMaxAttendees(String value) {
             base.addInput ("MaxAttendees", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of events to return on one result page.
         /// </summary>
         /// <param name="value">Value of the MaxResults input for this Choreo.</param>
         public void setMaxResults(String value) {
             base.addInput ("MaxResults", value);
         }
         /// <summary>
         /// (optional, date) The max start time to filter by (formatted like 2012-05-22T00:47:43.000Z).
         /// </summary>
         /// <param name="value">Value of the MaxTime input for this Choreo.</param>
         public void setMaxTime(String value) {
             base.addInput ("MaxTime", value);
         }
         /// <summary>
         /// (optional, date) The minimum start time to filter by (formatted like 2012-05-22T00:47:43.000Z).
         /// </summary>
         /// <param name="value">Value of the MinTime input for this Choreo.</param>
         public void setMinTime(String value) {
             base.addInput ("MinTime", value);
         }
         /// <summary>
         /// (optional, string) The order of the events returned in the result. Accepted values are: "startTime" (ordered by start date/time. Must set SingleEvents to 1 to use this) or "updated" (ordered by modification date/time).
         /// </summary>
         /// <param name="value">Value of the OrderBy input for this Choreo.</param>
         public void setOrderBy(String value) {
             base.addInput ("OrderBy", value);
         }
         /// <summary>
         /// (optional, integer) Indicates which result page to return. Used for paging through results.
         /// </summary>
         /// <param name="value">Value of the PageToken input for this Choreo.</param>
         public void setPageToken(String value) {
             base.addInput ("PageToken", value);
         }
         /// <summary>
         /// (optional, string) A keyword search to find events.
         /// </summary>
         /// <param name="value">Value of the Query input for this Choreo.</param>
         public void setQuery(String value) {
             base.addInput ("Query", value);
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
         /// (optional, boolean) Whether to include deleted events. Set to 1 (true) to include deleted events. Defaults to 0 (false).
         /// </summary>
         /// <param name="value">Value of the ShowDeleted input for this Choreo.</param>
         public void setShowDeleted(String value) {
             base.addInput ("ShowDeleted", value);
         }
         /// <summary>
         /// (optional, boolean) Whether to include hidden invitations in the result. Set to 1 (true) to enable. The default is 0 (false).
         /// </summary>
         /// <param name="value">Value of the ShowHiddenInvitations input for this Choreo.</param>
         public void setShowHiddenInvitations(String value) {
             base.addInput ("ShowHiddenInvitations", value);
         }
         /// <summary>
         /// (optional, boolean) Whether to expand recurring events into instances and only return single one-off events and instances of recurring events. Defaults to 0 (false).
         /// </summary>
         /// <param name="value">Value of the SingleEvent input for this Choreo.</param>
         public void setSingleEvent(String value) {
             base.addInput ("SingleEvent", value);
         }
         /// <summary>
         /// (optional, string) The time zone used in the response (i.e. America/Los_Angeles). The default is the time zone of the calendar.
         /// </summary>
         /// <param name="value">Value of the Timezone input for this Choreo.</param>
         public void setTimezone(String value) {
             base.addInput ("Timezone", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A SearchEventsResultSet containing execution metadata and results.</returns>
        new public SearchEventsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            SearchEventsResultSet results = new JavaScriptSerializer().Deserialize<SearchEventsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the SearchEvents Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class SearchEventsResultSet : Temboo.Core.ResultSet
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
