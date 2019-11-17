using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private BoxCollider2D c2d;
    // Start is called before the first frame update
    void Start()
    {
        c2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject g = collision.gameObject;
        Destroy(g);
    }
}
