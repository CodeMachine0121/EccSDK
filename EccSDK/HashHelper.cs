using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;

namespace EccGrpcSDK;

public abstract class HashHelper
{
    public static BigInteger Sha256(string message)
    {
        var bmsg = Encoding.ASCII.GetBytes(message);
        
        var sha256Digest = new Sha256Digest();
        sha256Digest.BlockUpdate(bmsg, 0, bmsg.Length);
        
        var hash = new byte[sha256Digest.GetDigestSize()];
        sha256Digest.DoFinal(hash, 0);
        
        return new BigInteger(hash);
    }
}