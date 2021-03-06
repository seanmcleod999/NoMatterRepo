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
    /// GetTeacherStats
    /// Returns teacher statistics for a school, district, or state. 
    /// </summary>
    public class GetTeacherStats : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetTeacherStats Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetTeacherStats(TembooSession session) : base(session, "/Library/SchoolFinder/GetTeacherStats")
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
         /// (conditional, string) The education.com district id.
         /// </summary>
         /// <param name="value">Value of the DistrictID input for this Choreo.</param>
         public void setDistrictID(String value) {
             base.addInput ("DistrictID", value);
         }
         /// <summary>
         /// (conditional, string) The LEA id of the district.
         /// </summary>
         /// <param name="value">Value of the DistrictLEA input for this Choreo.</param>
         public void setDistrictLEA(String value) {
             base.addInput ("DistrictLEA", value);
         }
         /// <summary>
         /// (conditional, string) The National Center for Education Statistics (NCES) id of the school.
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
         /// (conditional, string) The Education.com id of the school you want to find.
         /// </summary>
         /// <param name="value">Value of the SchoolID input for this Choreo.</param>
         public void setSchoolID(String value) {
             base.addInput ("SchoolID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetTeacherStatsResultSet containing execution metadata and results.</returns>
        new public GetTeacherStatsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetTeacherStatsResultSet results = new JavaScriptSerializer().Deserialize<GetTeacherStatsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetTeacherStats Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetTeacherStatsResultSet : Temboo.Core.ResultSet
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
