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
    /// Recovery
    /// Allows access to the information in the Recovery Act Recipient Reports database.
    /// </summary>
    public class Recovery : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the Recovery Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public Recovery(TembooSession session) : base(session, "/Library/FedSpending/Recovery")
        {
        }

         /// <summary>
         /// (conditional, string) Whether or not to search by activity. (Will provide a select list if "y"). y = yes, n = no. Defaults to n if not set.
         /// </summary>
         /// <param name="value">Value of the Activity input for this Choreo.</param>
         public void setActivity(String value) {
             base.addInput ("Activity", value);
         }
         /// <summary>
         /// (conditional, string) Grants: total Federal dollars. Loans: face value of loan obligated by the Federal Agency. Contracts: total amount obligated by Federal Agency. Vendors: payment amount. Recipients:  amount of award.
         /// </summary>
         /// <param name="value">Value of the AwardAmount input for this Choreo.</param>
         public void setAwardAmount(String value) {
             base.addInput ("AwardAmount", value);
         }
         /// <summary>
         /// (conditional, integer) Identifying number assigned by the awarding Federal Agency. e.g. federal grant number, federal contract number or federal loan number. For grants and loans, this is assigned by the prime recipient.
         /// </summary>
         /// <param name="value">Value of the AwardNumber input for this Choreo.</param>
         public void setAwardNumber(String value) {
             base.addInput ("AwardNumber", value);
         }
         /// <summary>
         /// (conditional, string) Acceptable values: G = Grants only,L = Loans only, C = Contracts only.
         /// </summary>
         /// <param name="value">Value of the AwardType input for this Choreo.</param>
         public void setAwardType(String value) {
             base.addInput ("AwardType", value);
         }
         /// <summary>
         /// (conditional, string) The 4-digit code for a specific governmental awarding agency that awarded and is administering the award on behalf of the funding agency.
         /// </summary>
         /// <param name="value">Value of the AwardingAgency input for this Choreo.</param>
         public void setAwardingAgency(String value) {
             base.addInput ("AwardingAgency", value);
         }
         /// <summary>
         /// (conditional, string) The Catalog of Federal Domestic Number is the number associated with the published description of a Federal Assistance program in the CFDA.
         /// </summary>
         /// <param name="value">Value of the CFDA input for this Choreo.</param>
         public void setCFDA(String value) {
             base.addInput ("CFDA", value);
         }
         /// <summary>
         /// (optional, string) Controls the level of detail of the output. Acceptable values: -1 (summary), 0 (low), 1 (medium), 2 (high), and 3 (extensive). Defaults to -1. See docs for more information.
         /// </summary>
         /// <param name="value">Value of the Detail input for this Choreo.</param>
         public void setDetail(String value) {
             base.addInput ("Detail", value);
         }
         /// <summary>
         /// (conditional, string) The prime recipient for the award's Dun & Bradstreet number (no vendor information).
         /// </summary>
         /// <param name="value">Value of the EntityDun input for this Choreo.</param>
         public void setEntityDun(String value) {
             base.addInput ("EntityDun", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the first year in a range of years from 2000-2006; if used, must be used with LastYearRange and without FiscalYear.
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
         /// (conditional, string) The 4-digit code for a specific governmental agency that is responsible for funding/distributing the ARRA funds to recipients.
         /// </summary>
         /// <param name="value">Value of the FundingAgency input for this Choreo.</param>
         public void setFundingAgency(String value) {
             base.addInput ("FundingAgency", value);
         }
         /// <summary>
         /// (conditional, string) The Agency Treasury Account Symbol (TAS) that identifies the funding Program Source. The Program Source is based out of the OMB TAS list.
         /// </summary>
         /// <param name="value">Value of the FundingTAS input for this Choreo.</param>
         public void setFundingTAS(String value) {
             base.addInput ("FundingTAS", value);
         }
         /// <summary>
         /// (conditional, string) The agency supplied code of the government contracting office that executed the transaction. (For prime recipients only.)
         /// </summary>
         /// <param name="value">Value of the GovtContractOffice input for this Choreo.</param>
         public void setGovtContractOffice(String value) {
             base.addInput ("GovtContractOffice", value);
         }
         /// <summary>
         /// (conditional, integer) Specifies the last year in a range of years; if used, must be used with FirstYearRange and without FiscalYear.
         /// </summary>
         /// <param name="value">Value of the LastYearRange input for this Choreo.</param>
         public void setLastYearRange(String value) {
             base.addInput ("LastYearRange", value);
         }
         /// <summary>
         /// (optional, integer) Allows you to set the maximum number of records retrieved. Defaults to 100.
         /// </summary>
         /// <param name="value">Value of the MaxRecords input for this Choreo.</param>
         public void setMaxRecords(String value) {
             base.addInput ("MaxRecords", value);
         }
         /// <summary>
         /// (conditional, integer) The number of Full-Time Equivalent (FTE) jobs created and retained.
         /// </summary>
         /// <param name="value">Value of the NumberOfJobs input for this Choreo.</param>
         public void setNumberOfJobs(String value) {
             base.addInput ("NumberOfJobs", value);
         }
         /// <summary>
         /// (conditional, integer) Total compensation of first highly compensated officer.
         /// </summary>
         /// <param name="value">Value of the OfficerComp input for this Choreo.</param>
         public void setOfficerComp(String value) {
             base.addInput ("OfficerComp", value);
         }
         /// <summary>
         /// (conditional, string) This is an identifying number assigned to the contract.
         /// </summary>
         /// <param name="value">Value of the OrderNumber input for this Choreo.</param>
         public void setOrderNumber(String value) {
             base.addInput ("OrderNumber", value);
         }
         /// <summary>
         /// (conditional, string) The city in which work was performed.
         /// </summary>
         /// <param name="value">Value of the PopCity input for this Choreo.</param>
         public void setPopCity(String value) {
             base.addInput ("PopCity", value);
         }
         /// <summary>
         /// (conditional, string) The two-letter country code for the country in which work was performed.
         /// </summary>
         /// <param name="value">Value of the PopCountry input for this Choreo.</param>
         public void setPopCountry(String value) {
             base.addInput ("PopCountry", value);
         }
         /// <summary>
         /// (conditional, string) The Congressional District in which work was performed.
         /// </summary>
         /// <param name="value">Value of the PopDistrict input for this Choreo.</param>
         public void setPopDistrict(String value) {
             base.addInput ("PopDistrict", value);
         }
         /// <summary>
         /// (conditional, string) The two-letter code for the state in which in which work was performed (the "place of performance").
         /// </summary>
         /// <param name="value">Value of the PopState input for this Choreo.</param>
         public void setPopState(String value) {
             base.addInput ("PopState", value);
         }
         /// <summary>
         /// (conditional, integer) The ZIP code in which work was performed.
         /// </summary>
         /// <param name="value">Value of the PopZip input for this Choreo.</param>
         public void setPopZip(String value) {
             base.addInput ("PopZip", value);
         }
         /// <summary>
         /// (conditional, string) A description of the project under which the award is funded.
         /// </summary>
         /// <param name="value">Value of the ProjectDescription input for this Choreo.</param>
         public void setProjectDescription(String value) {
             base.addInput ("ProjectDescription", value);
         }
         /// <summary>
         /// (conditional, string) A 4-character numeric designation for the Congressional District within which a recipient or vendor is located. (For prime recipients and sub-recipients only.)
         /// </summary>
         /// <param name="value">Value of the RecipientDistrict input for this Choreo.</param>
         public void setRecipientDistrict(String value) {
             base.addInput ("RecipientDistrict", value);
         }
         /// <summary>
         /// (conditional, string) The name of the recipient (prime recipient, sub-recipient, or vendor); value given is used as a text search.
         /// </summary>
         /// <param name="value">Value of the RecipientName input for this Choreo.</param>
         public void setRecipientName(String value) {
             base.addInput ("RecipientName", value);
         }
         /// <summary>
         /// (conditional, string) The postal state abbreviation for the state in the recipient's address (can be for prime recipient, sub-recipient, or vendor).
         /// </summary>
         /// <param name="value">Value of the RecipientStateCode input for this Choreo.</param>
         public void setRecipientStateCode(String value) {
             base.addInput ("RecipientStateCode", value);
         }
         /// <summary>
         /// (conditional, string) Recipient or vendor type: p = Prime recipients only, s = Sub-recipients only, v = Vendors only.
         /// </summary>
         /// <param name="value">Value of the RecipientType input for this Choreo.</param>
         public void setRecipientType(String value) {
             base.addInput ("RecipientType", value);
         }
         /// <summary>
         /// (conditional, integer) The ZIP code of the recipient (prime recipient, sub-recipient, or vendor).
         /// </summary>
         /// <param name="value">Value of the RecipientZip input for this Choreo.</param>
         public void setRecipientZip(String value) {
             base.addInput ("RecipientZip", value);
         }
         /// <summary>
         /// (optional, string) Determines the order in which records are sorted. The default value sorts by Recipient/Vendor Name. See doc for all other values.
         /// </summary>
         /// <param name="value">Value of the Sort input for this Choreo.</param>
         public void setSort(String value) {
             base.addInput ("Sort", value);
         }
         /// <summary>
         /// (conditional, string) Full text search.
         /// </summary>
         /// <param name="value">Value of the TextSearch input for this Choreo.</param>
         public void setTextSearch(String value) {
             base.addInput ("TextSearch", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A RecoveryResultSet containing execution metadata and results.</returns>
        new public RecoveryResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            RecoveryResultSet results = new JavaScriptSerializer().Deserialize<RecoveryResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the Recovery Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class RecoveryResultSet : Temboo.Core.ResultSet
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
