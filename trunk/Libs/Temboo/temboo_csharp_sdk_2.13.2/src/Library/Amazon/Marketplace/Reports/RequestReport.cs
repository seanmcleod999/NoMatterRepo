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

namespace Temboo.Library.Amazon.Marketplace.Reports
{
    /// <summary>
    /// RequestReport
    /// Creates a report request and submits the request to Amazon MWS.
    /// </summary>
    public class RequestReport : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the RequestReport Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public RequestReport(TembooSession session) : base(session, "/Library/Amazon/Marketplace/Reports/RequestReport")
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
         /// (required, string) The Marketplace ID provided by Amazon Web Services.
         /// </summary>
         /// <param name="value">Value of the AWSMarketplaceId input for this Choreo.</param>
         public void setAWSMarketplaceId(String value) {
             base.addInput ("AWSMarketplaceId", value);
         }
         /// <summary>
         /// (required, string) The Merchant ID provided by Amazon Web Services.
         /// </summary>
         /// <param name="value">Value of the AWSMerchantId input for this Choreo.</param>
         public void setAWSMerchantId(String value) {
             base.addInput ("AWSMerchantId", value);
         }
         /// <summary>
         /// (required, string) The Secret Key ID provided by Amazon Web Services.
         /// </summary>
         /// <param name="value">Value of the AWSSecretKeyId input for this Choreo.</param>
         public void setAWSSecretKeyId(String value) {
             base.addInput ("AWSSecretKeyId", value);
         }
         /// <summary>
         /// (optional, date) The end of a date range used for selecting the data to report, in ISO8601 date format (i.e. 2012-01-01).
         /// </summary>
         /// <param name="value">Value of the EndDate input for this Choreo.</param>
         public void setEndDate(String value) {
             base.addInput ("EndDate", value);
         }
         /// <summary>
         /// (conditional, string) The base URL for the MWS endpoint. Defaults to mws.amazonservices.co.uk.
         /// </summary>
         /// <param name="value">Value of the Endpoint input for this Choreo.</param>
         public void setEndpoint(String value) {
             base.addInput ("Endpoint", value);
         }
         /// <summary>
         /// (optional, string) The Amazon MWS authorization token for the given seller and developer.
         /// </summary>
         /// <param name="value">Value of the MWSAuthToken input for this Choreo.</param>
         public void setMWSAuthToken(String value) {
             base.addInput ("MWSAuthToken", value);
         }
         /// <summary>
         /// (optional, string) A Boolean value that shows or hides an additional column of information on several order reports. When set to ShowSalesChannel=true, an additional column is added showing the sales channel.
         /// </summary>
         /// <param name="value">Value of the ReportOptions input for this Choreo.</param>
         public void setReportOptions(String value) {
             base.addInput ("ReportOptions", value);
         }
         /// <summary>
         /// (optional, string) A ReportType enumeration value. Defaults to _GET_FLAT_FILE_OPEN_LISTINGS_DATA_.
         /// </summary>
         /// <param name="value">Value of the ReportType input for this Choreo.</param>
         public void setReportType(String value) {
             base.addInput ("ReportType", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are "xml" (the default) and "json".
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, date) The start of a date range used for selecting the data to report, in ISO8601 date format (i.e. 2012-01-01).
         /// </summary>
         /// <param name="value">Value of the StartDate input for this Choreo.</param>
         public void setStartDate(String value) {
             base.addInput ("StartDate", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RequestReportResultSet containing execution metadata and results.</returns>
        new public RequestReportResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RequestReportResultSet results = new JavaScriptSerializer().Deserialize<RequestReportResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the RequestReport Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RequestReportResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - Stores the response from Amazon.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ReportProcessingStatus" output from this Choreo execution
        /// <returns>String - (string) The status of the report request parsed from the Amazon response.</returns>
        /// </summary>
        public String ReportProcessingStatus
        {
            get
            {
                return (String) base.Output["ReportProcessingStatus"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ReportRequestId" output from this Choreo execution
        /// <returns>String - (integer) The ReportRequestId parsed from the Amazon response. This id is used in GetReportRequestList.</returns>
        /// </summary>
        public String ReportRequestId
        {
            get
            {
                return (String) base.Output["ReportRequestId"];
            }
        }
    }
}
