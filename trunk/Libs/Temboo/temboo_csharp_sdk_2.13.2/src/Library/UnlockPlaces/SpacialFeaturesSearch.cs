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

namespace Temboo.Library.UnlockPlaces
{
    /// <summary>
    /// SpacialFeaturesSearch
    /// Searches for feature types within a specified bounding box.
    /// </summary>
    public class SpacialFeaturesSearch : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the SpacialFeaturesSearch Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public SpacialFeaturesSearch(TembooSession session) : base(session, "/Library/UnlockPlaces/SpacialFeaturesSearch")
        {
        }

         /// <summary>
         /// (string) The feature type that the place is (i.e. "Cities"). See http://unlock.edina.ac.uk/ws/supportedFeatureTypes?format=txt for a complete list of supported Feature Types.
         /// </summary>
         /// <param name="value">Value of the FeatureType input for this Choreo.</param>
         public void setFeatureType(String value) {
             base.addInput ("FeatureType", value);
         }
         /// <summary>
         /// (optional, string) The format of the place search results. One of xml, kml, json, georss or txt. Defaults to "xml".
         /// </summary>
         /// <param name="value">Value of the Format input for this Choreo.</param>
         public void setFormat(String value) {
             base.addInput ("Format", value);
         }
         /// <summary>
         /// (optional, string) The place-name source to take locations from. The options are geonames, os, naturalearth or unlock which combines all the previous. Defaults to "unlock".
         /// </summary>
         /// <param name="value">Value of the Gazetteer input for this Choreo.</param>
         public void setGazetteer(String value) {
             base.addInput ("Gazetteer", value);
         }
         /// <summary>
         /// (decimal) The maximum latitude point of a bounding box.
         /// </summary>
         /// <param name="value">Value of the MaxLatitude input for this Choreo.</param>
         public void setMaxLatitude(String value) {
             base.addInput ("MaxLatitude", value);
         }
         /// <summary>
         /// (decimal) The maximum longitude point of a bounding box.
         /// </summary>
         /// <param name="value">Value of the MaxLongitude input for this Choreo.</param>
         public void setMaxLongitude(String value) {
             base.addInput ("MaxLongitude", value);
         }
         /// <summary>
         /// (optional, integer) The maximum number of results to return. Defaults to 20. Cannot exceed 1000.
         /// </summary>
         /// <param name="value">Value of the MaxRows input for this Choreo.</param>
         public void setMaxRows(String value) {
             base.addInput ("MaxRows", value);
         }
         /// <summary>
         /// (decimal) The minimum latitude point of a bounding box.
         /// </summary>
         /// <param name="value">Value of the MinLatitude input for this Choreo.</param>
         public void setMinLatitude(String value) {
             base.addInput ("MinLatitude", value);
         }
         /// <summary>
         /// (decimal) The minimum longitude point of a bounding box.
         /// </summary>
         /// <param name="value">Value of the MinLongitude input for this Choreo.</param>
         public void setMinLongitude(String value) {
             base.addInput ("MinLongitude", value);
         }
         /// <summary>
         /// Valid values are: "within" and "intersect". The results will therefore be entirely within, or overlapping with (intersecting), the bounding box. Defaults to "within".
         /// </summary>
         /// <param name="value">Value of the Operator input for this Choreo.</param>
         public void setOperator(String value) {
             base.addInput ("Operator", value);
         }
         /// <summary>
         /// (optional, integer) The row to start results display from. Defaults to 1.
         /// </summary>
         /// <param name="value">Value of the StartRow input for this Choreo.</param>
         public void setStartRow(String value) {
             base.addInput ("StartRow", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A SpacialFeaturesSearchResultSet containing execution metadata and results.</returns>
        new public SpacialFeaturesSearchResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            SpacialFeaturesSearchResultSet results = new JavaScriptSerializer().Deserialize<SpacialFeaturesSearchResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the SpacialFeaturesSearch Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class SpacialFeaturesSearchResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (XML) The response from Unlock. Defaults to XML based on the format input parameter.</returns>
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
