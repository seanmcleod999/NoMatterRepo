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

namespace Temboo.Library.Amazon.IAM
{
    /// <summary>
    /// ListVirtualMFADevices
    /// Lists the virtual MFA devices under the AWS account.
    /// </summary>
    public class ListVirtualMFADevices : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ListVirtualMFADevices Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ListVirtualMFADevices(TembooSession session) : base(session, "/Library/Amazon/IAM/ListVirtualMFADevices")
        {
        }

         /// <summary>
         /// (required, string) The Access Key ID provided by Amazon Web Services.
         /// </summary>
         /// <param name="value">Value of the AWSAccessKeyId input for this Choreo.</param>
         public void setAWSAccessKeyId(String value) {
             base.addInput ("AWSAccessKeyId", value);
         }
         /// <summary>
         /// (required, string) The Secret Key ID provided by Amazon Web Services.
         /// </summary>
         /// <param name="value">Value of the AWSSecretKeyId input for this Choreo.</param>
         public void setAWSSecretKeyId(String value) {
             base.addInput ("AWSSecretKeyId", value);
         }
         /// <summary>
         /// (optional, string) Filters by the whether the device is assigned or unassigned to a specific user. Valid values: "Unassigned", "Assigned" or "Any" (default - both assigned and unassigned devices).
         /// </summary>
         /// <param name="value">Value of the AssignmentStatus input for this Choreo.</param>
         public void setAssignmentStatus(String value) {
             base.addInput ("AssignmentStatus", value);
         }
         /// <summary>
         /// (optional, string) Used for pagination to indicate the starting point of the results to return.
         /// </summary>
         /// <param name="value">Value of the Marker input for this Choreo.</param>
         public void setMarker(String value) {
             base.addInput ("Marker", value);
         }
         /// <summary>
         /// (optional, integer) Used for pagination to limit the number of results returned. Defaults to 100.
         /// </summary>
         /// <param name="value">Value of the MaxItems input for this Choreo.</param>
         public void setMaxItems(String value) {
             base.addInput ("MaxItems", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are "xml" (the default) and "json".
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ListVirtualMFADevicesResultSet containing execution metadata and results.</returns>
        new public ListVirtualMFADevicesResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ListVirtualMFADevicesResultSet results = new JavaScriptSerializer().Deserialize<ListVirtualMFADevicesResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ListVirtualMFADevices Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ListVirtualMFADevicesResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Amazon.</returns>
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
