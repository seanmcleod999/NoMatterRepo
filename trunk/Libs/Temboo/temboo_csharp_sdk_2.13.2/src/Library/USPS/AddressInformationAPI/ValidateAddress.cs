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

namespace Temboo.Library.USPS.AddressInformationAPI
{
    /// <summary>
    /// ValidateAddress
    /// Validates and supplements incomplete address information.
    /// </summary>
    public class ValidateAddress : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the ValidateAddress Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public ValidateAddress(TembooSession session) : base(session, "/Library/USPS/AddressInformationAPI/ValidateAddress")
        {
        }

         /// <summary>
         /// (optional, string) Used to provide an apartment or suite number, if applicable. Maximum characters allowed: 38.
         /// </summary>
         /// <param name="value">Value of the AptOrSuite input for this Choreo.</param>
         public void setAptOrSuite(String value) {
             base.addInput ("AptOrSuite", value);
         }
         /// <summary>
         /// (optional, string) Maximum characters allowed: 15. Either City and State or Zip are required.
         /// </summary>
         /// <param name="value">Value of the City input for this Choreo.</param>
         public void setCity(String value) {
             base.addInput ("City", value);
         }
         /// <summary>
         /// (optional, string) If you are accessing the production server, set to 'production'. Defaults to 'testing' which indicates that you are using the sandbox.
         /// </summary>
         /// <param name="value">Value of the Endpoint input for this Choreo.</param>
         public void setEndpoint(String value) {
             base.addInput ("Endpoint", value);
         }
         /// <summary>
         /// (optional, string) Maximum characters allowed: 38.
         /// </summary>
         /// <param name="value">Value of the FirmName input for this Choreo.</param>
         public void setFirmName(String value) {
             base.addInput ("FirmName", value);
         }
         /// <summary>
         /// (required, password) The password assigned by USPS
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (optional, string) Maximum characters allowed: 2. Either City and State or Zip are required.
         /// </summary>
         /// <param name="value">Value of the State input for this Choreo.</param>
         public void setState(String value) {
             base.addInput ("State", value);
         }
         /// <summary>
         /// (required, string) Street address. Maximum characters allowed: 38.
         /// </summary>
         /// <param name="value">Value of the StreetAddress input for this Choreo.</param>
         public void setStreetAddress(String value) {
             base.addInput ("StreetAddress", value);
         }
         /// <summary>
         /// (optional, string) Maximum characters allowed: 28. For Puerto Rico addresses only.
         /// </summary>
         /// <param name="value">Value of the Urbanization input for this Choreo.</param>
         public void setUrbanization(String value) {
             base.addInput ("Urbanization", value);
         }
         /// <summary>
         /// (required, string) Alphanumeric ID assigned by USPS
         /// </summary>
         /// <param name="value">Value of the UserId input for this Choreo.</param>
         public void setUserId(String value) {
             base.addInput ("UserId", value);
         }
         /// <summary>
         /// (optional, integer) Maximum characters allowed: 4
         /// </summary>
         /// <param name="value">Value of the Zip4DigitCode input for this Choreo.</param>
         public void setZip4DigitCode(String value) {
             base.addInput ("Zip4DigitCode", value);
         }
         /// <summary>
         /// (optional, integer) Maximum characters allowed: 5. Either City and State or Zip are required.
         /// </summary>
         /// <param name="value">Value of the Zip input for this Choreo.</param>
         public void setZip(String value) {
             base.addInput ("Zip", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A ValidateAddressResultSet containing execution metadata and results.</returns>
        new public ValidateAddressResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            ValidateAddressResultSet results = new JavaScriptSerializer().Deserialize<ValidateAddressResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the ValidateAddress Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class ValidateAddressResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from USPS Web Service</returns>
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
