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

namespace Temboo.Library.Bugzilla
{
    /// <summary>
    /// RetrieveAttachment
    /// Retrieves a bug attachment by ID.
    /// </summary>
    public class RetrieveAttachment : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the RetrieveAttachment Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public RetrieveAttachment(TembooSession session) : base(session, "/Library/Bugzilla/RetrieveAttachment")
        {
        }

         /// <summary>
         /// (required, integer) The ID of the attachment to retrieve.
         /// </summary>
         /// <param name="value">Value of the AttachmentID input for this Choreo.</param>
         public void setAttachmentID(String value) {
             base.addInput ("AttachmentID", value);
         }
         /// <summary>
         /// (optional, integer) Enter 1 to obtain full bug attachments data.  If null, only attachments fields will be returned with no associated data.
         /// </summary>
         /// <param name="value">Value of the AttachmentsWithData input for this Choreo.</param>
         public void setAttachmentsWithData(String value) {
             base.addInput ("AttachmentsWithData", value);
         }
         /// <summary>
         /// (optional, password) Your Bugzilla password.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (optional, string) The base URL for the Bugzilla server to access. Defaults to https://api-dev.bugzilla.mozilla.org/latest. To access the test server, set to https://api-dev.bugzilla.mozilla.org/test/latest.
         /// </summary>
         /// <param name="value">Value of the Server input for this Choreo.</param>
         public void setServer(String value) {
             base.addInput ("Server", value);
         }
         /// <summary>
         /// (optional, string) Your Bugzilla username.
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RetrieveAttachmentResultSet containing execution metadata and results.</returns>
        new public RetrieveAttachmentResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RetrieveAttachmentResultSet results = new JavaScriptSerializer().Deserialize<RetrieveAttachmentResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the RetrieveAttachment Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RetrieveAttachmentResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Bugzilla.</returns>
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
