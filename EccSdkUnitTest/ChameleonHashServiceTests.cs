using EccSDK;
using EccSDK.Models.ChameleonHash;
using EccSDK.Services;
using NUnit.Framework;

namespace EccSdkUnitTest;

[TestFixture]
public class ChameleonHashServiceTests
{
    [Test]
    public void should_be_pass()
    {
        var keyPairDomain = EccGenerator.GetKeyDomain();

        var chameleonHashService = new ChameleonHashService(keyPairDomain);

        var chameleonSignature = chameleonHashService.Sign("Hello world");
        
        chameleonHashService.Verify(new ChameleonHashVerifyRequest()
        {
            KeyPairDomain = keyPairDomain,
            Message = "Hello world",
            StrSignature = chameleonSignature.Value
        });
    }
    
}