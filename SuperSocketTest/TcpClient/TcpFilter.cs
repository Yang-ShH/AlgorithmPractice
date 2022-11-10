using System.Text;
using SuperSocket.ProtoBase;

namespace SuperSocketTest.TcpClient
{
    public class TcpFilter : BeginEndMarkReceiveFilter<StringPackageInfo>
    {
        public static byte[] BeginBytes = new byte[] { 0x02, 0x02, 0x02, 0x02 };
        public static byte[] EndBytes = new byte[] { 0x03, 0x03, 0x03, 0x03 };
        public TcpFilter() : base(BeginBytes, EndBytes)
        {
        }

        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var body = bufferStream.ReadString((int) bufferStream.Length, Encoding.UTF8);
            body = body.Remove(body.Length - EndBytes.Length, EndBytes.Length);
            body = body.Remove(0, BeginBytes.Length);
            return new StringPackageInfo("", body, new string[] { });
        }
    }
}
