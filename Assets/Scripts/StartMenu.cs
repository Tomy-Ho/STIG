using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject credits;
    public void OnStartClicked()
    {
        Initiate.Fade("TalisScene", Color.black, 0.5f);
    }

    public void OnCreditClicked()
    {
        credits.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            credits.SetActive(false);
        }
    }
}
