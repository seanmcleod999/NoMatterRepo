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

namespace Temboo.Library.Google.Gmail
{
    /// <summary>
    /// GetLatestUnreadEmail
    /// Returns the latest unread email from a user's Gmail feed.
    /// </summary>
    public class GetLatestUnreadEmail : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the GetLatestUnreadEmail Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public GetLatestUnreadEmail(TembooSession session) : base(session, "/Library/Google/Gmail/GetLatestUnreadEmail")
        {
        }

         /// <summary>
         /// (optional, string) A valid Access Token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new Access Token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (conditional, string) The Client ID provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientID input for this Choreo.</param>
         public void setClientID(String value) {
             base.addInput ("ClientID", value);
         }
         /// <summary>
         /// (conditional, string) The Client Secret provided by Google. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the ClientSecret input for this Choreo.</param>
         public void setClientSecret(String value) {
             base.addInput ("ClientSecret", value);
         }
         /// <summary>
         /// (optional, string) The name of a Gmail Label to retrieve messages from (e.g., important, starred, sent, junk-e-mail, all).
         /// </summary>
         /// <param name="value">Value of the Label input for this Choreo.</param>
         public void setLabel(String value) {
             base.addInput ("Label", value);
         }
         /// <summary>
         /// (optional, password) A Google App-specific password that you've generated after enabling 2-Step Verification (Note: authenticating with OAuth credentials is the preferred authentication method).
         /// </summary>
         /// <param name="value">Value of the Password input for this Choreo.</param>
         public void setPassword(String value) {
             base.addInput ("Password", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new Access Token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (optional, string) The format for the response. Valid values are JSON and XML. This will be ignored when providng an XPath query because results are returned as a string or JSON depending on the Mode specified.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Your full Google email address e.g., martha.temboo@gmail.com (Note: authenticating with OAuth credentials is the preferred authentication method).
         /// </summary>
         /// <param name="value">Value of the Username input for this Choreo.</param>
         public void setUsername(String value) {
             base.addInput ("Username", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A GetLatestUnreadEmailResultSet containing execution metadata and results.</returns>
        new public GetLatestUnreadEmailResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            GetLatestUnreadEmailResultSet results = new JavaScriptSerializer().Deserialize<GetLatestUnreadEmailResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the GetLatestUnreadEmail Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class GetLatestUnreadEmailResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Google. This will contain the data from the Gmail feed, or if the XPath input is provided, it will contain the result of the XPath query.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "AuthorEmail" output from this Choreo execution
        /// <returns>String - (string) The author's email address.</returns>
        /// </summary>
        public String AuthorEmail
        {
            get
            {
                return (String) base.Output["AuthorEmail"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "AuthorName" output from this Choreo execution
        /// <returns>String - (string) The author's name.</returns>
        /// </summary>
        public String AuthorName
        {
            get
            {
                return (String) base.Output["AuthorName"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "MessageBody" output from this Choreo execution
        /// <returns>String - (string) The email body. Note that this corresponds to the "summary" element in the feed.</returns>
        /// </summary>
        public String MessageBody
        {
            get
            {
                return (String) base.Output["MessageBody"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "NewAccessToken" output from this Choreo execution
        /// <returns>String - (string) Contains a new AccessToken when the RefreshToken is provided.</returns>
        /// </summary>
        public String NewAccessToken
        {
            get
            {
                return (String) base.Output["NewAccessToken"];
            }
        }
        /// <summary> 
        /// Retrieve the value for the "Subject" output from this Choreo execution
        /// <returns>String - (string) The subject line of the email. Note that this corresponds to the "title" element in the feed.</returns>
        /// </summary>
        public String Subject
        {
            get
            {
                return (String) base.Output["Subject"];
            }
        }
    }
}
