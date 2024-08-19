# EccSDK 
- EccSDK is a library that provides a simple way to interact with Chameleon Hash based on the ECC API.
- You can search this package in this [nuget page](https://www.nuget.org/packages/EccSDK)

## Usage for 1.1.*
- add EccGenerator to generate key pair and stored into a file
- add KeyPairDomain model to handle key pair and session key
```csharp
var keyPairDomain = EccGenerator.GetKeyDomain();

var chameleonHashService = new ChameleonHashService(keyPairDomain);

var chameleonSignature = chameleonHashService.Sign("Hello world");

chameleonHashService.Verify(new ChameleonHashVerifyRequest()
{
    KeyPairDomain = keyPairDomain,
    Message = "Hello world",
    StrSignature = chameleonSignature.Value
});
```
---

## Usage for 1.0.*
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
