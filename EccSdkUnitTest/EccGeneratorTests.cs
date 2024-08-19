using EccSDK;

namespace EccSdkUnitTest;

[TestFixture]
public class EccGeneratorTests
{

    [Test]
    public void key_should_be_same()
    {
        var keyDomain1 = EccGenerator.GetKeyDomain();
        var keyDomain2 = EccGenerator.GetKeyDomain();
        
        Assert.Multiple(() =>
        {
            Assert.That(keyDomain2.PrivateKey.D, Is.EqualTo(keyDomain1.PrivateKey.D));
            Assert.That(keyDomain2.SessionKey, Is.EqualTo(keyDomain1.SessionKey));
        });
    }
}