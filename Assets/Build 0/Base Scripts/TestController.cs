using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject aimCircle;
    public GameObject magicCircle;

    public GameObject shotPrefab;
    private bool isCircle = false;
    private int coolDown = 30;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontal = new Vector3(Input.GetAxisRaw("MoveX"), Input.GetAxisRaw("MoveY"), 0.0f);

        AimTarget();

        transform.position = transform.position + (horizontal * 10) * Time.deltaTime;
        CircleCast();
        
    }

    private void AimTarget()
    {
        Vector3 aim = new Vector3(Input.GetAxis("AimX"), Input.GetAxis("AimY"), 0.0f);
        Vector2 shotDirection = new Vector2(Input.GetAxis("AimX"), Input.GetAxis("AimY"));

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            aimCircle.transform.localPosition = aim;
            aimCircle.SetActive(true);

            shotDirection.Normalize();
            if (Input.GetButtonDown("LightFire"))
            {
                GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
                shot.GetComponent<Rigidbody2D>().velocity = shotDirection * 10;
                shot.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg);
                Destroy(shot, 2.0f);
            }
        }
        else
        {
            aimCircle.SetActive(false);
        }
    }

    private void CircleCast()
    {
       if (isCircle == false)
       {
            if (Input.GetButtonDown("CirclePlace"))
            {
                GameObject circle = Instantiate(magicCircle);
            }
           isCircle = true; 
       }
        else
        {
           return;
        }
    }
}
