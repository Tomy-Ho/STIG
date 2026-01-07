using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    public Transform gummytransform;
    public float BulletRange = 5.0f;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float dist = Vector3.Distance(gummytransform.position, transform.position);
        if (dist >= BulletRange)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }
    }
}
