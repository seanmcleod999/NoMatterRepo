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

namespace Temboo.Library.Foursquare.Settings
{
    /// <summary>
    /// ChangeSetting
    /// Changes a setting for the given user.
    /// </summary>
    public class ChangeSetting : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ChangeSetting Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ChangeSetting(TembooSession session) : base(session, "/Library/Foursquare/Settings/ChangeSetting")
        {
        }

         /// <summary>
         /// (conditional, string) The Foursquare API OAuth token string.
         /// </summary>
         /// <param name="value">Value of the OauthToken input for this Choreo.</param>
         public void setOauthToken(String value) {
             base.addInput ("OauthToken", value);
         }
         /// <summary>
         /// (optional, string) The format that response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (required, string) Name of setting to change. Valid values are: sendMayorshipsToTwitter, sendBadgesToTwitter, sendMayorshipsToFacebook, sendBadgesToFacebook, receivePings, and receiveCommentPings.
         /// </summary>
         /// <param name="value">Value of the SettingID input for this Choreo.</param>
         public void setSettingID(String value) {
             base.addInput ("SettingID", value);
         }
         /// <summary>
         /// (required, boolean) The value of the setting you want to change. Set to 1 for true, and 0 for false.
         /// </summary>
         /// <param name="value">Value of the Value input for this Choreo.</param>
         public void setValue(String value) {
             base.addInput ("Value", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ChangeSettingResultSet containing execution metadata and results.</returns>
        new public ChangeSettingResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ChangeSettingResultSet results = new JavaScriptSerializer().Deserialize<ChangeSettingResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ChangeSetting Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ChangeSettingResultSet : Temboo.Core.ResultSet
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
