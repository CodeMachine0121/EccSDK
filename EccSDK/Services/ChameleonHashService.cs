using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
using EccSDK.models.Keys;
using EccSDK.Services.Interfaces;
using Org.BouncyCastle.Math;

namespace EccSDK.Services;

public class ChameleonHashService : IChameleonHashService
{
    private readonly KeyPairDomain _keyPairDomain;
    private readonly ChameleonHash _chameleonHash;

    // Kn = public key
    // kn = private key
    public ChameleonHashService(KeyPairDomain keyPairDomain)
    {
        _keyPairDomain = keyPairDomain;
        _chameleonHash = GetChameleonHash(keyPairDomain);
    }



    public ChameleonSignature Sign(string message)
    {
        // sign = (sessionKey - dn) mod n
        // dn = H(m) * kn
        var msgHash = HashHelper.Sha256(message);
        var dn = msgHash.Multiply(_keyPairDomain.PrivateKey.D);

        var signature = _keyPairDomain.SessionKey.Add(dn).Mod(_keyPairDomain.PublicKey.Parameters.N);

        return new ChameleonSignature
        {
            Value = signature.ToString(16)
        };
    }

    public bool Verify(ChameleonHashVerifyRequest verifyRequest)
    {
        var chameleonHashRequest = new ChameleonHashRequest()
        {
            Message = verifyRequest.Message,
            KeyPairDomain = verifyRequest.KeyPairDomain,
            Signature = new BigInteger(verifyRequest.StrSignature, 16) 
        };
        
        var chameleonHashCalculated = GetChameleonHashBy(chameleonHashRequest);
        
        return chameleonHashCalculated.Value.Equals(_chameleonHash.Value);
    }

    public ChameleonHash GetChameleonHashBy(ChameleonHashRequest request)
    {
        // chameleonHash = [Kn x H(m)] + [P x signature] 
        var hashedMessage = HashHelper.Sha256(request.Message);
        var knHash = request.KeyPairDomain.PublicKey.Q.Multiply(hashedMessage);
        var pSignature =request.KeyPairDomain.PublicKey.Parameters.G.Multiply(request.Signature);

        return new ChameleonHash()
        {
            Value = knHash.Add(pSignature)
        };
    }
    
    private ChameleonHash GetChameleonHash(KeyPairDomain keyPairDomain)
    {
        var signature = Sign("init chameleon hash");
        return GetChameleonHashBy(new ChameleonHashRequest()
        {
            Message = "init chameleon hash",
            KeyPairDomain = keyPairDomain,
            Signature = new BigInteger(signature.Value, 16) 
        });
    }
}