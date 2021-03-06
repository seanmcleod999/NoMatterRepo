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

namespace Temboo.Library.Highrise
{
    /// <summary>
    /// SearchPeople
    /// Lets you search your Highrise CRM by specifying an email search criteria.
    /// </summary>
    public class SearchPeople : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the SearchPeople Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public SearchPeople(TembooSession session) : base(session, "/Library/Highrise/SearchPeople")
        {
        }

         /// <summary>
         /// (required, string) A valid Highrise account name. This is the first part of the account's URL.
         /// </summary>
         /// <param name="value">Value of the AccountName input for this Choreo.</param>
         public void setAccountName(String value) {
             base.addInput ("AccountName", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the city field in Highrise.
         /// </summary>
         /// <param name="value">Value of the City input for this Choreo.</param>
         public void setCity(String value) {
             base.addInput ("City", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the country field in Highrise.
         /// </summary>
         /// <param name="value">Value of the Country input for this Choreo.</param>
         public void setCountry(String value) {
             base.addInput ("Country", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the email address field in Highrise.
         /// </summary>
         /// <param name="value">Value of the EmailAddress input for this Choreo.</param>
         public void setEmailAddress(String value) {
             base.addInput ("EmailAddress", value);
         }
         /// <summary>
         /// (required, password) The Highrise account password. Use the value 'X' when specifying an API Key for the Username input.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the phone field in Highrise.
         /// </summary>
         /// <param name="value">Value of the Phone input for this Choreo.</param>
         public void setPhone(String value) {
             base.addInput ("Phone", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the state field in Highrise.
         /// </summary>
         /// <param name="value">Value of the State input for this Choreo.</param>
         public void setState(String value) {
             base.addInput ("State", value);
         }
         /// <summary>
         /// (required, string) A Highrise account username or API Key.
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }
         /// <summary>
         /// (optional, string) Allows you to search by the ZIP field in Highrise.
         /// </summary>
         /// <param name="value">Value of the Zip input for this Choreo.</param>
         public void setZip(String value) {
             base.addInput ("Zip", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A SearchPeopleResultSet containing execution metadata and results.</returns>
        new public SearchPeopleResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            SearchPeopleResultSet results = new JavaScriptSerializer().Deserialize<SearchPeopleResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the SearchPeople Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class SearchPeopleResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from Highrise.</returns>
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
