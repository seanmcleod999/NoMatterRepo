using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Owin.Security.DataHandler.Serializer;

namespace CustomAuthLib
{
	public class RefreshTicketSerializer : IDataSerializer<RefreshTicket>
    {
        private const int FormatVersion = 2;

		public virtual byte[] Serialize(RefreshTicket model)
        {
            using (var memory = new MemoryStream())
            {
                using (var compression = new GZipStream(memory, CompressionLevel.Optimal))
                {
                    using (var writer = new BinaryWriter(compression))
                    {
                        Write(writer, model);
                    }
                }
                return memory.ToArray();
            }
        }

		public virtual RefreshTicket Deserialize(byte[] data)
        {
            using (var memory = new MemoryStream(data))
            {
                using (var compression = new GZipStream(memory, CompressionMode.Decompress))
                {
                    using (var reader = new BinaryReader(compression))
                    {
                        return Read(reader);
                    }
                }
            }
        }

		public static void Write(BinaryWriter writer, RefreshTicket model)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            writer.Write(FormatVersion);
			writer.Write(model.ProfileId);
			writer.Write(model.RefreshTicketId);
			//writer.Write(model.IssueDate.ToString("u"));
        }

		public static RefreshTicket Read(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (reader.ReadInt32() != FormatVersion)
            {
                return null;
            }

			var ticket = new RefreshTicket();
			ticket.ProfileId = reader.ReadString();
			ticket.RefreshTicketId = reader.ReadString();
			//ticket.IssueDate = DateTime.ParseExact(reader.ReadString(), "u", null);

			return ticket;
        }
    }
}