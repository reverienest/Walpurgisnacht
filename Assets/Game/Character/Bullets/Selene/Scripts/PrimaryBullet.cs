using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryBullet : MonoBehaviour, IMutatable
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

    IEnumerator IMutatable.IntrinsicMutate()
    {
        hasMutated = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 300; i++)
        {
            transform.RotateAround(shotOrigin, Vector3.forward, baseAngle * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
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

    void OnDestroy()
    {
        player.bulletList.Remove(this);
    }


}
