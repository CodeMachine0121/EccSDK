using EccSDK.models;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace EccSDK;

public static class ChameleonHashHelper
{
    // Kn = public key
    // kn = private key
    
    public static ChameleonSignature Sign(ChameleonHashRequest request)
    {
        // sign = (sessionKey - dn) mod n
        // dn = H(m) * kn
        var msgHash = HashHelper.Sha256(request.Message);
        var dn = msgHash.Multiply(request.KeyPair.PrivateKey);
        return new ChameleonSignature()
        {
            Value = request.SessionKey.Add(dn).Mod(request.Order)
        };
    }
    
    public static bool Verify(ChameleonHashRequest request, ECPoint rightChameleonHash)
    {
        var chameleohHash = GetChameleonHash(request);
        return chameleohHash.Value.Equals(rightChameleonHash);
    }

    public static ChameleonHash GetChameleonHash(ChameleonHashRequest request)
    {
        // chameleonHash = [Kn x H(m)] + [P x sessionKey]
        var msgHash = HashHelper.Sha256(request.Message);
        var rP = request.KeyPair.BasePoint.Multiply(request.Signature);

        return new ChameleonHash()
        {
            Value = request.KeyPair.PublicKey.Multiply(msgHash).Add(rP).Normalize() 
        };
    }

}

public class ChameleonHash
{
    public ECPoint Value { get; set; }
}

public class ChameleonSignature
{
    public BigInteger Value { get; set; }
}