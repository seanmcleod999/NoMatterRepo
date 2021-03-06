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

namespace Temboo.Library.EnviroFacts.UVForecast
{
    /// <summary>
    /// DailyUVByZipCode
    /// Retrieves EPA daily Ultraviolet (UV) Index readings in a given zip code. 
    /// </summary>
    public class DailyUVByZipCode : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the DailyUVByZipCode Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public DailyUVByZipCode(TembooSession session) : base(session, "/Library/EnviroFacts/UVForecast/DailyUVByZipCode")
        {
        }

         /// <summary>
         /// (optional, string) Results can be retrieved in either JSON or XML. Defaults to XML.
         /// </summary>
         /// <param name="value">Value of the ResponseType input for this Choreo.</param>
         public void setResponseType(String value) {
             base.addInput ("ResponseType", value);
         }
         /// <summary>
         /// (required, integer) A valid United States Postal Service (USPS) ZIP Code or Postal Code.
         /// </summary>
         /// <param name="value">Value of the Zip input for this Choreo.</param>
         public void setZip(String value) {
             base.addInput ("Zip", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DailyUVByZipCodeResultSet containing execution metadata and results.</returns>
        new public DailyUVByZipCodeResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DailyUVByZipCodeResultSet results = new JavaScriptSerializer().Deserialize<DailyUVByZipCodeResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the DailyUVByZipCode Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DailyUVByZipCodeResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from EnviroFacts.</returns>
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
