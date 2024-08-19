using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
using Org.BouncyCastle.Math.EC;

namespace EccSDK.Services.Interfaces;

public interface IChameleonHashService
{
    ChameleonSignature Sign(string message);
    bool Verify(ChameleonHashVerifyRequest verifyRequest);
    ChameleonHash CalculateChameleonHashBy(ChameleonHashRequest request);
    ChameleonHash GetChameleonHash();
}