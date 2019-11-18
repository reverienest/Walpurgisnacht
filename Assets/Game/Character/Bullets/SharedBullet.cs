using UnityEngine;

public class SharedBullet : MonoBehaviour
{
    public int playerNumber;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BulletCollider")
        {
            Destroy(gameObject);
        }
    }
}