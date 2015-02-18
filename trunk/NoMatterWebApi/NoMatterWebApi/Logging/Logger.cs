using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NoMatterWebApi.Logging
{
	public class Logger
	{
		public static void WriteLog(LogCategory category, System.Diagnostics.TraceEventType eventType, long? transactionId, string message, params object[] properties)
		{
			Dictionary<string, object> propDictionary = null;

			if (transactionId.HasValue)
			{
				propDictionary = new Dictionary<string, object>();
				propDictionary.Add("TransactionID", transactionId.Value);
			}

			Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry le
				= new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
					message,
					MakeCategory(category),
					SeverityToPriority(eventType),
					-1,
					eventType,
					string.Empty,
					BuildProperties(propDictionary, properties));

			Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(le);
		}

		public static void WriteGeneralError(Exception ex, params object[] properties)
		{
			StringBuilder logMessage = new StringBuilder();

			logMessage.AppendFormat("MSG:{0}\r\nTY:{1}\r\n",
				ex.Message,
				ex.GetType().ToString());

			logMessage.AppendFormat("ST:{0}\r\nIE:{1}",
				ex.StackTrace,
				ex.InnerException);
			WriteLog(LogCategory.General, System.Diagnostics.TraceEventType.Error, null, logMessage.ToString(), properties);
		}

		public static void WriteGeneralError(string message)
		{
			WriteLog(LogCategory.General, System.Diagnostics.TraceEventType.Error, null, message);
		}

		public static void WriteGeneralWarning(Exception ex, params object[] properties)
		{
			string message = String.Format("MSG:{0}\r\nTY:{1}\r\nST:{2}\r\nIE:{3}",
				ex.Message,
				ex.GetType().ToString(),
				ex.StackTrace,
				ex.InnerException);
			WriteLog(LogCategory.General, System.Diagnostics.TraceEventType.Warning, null, message, properties);
		}

		public static void WriteGeneralInformationLog(string message, params object[] properties)
		{
			WriteLog(LogCategory.General, System.Diagnostics.TraceEventType.Information, null, message, properties);
		}

		public static void WriteDebugInformationLog(string message, params object[] properties)
		{
			WriteLog(LogCategory.Debug, System.Diagnostics.TraceEventType.Information, null, message, properties);
		}

		private static IDictionary<string, object> BuildProperties(IDictionary<string, object> dictionary, object[] properties)
		{
			if (properties == null || properties.Length == 0)
				return dictionary;

			if (dictionary == null) dictionary = new Dictionary<string, object>();

			foreach (object o in properties)
			{
				if (o != null)
				{
					string name = o.GetType().Name;
					for (int i = 1; dictionary.Keys.Contains(name) && i < 500; i++)
					{
						name = string.Format("{0}({1})", o.GetType().Name, i);
					}
					dictionary.Add(name, o.ToString());
				}
			}

			return dictionary;
		}

		private static int SeverityToPriority(System.Diagnostics.TraceEventType eventType)
		{
			/*
			Priority - -1 Verbose - Verbose additional information being logged mainly for debug purposes.
						0 Information - Additional information being logged as things happen.
						1 Warning - Some parameters are on the boundary of acceptability or have been left out.
						2 Error - Error occured and could be recovered from.
						3 Critical Error
			 */
			switch (eventType)
			{
				case System.Diagnostics.TraceEventType.Critical:
					return 3;
				case System.Diagnostics.TraceEventType.Error:
					return 2;
				case System.Diagnostics.TraceEventType.Warning:
					return 1;
				case System.Diagnostics.TraceEventType.Information:
					return 0;
				case System.Diagnostics.TraceEventType.Resume:
				case System.Diagnostics.TraceEventType.Start:
				case System.Diagnostics.TraceEventType.Stop:
				case System.Diagnostics.TraceEventType.Suspend:
				case System.Diagnostics.TraceEventType.Transfer:
				case System.Diagnostics.TraceEventType.Verbose:
				default:
					return -1;
			}
		}

		private static ICollection<string> MakeCategory(LogCategory category)
		{
			List<string> list = new List<string>();
			if ((category & LogCategory.General) > 0)
				list.Add("General");
			if ((category & LogCategory.Debug) > 0)
				list.Add("Debug");
			if ((category & LogCategory.Trace) > 0)
				list.Add("Trace");
			if ((category & LogCategory.Security) > 0)
				list.Add("Security");

			if (list.Count == 0)
				throw new ArgumentException("No category specified.");

			return list;
		}
	}
}