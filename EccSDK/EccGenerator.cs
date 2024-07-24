using EccSDK.models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace EccSDK;

public static class EccGenerator
{
    public static KeyPair GenerateKeyPair(int keySize)
    {
        var gen = new ECKeyPairGenerator("ECDSA");
        gen.Init(new KeyGenerationParameters(new SecureRandom(), keySize));

        var keyGen = gen.GenerateKeyPair();

        var privateKey = (ECPrivateKeyParameters) keyGen.Private;
        var publicKey = (ECPublicKeyParameters) keyGen.Public;

        var generateKeyPair = new KeyPair()
        {
            PublicKey = publicKey.Q, 
            PrivateKey = privateKey.D,
            BasePoint = publicKey.Parameters.G,
            KeySize = keySize 
        };
        
        return generateKeyPair;
    }
    
}