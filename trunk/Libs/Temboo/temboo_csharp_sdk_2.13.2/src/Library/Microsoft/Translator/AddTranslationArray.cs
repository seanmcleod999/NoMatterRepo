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

namespace Temboo.Library.Microsoft.Translator
{
    /// <summary>
    /// AddTranslationArray
    /// Adds an array of translations to add translation memory.
    /// </summary>
    public class AddTranslationArray : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the AddTranslationArray Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public AddTranslationArray(TembooSession session) : base(session, "/Library/Microsoft/Translator/AddTranslationArray")
        {
        }

         /// <summary>
         /// (optional, string) A valid access token. This can be retrieved by running the GetToken Choreo. Required unless providing the ClientID and ClientSecret.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (optional, string) A string containing the category (domain) of the translation. Defaults to "general".
         /// </summary>
         /// <param name="value">Value of the Category input for this Choreo.</param>
         public void setCategory(String value) {
             base.addInput ("Category", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID obtained when signing up for Microsoft Translator on Azure Marketplace. This is required unless providing an AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret obtained when signing up for Microsoft Translator on Azure Marketplace. This is required unless providing an AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (optional, string) The format of the text being translated. The supported formats are "text/plain" and "text/html".
         /// </summary>
         /// <param name="value">Value of the ContentType input for this Choreo.</param>
         public void setContentType(String value) {
             base.addInput ("ContentType", value);
         }
         /// <summary>
         /// (required, string) A string containing the ISO 639-1 language code of the source language (e.g., en). Must be one of the languages returned by the method GetLanguagesForTranslate.
         /// </summary>
         /// <param name="value">Value of the From input for this Choreo.</param>
         public void setFrom(String value) {
             base.addInput ("From", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Valid values are: json (the default) and xml.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (required, string) A string containing the ISO 639-1 language code of the target language (e.g., es). Must be one of the languages returned by the method GetLanguagesForTranslate.
         /// </summary>
         /// <param name="value">Value of the To input for this Choreo.</param>
         public void setTo(String value) {
             base.addInput ("To", value);
         }
         /// <summary>
         /// (required, json) An array containing translations to add to translation memory. Each translation must contain: OriginalText, TranslatedText, and Rating. See Choreo description for formatting examples.
         /// </summary>
         /// <param name="value">Value of the Translations input for this Choreo.</param>
         public void setTranslations(String value) {
             base.addInput ("Translations", value);
         }
         /// <summary>
         /// (optional, string) A string containing the content location of this translation.
         /// </summary>
         /// <param name="value">Value of the URI input for this Choreo.</param>
         public void setURI(String value) {
             base.addInput ("URI", value);
         }
         /// <summary>
         /// (required, string) A string used to track the originator of the submission.
         /// </summary>
         /// <param name="value">Value of the User input for this Choreo.</param>
         public void setUser(String value) {
             base.addInput ("User", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AddTranslationArrayResultSet containing execution metadata and results.</returns>
        new public AddTranslationArrayResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AddTranslationArrayResultSet results = new JavaScriptSerializer().Deserialize<AddTranslationArrayResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the AddTranslationArray Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AddTranslationArrayResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Microsoft.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "ExpiresIn" output from this Choreo execution
        /// <returns>String - (integer) Contains the number of seconds for which the access token is valid when ClientID and ClientSecret are provided.</returns>
        /// </summary>
        public String ExpiresIn
        {
            get
            {
                return (String) base.Output["ExpiresIn"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "NewAccessToken" output from this Choreo execution
        /// <returns>String - (string) Contains a new AccessToken when the ClientID and ClientSecret are provided.</returns>
        /// </summary>
        public String NewAccessToken
        {
            get
            {
                return (String) base.Output["NewAccessToken"];
            }
        }
    }
}
