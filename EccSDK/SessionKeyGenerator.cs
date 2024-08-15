using EccSDK.models;
using Org.BouncyCastle.Math;

namespace EccSDK;

public class SessionKeyGenerator
{
    public static SessionKey GenerateSessionKey()
    {
        return new SessionKey()
        {
            Key =  new BigInteger(256, new Random())
        };
    }
}