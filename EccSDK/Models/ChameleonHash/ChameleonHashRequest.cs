using EccSDK.models.Keys;
using Org.BouncyCastle.Math;

namespace EccSDK.Models.ChameleonHash;

public class ChameleonHashRequest
{
    public string Message { get; set; }
    public KeyPairDomain KeyPairDomain { get; set; }
    public BigInteger Signature { get; set; }
}