using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryBullet : MonoBehaviour, IBaseBullet 
{
    [HideInInspector]
    public float baseAngle;
    public float angleSpeed;
    [HideInInspector]
    public Vector2 shotOrigin;
    private Rigidbody2D rb;

    [HideInInspector]
    public bool hasMutated;

    public SeleneFire player;

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

        for(int i = 0; i < 300; i++)
        {
            transform.RotateAround(shotOrigin, Vector3.forward, baseAngle * Time.deltaTime); 

            yield return new WaitForFixedUpdate();
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
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "BulletCollider")
        {
            DestroyBullet();
        }
    }
}
