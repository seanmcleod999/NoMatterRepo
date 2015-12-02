using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using NoMatterDataLibrary;
using NoMatterDataLibrary.Enums;
using NoMatterWebApi.Logging;
using NoMatterWebApiModels.Models;
using Temboo.Core;
using Temboo.Library.Google.Gmail;
using Temboo.Library.Utilities.Dates;

namespace NoMatterWebApi.Controllers.V1
{
	[RoutePrefix("api/v1/things")]
	public class ThingController : ApiController
	{

		private IThingRepository _thingRepository;


		public ThingController()
		{
			_thingRepository = new ThingRepository();
			
		}

		public ThingController(IThingRepository thingRepository)
		{
			_thingRepository = thingRepository;

		}


		[HttpPost]
		//[Route("{id}")]
		//[ResponseType(typeof(Product))]
		public async Task<IHttpActionResult> SaveThingAsync(string id)
		{
			try
			{
				var thing = await _thingRepository.GetOrInsertThing(id);
			
				//Get the parameters from the querystring
				var thingValuePairs = this.Request.GetQueryNameValuePairs();
					
				foreach (var thingValuePair in thingValuePairs)
				{
					int? intValue = null;
					bool? boolValue = null;
					string stringValue = null;


					var thingFieldName = thingValuePair.Key;
					var thingValue = thingValuePair.Value;

					//Figure out if the value is a string or can be an int

					int tempIntValue;
					bool tempBoolValue;

					if (int.TryParse(thingValue, out tempIntValue))
					{
						intValue = tempIntValue;
					}
					else if (bool.TryParse(thingValue, out tempBoolValue))
					{
						boolValue = tempBoolValue;
					}
					else
					{
						stringValue = thingValue;
					}

					//See if the thing has this field
					var thingField = thing.ThingFields.FirstOrDefault(x => x.FieldName == thingFieldName);

					int thingFieldId;

					if (thingField == null)
					{
						thingFieldId = await _thingRepository.InsertThingField(thing.ThingId, thingFieldName);
					}
					else
					{
						thingFieldId = thingField.ThingFieldId;
					}

					//Insert the value
					await _thingRepository.InsertThingFieldValue(thingFieldId, intValue, boolValue, stringValue);

					if (intValue != null)
					{
						//See if there is an alert for this thing and this particualar field
						var thingAlerts = await _thingRepository.GetThingAlert(id, thingFieldName);

						foreach (var thingAlert in thingAlerts)
						{
							var alertAlarm = false;
							
							switch (thingAlert.ThingAlertTypeId)
							{
								case (byte)ThingAlertTypeEnum.Equals:
									if (intValue == thingAlert.Criteria)
									{
										alertAlarm = true;
									}
									break;

								case (byte)ThingAlertTypeEnum.GreaterThan:

									if (intValue > thingAlert.Criteria)
									{
										alertAlarm = true;
									}
									break;

								case (byte)ThingAlertTypeEnum.LessThan:
									if (intValue < thingAlert.Criteria)
									{
										alertAlarm = true;										
									}
									break;

							}

							if (alertAlarm)
							{
								var sendAlert = false;

								switch (thingAlert.ThingAlertFrequencyId)
								{
									case (byte)ThingAlertFrequencyEnum.HalfHourly:

										if (thingAlert.LastAlertSent == null || thingAlert.LastAlertSent < DateTime.Now.AddMinutes(-30))
										{
											sendAlert = true;
										}
										break;

									case (byte) ThingAlertFrequencyEnum.Hourly:
										if (thingAlert.LastAlertSent == null || thingAlert.LastAlertSent < DateTime.Now.AddHours(-1))
										{
											sendAlert = true;
										}										
										break;

									case (byte)ThingAlertFrequencyEnum.Daily:
										if (thingAlert.LastAlertSent == null || thingAlert.LastAlertSent < DateTime.Now.AddDays(-1))
										{
											sendAlert = true;
										}											
										break;

									case (byte)ThingAlertFrequencyEnum.Weekly:
										if (thingAlert.LastAlertSent == null || thingAlert.LastAlertSent < DateTime.Now.AddDays(-7))
										{
											sendAlert = true;
										}										
										break;

									case (byte)ThingAlertFrequencyEnum.Monthly:
										if (thingAlert.LastAlertSent == null || thingAlert.LastAlertSent < DateTime.Now.AddMonths(-1))
										{
											sendAlert = true;
										}											
										break;

								}

								if (sendAlert)
								{
									await SendAlertAsync(thingAlert.ThingAlertId, thingAlert.AlertSubject, thingAlert.AlertBody, intValue.Value);
								}
							}
						}
					}
				}

				return Ok();
			}
			catch (Exception ex)
			{
				Logger.WriteGeneralError(ex);
				return InternalServerError(ex);
			}
		}

		private async Task SendAlertAsync(int thingAlertId, string alertSubject, string alertBody, int value)
		{
			//Send the alert

			// Instantiate a TembooSession object using your Account name and Application key
			TembooSession session = new TembooSession("seanmcleod", "MelvilleGreen", "st812ho9sjnKC4YJ6GF13cAv83oOt6PT");

			// Instantiate the Choreo using the TembooSession
			SendEmail sendEmailChoreo = new SendEmail(session);

			// Set inputs
			sendEmailChoreo.setUsername("mcleod.sean@gmail.com");
			sendEmailChoreo.setPassword("56546546456"); //App password
			sendEmailChoreo.setFromAddress("mcleod.sean@gmail.com");
			sendEmailChoreo.setToAddress("mcleod.sean@gmail.com");
			sendEmailChoreo.setSubject(alertSubject);
			sendEmailChoreo.setMessageBody(alertBody);

			// Set Profile
			//sendEmailChoreo.setCredential("YOUR_GMAIL_PROFILE_NAME");

			// Execute Choreo
			SendEmailResultSet sendEmailResultSet = sendEmailChoreo.execute();

			// Print results
			//Console.WriteLine(sendEmailResults.Success);

			//if (sendEmailResultSet.Success.)
			//{
				
			//}

			//update the lastAlertSent date

			await _thingRepository.UpdateThingAlertSent(thingAlertId);


		}
	}
}