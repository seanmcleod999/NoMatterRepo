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

namespace Temboo.Library.Labs.Social
{
    /// <summary>
    /// GetNearbyContacts
    /// Searches Foursquare recent check-ins and the Facebook social graph with a given set of Geo coordinates
    /// </summary>
    public class GetNearbyContacts : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetNearbyContacts Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetNearbyContacts(TembooSession session) : base(session, "/Library/Labs/Social/GetNearbyContacts")
        {
        }

         /// <summary>
         /// (required, json) A JSON dictionary containing Facebook and Foursquare credentials.
         /// </summary>
         /// <param name="value">Value of the APICredentials input for this Choreo.</param>
         public void setAPICredentials(String value) {
             base.addInput ("APICredentials", value);
         }
         /// <summary>
         /// (optional, date) Seconds after which to look for checkins, e.g. for looking for new checkins since the last fetch.
         /// </summary>
         /// <param name="value">Value of the AfterTimestamp input for this Choreo.</param>
         public void setAfterTimestamp(String value) {
             base.addInput ("AfterTimestamp", value);
         }
         /// <summary>
         /// (required, decimal) The latitude coordinate of the location to search for.
         /// </summary>
         /// <param name="value">Value of the Latitude input for this Choreo.</param>
         public void setLatitude(String value) {
             base.addInput ("Latitude", value);
         }
         /// <summary>
         /// (optional, integer) Used to page through results. Limits the number of records returned in the API responses.
         /// </summary>
         /// <param name="value">Value of the Limit input for this Choreo.</param>
         public void setLimit(String value) {
             base.addInput ("Limit", value);
         }
         /// <summary>
         /// (conditional, decimal) The longitude coordinate of the location to search for.
         /// </summary>
         /// <param name="value">Value of the Longitude input for this Choreo.</param>
         public void setLongitude(String value) {
             base.addInput ("Longitude", value);
         }
         /// <summary>
         /// (optional, integer) Used to page through Facebook results. Returns results starting from the specified number.
         /// </summary>
         /// <param name="value">Value of the Offset input for this Choreo.</param>
         public void setOffset(String value) {
             base.addInput ("Offset", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetNearbyContactsResultSet containing execution metadata and results.</returns>
        new public GetNearbyContactsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetNearbyContactsResultSet results = new JavaScriptSerializer().Deserialize<GetNearbyContactsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetNearbyContacts Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetNearbyContactsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) A merged result of Foursquare and Facebook location based searches.</returns>
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
