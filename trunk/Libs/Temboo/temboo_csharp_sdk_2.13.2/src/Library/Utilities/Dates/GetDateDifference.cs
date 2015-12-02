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

namespace Temboo.Library.Utilities.Dates
{
    /// <summary>
    /// GetDateDifference
    /// Returns the difference between two specified dates, expressed as the number of milliseconds since January 1, 1970 (epoch time).
    /// </summary>
    public class GetDateDifference : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetDateDifference Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetDateDifference(TembooSession session) : base(session, "/Library/Utilities/Dates/GetDateDifference")
        {
        }

         /// <summary>
         /// (required, date) The earlier date to use for the date comparision (e.g., March 2, 2014).
         /// </summary>
         /// <param name="value">Value of the EarlierDate input for this Choreo.</param>
         public void setEarlierDate(String value) {
             base.addInput ("EarlierDate", value);
         }
         /// <summary>
         /// (required, date) The later date to use for the date comparision (e.g., March 3, 2014).
         /// </summary>
         /// <param name="value">Value of the LaterDate input for this Choreo.</param>
         public void setLaterDate(String value) {
             base.addInput ("LaterDate", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetDateDifferenceResultSet containing execution metadata and results.</returns>
        new public GetDateDifferenceResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetDateDifferenceResultSet results = new JavaScriptSerializer().Deserialize<GetDateDifferenceResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetDateDifference Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetDateDifferenceResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Difference" output from this Choreo execution
        /// <returns>String - (integer) The difference between two specified dates, expressed as the number of milliseconds since January 1, 1970 (epoch time). </returns>
        /// </summary>
        public String Difference
        {
            get
            {
                return (String) base.Output["Difference"];
            }
        }
    }
}