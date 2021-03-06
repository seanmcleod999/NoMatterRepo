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

namespace Temboo.Library.Fitbit.Foods
{
    /// <summary>
    /// CreateFood
    /// Create new private food for a user.
    /// </summary>
    public class CreateFood : Temboo.Core.Choreography
    {

        /// <summary>
        /// Create a new instance of the CreateFood Choreo
        /// </summary>
        /// <param name="session">A TembooSession object, containing a valid set of Temboo credentials.</param>
        public CreateFood(TembooSession session) : base(session, "/Library/Fitbit/Foods/CreateFood")
        {
        }

         /// <summary>
         /// (required, string) The Access Token Secret retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessTokenSecret input for this Choreo.</param>
         public void setAccessTokenSecret(String value) {
             base.addInput ("AccessTokenSecret", value);
         }
         /// <summary>
         /// (required, string) The Access Token retrieved during the OAuth process.
         /// </summary>
         /// <param name="value">Value of the AccessToken input for this Choreo.</param>
         public void setAccessToken(String value) {
             base.addInput ("AccessToken", value);
         }
         /// <summary>
         /// (required, integer) The number of calories per serving size.
         /// </summary>
         /// <param name="value">Value of the Calories input for this Choreo.</param>
         public void setCalories(String value) {
             base.addInput ("Calories", value);
         }
         /// <summary>
         /// (required, string) The Consumer Key provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerKey input for this Choreo.</param>
         public void setConsumerKey(String value) {
             base.addInput ("ConsumerKey", value);
         }
         /// <summary>
         /// (required, string) The Consumer Secret provided by Fitbit.
         /// </summary>
         /// <param name="value">Value of the ConsumerSecret input for this Choreo.</param>
         public void setConsumerSecret(String value) {
             base.addInput ("ConsumerSecret", value);
         }
         /// <summary>
         /// (optional, string) A description for the food entry.
         /// </summary>
         /// <param name="value">Value of the Description input for this Choreo.</param>
         public void setDescription(String value) {
             base.addInput ("Description", value);
         }
         /// <summary>
         /// (optional, string) Form type; (LIQUID or DRY).
         /// </summary>
         /// <param name="value">Value of the FormType input for this Choreo.</param>
         public void setFormType(String value) {
             base.addInput ("FormType", value);
         }
         /// <summary>
         /// (required, integer) The default measurement unit.
         /// </summary>
         /// <param name="value">Value of the MeasurementUnitID input for this Choreo.</param>
         public void setMeasurementUnitID(String value) {
             base.addInput ("MeasurementUnitID", value);
         }
         /// <summary>
         /// (required, string) The name of the food.
         /// </summary>
         /// <param name="value">Value of the Name input for this Choreo.</param>
         public void setName(String value) {
             base.addInput ("Name", value);
         }
         /// <summary>
         /// (optional, string) The format that you want the response to be in: xml or json. Defaults to json.
         /// </summary>
         /// <param name="value">Value of the ResponseFormat input for this Choreo.</param>
         public void setResponseFormat(String value) {
             base.addInput ("ResponseFormat", value);
         }
         /// <summary>
         /// (required, integer) The default serving size.
         /// </summary>
         /// <param name="value">Value of the ServingSize input for this Choreo.</param>
         public void setServingSize(String value) {
             base.addInput ("ServingSize", value);
         }

        /// <summary>
        /// Execute the Choreo using the specified InputSet as parameters, wait for the Choreo to complete
        /// and return a ResultSet containing the execution results
        /// </summary>
        /// <returns>A CreateFoodResultSet containing execution metadata and results.</returns>
        new public CreateFoodResultSet execute()
        {
            String json = base.getResponseJSON(false, true);
            CreateFoodResultSet results = new JavaScriptSerializer().Deserialize<CreateFoodResultSet>(json);

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
    /// A ResultSet with methods tailored to the values returned by the CreateFood Choreo
    /// The ResultSet object is used to retrieve the results of a Choreo execution
    /// </summary>
    public class CreateFoodResultSet : Temboo.Core.ResultSet
    {
        /// <summary> 
        /// Retrieve the value for the "Response" output from this Choreo execution
        /// <returns>String - The response from Fitbit.</returns>
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
