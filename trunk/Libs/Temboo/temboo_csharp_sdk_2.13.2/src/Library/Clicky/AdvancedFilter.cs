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

namespace Temboo.Library.Clicky
{
    /// <summary>
    /// AdvancedFilter
    /// Allows you to retrieve analytics, using more advanced filter options.
    /// </summary>
    public class AdvancedFilter : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the AdvancedFilter Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public AdvancedFilter(TembooSession session) : base(session, "/Library/Clicky/AdvancedFilter")
        {
        }

         /// <summary>
         /// (optional, string) The date or date range you want to access. Use YYYY-MM-DD format for date and YYYY-MM-DD,YYYY-MM-DD for a range. See docs for more options for this input. Defaults to 'today'.
         /// </summary>
         /// <param name="value">Value of the Date input for this Choreo.</param>
         public void setDate(String value) {
             base.addInput ("Date", value);
         }
         /// <summary>
         /// (required, string) The name of the data you want to filter by (i.e. ip_address). See docs for a complete list of supported filters.
         /// </summary>
         /// <param name="value">Value of the FilterName input for this Choreo.</param>
         public void setFilterName(String value) {
             base.addInput ("FilterName", value);
         }
         /// <summary>
         /// (required, string) The value of the filter you want to apply to the request. For example, if your FilterName is "ip_address", you could use "65.0.0.0,85.0.0.0" in the FilterValue.
         /// </summary>
         /// <param name="value">Value of the FilterValue input for this Choreo.</param>
         public void setFilterValue(String value) {
             base.addInput ("FilterValue", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of results that will be returned. Defaults to 10.
         /// </summary>
         /// <param name="value">Value of the Limit input for this Choreo.</param>
         public void setLimit(String value) {
             base.addInput ("Limit", value);
         }
         /// <summary>
         /// (optional, string) What format you want the returned data to be in. Accepted values: xml, php, json, csv. Defaults to 'xml'.
         /// </summary>
         /// <param name="value">Value of the Output input for this Choreo.</param>
         public void setOutput(String value) {
             base.addInput ("Output", value);
         }
         /// <summary>
         /// (required, integer) Your request must include the site's ID that you want to access data from. Available from your site preferences page.
         /// </summary>
         /// <param name="value">Value of the SiteID input for this Choreo.</param>
         public void setSiteID(String value) {
             base.addInput ("SiteID", value);
         }
         /// <summary>
         /// (required, string) The unique key assigned to you when you first register with Clicky. Available from your site preferences page.
         /// </summary>
         /// <param name="value">Value of the SiteKey input for this Choreo.</param>
         public void setSiteKey(String value) {
             base.addInput ("SiteKey", value);
         }
         /// <summary>
         /// (required, string) The type of data you want to retrieve. Note that not all types are available for this Choreo. Use types: visitors-list, segmentation, or actions-list.
         /// </summary>
         /// <param name="value">Value of the Type input for this Choreo.</param>
         public void setType(String value) {
             base.addInput ("Type", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AdvancedFilterResultSet containing execution metadata and results.</returns>
        new public AdvancedFilterResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AdvancedFilterResultSet results = new JavaScriptSerializer().Deserialize<AdvancedFilterResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the AdvancedFilter Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AdvancedFilterResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Clicky formatted as specified in the Output parameter. Default is XML.</returns>
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
