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

namespace Temboo.Library.YouTube.Activities
{
    /// <summary>
    /// ListMyActivities
    /// Returns a list of activity events for the authenticated user.
    /// </summary>
    public class ListMyActivities : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ListMyActivities Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ListMyActivities(TembooSession session) : base(session, "/Library/YouTube/Activities/ListMyActivities")
        {
        }

         /// <summary>
         /// (optional, string) A valid access token retrieved during the OAuth process. This is required for OAuth authentication unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new access token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by Google. Required for OAuth authentication unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by Google. Required for OAuth authentication unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (optional, string) Allows you to specify a subset of fields to include in the response using an xpath-like syntax (i.e. items/snippet/title).
         /// </summary>
         /// <param name="value">Value of the Fields input for this Choreo.</param>
         public void setFields(String value) {
             base.addInput ("Fields", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of results to return.
         /// </summary>
         /// <param name="value">Value of the MaxResults input for this Choreo.</param>
         public void setMaxResults(String value) {
             base.addInput ("MaxResults", value);
         }
         /// <summary>
         /// (optional, string) The "nextPageToken" found in the response which is used to page through results.
         /// </summary>
         /// <param name="value">Value of the PageToken input for this Choreo.</param>
         public void setPageToken(String value) {
             base.addInput ("PageToken", value);
         }
         /// <summary>
         /// (optional, string) Specifies a comma-separated list of one or more activity resource properties that the API response will include. Part names that you can pass are id, snippet, and contentDetails.
         /// </summary>
         /// <param name="value">Value of the Part input for this Choreo.</param>
         public void setPart(String value) {
             base.addInput ("Part", value);
         }
         /// <summary>
         /// (optional, date) Returns only results created after the specified time (formatted as a RFC 3339 date-time i.e. 1970-01-01T00:00:00Z).
         /// </summary>
         /// <param name="value">Value of the PublishedAfter input for this Choreo.</param>
         public void setPublishedAfter(String value) {
             base.addInput ("PublishedAfter", value);
         }
         /// <summary>
         /// (optional, date) Returns only results created before the specified time (formatted as a RFC 3339 date-time i.e. 1970-01-01T00:00:00Z).
         /// </summary>
         /// <param name="value">Value of the PublishedBefore input for this Choreo.</param>
         public void setPublishedBefore(String value) {
             base.addInput ("PublishedBefore", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth refresh token used to generate a new access token when the original token is expired. Required for OAuth authentication unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ListMyActivitiesResultSet containing execution metadata and results.</returns>
        new public ListMyActivitiesResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ListMyActivitiesResultSet results = new JavaScriptSerializer().Deserialize<ListMyActivitiesResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ListMyActivities Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ListMyActivitiesResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from YouTube.</returns>
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
