using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 MovementSpeed = new Vector2(1.0f, 1.0f);
    private new Rigidbody2D rigidbody2D;
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);

    public HashSet<Color> CollectedColors { get; set; }
    [SerializeField] List<Torii> toriiGates;

    [SerializeField] Animator animator;
    [SerializeField] public Color DefaultColor = Color.white;

    public GameObject BulletObject;

    SpriteRenderer sr;

    bool flipSprite = false;


    void Awake()
    {
        CollectedColors = new();

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        rigidbody2D.angularDamping = 0.0f;
        rigidbody2D.gravityScale = 0.0f;

        //Camera.main.orthographicSize = 5;

        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Camera.main.orthographicSize += 0.25f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Camera.main.orthographicSize -= 0.25f;
        }

        Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 1.0f);
        Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, 8.0f);

        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.D))
        {
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = false;
        }

    }

    void FixedUpdate()
    {
        // Rigidbody2D affects physics so any ops on it should happen in FixedUpdate
        // See why here: https://learn.unity.com/tutorial/update-and-fixedupdate#
        rigidbody2D.MovePosition(rigidbody2D.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GummyBear"))
        {

            CollectedColors.Add(col.gameObject.GetComponent<GummyBearController>().color);

            Color resultColor = Color.black;
            foreach (var color in CollectedColors)
            {
                resultColor += color;
            }
            //resultColor /= CollectedColors.Count;
            sr.color = col.gameObject.GetComponent<GummyBearController>().color;
        }
        if (col.gameObject.CompareTag("Bullet"))
        {
            GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color; // maybe instead of losing color, losing health? or collect a random (unwanted) color?       
            CollectedColors.Add(col.gameObject.GetComponent<SpriteRenderer>().color);
        }

        foreach (var gate in toriiGates)
        {
            if (Mathf.Abs(gate.toricolor.r - sr.color.r) < 0.1f &&
                Mathf.Abs(gate.toricolor.g - sr.color.g) < 0.1f &&
                Mathf.Abs(gate.toricolor.b - sr.color.b) < 0.1f) // TODO: save 0.1f as threshold variable
            {
                gate.OpenGate();
            }
            else
            {
                gate.CloseGate();
            }
        }
    }
}

// wasser wird dreckig beim benutzen -> timer für regeneratjion von wasser
// Gegner schießen ihre farben. Wenn getroffen, dann verfärbt man sich. farben landen auf boden, durch drüber laufen verfärbt man sich auch.