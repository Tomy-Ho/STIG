using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWater : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] public Color defaultColor = Color.white;
    [SerializeField] string cleantext = "Take a shower immediately!";
    [SerializeField] Sprite cleanWaterSprite;
    [SerializeField] Sprite dirtyWaterSprite;
    [SerializeField] float TimeSec = 5.0f;
    float currentTimer;
    bool TimerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = TimeSec;
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActive && currentTimer >= 0.0f)
        {
            currentTimer -= Time.deltaTime;
        }
        if (currentTimer <= 0.0f)
        {
            TimerActive = false;
            GetComponent<SpriteRenderer>().sprite = cleanWaterSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (TimerActive == false))
        {
            if (PC.CollectedColors.Count > 0)
            {
                PC.GetComponent<SpriteRenderer>().color = defaultColor;
                PC.CollectedColors.Clear();
                Debug.Log(cleantext);
                currentTimer = TimeSec;
                TimerActive = true;
                GetComponent<SpriteRenderer>().sprite = dirtyWaterSprite;
            }
        }
    }
}
