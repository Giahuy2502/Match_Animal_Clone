
using UnityEngine;
using Assets.SimpleLocalization.Scripts;
using UnityEngine.Events;

public class MultiLanguage : MonoBehaviour
{
    public static UnityEvent multiLanguage = new UnityEvent();
    private void Awake()
    {
        LocalizationManager.Read();
    }
    public void Language(string language)
    {
        LocalizationManager.Language=language;

        multiLanguage.Invoke();
    }
}
