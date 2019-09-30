using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float sprintFactor = 1.5f;
    
    private Vector3 velocity;
    Rigidbody2D r;

    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += velocity.normalized * Time.deltaTime * speed * isSprint();
    }

    private float isSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return sprintFactor;
        }
        return 1;
    }

    private void runSprintCooldown()
    {

    }

}
