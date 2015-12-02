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
    /// GetReportRequestList
    /// Returns a list of report requests that you can use to get the ReportProcessingStatus and ReportId in order to retrieve a report.
    /// </summary>
    public class GetReportRequestList : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetReportRequestList Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetReportRequestList(TembooSession session) : base(session, "/Library/Amazon/Marketplace/Reports/GetReportRequestList")
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
         /// (optional, integer) A non-negative integer that represents the maximum number of report requests to return. Defaults to 10. Max is 100.
         /// </summary>
         /// <param name="value">Value of the MaxCount input for this Choreo.</param>
         public void setMaxCount(String value) {
             base.addInput ("MaxCount", value);
         }
         /// <summary>
         /// (optional, string) A comma separated list of up to three ReportProcessingStatuses by which to filter report requests.
         /// </summary>
         /// <param name="value">Value of the ReportProcessingStatusList input for this Choreo.</param>
         public void setReportProcessingStatusList(String value) {
             base.addInput ("ReportProcessingStatusList", value);
         }
         /// <summary>
         /// (optional, string) A comma separated list of up to three ReportRequestId values. If you pass in a ReportRequestId values, other query conditions are ignored.
         /// </summary>
         /// <param name="value">Value of the ReportRequestIdList input for this Choreo.</param>
         public void setReportRequestIdList(String value) {
             base.addInput ("ReportRequestIdList", value);
         }
         /// <summary>
         /// (optional, string) A comma separated list of up to three ReportType enumeration values.
         /// </summary>
         /// <param name="value">Value of the ReportTypeList input for this Choreo.</param>
         public void setReportTypeList(String value) {
             base.addInput ("ReportTypeList", value);
         }
         /// <summary>
         /// (optional, date) The start of the date range used for selecting the data to report, in ISO8601 date format (i.e. 2012-01-01).
         /// </summary>
         /// <param name="value">Value of the RequestedFromDate input for this Choreo.</param>
         public void setRequestedFromDate(String value) {
             base.addInput ("RequestedFromDate", value);
         }
         /// <summary>
         /// (optional, date) The end of the date range used for selecting the data to report, in ISO8601 date format (i.e. 2012-01-01).
         /// </summary>
         /// <param name="value">Value of the RequestedToDate input for this Choreo.</param>
         public void setRequestedToDate(String value) {
             base.addInput ("RequestedToDate", value);
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
        /// <returns>A GetReportRequestListResultSet containing execution metadata and results.</returns>
        new public GetReportRequestListResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetReportRequestListResultSet results = new JavaScriptSerializer().Deserialize<GetReportRequestListResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetReportRequestList Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetReportRequestListResultSet : Temboo.Core.ResultSet
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
        /// Retrieve the value for the "GeneratedReportId" output from this Choreo execution
        /// <returns>String - (integer) The GeneratedReportId parsed from the Amazon response. If multiple records are returned, this output variable will contain the first id in the list.</returns>
        /// </summary>
        public String GeneratedReportId
        {
            get
            {
                return (String) base.Output["GeneratedReportId"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ReportProcessingStatus" output from this Choreo execution
        /// <returns>String - (string) The report status parsed from the Amazon response. If multiple records are returned, this output variable will contain the report status in the list.</returns>
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
        /// <returns>String - (integer) The report request id parsed from the Amazon response. If multiple records are returned, this output variable will contain the first id in the list.</returns>
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
