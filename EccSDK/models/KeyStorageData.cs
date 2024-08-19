using System.Text.Json;

namespace EccSDK.models;

public class KeyStorageData
{
    public KeyPair KeyPair { get; set; }
    public SessionKey SessionKey { get; set; }

    public void SaveKeys(string keyPath)
    {
        var keyWithJson = JsonSerializer.Serialize(this);
        File.WriteAllText(keyPath, keyWithJson);
    }
}