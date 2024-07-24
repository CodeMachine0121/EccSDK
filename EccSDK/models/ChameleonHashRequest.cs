using Org.BouncyCastle.Math;

namespace EccSDK.models;

public class ChameleonHashRequest
{
    public KeyPair KeyPair { get; set; }
    public string Message { get; set; }
    public BigInteger SessionKey { get; set; }
    public BigInteger Order { get; set; }
    public BigInteger Signature { get; set; }
}