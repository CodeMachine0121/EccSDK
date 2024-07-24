using EccGrpcSDK.models;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace EccGrpcSDK;

public class ChameleonHashHelper
{
    // Kn = public key
    // kn = private key
    
    public static BigInteger Sign(ChameleonHashRequest request)
    {
        // sign = (sessionKey - dn) mod n
        // dn = H(m) * kn
        var msgHash = HashHelper.Sha256(request.Message);
        var dn = msgHash.Multiply(request.KeyPair.PrivateKey);
        return request.SessionKey.Add(dn).Mod(request.Order);
    }
    
    public static bool Verify(ChameleonHashRequest request, ECPoint rightChameleonHash)
    {
        return GetChameleonHash(request).Equals(rightChameleonHash);
    }

    private static ECPoint GetChameleonHash(ChameleonHashRequest request)
    {
        // chameleonHash = [Kn x H(m)] + [P x sessionKey]
        var msgHash = HashHelper.Sha256(request.Message);
        var rP = request.KeyPair.BasePoint.Multiply(request.Signature);
        var chameleonHash = request.KeyPair.PublicKey.Multiply(msgHash).Add(rP).Normalize();
        return chameleonHash;
    }
}