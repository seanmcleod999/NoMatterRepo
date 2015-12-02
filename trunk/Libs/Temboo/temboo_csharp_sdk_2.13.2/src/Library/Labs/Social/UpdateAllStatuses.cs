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

namespace Temboo.Library.Labs.Social
{
    /// <summary>
    /// UpdateAllStatuses
    /// Shares a post across multiple social networks such as Facebook, Tumblr, and Twitter.
    /// </summary>
    public class UpdateAllStatuses : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateAllStatuses Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateAllStatuses(TembooSession session) : base(session, "/Library/Labs/Social/UpdateAllStatuses")
        {
        }

         /// <summary>
         /// (required, json) A list of credentials for the APIs you wish to access. See Choreo documentation for formatting examples.
         /// </summary>
         /// <param name="value">Value of the APICredentials input for this Choreo.</param>
         public void setAPICredentials(String value) {
             base.addInput ("APICredentials", value);
         }
         /// <summary>
         /// (required, string) The message to be posted across social networks.
         /// </summary>
         /// <param name="value">Value of the Message input for this Choreo.</param>
         public void setMessage(String value) {
             base.addInput ("Message", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A UpdateAllStatusesResultSet containing execution metadata and results.</returns>
        new public UpdateAllStatusesResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateAllStatusesResultSet results = new JavaScriptSerializer().Deserialize<UpdateAllStatusesResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateAllStatuses Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateAllStatusesResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) A list of results for each API.</returns>
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
