using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EonaCat.DnsTester.Helpers
{
    class DnsHelper
    {
        public static event EventHandler<string> OnLog;
        public static async Task<DnsResponse> SendDnsQueryPacket(string dnsId, string server, int port, byte[] queryBytes)
        {
            // Start the clock
            var startTime = DateTime.Now.Ticks;

            var endPoint = new IPEndPoint(IPAddress.Parse(server), port);
            using (var client = new UdpClient(endPoint.AddressFamily))
            {
                client.DontFragment = true;
                client.EnableBroadcast = false;
                client.Client.SendTimeout = DnsSendTimeout;
                client.Client.ReceiveTimeout = DnsReceiveTimeout;
                byte[] responseBytes = null;
                if (FakeResponse)
                {
                    responseBytes = DnsHelper.GetExampleResponse();
                }
                else
                {
                    await client.SendAsync(queryBytes, queryBytes.Length, endPoint);
                    var responseResult = await client.ReceiveAsync();
                    responseBytes = responseResult.Buffer;
                }

                DnsResponse response = ParseDnsResponsePacket(dnsId, startTime, server, responseBytes);
                return response;
            }
        }

        // For testing purposes
        public static bool FakeResponse { get; set; }

        public static int DnsSendTimeout { get; set; } = 5;
        public static int DnsReceiveTimeout { get; set; } = 5;


        public static byte[] CreateDnsQueryPacket(string domainName, DnsRecordType recordType)
        {
            Random random = new Random();

            // DNS header
            ushort id = (ushort)random.Next(0, 65536);
            ushort flags = (ushort)0x0100; // recursion desired
            ushort qdcount = 1;
            ushort ancount = 0;
            ushort nscount = 0;
            ushort arcount = 0;
            byte[] headerBytes = new byte[]
            {
                (byte)(id >> 8), (byte)(id & 0xff),
                (byte)(flags >> 8), (byte)(flags & 0xff),
                (byte)(qdcount >> 8), (byte)(qdcount & 0xff),
                (byte)(ancount >> 8), (byte)(ancount & 0xff),
                (byte)(nscount >> 8), (byte)(nscount & 0xff),
                (byte)(arcount >> 8), (byte)(arcount & 0xff),
            };

            // DNS question
            string[] labels = domainName.Split('.');
            byte[] qnameBytes = new byte[domainName.Length + 2];
            int qnameIndex = 0;
            foreach (string label in labels)
            {
                qnameBytes[qnameIndex++] = (byte)label.Length;
                foreach (char c in label)
                {
                    qnameBytes[qnameIndex++] = (byte)c;
                }
            }
            qnameBytes[qnameIndex++] = 0;

            byte[] qtypeBytes = new byte[] { (byte)((ushort)recordType >> 8), (byte)((ushort)recordType & 0xff) };
            byte[] qclassBytes = new byte[] { 0, 1 }; // internet class
            byte[] questionBytes = new byte[qnameBytes.Length + qtypeBytes.Length + qclassBytes.Length];
            qnameBytes.CopyTo(questionBytes, 0);
            qtypeBytes.CopyTo(questionBytes, qnameBytes.Length);
            qclassBytes.CopyTo(questionBytes, qnameBytes.Length + qtypeBytes.Length);

            // Combine the header and question to form the DNS query packet
            byte[] queryBytes = new byte[headerBytes.Length + questionBytes.Length];
            headerBytes.CopyTo(queryBytes, 0);
            questionBytes.CopyTo(queryBytes, headerBytes.Length);

            return queryBytes;
        }

        static DnsQuestion ParseDnsQuestionRecord(byte[] queryBytes, ref int offset)
        {
            // Parse the DNS name
            string name = DnsNameParser.ParseName(queryBytes, ref offset);
            if (name == null)
            {
                return null;
            }

            // Parse the DNS type and class
            ushort type = (ushort)((queryBytes[offset] << 8) | queryBytes[offset + 1]);
            ushort qclass = (ushort)((queryBytes[offset + 2] << 8) | queryBytes[offset + 3]);
            offset += 4;

            return new DnsQuestion
            {
                Name = name,
                Type = (DnsRecordType)type,
                Class = (DnsRecordClass)qclass,
            };
        }

        public static byte[] GetExampleResponse()
        {
            // Example response bytes for the A record of google.com
            byte[] response = {
                0x9d, 0xa9, // Query ID
                0x81, 0x80, // Flags
                0x00, 0x01, // Questions: 1
                0x00, 0x01, // Answer RRs: 1
                0x00, 0x00, // Authority RRs: 0
                0x00, 0x00, // Additional RRs: 0
                0x06, 0x67, 0x6f, 0x6f, 0x67, 0x6c, 0x65, // Query: google
                0x03, 0x63, 0x6f, 0x6d, // Query: com
                0x00,
                0x00, 0x01, // Record type: A
                0x00, 0x01, // Record class: IN
                0xc0, 0x0c, // Name pointer to google.com
                0x00, 0x01, // Record type: A
                0x00, 0x01, // Record class: IN
                0x00, 0x00, 0x00, 0x3d, // TTL: 61 seconds
                0x00, 0x04, // Data length: 4 bytes
                0xac, 0xd9, 0x03, 0x3d // Data: 172.217.3.61
            };
            return response;
        }

        static DnsResponse ParseDnsResponsePacket(string dnsId, long startTime, string server, byte[] responseBytes)
        {
            Console.WriteLine("new byte[] { " + responseBytes + " }");

            // Check if response is valid
            if (responseBytes.Length < 12)
            {
                throw new Exception("Invalid DNS response");
            }

            // Set the offset to the start
            var offset = 0;

            // Parse the DNS header
            ushort id = (ushort)((responseBytes[0] << 8) | responseBytes[1]);
            ushort flags = (ushort)((responseBytes[2] << 8) | responseBytes[3]);
            bool isResponse = (flags & 0x8000) != 0;
            ushort qdcount = (ushort)((responseBytes[4] << 8) | responseBytes[5]);
            ushort ancount = (ushort)((responseBytes[6] << 8) | responseBytes[7]);

            if (!isResponse)
            {
                throw new Exception("Invalid DNS response");
            }

            ushort nscount = (ushort)((responseBytes[8] << 8) | responseBytes[9]);
            ushort arcount = (ushort)((responseBytes[10] << 8) | responseBytes[11]);
            
            // We parsed the header set the offset past the header
            offset = 12;

            List<DnsQuestion> questions = new List<DnsQuestion>();

            for (int i = 0; i < qdcount; i++)
            {
                DnsQuestion question = ParseDnsQuestionRecord(responseBytes, ref offset);
                if (question != null)
                {
                    questions.Add(question);
                }
            }

            // Parse the DNS answer records
            List<ResourceRecord> answers = new List<ResourceRecord>();
            for (int i = 0; i < ancount; i++)
            {
                try
                {
                    ResourceRecord answer = ParseDnsAnswerRecord(responseBytes, ref offset);
                    if (answer != null)
                    {
                        answers.Add(answer);
                    }
                }
                catch (Exception exception)
                {
                    OnLog?.Invoke(null, $"Answer exception: " + exception.Message);
                }
            }

            // Parse the DNS authority records
            List<ResourceRecord> authorities = new List<ResourceRecord>();
            for (int i = 0; i < nscount; i++)
            {
                try
                {
                    ResourceRecord authority = ParseDnsAnswerRecord(responseBytes, ref offset);
                    if (authority != null)
                    {
                        authorities.Add(authority);
                    }
                }
                catch (Exception exception)
                {
                    OnLog?.Invoke(null, $"Authority answer exception: " + exception.Message);
                }
            }

            // Parse the DNS additional records
            List<ResourceRecord> additionals = new List<ResourceRecord>();
            for (int i = 0; i < arcount; i++)
            {
                try
                {
                    ResourceRecord additional = ParseDnsAnswerRecord(responseBytes, ref offset);
                    if (additional != null)
                    {
                        additionals.Add(additional);
                    }
                }
                catch (Exception exception)
                {
                    OnLog?.Invoke(null, $"Additional answer exception: " + exception.Message);
                }
            }

            return new DnsResponse
            {
                Id = id,
                StartTime = startTime,
                Resolver = server,
                Flags = flags,
                Class = (DnsRecordClass)((flags >> 3) & 0x0f),
                DnsId = dnsId,
                Questions = questions,
                Answers = answers,
                Authorities = authorities,
                Additionals = additionals,
            };
        }

        static ResourceRecord ParseDnsAnswerRecord(byte[] responseBytes, ref int offset)
        {
            // Parse the DNS name
            string name = DnsNameParser.ExtractDomainName(responseBytes, ref offset);
            if (name == null)
            {
                return null;
            }

            // Parse the DNS type, class, ttl, and data length
            DnsRecordType type = (DnsRecordType)((responseBytes[offset++] << 8) + responseBytes[offset++]);
            DnsRecordClass klass = (DnsRecordClass)((responseBytes[offset++] << 8) + responseBytes[offset++]);
            int ttl = (responseBytes[offset++] << 24) + (responseBytes[offset++] << 16) + (responseBytes[offset++] << 8) + responseBytes[offset++];

            // Extract record data length
            int dataLength = (responseBytes[offset] << 8) + responseBytes[offset + 1];
            offset += 2;

            // Extract record data
            byte[] recordData = new byte[dataLength];
            Buffer.BlockCopy(responseBytes, offset, recordData, 0, dataLength);

            string recordDataAsString = null;
            switch ((DnsRecordType)type)
            {
                case DnsRecordType.A:
                    if (dataLength != 4)
                    {
                        return null;
                    }
                    recordDataAsString = new IPAddress(recordData).ToString();
                    offset += recordData.Length;
                    break;
                case DnsRecordType.CNAME:
                    recordDataAsString = DnsNameParser.ExtractDomainName(responseBytes, ref offset);
                    break;
                case DnsRecordType.NS:
                    recordDataAsString = DnsNameParser.ExtractDomainName(responseBytes, ref offset);
                    break;
                case DnsRecordType.MX:
                    int preference = (responseBytes[0] << 8) + responseBytes[1];
                    offset += 2;
                    string exchange = DnsNameParser.ExtractDomainName(responseBytes, ref offset);
                    recordDataAsString = $"{preference} {exchange}";
                    break;
                case DnsRecordType.TXT:
                    recordDataAsString = Encoding.ASCII.GetString(recordData);
                    break;
                default:
                    offset += dataLength;
                    break;
            }

            return new ResourceRecord
            {
                Name = name,
                Type = type,
                Class = klass,
                Ttl = TimeSpan.FromSeconds(ttl),
                Data = recordDataAsString,
            };
        }

        static string GetString(byte[] bytes, ref int index, int length)
        {
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += (char)bytes[index++];
            }
            return str;
        }
    }

    public class DnsQuestion
    {
        public string Name { get; set; }
        public DnsRecordType Type { get; set; }
        public DnsRecordClass Class { get; set; }
    }

    public class ResourceRecord
    {
        public string Name { get; set; }
        public DnsRecordType Type { get; set; }
        public string Data { get; set; }
        public DnsRecordClass Class { get; set; }
        public TimeSpan Ttl { get; set; }
        public ushort DataLength { get; set; }
    }

    public class DnsResponse
    {
        public ushort Id { get; set; }
        public ushort Flags { get; set; }
        public DnsRecordClass Class { get; set; }
        public List<ResourceRecord> Answers { get; set; }
        public long StartTime { get; set; }
        public string Resolver { get; set; }
        public string DnsId { get; set; }
        public List<DnsQuestion> Questions { get; set; }
        public List<ResourceRecord> Authorities { get; set; }
        public List<ResourceRecord> Additionals { get; set; }
    }

    public enum DnsRecordType : ushort
    {
        A = 1,
        NS = 2,
        CNAME = 6,
        MX = 15,
        TXT = 16,
        AAAA = 28,
    }

    public enum DnsRecordClass : ushort
    {
        /// <summary>
        ///   The Internet.
        /// </summary>
        Internet = 1,

        /// <summary>
        ///   The CSNET class (Obsolete - used only for examples insome obsolete RFCs).
        /// </summary>
        CS = 2,

        /// <summary>
        ///   The CHAOS class.
        /// </summary>
        CH = 3,

        /// <summary>
        ///   Hesiod[Dyer 87].
        /// </summary>
        HS = 4,

        /// <summary>
        ///   Used in UPDATE message to signify no class.
        /// </summary>
        None = 254,

        /// <summary>
        ///   Only used in QCLASS.
        /// </summary>
        /// <seealso cref="Question.Class"/>
        ANY = 255
    }
}
