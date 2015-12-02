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

namespace Temboo.Library.Google.ComputeEngine.Instances
{
    /// <summary>
    /// AddAccessConfig
    /// Adds an access config to an instance's network interface.
    /// </summary>
    public class AddAccessConfig : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the AddAccessConfig Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public AddAccessConfig(TembooSession session) : base(session, "/Library/Google/ComputeEngine/Instances/AddAccessConfig")
        {
        }

         /// <summary>
         /// (optional, json) A JSON string containing the access configuration properties you wish to set. This can be used as an alternative to individual inputs that represent access configuration properties.
         /// </summary>
         /// <param name="value">Value of the AccessConfiguration input for this Choreo.</param>
         public void setAccessConfiguration(String value) {
             base.addInput ("AccessConfiguration", value);
         }
         /// <summary>
         /// (optional, string) A valid access token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new access token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (required, string) Name of the instance for which to add an access configuration.
         /// </summary>
         /// <param name="value">Value of the Instance input for this Choreo.</param>
         public void setInstance(String value) {
             base.addInput ("Instance", value);
         }
         /// <summary>
         /// (optional, string) The name of this access configuration. Defaults to "External NAT" if not specified.
         /// </summary>
         /// <param name="value">Value of the Name input for this Choreo.</param>
         public void setName(String value) {
             base.addInput ("Name", value);
         }
         /// <summary>
         /// (optional, string) An external IP address associated with this instance. Specify an unused static IP address available to the project. An external IP will be drawn from a shared ephemeral pool when not specified.
         /// </summary>
         /// <param name="value">Value of the NatIP input for this Choreo.</param>
         public void setNatIP(String value) {
             base.addInput ("NatIP", value);
         }
         /// <summary>
         /// (required, string) The name of the network interface to add the access config (e.g. nic0, nic1, etc).
         /// </summary>
         /// <param name="value">Value of the NetworkInterface input for this Choreo.</param>
         public void setNetworkInterface(String value) {
             base.addInput ("NetworkInterface", value);
         }
         /// <summary>
         /// (required, string) The ID of a Google Compute project.
         /// </summary>
         /// <param name="value">Value of the Project input for this Choreo.</param>
         public void setProject(String value) {
             base.addInput ("Project", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth refresh token used to generate a new access token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (required, string) Type of configuration. Must be set to ONE_TO_ONE_NAT.
         /// </summary>
         /// <param name="value">Value of the Type input for this Choreo.</param>
         public void setType(String value) {
             base.addInput ("Type", value);
         }
         /// <summary>
         /// (required, string) The name of the zone associated with this request.
         /// </summary>
         /// <param name="value">Value of the Zone input for this Choreo.</param>
         public void setZone(String value) {
             base.addInput ("Zone", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AddAccessConfigResultSet containing execution metadata and results.</returns>
        new public AddAccessConfigResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AddAccessConfigResultSet results = new JavaScriptSerializer().Deserialize<AddAccessConfigResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the AddAccessConfig Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AddAccessConfigResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) The response from Google.</returns>
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
