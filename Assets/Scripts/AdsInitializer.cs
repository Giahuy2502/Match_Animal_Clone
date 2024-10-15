using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : IUnityAdsInitializationListener
{
    /*[SerializeField]*/ string _androidGameId= "5709443";
    //[SerializeField] string _iOSGameId= "5709442";
    /*[SerializeField] */bool _testMode = true;
    public string _gameId="";

    
    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
        Debug.Log("UnityPlatform");
#elif UNITY_ANDROID
        _gameId = _androidGameId;
        Debug.Log("UnityPlatform");

#endif

        Advertisement.Initialize(_gameId, _testMode, this);
        Debug.Log($"game id = {_gameId}");
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
