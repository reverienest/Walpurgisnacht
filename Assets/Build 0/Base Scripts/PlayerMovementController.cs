using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float speed = 1;
    float diagFactor = Mathf.Sqrt((float) 0.5);
    Rigidbody2D r;

    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(speed, speed, 0) * diagFactor;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(speed, -1 * speed, 0) * diagFactor;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(-1 * speed, speed, 0) * diagFactor;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(speed, speed, 0) * diagFactor;
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position -= new Vector3(speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, speed, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position -= new Vector3(0, speed, 0);
            }
        }


    }

}
