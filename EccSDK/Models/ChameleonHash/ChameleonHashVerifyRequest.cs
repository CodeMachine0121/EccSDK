using EccSDK.models.Keys;
using Org.BouncyCastle.Math;

namespace EccSDK.Models.ChameleonHash;

public class ChameleonHashVerifyRequest
{
    public KeyPairDomain KeyPairDomain { get; set; }
    public string Message { get; set; }
    public string StrSignature { get; set; }
}