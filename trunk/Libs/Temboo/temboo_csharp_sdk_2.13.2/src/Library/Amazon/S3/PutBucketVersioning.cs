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

namespace Temboo.Library.Amazon.S3
{
    /// <summary>
    /// PutBucketVersioning
    /// Sets the versioning state of an existing bucket.
    /// </summary>
    public class PutBucketVersioning : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the PutBucketVersioning Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public PutBucketVersioning(TembooSession session) : base(session, "/Library/Amazon/S3/PutBucketVersioning")
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
         /// (required, string) The name of the bucket to create a notification for.
         /// </summary>
         /// <param name="value">Value of the BucketName input for this Choreo.</param>
         public void setBucketName(String value) {
             base.addInput ("BucketName", value);
         }
         /// <summary>
         /// (required, string) Indicates whether or not the bucket has versioning enabled. Valid values are: Enabled or Suspended (case-sensitive).
         /// </summary>
         /// <param name="value">Value of the Status input for this Choreo.</param>
         public void setStatus(String value) {
             base.addInput ("Status", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A PutBucketVersioningResultSet containing execution metadata and results.</returns>
        new public PutBucketVersioningResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            PutBucketVersioningResultSet results = new JavaScriptSerializer().Deserialize<PutBucketVersioningResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the PutBucketVersioning Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class PutBucketVersioningResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - Stores the response from Amazon. Note that for a successful execution, no content is returned and this output variable should be empty.</returns>
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
