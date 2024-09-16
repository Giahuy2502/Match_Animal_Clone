using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorePanelManager : MonoBehaviour
{
    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
