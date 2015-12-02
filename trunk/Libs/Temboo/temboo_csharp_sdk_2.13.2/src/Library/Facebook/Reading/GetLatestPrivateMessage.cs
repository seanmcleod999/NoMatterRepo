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

namespace Temboo.Library.Facebook.Reading
{
    /// <summary>
    /// GetLatestPrivateMessage
    /// Retrieves the latest private message in a user's inbox.
    /// </summary>
    public class GetLatestPrivateMessage : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetLatestPrivateMessage Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetLatestPrivateMessage(TembooSession session) : base(session, "/Library/Facebook/Reading/GetLatestPrivateMessage")
        {
        }

         /// <summary>
         /// (required, string) The access token retrieved from the final step of the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (optional, string) The id of the profile to retrieve the latest message for. Defaults to "me" indicating the authenticated user.
         /// </summary>
         /// <param name="value">Value of the ProfileID input for this Choreo.</param>
         public void setProfileID(String value) {
             base.addInput ("ProfileID", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetLatestPrivateMessageResultSet containing execution metadata and results.</returns>
        new public GetLatestPrivateMessageResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetLatestPrivateMessageResultSet results = new JavaScriptSerializer().Deserialize<GetLatestPrivateMessageResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetLatestPrivateMessage Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetLatestPrivateMessageResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Facebook. Corresponds to the ResponseFormat input. Defaults to JSON.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "CreatedTime" output from this Choreo execution
        /// <returns>String - (date) The date that the latest message was created.</returns>
        /// </summary>
        public String CreatedTime
        {
            get
            {
                return (String) base.Output["CreatedTime"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "FromID" output from this Choreo execution
        /// <returns>String - (string) The ID of the message sender.</returns>
        /// </summary>
        public String FromID
        {
            get
            {
                return (String) base.Output["FromID"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "FromName" output from this Choreo execution
        /// <returns>String - (string) The name of the message sender.</returns>
        /// </summary>
        public String FromName
        {
            get
            {
                return (String) base.Output["FromName"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ID" output from this Choreo execution
        /// <returns>String - (string) The ID of the message.</returns>
        /// </summary>
        public String ID
        {
            get
            {
                return (String) base.Output["ID"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Message" output from this Choreo execution
        /// <returns>String - (string) The latest private message.</returns>
        /// </summary>
        public String Message
        {
            get
            {
                return (String) base.Output["Message"];
            }
        }
    }
}
