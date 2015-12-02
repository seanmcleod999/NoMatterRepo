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

namespace Temboo.Library.Amazon.CloudDrive.Children
{
    /// <summary>
    /// AddChild
    /// Moves a specified folder into a parent folder.
    /// </summary>
    public class AddChild : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the AddChild Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public AddChild(TembooSession session) : base(session, "/Library/Amazon/CloudDrive/Children/AddChild")
        {
        }

         /// <summary>
         /// (optional, string) A valid access token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new access token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, string) The ID of the folder that is being moved within a parent folder.
         /// </summary>
         /// <param name="value">Value of the ChildID input for this Choreo.</param>
         public void setChildID(String value) {
             base.addInput ("ChildID", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by Amazon. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by Amazon. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (optional, boolean) Whether or not to perform a retry sequence if a throttling error occurs. Set to true to enable this feature. The request will be retried up-to five times when enabled.
         /// </summary>
         /// <param name="value">Value of the HandleRequestThrottling input for this Choreo.</param>
         public void setHandleRequestThrottling(String value) {
             base.addInput ("HandleRequestThrottling", value);
         }
         /// <summary>
         /// (optional, string) The appropriate metadataUrl for your account. When not provided, the Choreo will lookup the URL using the Account.GetEndpoint Choreo.
         /// </summary>
         /// <param name="value">Value of the MetaDataURL input for this Choreo.</param>
         public void setMetaDataURL(String value) {
             base.addInput ("MetaDataURL", value);
         }
         /// <summary>
         /// (required, string) The ID of the parent folder that will contain the child folder that's being moved.
         /// </summary>
         /// <param name="value">Value of the ParentID input for this Choreo.</param>
         public void setParentID(String value) {
             base.addInput ("ParentID", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth refresh token used to generate a new access token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AddChildResultSet containing execution metadata and results.</returns>
        new public AddChildResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AddChildResultSet results = new JavaScriptSerializer().Deserialize<AddChildResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the AddChild Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AddChildResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Amazon.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "NewAccessToken" output from this Choreo execution
        /// <returns>String - (string) Contains a new AccessToken when the RefreshToken is provided.</returns>
        /// </summary>
        public String NewAccessToken
        {
            get
            {
                return (String) base.Output["NewAccessToken"];
            }
        }
    }
}