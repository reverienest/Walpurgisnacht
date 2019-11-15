using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryBullet : MonoBehaviour, IBaseBullet 
{
    public SeleneFire player;
    public Transform orbitOrigin;

    public float angleSpeed;

    public float baseAngle;
    public Vector2 shotOrigin;
    private Rigidbody2D rb;

    public bool hasMutated;


    [SerializeField]

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        baseAngle = baseAngle + angleSpeed;
    }

    IEnumerator IBaseBullet.IntrinsicMutate()
    {
        hasMutated = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 600; i++)
        {
            transform.RotateAround(shotOrigin, Vector3.forward, baseAngle * Time.deltaTime); 
            //Add time.deltaTime in update to the angle
            // Constant Linear speed
            yield return new WaitForFixedUpdate();
            //Begins to orbit them around their fired position
        }

        DestroyBullet();
    }

    public bool CheckMutation()
    {
        if (hasMutated != true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DestroyBullet()
    {
        player.bulletList.Remove(this);
        Destroy(this.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DestroyBullet();
        }
    }
}
