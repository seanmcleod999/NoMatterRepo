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

namespace Temboo.Library.Foursquare.Tips
{
    /// <summary>
    /// Done
    /// Returns an array of users have done this tip.
    /// </summary>
    public class Done : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the Done Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public Done(TembooSession session) : base(session, "/Library/Foursquare/Tips/Done")
        {
        }

         /// <summary>
         /// (optional, integer) Number of results to return, up to 200.
         /// </summary>
         /// <param name="value">Value of the Limit input for this Choreo.</param>
         public void setLimit(String value) {
             base.addInput ("Limit", value);
         }
         /// <summary>
         /// (required, string) The Foursquare API OAuth token string.
         /// </summary>
         /// <param name="value">Value of the OauthToken input for this Choreo.</param>
         public void setOauthToken(String value) {
             base.addInput ("OauthToken", value);
         }
         /// <summary>
         /// (optional, integer) Used to page through results.
         /// </summary>
         /// <param name="value">Value of the Offset input for this Choreo.</param>
         public void setOffset(String value) {
             base.addInput ("Offset", value);
         }
         /// <summary>
         /// (optional, string) The format that response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (required, string) The id of a tip to get users who have marked the tip as done.
         /// </summary>
         /// <param name="value">Value of the TipID input for this Choreo.</param>
         public void setTipID(String value) {
             base.addInput ("TipID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DoneResultSet containing execution metadata and results.</returns>
        new public DoneResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DoneResultSet results = new JavaScriptSerializer().Deserialize<DoneResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the Done Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DoneResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Foursquare. Corresponds to the ResponseFormat input. Defaults to JSON.</returns>
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
