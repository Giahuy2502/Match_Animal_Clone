using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class BaseSceneManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float timeLoad = 5f;
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        slider.value = 0f;
        FillBar();
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
