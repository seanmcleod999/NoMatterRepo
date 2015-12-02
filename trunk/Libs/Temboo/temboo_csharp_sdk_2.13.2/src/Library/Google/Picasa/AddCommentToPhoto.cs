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

namespace Temboo.Library.Google.Picasa
{
    /// <summary>
    /// AddCommentToPhoto
    /// Adds a comment to a specified photo in Google Picasa.
    /// </summary>
    public class AddCommentToPhoto : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the AddCommentToPhoto Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public AddCommentToPhoto(TembooSession session) : base(session, "/Library/Google/Picasa/AddCommentToPhoto")
        {
        }

         /// <summary>
         /// (optional, string) A valid access token retrieved during the OAuth process. This is required unless you provide the ClientID, ClientSecret, and RefreshToken to generate a new access token.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, integer) The id of the album which contains the photo you want to add a comment to.
         /// </summary>
         /// <param name="value">Value of the AlbumID input for this Choreo.</param>
         public void setAlbumID(String value) {
             base.addInput ("AlbumID", value);
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
         /// (required, string) The comment that you want to add to a photo.
         /// </summary>
         /// <param name="value">Value of the Comment input for this Choreo.</param>
         public void setComment(String value) {
             base.addInput ("Comment", value);
         }
         /// <summary>
         /// (required, integer) The id of the photo you want to add a comment to.
         /// </summary>
         /// <param name="value">Value of the PhotoID input for this Choreo.</param>
         public void setPhotoID(String value) {
             base.addInput ("PhotoID", value);
         }
         /// <summary>
         /// (conditional, string) An OAuth Refresh Token used to generate a new access token when the original token is expired. Required unless providing a valid AccessToken.
         /// </summary>
         /// <param name="value">Value of the RefreshToken input for this Choreo.</param>
         public void setRefreshToken(String value) {
             base.addInput ("RefreshToken", value);
         }
         /// <summary>
         /// (optional, string) Google Picasa username. Defaults to 'default' which means the server will use the UserID of the user whose access token was specified.
         /// </summary>
         /// <param name="value">Value of the UserID input for this Choreo.</param>
         public void setUserID(String value) {
             base.addInput ("UserID", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A AddCommentToPhotoResultSet containing execution metadata and results.</returns>
        new public AddCommentToPhotoResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            AddCommentToPhotoResultSet results = new JavaScriptSerializer().Deserialize<AddCommentToPhotoResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the AddCommentToPhoto Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class AddCommentToPhotoResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - (xml) The response from Google Picasa.</returns>
        /// </summary>
        public String Response
        {
            get
            {
                return (String) base.Output["Response"];
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
    }
}
