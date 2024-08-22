using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
using EccSDK.models.Keys;
using EccSDK.Services.Interfaces;
using Org.BouncyCastle.Math;

namespace EccSDK.Services;

public class ChameleonHashService : IChameleonHashService
{
    private readonly ChameleonHash _chameleonHash;
    private readonly KeyPairDomain _keyPairDomain;

    // Kn = public key
    // kn = private key
    public ChameleonHashService(KeyPairDomain keyPairDomain)
    {
        _keyPairDomain = keyPairDomain;
        _chameleonHash = GetChameleonHash ("init chameleon hash");
    }


    public ChameleonSignature Sign(string message)
    {
        // sign = (sessionKey - dn) mod n
        // dn = H(m) * kn
        var msgHash = HashHelper.Sha256(message);
        var dn = msgHash.Multiply(_keyPairDomain.PrivateKey.D);

        var signature = _keyPairDomain.SessionKey.Subtract(dn).Mod(_keyPairDomain.PublicKey.Parameters.N);

        return new ChameleonSignature
        {
            Value = signature.ToString(16)
        };
    }

    public bool Verify(ChameleonHashVerifyRequest verifyRequest)
    {
        var chameleonHashRequest = new ChameleonHashRequest
        {
            Message = verifyRequest.Message,
            KeyPairDomain = verifyRequest.KeyPairDomain,
            Signature = new BigInteger(verifyRequest.StrSignature, 16)
        };

        var chameleonHashCalculated = CalculateChameleonHashBy(chameleonHashRequest);

        return chameleonHashCalculated.Value.Equals(_chameleonHash.Value);
    }

    public ChameleonHash GetChameleonHash(string message)
    {
        var signature = Sign(message);
        return CalculateChameleonHashBy(new ChameleonHashRequest
        {
            Message = message,
            KeyPairDomain = _keyPairDomain,
            Signature = new BigInteger(signature.Value, 16)
        });
    }
    
    private ChameleonHash CalculateChameleonHashBy(ChameleonHashRequest request)
    {
        // chameleonHash = [Kn x H(m)] + [P x signature] 
        var hashedMessage = HashHelper.Sha256(request.Message);
        var knHash = request.KeyPairDomain.PublicKey.Q.Multiply(hashedMessage);
        var pSignature = request.KeyPairDomain.PublicKey.Parameters.G.Multiply(request.Signature);

        return new ChameleonHash
        {
            Value = knHash.Add(pSignature)
        };
    }

}