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

namespace Temboo.Library.Zoho.Writer
{
    /// <summary>
    /// DownloadDocument
    /// Downloads a specified document in a user's Zoho Writer Account.
    /// </summary>
    public class DownloadDocument : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the DownloadDocument Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public DownloadDocument(TembooSession session) : base(session, "/Library/Zoho/Writer/DownloadDocument")
        {
        }

         /// <summary>
         /// (required, string) The API Key provided by Zoho
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (required, integer) Specifies the unique document id to download.
         /// </summary>
         /// <param name="value">Value of the DocumentId input for this Choreo.</param>
         public void setDocumentId(String value) {
             base.addInput ("DocumentId", value);
         }
         /// <summary>
         /// (required, string) Specifies the file format in which the documents need to be downloaded. Possible values for documents: doc, docx, pdf, html, sxw, odt, rtf.
         /// </summary>
         /// <param name="value">Value of the DownloadFormat input for this Choreo.</param>
         public void setDownloadFormat(String value) {
             base.addInput ("DownloadFormat", value);
         }
         /// <summary>
         /// (required, string) Your Zoho username (or login id)
         /// </summary>
         /// <param name="value">Value of the LoginID input for this Choreo.</param>
         public void setLoginID(String value) {
             base.addInput ("LoginID", value);
         }
         /// <summary>
         /// (required, password) Your Zoho password
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DownloadDocumentResultSet containing execution metadata and results.</returns>
        new public DownloadDocumentResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DownloadDocumentResultSet results = new JavaScriptSerializer().Deserialize<DownloadDocumentResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the DownloadDocument Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DownloadDocumentResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Zoho. Corresponds to the DownloadFormat input.</returns>
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
