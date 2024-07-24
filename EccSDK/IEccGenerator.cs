using EccGrpcSDK.models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;

namespace EccGrpcSDK;

public interface IEccGenerator
{
    KeyPair GenerateKeyPair(int keySize);
}