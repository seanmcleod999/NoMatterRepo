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
    /// TopRecipientOrganizations
    /// Returns the top organizations to which the specified individual has given money.
    /// </summary>
    public class TopRecipientOrganizations : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the TopRecipientOrganizations Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public TopRecipientOrganizations(TembooSession session) : base(session, "/Library/InfluenceExplorer/TopRecipientOrganizations")
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
         /// (optional, integer) The number of resutls to return.
         /// </summary>
         /// <param name="value">Value of the Limit input for this Choreo.</param>
         public void setLimit(String value) {
             base.addInput ("Limit", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A TopRecipientOrganizationsResultSet containing execution metadata and results.</returns>
        new public TopRecipientOrganizationsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            TopRecipientOrganizationsResultSet results = new JavaScriptSerializer().Deserialize<TopRecipientOrganizationsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the TopRecipientOrganizations Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class TopRecipientOrganizationsResultSet : Temboo.Core.ResultSet
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
