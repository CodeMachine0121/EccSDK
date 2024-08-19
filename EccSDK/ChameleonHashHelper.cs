using EccSDK.models;
using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
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
        // var msgHash = HashHelper.Sha256(request.Message);
        // var dn = msgHash.Multiply(new BigInteger(request.KeyPairDomain.PrivateKey));
        // return new ChameleonSignature()
        // {
        //     Value = request.SessionKey.Add(dn).Mod(request.Order)
        // };
        return new ChameleonSignature();
    }
    
    public static bool Verify(ChameleonHashRequest request, ECPoint rightChameleonHash)
    {
        var chameleohHash = GetChameleonHash(request);
        return chameleohHash.Value.Equals(rightChameleonHash);
    }

    public static ChameleonHash GetChameleonHash(ChameleonHashRequest request)
    {
        
        return new ChameleonHash();
    }

}