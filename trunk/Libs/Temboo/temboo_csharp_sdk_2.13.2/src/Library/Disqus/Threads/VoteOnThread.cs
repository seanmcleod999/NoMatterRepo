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

namespace Temboo.Library.Disqus.Threads
{
    /// <summary>
    /// VoteOnThread
    /// Vote on a thread.
    /// </summary>
    public class VoteOnThread : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the VoteOnThread Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public VoteOnThread(TembooSession session) : base(session, "/Library/Disqus/Threads/VoteOnThread")
        {
        }

         /// <summary>
         /// (optional, string) Forum Short Name (i.e., the subdomain of the Disqus Site URL) of a thread that is being voted on. Required if setting either ThreadByIdentification, or ThreadByLink.
         /// </summary>
         /// <param name="value">Value of the Forum input for this Choreo.</param>
         public void setForum(String value) {
             base.addInput ("Forum", value);
         }
         /// <summary>
         /// (required, string) The Public Key provided by Disqus (AKA the API Key).
         /// </summary>
         /// <param name="value">Value of the PublicKey input for this Choreo.</param>
         public void setPublicKey(String value) {
             base.addInput ("PublicKey", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: json (the default) and jsonp.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, integer) The ID of a thread that is being voted on. Required unless specifying the ThreadIdentifier or ThreadLink. If using this parameter, ThreadIdentifier cannot be set.
         /// </summary>
         /// <param name="value">Value of the ThreadID input for this Choreo.</param>
         public void setThreadID(String value) {
             base.addInput ("ThreadID", value);
         }
         /// <summary>
         /// (conditional, string) The identifier for the thread that is being voted on. Note that a Forum must also be provided when using this parameter. If set, ThreadID and ThreadLink cannot be used.
         /// </summary>
         /// <param name="value">Value of the ThreadIdentifier input for this Choreo.</param>
         public void setThreadIdentifier(String value) {
             base.addInput ("ThreadIdentifier", value);
         }
         /// <summary>
         /// (conditional, string) A link pointing to the thread that is being voted on. Note that a Forum must also be provided when using this parameter. If set, ThreadID and ThreadIdentifier cannot be set.
         /// </summary>
         /// <param name="value">Value of the ThreadLink input for this Choreo.</param>
         public void setThreadLink(String value) {
             base.addInput ("ThreadLink", value);
         }
         /// <summary>
         /// (required, integer) A numberic value for your vote. Valid choices are: -1, 0, or 1.
         /// </summary>
         /// <param name="value">Value of the Vote input for this Choreo.</param>
         public void setVote(String value) {
             base.addInput ("Vote", value);
         }


        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A VoteOnThreadResultSet containing execution metadata and results.</returns>
        new public VoteOnThreadResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            VoteOnThreadResultSet results = new JavaScriptSerializer().Deserialize<VoteOnThreadResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the VoteOnThread Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class VoteOnThreadResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Disqus.</returns>
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
