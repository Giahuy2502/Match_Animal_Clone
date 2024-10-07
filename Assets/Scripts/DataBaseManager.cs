using System;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Enum to define the keys for PlayerPrefs
    public enum DatabaseKey
    {
        HighScore,
        PlayerName,
        Coin,
        undoCount,
        magCount,
        sortCount
    }

    // Generic method to save data to PlayerPrefs
    public static void SaveData<T>(DatabaseKey key, T data)
    {
        string keyString = key.ToString();
        string dataString = string.Empty;
        var datatType = typeof(T);
        if (datatType.IsPrimitive || datatType == typeof(string))
        {
            dataString = data.ToString();
        }
        else
        {
            dataString = JsonUtility.ToJson(data);
        }
        PlayerPrefs.SetString(keyString, dataString);
        PlayerPrefs.Save();
        Debug.Log($"Save: {keyString}: {dataString}");
    }
    // Generic method to load data from PlayerPrefs
    public static T LoadData<T>(DatabaseKey key, string defaultValue = "")
    {
        string keyString = key.ToString();
        string dataString = PlayerPrefs.GetString(keyString, defaultValue);
        if (PlayerPrefs.HasKey(keyString) || !string.IsNullOrEmpty(dataString))
        {
            Debug.Log($"Load: {keyString}: {dataString}");
            var datatType = typeof(T);
            if (datatType.IsPrimitive || datatType == typeof(string))
            {
                Debug.Log($"string data: {dataString}");
                return (T)Convert.ChangeType(dataString, typeof(T));
            }
            else
            {
                return JsonUtility.FromJson<T>(dataString);
            }
        }
        else
        {
            Debug.Log($"Load: {keyString}: default");
            return default(T);
        }
    }
    // Example usage
    //public static void ExampleUsage()
    //{
    //    // Save high score
    //    int highScore = 1000;
    //    SaveData(DatabaseKey.HighScore, highScore);

    //    // Load high score
    //    int loadedHighScore = LoadData<int>(DatabaseKey.HighScore);
    //    Debug.Log("High Score: " + loadedHighScore);

    //    // Save player name
    //    string playerName = "John Doe";
    //    //SaveData(DatabaseKey.PlayerName, playerName);

    //    // Load player name
    //    string loadedPlayerName = LoadData<string>(DatabaseKey.PlayerName);
    //    Debug.Log("Player Name: " + loadedPlayerName);
    //}
}