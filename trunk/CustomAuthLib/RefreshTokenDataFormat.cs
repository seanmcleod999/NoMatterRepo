using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataProtection;

namespace CustomAuthLib
{
	public class RefreshTokenDataFormat : SecureDataFormat<RefreshTicket>
	{
		public RefreshTokenDataFormat(IDataProtector protector)
			: base(new RefreshTicketSerializer(), protector, TextEncodings.Base64Url)
		{
		}
	}
}