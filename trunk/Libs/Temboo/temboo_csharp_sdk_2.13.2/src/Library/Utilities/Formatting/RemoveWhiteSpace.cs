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

namespace Temboo.Library.Utilities.Formatting
{
    /// <summary>
    /// RemoveWhiteSpace
    /// Returns the specified formatted text as a compact string with no new lines, tabs, or preceding/trailing white space.
    /// </summary>
    public class RemoveWhiteSpace : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the RemoveWhiteSpace Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public RemoveWhiteSpace(TembooSession session) : base(session, "/Library/Utilities/Formatting/RemoveWhiteSpace")
        {
        }

         /// <summary>
         /// (required, multiline) The formatted text that should have line breaks and tabs removed.
         /// </summary>
         /// <param name="value">Value of the FormattedText input for this Choreo.</param>
         public void setFormattedText(String value) {
             base.addInput ("FormattedText", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RemoveWhiteSpaceResultSet containing execution metadata and results.</returns>
        new public RemoveWhiteSpaceResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RemoveWhiteSpaceResultSet results = new JavaScriptSerializer().Deserialize<RemoveWhiteSpaceResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the RemoveWhiteSpace Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RemoveWhiteSpaceResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "CompactText" output from this Choreo execution
        /// <returns>String - (string) </returns>
        /// </summary>
        public String CompactText
        {
            get
            {
                return (String) base.Output["CompactText"];
            }
        }
    }
}
