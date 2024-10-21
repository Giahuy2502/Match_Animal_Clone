using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayButton : MonoBehaviour
{
    [SerializeField] Button itemButton;
    [SerializeField] float timedelay=1f;
    private void Start()
    {
        itemButton = this.gameObject.GetComponent<Button>();
    }
    public void OnClick()
    {
        itemButton.interactable = false;
        Invoke("Delay",timedelay);
    }
    private void Delay()
    {
        itemButton.interactable=true;
    }
}
