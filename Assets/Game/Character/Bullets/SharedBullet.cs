using UnityEngine;

public class SharedBullet : MonoBehaviour
{
    public int playerNumber;
    public bool destroyOnHit = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (destroyOnHit && col.gameObject.tag == "BulletCollider")
        {
            Destroy(gameObject);
        }
    }
}