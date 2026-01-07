using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GummyBearController : MonoBehaviour
{

    [SerializeField] public Color color = Color.white;
    ShootingAI bulletshoot;
    [SerializeField] float bulletdelay = 1.2f;
    [SerializeField] bool shootRandom = false;
    [SerializeField] bool randomColor = true;
    float currenttimer;
    bool TimerActive = true;
    List<Color> randColors = new List<Color> { Color.red, Color.blue, Color.cyan, Color.green, Color.grey, Color.magenta, Color.yellow };
    void Start()
    {
        if (randomColor)
        {
            color = randColors[UnityEngine.Random.Range(0, randColors.Count)];
        }



        GetComponent<SpriteRenderer>().color = color;
        bulletshoot = GetComponent<ShootingAI>();
        currenttimer = bulletdelay;
    }
    void Update()
    {
        if (TimerActive && currenttimer > 0.0f)
        {
            currenttimer -= Time.deltaTime;
        }
        if (currenttimer <= 0.0f)
        {
            TimerActive = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (TimerActive == false)
        {
            if (col.CompareTag("Player"))
            {
                if (shootRandom)
                {
                    bulletshoot.ShootRandom();
                }
                else
                {
                    bulletshoot.ShootPlayer();
                }
            }
            currenttimer = bulletdelay;
            TimerActive = true;
        }
    }
}
