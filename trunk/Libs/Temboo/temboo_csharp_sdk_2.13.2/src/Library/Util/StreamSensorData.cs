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

namespace Temboo.Library.Util
{
    /// <summary>
    /// StreamSensorData
    /// Creates a new label.
    /// </summary>
    public class StreamSensorData : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the StreamSensorData Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public StreamSensorData(TembooSession session) : base(session, "/Library/Util/StreamSensorData")
        {
        }

         /// <summary>
         /// (optional, boolean) When set to "true" the request to the data service happens asyncronously. Set to "false" if you want the Choreo to wait for the execution to complete and return API's response.
         /// </summary>
         /// <param name="value">Value of the Async input for this Choreo.</param>
         public void setAsync(String value) {
             base.addInput ("Async", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by the data service.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by the data service.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (required, string) The ID of the dataset that your table belongs to.
         /// </summary>
         /// <param name="value">Value of the DatasetID input for this Choreo.</param>
         public void setDatasetID(String value) {
             base.addInput ("DatasetID", value);
         }
         /// <summary>
         /// (required, string) The ID of your Google API project.
         /// </summary>
         /// <param name="value">Value of the ProjectID input for this Choreo.</param>
         public void setProjectID(String value) {
             base.addInput ("ProjectID", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new Access Token when the original token is expired.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (required, json) A JSON object containing key/value pairs.
         /// </summary>
         /// <param name="value">Value of the SensorData input for this Choreo.</param>
         public void setSensorData(String value) {
             base.addInput ("SensorData", value);
         }
         /// <summary>
         /// (required, string) Indicates the service to stream to. Valid values are: BigQuery or Power BI
         /// </summary>
         /// <param name="value">Value of the Service input for this Choreo.</param>
         public void setService(String value) {
             base.addInput ("Service", value);
         }
         /// <summary>
         /// (required, string) The ID (or name) of the table to insert a row into.
         /// </summary>
         /// <param name="value">Value of the TableID input for this Choreo.</param>
         public void setTableID(String value) {
             base.addInput ("TableID", value);
         }
         /// <summary>
         /// (optional, string) The name of the column that that the choreo will auto-generate a timestamp for.
         /// </summary>
         /// <param name="value">Value of the TimestampColumn input for this Choreo.</param>
         public void setTimestampColumn(String value) {
             base.addInput ("TimestampColumn", value);
         }
         /// <summary>
         /// (optional, string) The format of the auto generated timestamp (e.g. yyyy-MM-dd HH:mm:ss.SSS). If set to "milliseconds" or "seconds", the timestamp will be an epoch date.
         /// </summary>
         /// <param name="value">Value of the TimestampFormat input for this Choreo.</param>
         public void setTimestampFormat(String value) {
             base.addInput ("TimestampFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A StreamSensorDataResultSet containing execution metadata and results.</returns>
        new public StreamSensorDataResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            StreamSensorDataResultSet results = new JavaScriptSerializer().Deserialize<StreamSensorDataResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the StreamSensorData Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class StreamSensorDataResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (json) Contains the response from Google when using the Async=fase option.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ResponseStatusCode" output from this Choreo execution
        /// <returns>String - (integer) The response status code from the API.</returns>
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
