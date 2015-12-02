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

namespace Temboo.Library.FedSpending
{
    /// <summary>
    /// Assistance
    /// Allows access to the information in the Federal Assisatance Award Data System (FAADS) database, which reports all financial assistance made by federal agencies.
    /// </summary>
    public class Assistance : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the Assistance Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public Assistance(TembooSession session) : base(session, "/Library/FedSpending/Assistance")
        {
        }

         /// <summary>
         /// (conditional, string) The 4-character code for a specific governmental agency providing assistance.
         /// </summary>
         /// <param name="value">Value of the AgencyCode input for this Choreo.</param>
         public void setAgencyCode(String value) {
             base.addInput ("AgencyCode", value);
         }
         /// <summary>
         /// (conditional, string) The type of assistance provided. Valid values are: d = Direct Payments (specified and unrestricted), g = Grants and Cooperative Agreements, i = Insurance, l = Loans (direct and guaranteed), o = Other.
         /// </summary>
         /// <param name="value">Value of the AssistanceType input for this Choreo.</param>
         public void setAssistanceType(String value) {
             base.addInput ("AssistanceType", value);
         }
         /// <summary>
         /// (conditional, string) An ID for the governmental program.
         /// </summary>
         /// <param name="value">Value of the CFDAProgram input for this Choreo.</param>
         public void setCFDAProgram(String value) {
             base.addInput ("CFDAProgram", value);
         }
         /// <summary>
         /// (optional, string) Controls the level of detail of the output. Acceptable values: -1 (summary), 0 (low), 1 (medium), 2 (high), and 3 (extensive). Defaults to -1. See docs for more information.
         /// </summary>
         /// <param name="value">Value of the Detail input for this Choreo.</param>
         public void setDetail(String value) {
             base.addInput ("Detail", value);
         }
         /// <summary>
         /// (conditional, string) A Federal ID for the award.
         /// </summary>
         /// <param name="value">Value of the FederalID input for this Choreo.</param>
         public void setFederalID(String value) {
             base.addInput ("FederalID", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the first year in a range of years from 2000-2006; if used, must be used with LastYearRange and without FiscalYear.
         /// </summary>
         /// <param name="value">Value of the FirstYearRange input for this Choreo.</param>
         public void setFirstYearRange(String value) {
             base.addInput ("FirstYearRange", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies a single year from 2000-2006; defaults to all years.
         /// </summary>
         /// <param name="value">Value of the FiscalYear input for this Choreo.</param>
         public void setFiscalYear(String value) {
             base.addInput ("FiscalYear", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the last year in a range of years from 2000-2006; if used, must be used with FirstYearRange and without FiscalYear.
         /// </summary>
         /// <param name="value">Value of the LastYearRange input for this Choreo.</param>
         public void setLastYearRange(String value) {
             base.addInput ("LastYearRange", value);
         }
         /// <summary>
         /// (conditional, string) The 2-character code for a major governmental agency providing assistance.
         /// </summary>
         /// <param name="value">Value of the MajAgency input for this Choreo.</param>
         public void setMajAgency(String value) {
             base.addInput ("MajAgency", value);
         }
         /// <summary>
         /// (optional, integer) Allows you to set the maximum number of records retrieved. Defaults to 100.
         /// </summary>
         /// <param name="value">Value of the MaxRecords input for this Choreo.</param>
         public void setMaxRecords(String value) {
             base.addInput ("MaxRecords", value);
         }
         /// <summary>
         /// (conditional, string) The city or county of the place of performance.
         /// </summary>
         /// <param name="value">Value of the PrincipalPlaceCC input for this Choreo.</param>
         public void setPrincipalPlaceCC(String value) {
             base.addInput ("PrincipalPlaceCC", value);
         }
         /// <summary>
         /// (conditional, string) The FIPS state code for the state of the place of performance.
         /// </summary>
         /// <param name="value">Value of the PrincipalPlaceStateCode input for this Choreo.</param>
         public void setPrincipalPlaceStateCode(String value) {
             base.addInput ("PrincipalPlaceStateCode", value);
         }
         /// <summary>
         /// (conditional, string) The city in the address of a recipient.
         /// </summary>
         /// <param name="value">Value of the RecipientCityName input for this Choreo.</param>
         public void setRecipientCityName(String value) {
             base.addInput ("RecipientCityName", value);
         }
         /// <summary>
         /// (conditional, string) The county in which a recipient is located.
         /// </summary>
         /// <param name="value">Value of the RecipientCountyName input for this Choreo.</param>
         public void setRecipientCountyName(String value) {
             base.addInput ("RecipientCountyName", value);
         }
         /// <summary>
         /// (conditional, string) The Congressional District in which the recipient is located, formatted with four characters.
         /// </summary>
         /// <param name="value">Value of the RecipientDistrict input for this Choreo.</param>
         public void setRecipientDistrict(String value) {
             base.addInput ("RecipientDistrict", value);
         }
         /// <summary>
         /// (conditional, string) The name of a recipient of assistance.
         /// </summary>
         /// <param name="value">Value of the RecipientName input for this Choreo.</param>
         public void setRecipientName(String value) {
             base.addInput ("RecipientName", value);
         }
         /// <summary>
         /// (conditional, string) The FIPS state code for the state in the address of a recipient.
         /// </summary>
         /// <param name="value">Value of the RecipientStateCode input for this Choreo.</param>
         public void setRecipientStateCode(String value) {
             base.addInput ("RecipientStateCode", value);
         }
         /// <summary>
         /// (conditional, string) The type of recipient. Valid values are: f = For Profits, g = Government,h = Higher Education, i = Individuals,n = Nonprofits, o = Other.
         /// </summary>
         /// <param name="value">Value of the RecipientType input for this Choreo.</param>
         public void setRecipientType(String value) {
             base.addInput ("RecipientType", value);
         }
         /// <summary>
         /// (conditional, integer) The ZIP code in the address of a recipient.
         /// </summary>
         /// <param name="value">Value of the RecipientZip input for this Choreo.</param>
         public void setRecipientZip(String value) {
             base.addInput ("RecipientZip", value);
         }
         /// <summary>
         /// (optional, string) Determines how records are sorted. Valid values: r (contractor/recipient name), f (dollars of awards),g (major contracting agency), p (CFDA Program), d (date of award). Defaults to f.
         /// </summary>
         /// <param name="value">Value of the SortBy input for this Choreo.</param>
         public void setSortBy(String value) {
             base.addInput ("SortBy", value);
         }
         /// <summary>
         /// (conditional, string) A free text search on a description of the project.
         /// </summary>
         /// <param name="value">Value of the TextSearch input for this Choreo.</param>
         public void setTextSearch(String value) {
             base.addInput ("TextSearch", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AssistanceResultSet containing execution metadata and results.</returns>
        new public AssistanceResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AssistanceResultSet results = new JavaScriptSerializer().Deserialize<AssistanceResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the Assistance Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AssistanceResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from FedSpending.org.</returns>
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
