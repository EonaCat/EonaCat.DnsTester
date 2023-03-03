using System.Collections.Generic;
using System.Text;

namespace EonaCat.DnsTester.Helpers
{
    public class DnsNameParser
    {
        public static string ParseName(byte[] responseBytes, ref int offset)
        {
            List<string> labels = new List<string>();
            int length;

            while ((length = responseBytes[offset++]) != 0)
            {
                if ((length & 0xC0) == 0xC0)
                {
                    // The name is compressed
                    int pointer = ((length & 0x3F) << 8) | responseBytes[offset++];
                    int savedOffset = offset;
                    offset = pointer;
                    labels.AddRange(ParseName(responseBytes, ref offset).Split('.'));
                    offset = savedOffset;
                    break;
                }

                // The name is not compressed
                labels.Add(System.Text.Encoding.ASCII.GetString(responseBytes, offset, length));
                offset += length;
            }

            return string.Join(".", labels);
        }

        public static string ExtractDomainName(byte[] buffer, ref int offset)
        {
            List<string> labels = new List<string>();

            while (true)
            {
                byte labelLength = buffer[offset++];

                if (labelLength == 0)
                {
                    break;
                }

                if ((labelLength & 0xC0) == 0xC0)
                {
                    // Compressed domain name
                    int pointer = (int)(((labelLength & 0x3F) << 8) + buffer[offset++]);
                    labels.Add(ExtractDomainName(buffer, ref pointer));
                    break;
                }

                string label = Encoding.ASCII.GetString(buffer, offset, labelLength);
                labels.Add(label);
                offset += labelLength;
            }

            return string.Join(".", labels);
        }
    }
}
