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

namespace Temboo.Library.eBay.Finding
{
    /// <summary>
    /// FindItemsByProduct
    /// Finds items based upon a product ID, such as an ISBN, UPC, EAN, or ePID.
    /// </summary>
    public class FindItemsByProduct : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the FindItemsByProduct Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public FindItemsByProduct(TembooSession session) : base(session, "/Library/eBay/Finding/FindItemsByProduct")
        {
        }

         /// <summary>
         /// (optional, xml) The complete XML request body containing properties you wish to set. This can be used as an alternative to individual inputs that represent request properties.
         /// </summary>
         /// <param name="value">Value of the FindItemsByProductRequest input for this Choreo.</param>
         public void setFindItemsByProductRequest(String value) {
             base.addInput ("FindItemsByProductRequest", value);
         }
         /// <summary>
         /// (required, string) The unique identifier for the application.
         /// </summary>
         /// <param name="value">Value of the AppID input for this Choreo.</param>
         public void setAppID(String value) {
             base.addInput ("AppID", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of records to return in the result.
         /// </summary>
         /// <param name="value">Value of the EntriesPerPage input for this Choreo.</param>
         public void setEntriesPerPage(String value) {
             base.addInput ("EntriesPerPage", value);
         }
         /// <summary>
         /// (optional, integer) The global ID of the eBay site to access (e.g., EBAY-US).
         /// </summary>
         /// <param name="value">Value of the GlobalID input for this Choreo.</param>
         public void setGlobalID(String value) {
             base.addInput ("GlobalID", value);
         }
         /// <summary>
         /// (optional, json) A dictionary of key/value pairs to use as item filters for the request.
         /// </summary>
         /// <param name="value">Value of the ItemFilters input for this Choreo.</param>
         public void setItemFilters(String value) {
             base.addInput ("ItemFilters", value);
         }
         /// <summary>
         /// (optional, string) Controls the fields that are returned in the response (e.g., GalleryInfo).
         /// </summary>
         /// <param name="value">Value of the OutputSelector input for this Choreo.</param>
         public void setOutputSelector(String value) {
             base.addInput ("OutputSelector", value);
         }
         /// <summary>
         /// (optional, integer) Specifies the page number of the results to return.
         /// </summary>
         /// <param name="value">Value of the PageNumber input for this Choreo.</param>
         public void setPageNumber(String value) {
             base.addInput ("PageNumber", value);
         }
         /// <summary>
         /// (required, string) The type of identifier being used to find a product. Valid values are: ReferenceID, ISBN, UPC, and EAN.
         /// </summary>
         /// <param name="value">Value of the ProductIDType input for this Choreo.</param>
         public void setProductIDType(String value) {
             base.addInput ("ProductIDType", value);
         }
         /// <summary>
         /// (required, string) The ID of a product to find.
         /// </summary>
         /// <param name="value">Value of the ProductID input for this Choreo.</param>
         public void setProductID(String value) {
             base.addInput ("ProductID", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: json (the default) and xml.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (conditional, boolean) Indicates that the request should be made to the sandbox endpoint instead of the production endpoint. Set to 1 to enable sandbox mode.
         /// </summary>
         /// <param name="value">Value of the SandboxMode input for this Choreo.</param>
         public void setSandboxMode(String value) {
             base.addInput ("SandboxMode", value);
         }
         /// <summary>
         /// (optional, string) Valid values: BestMatch, BidCountMost, CountryAscending, CountryDescending, DistanceNearest, CurrentPriceHighest, EndTimeSoonest, PricePlusShippingHighest, PricePlusShippingLowest, StartTimeNewest.
         /// </summary>
         /// <param name="value">Value of the SortOrder input for this Choreo.</param>
         public void setSortOrder(String value) {
             base.addInput ("SortOrder", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A FindItemsByProductResultSet containing execution metadata and results.</returns>
        new public FindItemsByProductResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            FindItemsByProductResultSet results = new JavaScriptSerializer().Deserialize<FindItemsByProductResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the FindItemsByProduct Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class FindItemsByProductResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from eBay.</returns>
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
