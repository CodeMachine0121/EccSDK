using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace EccGrpcSDK.models;

public class KeyPair
{
    public BigInteger PrivateKey { get; set; }
    public ECPoint PublicKey { get; set; }
}