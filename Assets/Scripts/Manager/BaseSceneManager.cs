using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class BaseSceneManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float timeLoad = 5f;
    private void OnEnable()
    {
        SetSoundable();
        SetVibable();
        Application.targetFrameRate = 60;
        slider.value = 0f;
        FillBar();
    }

    private static void SetSoundable()
    {
        int soundable = PlayerPrefs.GetInt("soundable", 1);
        AudioSourceManager.Soundable = soundable == 1;
        Debug.Log(AudioSourceManager.Soundable);
    }
    private static void SetVibable()
    {
        int vibable = PlayerPrefs.GetInt("vibable", 1);
        VibrationController.vibable = vibable == 1;
        Debug.Log(VibrationController.vibable);
    }

    void FillBar()
    {
        DOTween.To(() => 0f, x => slider.value = x, 1f, timeLoad)
              .OnComplete(CloseScene);
    }
    void CloseScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
