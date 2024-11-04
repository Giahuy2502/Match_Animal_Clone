#if UNITY_ANDROID
using UnityEngine;

public class VibrationController : MonoSingleton<VibrationController>
{
    private AndroidJavaObject vibrator;
    public static bool vibable = true;

    protected override void DoOnAwake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            if (vibrator != null && !vibrator.Call<bool>("hasVibrator"))
            {
                Debug.LogWarning("Thiết bị không hỗ trợ rung.");
                vibrator = null;
            }
        }
    }

    public void StartVibration(long milliseconds=500)
    {
        if (!vibable) return;
        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
    }

    public void StopVibration()
    {
        if (vibrator != null)
        {
            vibrator.Call("cancel");
        }
    }
}
#endif
