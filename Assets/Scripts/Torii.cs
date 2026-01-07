using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torii : MonoBehaviour
{
    [SerializeField] public Color toricolor = Color.red;
    [SerializeField] PlayerController PC;
    [SerializeField] GameObject barrier;
    [SerializeField] string nextScene = "TalisScene";

    public bool locked = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = toricolor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGate()
    {
        barrier.SetActive(false);
        locked = false;
    }

    public void CloseGate()
    {
        barrier.SetActive(true);
        locked = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!locked)
        {
            Initiate.Fade(nextScene, Color.black, .5f);
        }
    }
}
