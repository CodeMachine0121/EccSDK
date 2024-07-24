# EccSDK 
EccSDK is a library that provides a simple way to interact with Chameleon Hash based on the ECC API.

## Usage
```csharp
var keyPair = EccKeyPair.GenerateKeyPair();

var signature = ChameleonHashHelper.Sign(new ChameleonHashRequest
{
    KeyPair = _keyPair,
    Message = "Hello World",
    Order = keyPair.PublicKey.Curve.Order,
    SessionKey = new BigInteger("1234567890")
});

var chameleonHash = ChameleonHashHelper.GetChameleonHash(new ChameleonHashRequest
{
    KeyPair = _keyPair,
    Message = "Hello World",
    Order = keyPair.PublicKey.Curve.Order,
    SessionKey = new BigInteger("1234567890"),
    Signature = sign
});

var result = ChameleonHashHelper.Verify(new ChameleonHashRequest
{
    KeyPair = _keyPair,
    Message = "Hello World",
    Order = keyPair.PublicKey.Curve.Order,
    Signature = sign
}, chameleonHash);
```