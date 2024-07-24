using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace EccSDK.models;

public class KeyPair
{
    public BigInteger PrivateKey { get; set; }
    public ECPoint PublicKey { get; set; }
    public ECPoint BasePoint { get; set; }
    public int KeySize { get; set; }
}