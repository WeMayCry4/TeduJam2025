using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header(" Interactions : ")]
    [SerializeField] Text interactionText;


    public void EnableInteractionTest(string text)
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = text;
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
