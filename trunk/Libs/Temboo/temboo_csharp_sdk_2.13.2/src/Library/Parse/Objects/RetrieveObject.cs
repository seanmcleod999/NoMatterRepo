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

namespace Temboo.Library.Parse.Objects
{
    /// <summary>
    /// RetrieveObject
    /// Retrieves a given object.
    /// </summary>
    public class RetrieveObject : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the RetrieveObject Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public RetrieveObject(TembooSession session) : base(session, "/Library/Parse/Objects/RetrieveObject")
        {
        }

         /// <summary>
         /// (required, string) The Application ID provided by Parse.
         /// </summary>
         /// <param name="value">Value of the ApplicationID input for this Choreo.</param>
         public void setApplicationID(String value) {
             base.addInput ("ApplicationID", value);
         }
         /// <summary>
         /// (required, string) The class name for the object being retrieved.
         /// </summary>
         /// <param name="value">Value of the ClassName input for this Choreo.</param>
         public void setClassName(String value) {
             base.addInput ("ClassName", value);
         }
         /// <summary>
         /// (required, string) The ID of the object to retrieve.
         /// </summary>
         /// <param name="value">Value of the ObjectID input for this Choreo.</param>
         public void setObjectID(String value) {
             base.addInput ("ObjectID", value);
         }
         /// <summary>
         /// (required, string) The REST API Key provided by Parse.
         /// </summary>
         /// <param name="value">Value of the RESTAPIKey input for this Choreo.</param>
         public void setRESTAPIKey(String value) {
             base.addInput ("RESTAPIKey", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RetrieveObjectResultSet containing execution metadata and results.</returns>
        new public RetrieveObjectResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RetrieveObjectResultSet results = new JavaScriptSerializer().Deserialize<RetrieveObjectResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the RetrieveObject Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RetrieveObjectResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Parse.</returns>
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
