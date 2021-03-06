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

namespace Temboo.Library.Box.Folders
{
    /// <summary>
    /// DeleteFolder
    /// Deletes a specified folder.
    /// </summary>
    public class DeleteFolder : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the DeleteFolder Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public DeleteFolder(TembooSession session) : base(session, "/Library/Box/Folders/DeleteFolder")
        {
        }

         /// <summary>
         /// (required, string) The access token retrieved during the OAuth2 process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (optional, string) The ID of the user. Only used for enterprise administrators to make API calls for their managed users.
         /// </summary>
         /// <param name="value">Value of the AsUser input for this Choreo.</param>
         public void setAsUser(String value) {
             base.addInput ("AsUser", value);
         }
         /// <summary>
         /// (required, string) The id of the folder that you want to delete.
         /// </summary>
         /// <param name="value">Value of the FolderID input for this Choreo.</param>
         public void setFolderID(String value) {
             base.addInput ("FolderID", value);
         }
         /// <summary>
         /// (optional, boolean) Whether or not to delete this folder if it has items within in. Defaults to true.
         /// </summary>
         /// <param name="value">Value of the Recursive input for this Choreo.</param>
         public void setRecursive(String value) {
             base.addInput ("Recursive", value);
         }


        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DeleteFolderResultSet containing execution metadata and results.</returns>
        new public DeleteFolderResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DeleteFolderResultSet results = new JavaScriptSerializer().Deserialize<DeleteFolderResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the DeleteFolder Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DeleteFolderResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Box.</returns>
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
