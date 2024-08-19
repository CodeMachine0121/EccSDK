using System.Text.Json;
using EccSDK.Models.Keys;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace EccSDK.models.Keys;

public class KeyPairDomain
{
    public ECPrivateKeyParameters PrivateKey { get; set; }
    public ECPublicKeyParameters PublicKey { get; set; }
    public BigInteger SessionKey { get; set; }

    public void SaveAsFile(string path)
    {
        File.WriteAllText(path, JsonSerializer.Serialize(new KeyPairSaved
        {
            StrPrivateKey = PrivateKey.D.ToString(16),
            StrSessionKey = SessionKey.ToString(16)
        }));
    }
}