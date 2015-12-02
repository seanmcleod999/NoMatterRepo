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
    /// Contracts
    /// Allows access to the information in the Federal Procurement Data System (FPDS) database, which reports all federal contracts awarded. 
    /// </summary>
    public class Contracts : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the Contracts Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public Contracts(TembooSession session) : base(session, "/Library/FedSpending/Contracts")
        {
        }

         /// <summary>
         /// (conditional, string) The city within a contractor's address.
         /// </summary>
         /// <param name="value">Value of the City input for this Choreo.</param>
         public void setCity(String value) {
             base.addInput ("City", value);
         }
         /// <summary>
         /// (conditional, string) The name of a a contractor or contractor parent company.
         /// </summary>
         /// <param name="value">Value of the CompanyName input for this Choreo.</param>
         public void setCompanyName(String value) {
             base.addInput ("CompanyName", value);
         }
         /// <summary>
         /// (conditional, string) The competition status of a contract. Valid values: c=Full competition, o=Full competition, one bid, p=Competition, exclusion of sources, n=Not complete, a=Not available, f=Follow-up, u=Unknown.
         /// </summary>
         /// <param name="value">Value of the Completion input for this Choreo.</param>
         public void setCompletion(String value) {
             base.addInput ("Completion", value);
         }
         /// <summary>
         /// (optional, string) Controls the level of detail of the output. Acceptable values: -1 (summary), 0 (low), 1 (medium), 2 (high), and 3 (extensive). Defaults to -1. See docs for more information.
         /// </summary>
         /// <param name="value">Value of the Detail input for this Choreo.</param>
         public void setDetail(String value) {
             base.addInput ("Detail", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the first year in a range of years; if used, must be used with LastYearRange and without FiscalYear.
         /// </summary>
         /// <param name="value">Value of the FirstYearRange input for this Choreo.</param>
         public void setFirstYearRange(String value) {
             base.addInput ("FirstYearRange", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies a single year; defaults to all years.
         /// </summary>
         /// <param name="value">Value of the FiscalYear input for this Choreo.</param>
         public void setFiscalYear(String value) {
             base.addInput ("FiscalYear", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the last year in a range of years; if used, must be used with FirstYearRange and without FiscalYear.
         /// </summary>
         /// <param name="value">Value of the LastYearRange input for this Choreo.</param>
         public void setLastYearRange(String value) {
             base.addInput ("LastYearRange", value);
         }
         /// <summary>
         /// (conditional, string) The 2-character code for a major governmental agency issuing contracts.
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
         /// (conditional, string) The 4-digit code for a specific governmental agency issuing contracts.
         /// </summary>
         /// <param name="value">Value of the ModAgency input for this Choreo.</param>
         public void setModAgency(String value) {
             base.addInput ("ModAgency", value);
         }
         /// <summary>
         /// (conditional, integer) A Federal ID number for the contract.
         /// </summary>
         /// <param name="value">Value of the PIID input for this Choreo.</param>
         public void setPIID(String value) {
             base.addInput ("PIID", value);
         }
         /// <summary>
         /// (conditional, string) The 2-character code for a major product or service category.
         /// </summary>
         /// <param name="value">Value of the PSCCategory input for this Choreo.</param>
         public void setPSCCategory(String value) {
             base.addInput ("PSCCategory", value);
         }
         /// <summary>
         /// (conditional, string) The 4-character code for a product or service.
         /// </summary>
         /// <param name="value">Value of the PSC input for this Choreo.</param>
         public void setPSC(String value) {
             base.addInput ("PSC", value);
         }
         /// <summary>
         /// (conditional, string) The two-letter country code for the place of performance country.
         /// </summary>
         /// <param name="value">Value of the PopCountryCode input for this Choreo.</param>
         public void setPopCountryCode(String value) {
             base.addInput ("PopCountryCode", value);
         }
         /// <summary>
         /// (conditional, string) The Congressional District of the place of performance.
         /// </summary>
         /// <param name="value">Value of the PopDistrict input for this Choreo.</param>
         public void setPopDistrict(String value) {
             base.addInput ("PopDistrict", value);
         }
         /// <summary>
         /// (conditional, integer) The ZIP code of the place of performance.
         /// </summary>
         /// <param name="value">Value of the PopZipCode input for this Choreo.</param>
         public void setPopZipCode(String value) {
             base.addInput ("PopZipCode", value);
         }
         /// <summary>
         /// (optional, string) Determines how records are sorted. Valid values: r (contractor/recipient name), f (dollars of awards),g (major contracting agency),p (Product or Service Category),d (date of award). Defaults to f.
         /// </summary>
         /// <param name="value">Value of the SortBy input for this Choreo.</param>
         public void setSortBy(String value) {
             base.addInput ("SortBy", value);
         }
         /// <summary>
         /// (conditional, string) The state abbreviation of the state of the place of performance.
         /// </summary>
         /// <param name="value">Value of the StateCode input for this Choreo.</param>
         public void setStateCode(String value) {
             base.addInput ("StateCode", value);
         }
         /// <summary>
         /// (conditional, string) The state abbreviation within a contractor's address.
         /// </summary>
         /// <param name="value">Value of the State input for this Choreo.</param>
         public void setState(String value) {
             base.addInput ("State", value);
         }
         /// <summary>
         /// (conditional, string) Free text search within the text that describes what the contract is for.
         /// </summary>
         /// <param name="value">Value of the TextSearch input for this Choreo.</param>
         public void setTextSearch(String value) {
             base.addInput ("TextSearch", value);
         }
         /// <summary>
         /// (conditional, string) The two-letter country code for the country in a contractor's address.
         /// </summary>
         /// <param name="value">Value of the VendorCountryCode input for this Choreo.</param>
         public void setVendorCountryCode(String value) {
             base.addInput ("VendorCountryCode", value);
         }
         /// <summary>
         /// (conditional, string) The 4-character Congressional District within which a contractor is located.
         /// </summary>
         /// <param name="value">Value of the VendorDistrict input for this Choreo.</param>
         public void setVendorDistrict(String value) {
             base.addInput ("VendorDistrict", value);
         }
         /// <summary>
         /// (conditional, integer) The ZIP code within a contractor's address.
         /// </summary>
         /// <param name="value">Value of the ZipCode input for this Choreo.</param>
         public void setZipCode(String value) {
             base.addInput ("ZipCode", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ContractsResultSet containing execution metadata and results.</returns>
        new public ContractsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ContractsResultSet results = new JavaScriptSerializer().Deserialize<ContractsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the Contracts Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ContractsResultSet : Temboo.Core.ResultSet
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
