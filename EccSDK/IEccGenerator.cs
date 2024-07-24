using EccGrpcSDK.models;
using Org.BouncyCastle.Crypto;

namespace EccGrpcSDK;

public interface IEccGenerator
{
    KeyPair GenerateKeyPair(int keySize);
}