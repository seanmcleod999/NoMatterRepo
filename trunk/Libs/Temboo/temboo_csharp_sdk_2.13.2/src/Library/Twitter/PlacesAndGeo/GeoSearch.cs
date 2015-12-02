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

namespace Temboo.Library.Twitter.PlacesAndGeo
{
    /// <summary>
    /// GeoSearch
    /// Searches for places that can be attached to a status update using a latitude and a longitude pair, an IP address, or a name.
    /// </summary>
    public class GeoSearch : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GeoSearch Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GeoSearch(TembooSession session) : base(session, "/Library/Twitter/PlacesAndGeo/GeoSearch")
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
         /// (optional, string) A hint on the "region" in which to search. If a number, then this is a radius in meters. You can also specify feet by using the ft suffix (i.e. 5ft).
         /// </summary>
         /// <param name="value">Value of the Accuracy input for this Choreo.</param>
         public void setAccuracy(String value) {
             base.addInput ("Accuracy", value);
         }
         /// <summary>
         /// (optional, string) If supplied, the response will use the JSONP format with a callback of the given name.
         /// </summary>
         /// <param name="value">Value of the Callback input for this Choreo.</param>
         public void setCallback(String value) {
             base.addInput ("Callback", value);
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
         /// (optional, string) This is the place_id which you would like to restrict the search results to. This id can be retrieved with the GetPlaceInformation Choreo.
         /// </summary>
         /// <param name="value">Value of the ContainedWithin input for this Choreo.</param>
         public void setContainedWithin(String value) {
             base.addInput ("ContainedWithin", value);
         }
         /// <summary>
         /// (optional, string) This is the minimal granularity of place types to return and must be one of: poi, neighborhood, city, admin or country. Defaults to neighborhood.
         /// </summary>
         /// <param name="value">Value of the Granularity input for this Choreo.</param>
         public void setGranularity(String value) {
             base.addInput ("Granularity", value);
         }
         /// <summary>
         /// (conditional, string) An IP address. Used when attempting to fix geolocation based off of the user's IP address. You must provide Latitude and Longitude, IP, or Query.
         /// </summary>
         /// <param name="value">Value of the IP input for this Choreo.</param>
         public void setIP(String value) {
             base.addInput ("IP", value);
         }
         /// <summary>
         /// (conditional, decimal) The latitude to search around. You must provide Latitude and Longitude, IP, or Query.
         /// </summary>
         /// <param name="value">Value of the Latitude input for this Choreo.</param>
         public void setLatitude(String value) {
             base.addInput ("Latitude", value);
         }
         /// <summary>
         /// (conditional, decimal) The longitude to search around. You must provide Latitude and Longitude, IP, or Query.
         /// </summary>
         /// <param name="value">Value of the Longitude input for this Choreo.</param>
         public void setLongitude(String value) {
             base.addInput ("Longitude", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of results to return.
         /// </summary>
         /// <param name="value">Value of the MaxResults input for this Choreo.</param>
         public void setMaxResults(String value) {
             base.addInput ("MaxResults", value);
         }
         /// <summary>
         /// (conditional, string) Free-form text to match against while executing a geo-based query. You must provide Latitude and Longitude, IP, or Query.
         /// </summary>
         /// <param name="value">Value of the Query input for this Choreo.</param>
         public void setQuery(String value) {
             base.addInput ("Query", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GeoSearchResultSet containing execution metadata and results.</returns>
        new public GeoSearchResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GeoSearchResultSet results = new JavaScriptSerializer().Deserialize<GeoSearchResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GeoSearch Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GeoSearchResultSet : Temboo.Core.ResultSet
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
