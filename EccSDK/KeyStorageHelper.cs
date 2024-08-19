using System.Text.Json;
using EccSDK.models;

namespace EccSDK;

public static class KeyStorageHelper
{
    private const string KeysPath = "~/keys.json";

    public static KeyStorageData LoadKeyPair()
    {
        if (File.Exists(KeysPath))
        {
            return RestoreKeys();
        }

        var keyStorageData = new KeyStorageData
        {
            KeyPair = EccGenerator.GenerateKeyPair(256),
            SessionKey = SessionKeyGenerator.GenerateSessionKey()
        };

        keyStorageData.SaveKeys(KeysPath);

        return keyStorageData;
    }

    private static KeyStorageData RestoreKeys()
    {
        var keyData = File.ReadAllText(KeysPath);
        return JsonSerializer.Deserialize<KeyStorageData>(keyData)!;
    }
}