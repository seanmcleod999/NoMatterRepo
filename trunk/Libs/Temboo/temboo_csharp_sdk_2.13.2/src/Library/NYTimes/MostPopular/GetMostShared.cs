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

namespace Temboo.Library.NYTimes.MostPopular
{
    /// <summary>
    /// GetMostShared
    /// Retrieves information for the blog posts and articles that are most frequently shared.
    /// </summary>
    public class GetMostShared : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetMostShared Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetMostShared(TembooSession session) : base(session, "/Library/NYTimes/MostPopular/GetMostShared")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by NY Times.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (optional, string) The first 20 results are shown by default. To page through the results, set Offset to the appropriate value.
         /// </summary>
         /// <param name="value">Value of the Offset input for this Choreo.</param>
         public void setOffset(String value) {
             base.addInput ("Offset", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: json (the default) and xml.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (required, string) Limits the results by one or more sections (i.e. arts).  To get all sections, specify all-sections.
         /// </summary>
         /// <param name="value">Value of the Section input for this Choreo.</param>
         public void setSection(String value) {
             base.addInput ("Section", value);
         }
         /// <summary>
         /// (required, string) Limits the results by the method used to share the items.  Separate multiple share types by semicolons (i.e. facebook; twitter).
         /// </summary>
         /// <param name="value">Value of the ShareTypes input for this Choreo.</param>
         public void setShareTypes(String value) {
             base.addInput ("ShareTypes", value);
         }
         /// <summary>
         /// (required, integer) Allowed integer values: 1, 7, or 30, which corresponds to a day (1) , a week (7), or a month (30) of content.
         /// </summary>
         /// <param name="value">Value of the TimePeriod input for this Choreo.</param>
         public void setTimePeriod(String value) {
             base.addInput ("TimePeriod", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetMostSharedResultSet containing execution metadata and results.</returns>
        new public GetMostSharedResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetMostSharedResultSet results = new JavaScriptSerializer().Deserialize<GetMostSharedResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetMostShared Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetMostSharedResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from the NY Times API.</returns>
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
