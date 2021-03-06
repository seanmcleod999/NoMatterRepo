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

namespace Temboo.Library.Xively.Devices
{
    /// <summary>
    /// UpdateDevice
    /// Updates a single device's serial number. 
    /// </summary>
    public class UpdateDevice : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the UpdateDevice Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public UpdateDevice(TembooSession session) : base(session, "/Library/Xively/Devices/UpdateDevice")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Xively.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, string) The new serial number you want to update the existing device to. Allowed characters: case-sensitive alphanumeric characters (Ab3) and certain characters: "+", "-", "_", and ":". Ex: abc:123,aBc-124.
         /// </summary>
         /// <param name="value">Value of the NewSerialNumber input for this Choreo.</param>
         public void setNewSerialNumber(String value) {
             base.addInput ("NewSerialNumber", value);
         }
         /// <summary>
         /// (required, string) The product ID for the device you would like to update.
         /// </summary>
         /// <param name="value">Value of the ProductID input for this Choreo.</param>
         public void setProductID(String value) {
             base.addInput ("ProductID", value);
         }
         /// <summary>
         /// (required, string) The existing serial number for the device you would like to update.
         /// </summary>
         /// <param name="value">Value of the SerialNumber input for this Choreo.</param>
         public void setSerialNumber(String value) {
             base.addInput ("SerialNumber", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A UpdateDeviceResultSet containing execution metadata and results.</returns>
        new public UpdateDeviceResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            UpdateDeviceResultSet results = new JavaScriptSerializer().Deserialize<UpdateDeviceResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the UpdateDevice Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class UpdateDeviceResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "ResponseStatusCode" output from this Choreo execution
        /// <returns>String - (integer) The response status code returned from Xively. For a successful device update, the code should be 200.</returns>
        /// </summary>
        public String ResponseStatusCode
        {
            get
            {
                return (String) base.Output["ResponseStatusCode"];
            }
        }
    }
}
