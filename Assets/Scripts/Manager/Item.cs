using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelTxt;
    public Level levelData;
    [SerializeField] Button button;
    

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickButton);
    }
    public void OnClickButton()
    {
        BoardManager.levelCurrent = level;
        SceneManager.LoadScene(1);
    }
}