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

namespace Temboo.Library.SchoolFinder
{
    /// <summary>
    /// SchoolSearch
    /// Returns a list of school district profiles and related school information for a zip code, city, state, or other criteria in the search parameters.
    /// </summary>
    public class SchoolSearch : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the SchoolSearch Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public SchoolSearch(TembooSession session) : base(session, "/Library/SchoolFinder/SchoolSearch")
        {
        }

         /// <summary>
         /// (required, string) Your School Finder API Key.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (conditional, string) The name of a city. Must also be accompanied by the corresponding state parameter.
         /// </summary>
         /// <param name="value">Value of the City input for this Choreo.</param>
         public void setCity(String value) {
             base.addInput ("City", value);
         }
         /// <summary>
         /// (conditional, string) The name of a county.
         /// </summary>
         /// <param name="value">Value of the County input for this Choreo.</param>
         public void setCounty(String value) {
             base.addInput ("County", value);
         }
         /// <summary>
         /// (conditional, decimal) A distance in miles from a specific latitude/longitude. The suggested value is around 1.5 miles. Please note that distance is required when using latitude and longitude parameters.
         /// </summary>
         /// <param name="value">Value of the Distance input for this Choreo.</param>
         public void setDistance(String value) {
             base.addInput ("Distance", value);
         }
         /// <summary>
         /// (optional, string) The internal Education.com id of a school district.
         /// </summary>
         /// <param name="value">Value of the DistrictID input for this Choreo.</param>
         public void setDistrictID(String value) {
             base.addInput ("DistrictID", value);
         }
         /// <summary>
         /// (optional, string) The NCES Local Education Agency (LEA) id of a school district.
         /// </summary>
         /// <param name="value">Value of the DistrictLEA input for this Choreo.</param>
         public void setDistrictLEA(String value) {
             base.addInput ("DistrictLEA", value);
         }
         /// <summary>
         /// (conditional, decimal) A latitude which serves as the center for distance searches. Please note that distance is required when using latitude and longitude parameters.
         /// </summary>
         /// <param name="value">Value of the Latitude input for this Choreo.</param>
         public void setLatitude(String value) {
             base.addInput ("Latitude", value);
         }
         /// <summary>
         /// (conditional, decimal) A longitude which serves as the center for distance searches. Please note that distance is required when using latitude and longitude parameters.
         /// </summary>
         /// <param name="value">Value of the Longitude input for this Choreo.</param>
         public void setLongitude(String value) {
             base.addInput ("Longitude", value);
         }
         /// <summary>
         /// (optional, decimal) Minimum number of search results to return. The search will be expanded in increments of 0.5 miles until the minresult is reached. minResult is only valid for zip code and latitude/longitude requests.
         /// </summary>
         /// <param name="value">Value of the MinResult input for this Choreo.</param>
         public void setMinResult(String value) {
             base.addInput ("MinResult", value);
         }
         /// <summary>
         /// (optional, string) The National Center for Education Statistics (NCES) id of the school you want to find.
         /// </summary>
         /// <param name="value">Value of the NCES input for this Choreo.</param>
         public void setNCES(String value) {
             base.addInput ("NCES", value);
         }
         /// <summary>
         /// (optional, string) Format of the response returned by Education.com. Defaluts to XML. JSON is also possible.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) The Education.com id of the school you want to find.
         /// </summary>
         /// <param name="value">Value of the SchoolID input for this Choreo.</param>
         public void setSchoolID(String value) {
             base.addInput ("SchoolID", value);
         }
         /// <summary>
         /// (conditional, string) The two letter abbreviation of a state e.g. South Caroline should be formatted “SC”.
         /// </summary>
         /// <param name="value">Value of the State input for this Choreo.</param>
         public void setState(String value) {
             base.addInput ("State", value);
         }
         /// <summary>
         /// (conditional, integer) A five digit US postal code.
         /// </summary>
         /// <param name="value">Value of the Zip input for this Choreo.</param>
         public void setZip(String value) {
             base.addInput ("Zip", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A SchoolSearchResultSet containing execution metadata and results.</returns>
        new public SchoolSearchResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            SchoolSearchResultSet results = new JavaScriptSerializer().Deserialize<SchoolSearchResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the SchoolSearch Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class SchoolSearchResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Education.com.</returns>
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
