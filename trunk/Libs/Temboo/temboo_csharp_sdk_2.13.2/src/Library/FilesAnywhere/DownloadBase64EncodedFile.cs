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

namespace Temboo.Library.FilesAnywhere
{
    /// <summary>
    /// DownloadBase64EncodedFile
    /// Downloads a file from a specified directory in your FilesAnywhere account, and returns the content as Base64 encoded data.
    /// </summary>
    public class DownloadBase64EncodedFile : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the DownloadBase64EncodedFile Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public DownloadBase64EncodedFile(TembooSession session) : base(session, "/Library/FilesAnywhere/DownloadBase64EncodedFile")
        {
        }

         /// <summary>
         /// (conditional, string) The API Key provided by FilesAnywhere. Required unless supplying a valid Token input.
         /// </summary>
         /// <param name="value">Value of the APIKey input for this Choreo.</param>
         public void setAPIKey(String value) {
             base.addInput ("APIKey", value);
         }
         /// <summary>
         /// (conditional, integer) Defaults to 0 for a FilesAnywhere Web account.  Use 50 for a FilesAnywhere WebAdvanced account.
         /// </summary>
         /// <param name="value">Value of the OrgID input for this Choreo.</param>
         public void setOrgID(String value) {
             base.addInput ("OrgID", value);
         }
         /// <summary>
         /// (conditional, password) Your FilesAnywhere password. Required unless supplying a valid Token input.
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (required, string) The path to the file you want to download (i.e. \JOHNSMITH\MyFolder\MyFile.txt).
         /// </summary>
         /// <param name="value">Value of the Path input for this Choreo.</param>
         public void setPath(String value) {
             base.addInput ("Path", value);
         }
         /// <summary>
         /// (conditional, string) If provided, the Choreo will use the token to authenticate. If the token is expired or not provided, the Choreo will relogin and retrieve a new token when APIKey, Username, and Password are supplied.
         /// </summary>
         /// <param name="value">Value of the Token input for this Choreo.</param>
         public void setToken(String value) {
             base.addInput ("Token", value);
         }
         /// <summary>
         /// (conditional, string) Your FilesAnywhere username. Required unless supplying a valid Token input.
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A DownloadBase64EncodedFileResultSet containing execution metadata and results.</returns>
        new public DownloadBase64EncodedFileResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            DownloadBase64EncodedFileResultSet results = new JavaScriptSerializer().Deserialize<DownloadBase64EncodedFileResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the DownloadBase64EncodedFile Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class DownloadBase64EncodedFileResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (string) The response from FilesAnywhere. The response contains the Base64 encoded content of the file you are downloading.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Token" output from this Choreo execution
        /// <returns>String - (conditional, string) If provided, the Choreo will use the token to authenticate. If the token is expired or not provided, the Choreo will relogin and retrieve a new token when APIKey, Username, and Password are supplied.</returns>
        /// </summary>
        public String Token
        {
            get
            {
                return (String) base.Output["Token"];
            }
        }
    }
}
