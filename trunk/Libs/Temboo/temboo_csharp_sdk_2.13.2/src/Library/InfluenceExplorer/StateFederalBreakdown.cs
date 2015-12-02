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

namespace Temboo.Library.InfluenceExplorer
{
    /// <summary>
    /// StateFederalBreakdown
    /// Returns the portion of contribution given by an individual or organization that went to state versus federal candidates.
    /// </summary>
    public class StateFederalBreakdown : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the StateFederalBreakdown Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public StateFederalBreakdown(TembooSession session) : base(session, "/Library/InfluenceExplorer/StateFederalBreakdown")
        {
        }

         /// <summary>
         /// (required, string) The API key provided by Sunlight Data Services.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, string) The ID for the Entity that you want to return information for. This ID can be retrieved by running the SearchByName Choreo.
         /// </summary>
         /// <param name="value">Value of the EntityID input for this Choreo.</param>
         public void setEntityID(String value) {
             base.addInput ("EntityID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A StateFederalBreakdownResultSet containing execution metadata and results.</returns>
        new public StateFederalBreakdownResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            StateFederalBreakdownResultSet results = new JavaScriptSerializer().Deserialize<StateFederalBreakdownResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the StateFederalBreakdown Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class StateFederalBreakdownResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Influence Explorer.</returns>
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
