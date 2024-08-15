using EccSDK;
using EccSDK.models;
using Org.BouncyCastle.Math;

namespace EccSdkUnitTest;

[TestFixture]
public class EccSdkTests
{
    private KeyPair _keyPair;

    [SetUp]
    public void SetUp()
    {
        _keyPair = EccGenerator.GenerateKeyPair(256);
    }

    [Test]
    public void should_pass_verification()
    {
        var sign = ChameleonHashHelper.Sign(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World",
            Order = _keyPair.PublicKey.Curve.Order,
            SessionKey = new BigInteger("1234567890")
        });

        var chameleonHash = ChameleonHashHelper.GetChameleonHash(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World",
            Order = _keyPair.PublicKey.Curve.Order,
            SessionKey = new BigInteger("1234567890"),
            Signature = sign.Value
        });

        var result = ChameleonHashHelper.Verify(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World",
            Order = _keyPair.PublicKey.Curve.Order,
            Signature = sign.Value
        }, chameleonHash.Value);

        Assert.That(result, Is.True);
    }

    [Test]
    public void should_not_pass()
    {
        var sign = ChameleonHashHelper.Sign(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World",
            Order = _keyPair.PublicKey.Curve.Order,
            SessionKey = new BigInteger("1234567890")
        });

        var chameleonHash = ChameleonHashHelper.GetChameleonHash(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World",
            Order = _keyPair.PublicKey.Curve.Order,
            SessionKey = new BigInteger("1234567890"),
            Signature = sign.Value
        });

        var result = ChameleonHashHelper.Verify(new ChameleonHashRequest
        {
            KeyPair = _keyPair,
            Message = "Hello World123",
            Order = _keyPair.PublicKey.Curve.Order,
            Signature = sign.Value
        }, chameleonHash.Value);

        Assert.That(result, Is.False);
        
    }
}