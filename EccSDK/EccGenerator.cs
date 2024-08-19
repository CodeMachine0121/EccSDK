using System.Text.Json;
using EccSDK.models.Keys;
using EccSDK.Models.Keys;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using BigInteger = Org.BouncyCastle.Math.BigInteger;

namespace EccSDK;

public static class EccGenerator
{
    private static readonly string KeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "key.json");
    private static readonly X9ECParameters Curve = SecNamedCurves.GetByName("secp256k1");
    private static readonly ECDomainParameters Domain = new(Curve.Curve, Curve.G, Curve.N, Curve.H);

    public static KeyPairDomain GetKeyDomain()
    {
        if (File.Exists(KeyPath))
        {
            var keyInText = File.ReadAllText(KeyPath);
            var keyPairSaved = JsonSerializer.Deserialize<KeyPairSaved>(keyInText);
            return LoadEccKeyPair(keyPairSaved);
        }

        return GenerateKeyPairWithSessionKey();
    }

    private static KeyPairDomain GenerateKeyPairWithSessionKey()
    {
        var keyGen = new ECKeyPairGenerator();
        var keyGenParam = new ECKeyGenerationParameters(Domain, new SecureRandom());
        keyGen.Init(keyGenParam);
        var keyPair = keyGen.GenerateKeyPair();
        
        var keyPairDomain = new KeyPairDomain
        {
            PrivateKey = (ECPrivateKeyParameters)keyPair.Private,
            PublicKey = (ECPublicKeyParameters)keyPair.Public,
            SessionKey = new BigInteger(256, new SecureRandom())
        };
        
        keyPairDomain.SaveAsFile(KeyPath);
        return keyPairDomain;
    }


    private static KeyPairDomain LoadEccKeyPair(KeyPairSaved keyPairSaved)
    {
        var privateKeyD = new BigInteger(keyPairSaved.StrPrivateKey, 16);
        var privateKey = new ECPrivateKeyParameters(privateKeyD, Domain);

        var q = Domain.G.Multiply(privateKey.D);
        var publicKey = new ECPublicKeyParameters(q, Domain);

        return new KeyPairDomain
        {
            PrivateKey = privateKey,
            PublicKey = publicKey,
            SessionKey = new BigInteger(keyPairSaved.StrSessionKey, 16)
        };
    }
}