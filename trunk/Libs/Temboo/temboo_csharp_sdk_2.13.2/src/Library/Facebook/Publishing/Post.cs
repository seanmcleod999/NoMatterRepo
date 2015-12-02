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

namespace Temboo.Library.Facebook.Publishing
{
    /// <summary>
    /// Post
    /// Adds an entry to a user's profile feed.
    /// </summary>
    public class Post : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the Post Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public Post(TembooSession session) : base(session, "/Library/Facebook/Publishing/Post")
        {
        }

         /// <summary>
         /// (required, string) The access token retrieved from the final step of the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (optional, string) Caption of the post (only used if link is specified).
         /// </summary>
         /// <param name="value">Value of the Caption input for this Choreo.</param>
         public void setCaption(String value) {
             base.addInput ("Caption", value);
         }
         /// <summary>
         /// (optional, string) Description of the post (only used if link is specified).
         /// </summary>
         /// <param name="value">Value of the Description input for this Choreo.</param>
         public void setDescription(String value) {
             base.addInput ("Description", value);
         }
         /// <summary>
         /// (conditional, string) Link to Post. Supply either a message or a link
         /// </summary>
         /// <param name="value">Value of the Link input for this Choreo.</param>
         public void setLink(String value) {
             base.addInput ("Link", value);
         }
         /// <summary>
         /// (required, string) The message to Post. Supply either a message or a link.
         /// </summary>
         /// <param name="value">Value of the Message input for this Choreo.</param>
         public void setMessage(String value) {
             base.addInput ("Message", value);
         }
         /// <summary>
         /// (optional, string) Name of the post (only used if link is specified).
         /// </summary>
         /// <param name="value">Value of the Name input for this Choreo.</param>
         public void setName(String value) {
             base.addInput ("Name", value);
         }
         /// <summary>
         /// (optional, string) Post thumbnail image (only used if link is specified).
         /// </summary>
         /// <param name="value">Value of the Picture input for this Choreo.</param>
         public void setPicture(String value) {
             base.addInput ("Picture", value);
         }
         /// <summary>
         /// (optional, string) Facebook Page ID of the location associated with this post.
         /// </summary>
         /// <param name="value">Value of the PlaceID input for this Choreo.</param>
         public void setPlaceID(String value) {
             base.addInput ("PlaceID", value);
         }
         /// <summary>
         /// (optional, string) The id of the profile that is being updated. Defaults to "me" indicating the authenticated user.
         /// </summary>
         /// <param name="value">Value of the ProfileID input for this Choreo.</param>
         public void setProfileID(String value) {
             base.addInput ("ProfileID", value);
         }
         /// <summary>
         /// (optional, string) The format that the response should be in. Can be set to xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (optional, string) Comma-separated list of Facebook IDs of people tagged in this Post. NOTE: You cannot specify this field without also specifying a place.
         /// </summary>
         /// <param name="value">Value of the Tags input for this Choreo.</param>
         public void setTags(String value) {
             base.addInput ("Tags", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A PostResultSet containing execution metadata and results.</returns>
        new public PostResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            PostResultSet results = new JavaScriptSerializer().Deserialize<PostResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the Post Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class PostResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Facebook. Corresponds to the ResponseFormat input. Defaults to JSON.</returns>
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
