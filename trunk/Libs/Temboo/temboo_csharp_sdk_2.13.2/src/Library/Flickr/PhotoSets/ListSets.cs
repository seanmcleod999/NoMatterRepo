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

namespace Temboo.Library.Flickr.PhotoSets
{
    /// <summary>
    /// ListSets
    /// Returns the photosets belonging to the specified user.
    /// </summary>
    public class ListSets : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ListSets Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ListSets(TembooSession session) : base(session, "/Library/Flickr/PhotoSets/ListSets")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Flickr (AKA the OAuth Consumer Key).
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (conditional, string) The API Secret provided by Flickr (AKA the OAuth Consumer Secret). Required when accessing a protected resource or when UserID is not provided.
         /// </summary>
         /// <param name="value">Value of the APISecret input for this Choreo.</param>
         public void setAPISecret(String value) {
             base.addInput ("APISecret", value);
         }
         /// <summary>
         /// (conditional, string) The Access Token Secret retrieved during the OAuth process. Required when accessing a protected resource or when UserID is not provided.
         /// </summary>
         /// <param name="value">Value of the AccessTokenSecret input for this Choreo.</param>
         public void setAccessTokenSecret(String value) {
             base.addInput ("AccessTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The Access Token retrieved during the OAuth process. Required when accessing a protected resource or when UserID is not provided.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (optional, integer) The page of results to get. Currently, if this is not provided, all sets are returned, but this behaviour may change in future.
         /// </summary>
         /// <param name="value">Value of the Page input for this Choreo.</param>
         public void setPage(String value) {
             base.addInput ("Page", value);
         }
         /// <summary>
         /// (optional, integer) The number of sets to get per page. If paging is enabled, the maximum number of sets per page is 500.
         /// </summary>
         /// <param name="value">Value of the PerPage input for this Choreo.</param>
         public void setPerPage(String value) {
             base.addInput ("PerPage", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: xml and json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, string) The NSID of the user to get a photoset list for. When OAuth parameters are passed, the authenticated user is assumed.
         /// </summary>
         /// <param name="value">Value of the UserID input for this Choreo.</param>
         public void setUserID(String value) {
             base.addInput ("UserID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ListSetsResultSet containing execution metadata and results.</returns>
        new public ListSetsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ListSetsResultSet results = new JavaScriptSerializer().Deserialize<ListSetsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ListSets Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ListSetsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Flickr.</returns>
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
