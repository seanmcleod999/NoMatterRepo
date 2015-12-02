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

namespace Temboo.Library.Zoho.CRM
{
    /// <summary>
    /// InsertRecords
    /// Inserts records into your Zoho CRM account.
    /// </summary>
    public class InsertRecords : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the InsertRecords Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public InsertRecords(TembooSession session) : base(session, "/Library/Zoho/CRM/InsertRecords")
        {
        }

         /// <summary>
         /// (optional, string) Corresponds to the Annual Revenue field in Zoho
         /// </summary>
         /// <param name="value">Value of the AnnualRevenue input for this Choreo.</param>
         public void setAnnualRevenue(String value) {
             base.addInput ("AnnualRevenue", value);
         }
         /// <summary>
         /// (required, string) A valid authentication token. Permanent authentication tokens can be generated by the GenerateAuthToken Choreo.
         /// </summary>
         /// <param name="value">Value of the AuthenticationToken input for this Choreo.</param>
         public void setAuthenticationToken(String value) {
             base.addInput ("AuthenticationToken", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Campaign Source field in Zoho
         /// </summary>
         /// <param name="value">Value of the CampaignSource input for this Choreo.</param>
         public void setCampaignSource(String value) {
             base.addInput ("CampaignSource", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the City field in Zoho
         /// </summary>
         /// <param name="value">Value of the City input for this Choreo.</param>
         public void setCity(String value) {
             base.addInput ("City", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Company field in Zoho
         /// </summary>
         /// <param name="value">Value of the Company input for this Choreo.</param>
         public void setCompany(String value) {
             base.addInput ("Company", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Country field in Zoho
         /// </summary>
         /// <param name="value">Value of the Country input for this Choreo.</param>
         public void setCountry(String value) {
             base.addInput ("Country", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Description field in Zoho
         /// </summary>
         /// <param name="value">Value of the Description input for this Choreo.</param>
         public void setDescription(String value) {
             base.addInput ("Description", value);
         }
         /// <summary>
         /// (optional, boolean) Corresponds to the Email Opt Out field in Zoho. Defaults to 0 for false.
         /// </summary>
         /// <param name="value">Value of the EmailOptOut input for this Choreo.</param>
         public void setEmailOptOut(String value) {
             base.addInput ("EmailOptOut", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Email field in Zoho
         /// </summary>
         /// <param name="value">Value of the Email input for this Choreo.</param>
         public void setEmail(String value) {
             base.addInput ("Email", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Fax field in Zoho
         /// </summary>
         /// <param name="value">Value of the Fax input for this Choreo.</param>
         public void setFax(String value) {
             base.addInput ("Fax", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the First Name field in Zoho
         /// </summary>
         /// <param name="value">Value of the FirstName input for this Choreo.</param>
         public void setFirstName(String value) {
             base.addInput ("FirstName", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Industry field in Zoho
         /// </summary>
         /// <param name="value">Value of the Industry input for this Choreo.</param>
         public void setIndustry(String value) {
             base.addInput ("Industry", value);
         }
         /// <summary>
         /// (required, string) Corresponds to the Last Name field in Zoho
         /// </summary>
         /// <param name="value">Value of the LastName input for this Choreo.</param>
         public void setLastName(String value) {
             base.addInput ("LastName", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Lead Owner field in Zoho
         /// </summary>
         /// <param name="value">Value of the LeadOwner input for this Choreo.</param>
         public void setLeadOwner(String value) {
             base.addInput ("LeadOwner", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Lead Source field in Zoho
         /// </summary>
         /// <param name="value">Value of the LeadSource input for this Choreo.</param>
         public void setLeadSource(String value) {
             base.addInput ("LeadSource", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Lead Status field in Zoho
         /// </summary>
         /// <param name="value">Value of the LeadStatus input for this Choreo.</param>
         public void setLeadStatus(String value) {
             base.addInput ("LeadStatus", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Mobile field in Zoho
         /// </summary>
         /// <param name="value">Value of the Mobile input for this Choreo.</param>
         public void setMobile(String value) {
             base.addInput ("Mobile", value);
         }
         /// <summary>
         /// (optional, string) The Zoho module you want to access. Defaults to 'Leads'.
         /// </summary>
         /// <param name="value">Value of the Module input for this Choreo.</param>
         public void setModule(String value) {
             base.addInput ("Module", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Num Of Employees field in Zoho
         /// </summary>
         /// <param name="value">Value of the NumOfEmployees input for this Choreo.</param>
         public void setNumOfEmployees(String value) {
             base.addInput ("NumOfEmployees", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Phone field in Zoho
         /// </summary>
         /// <param name="value">Value of the Phone input for this Choreo.</param>
         public void setPhone(String value) {
             base.addInput ("Phone", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Rating field in Zoho
         /// </summary>
         /// <param name="value">Value of the Rating input for this Choreo.</param>
         public void setRating(String value) {
             base.addInput ("Rating", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid formats are: json and xml (the default).
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Salutation field in Zoho
         /// </summary>
         /// <param name="value">Value of the Salutation input for this Choreo.</param>
         public void setSalutation(String value) {
             base.addInput ("Salutation", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Skype ID field in Zoho
         /// </summary>
         /// <param name="value">Value of the SkypeID input for this Choreo.</param>
         public void setSkypeID(String value) {
             base.addInput ("SkypeID", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the State field in Zoho
         /// </summary>
         /// <param name="value">Value of the State input for this Choreo.</param>
         public void setState(String value) {
             base.addInput ("State", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Street field in Zoho
         /// </summary>
         /// <param name="value">Value of the Street input for this Choreo.</param>
         public void setStreet(String value) {
             base.addInput ("Street", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Title field in Zoho
         /// </summary>
         /// <param name="value">Value of the Title input for this Choreo.</param>
         public void setTitle(String value) {
             base.addInput ("Title", value);
         }
         /// <summary>
         /// (optional, string) Corresponds to the Website field in Zoho
         /// </summary>
         /// <param name="value">Value of the Website input for this Choreo.</param>
         public void setWebsite(String value) {
             base.addInput ("Website", value);
         }
         /// <summary>
         /// (optional, integer) Corresponds to the Zip Code field in Zoho
         /// </summary>
         /// <param name="value">Value of the ZipCode input for this Choreo.</param>
         public void setZipCode(String value) {
             base.addInput ("ZipCode", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A InsertRecordsResultSet containing execution metadata and results.</returns>
        new public InsertRecordsResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            InsertRecordsResultSet results = new JavaScriptSerializer().Deserialize<InsertRecordsResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the InsertRecords Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class InsertRecordsResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Zoho. Format corresponds to the ResponseFormat input. Defaults to xml.</returns>
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