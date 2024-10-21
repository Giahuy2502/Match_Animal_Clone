using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization.Scripts;
using TMPro;

public class LanguageText : MonoBehaviour
{
    [SerializeField] KeyTxt key;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        MultiLanguage.multiLanguage.AddListener(UpdateText);
        UpdateText();
    }
    private void OnValidate()
    {
        text=GetComponent<TextMeshProUGUI>();
    }
    private void UpdateText()
    {
        if (text == null) Debug.Log("Nulll");
        string keyString= ConvertEnumToString.GetStringByKey(key);
        text.text=LocalizationManager.Localize(keyString);
        Debug.Log("sdbcgb");
    }
}
