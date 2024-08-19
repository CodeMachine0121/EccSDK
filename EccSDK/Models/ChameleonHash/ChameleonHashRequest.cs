using EccSDK.models.Keys;
using Org.BouncyCastle.Math;

namespace EccSDK.Models.ChameleonHash;

public class ChameleonHashRequest
{
    public KeyPairDomain KeyPairDomain { get; set; }
    public string Message { get; set; }
    public BigInteger SessionKey { get; set; }
    public BigInteger Order { get; set; }
    public BigInteger Signature { get; set; }
}