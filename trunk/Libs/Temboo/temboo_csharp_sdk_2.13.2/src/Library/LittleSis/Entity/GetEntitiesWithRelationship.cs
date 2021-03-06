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

namespace Temboo.Library.LittleSis.Entity
{
    /// <summary>
    /// GetEntitiesWithRelationship
    /// Retrieves a list of Entities (person or organization) to which a known relationship exists in LittleSis for any Entity.
    /// </summary>
    public class GetEntitiesWithRelationship : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetEntitiesWithRelationship Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetEntitiesWithRelationship(TembooSession session) : base(session, "/Library/LittleSis/Entity/GetEntitiesWithRelationship")
        {
        }

         /// <summary>
         /// (required, string) The API Key obtained from LittleSis.org.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (optional, string) Comma delimited list of category IDs of the categories to which the resulting Entities should belong.
         /// </summary>
         /// <param name="value">Value of the CategoryIDs input for this Choreo.</param>
         public void setCategoryIDs(String value) {
             base.addInput ("CategoryIDs", value);
         }
         /// <summary>
         /// (optional, integer) Set to 1 to limit the relationships returned to only past relationships. Set to 0 to limit relationships returned to only current relationships. Defaults to all.
         /// </summary>
         /// <param name="value">Value of the Current input for this Choreo.</param>
         public void setCurrent(String value) {
             base.addInput ("Current", value);
         }
         /// <summary>
         /// (required, integer) The ID of the person or organization fro which a record is to be returned.
         /// </summary>
         /// <param name="value">Value of the EntityID input for this Choreo.</param>
         public void setEntityID(String value) {
             base.addInput ("EntityID", value);
         }
         /// <summary>
         /// (optional, integer) Specifies what number of results to show. Used in conjunction with Page parameter, a Number of 20 and a Page of 6 will show results 100-120.
         /// </summary>
         /// <param name="value">Value of the Number input for this Choreo.</param>
         public void setNumber(String value) {
             base.addInput ("Number", value);
         }
         /// <summary>
         /// (optional, integer) Specifies what order the given entity must have in the relationship.
         /// </summary>
         /// <param name="value">Value of the Order input for this Choreo.</param>
         public void setOrder(String value) {
             base.addInput ("Order", value);
         }
         /// <summary>
         /// (optional, integer) Specifies what page of results to show. Used in conjunction with Number parameter. A number of 20 and a Page of 6 will show results 100-120.
         /// </summary>
         /// <param name="value">Value of the Page input for this Choreo.</param>
         public void setPage(String value) {
             base.addInput ("Page", value);
         }
         /// <summary>
         /// (optional, string) Format of the response returned by LittleSis.org. Acceptable inputs: xml or json. Defaults to xml
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Defaults to sorting by entity, which returns a list of relationships grouped by related entity. Specify another sort order for the results. Acceptable inputs: category or relationship.
         /// </summary>
         /// <param name="value">Value of the SortBy input for this Choreo.</param>
         public void setSortBy(String value) {
             base.addInput ("SortBy", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetEntitiesWithRelationshipResultSet containing execution metadata and results.</returns>
        new public GetEntitiesWithRelationshipResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetEntitiesWithRelationshipResultSet results = new JavaScriptSerializer().Deserialize<GetEntitiesWithRelationshipResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetEntitiesWithRelationship Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetEntitiesWithRelationshipResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from LittleSis.org.</returns>
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
