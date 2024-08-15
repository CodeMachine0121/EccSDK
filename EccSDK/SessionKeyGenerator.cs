using Org.BouncyCastle.Math;

namespace EccSDK;

public class SessionKeyGenerator
{
    public static BigInteger GenerateSessionKey()
    {
        return new BigInteger(256, new Random());
    }
}