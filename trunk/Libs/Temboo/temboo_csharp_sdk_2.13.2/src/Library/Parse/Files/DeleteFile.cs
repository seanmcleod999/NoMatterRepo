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

namespace Temboo.Library.Parse.Files
{
    /// <summary>
    /// DeleteFile
    /// Deletes a file stored on Parse.
    /// </summary>
    public class DeleteFile : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the DeleteFile Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public DeleteFile(TembooSession session) : base(session, "/Library/Parse/Files/DeleteFile")
        {
        }

         /// <summary>
         /// (required, string) The name of the file to delete. Note that this is the name generated and returned by Parse after uploading the file.
         /// </summary>
         /// <param name="value">Value of the FileName input for this Choreo.</param>
         public void setFileName(String value) {
             base.addInput ("FileName", value);
         }
         /// <summary>
         /// (required, string) The Application ID provided by Parse.
         /// </summary>
         /// <param name="value">Value of the ApplicationID input for this Choreo.</param>
         public void setApplicationID(String value) {
             base.addInput ("ApplicationID", value);
         }
         /// <summary>
         /// (required, string) The Master Key provided by Parse.
         /// </summary>
         /// <param name="value">Value of the MasterKey input for this Choreo.</param>
         public void setMasterKey(String value) {
             base.addInput ("MasterKey", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DeleteFileResultSet containing execution metadata and results.</returns>
        new public DeleteFileResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DeleteFileResultSet results = new JavaScriptSerializer().Deserialize<DeleteFileResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the DeleteFile Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DeleteFileResultSet : Temboo.Core.ResultSet
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
