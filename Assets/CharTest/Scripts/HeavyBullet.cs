using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : MonoBehaviour, IBaseBullet
{
     public SeleneFire player;
     public Vector2 startHeavyShot;

    [SerializeField]
     private float heavySpeed;

    [SerializeField]
     private Transform target;

     public GameObject heavyPrefab;

     public bool hasMutated;

     private Rigidbody2D rb;

    private float deltaTarget;
    
    private Vector2 newTarDir;

    public Vector2 TargetDir() { return (target.position - transform.position).normalized;}
    // Start is called before the first frame update

  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void update()
    {
        startHeavyShot = transform.position;
        newTarDir = TargetDir();
    }
    IEnumerator IBaseBullet.IntrinsicMutate()
    {
        hasMutated = true; 
        rb.velocity = Vector2.zero;
        deltaTarget = Mathf.Atan2(newTarDir.y, newTarDir.x) * Mathf.Rad2Deg;
        float newXPos = Mathf.Cos(deltaTarget * Mathf.Deg2Rad);
        float newYpos = Mathf.Sin(deltaTarget * Mathf.Deg2Rad);
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(newXPos, newYpos) * heavySpeed;
        //FIX: heavybullet cannot travel correctly if on right of target
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
    public void SpawnChildren(Vector2 newHeavyShot, float newAngle, int heavyProjectiles)
    {
        for (int i = 0; i < heavyProjectiles + 1; i++)
            {
                float shotDirXPosSub = Mathf.Cos((newAngle * Mathf.Deg2Rad) - 180f);
                float shotDirYPosSub = Mathf.Sin((newAngle * Mathf.Deg2Rad) - 180f);

                Vector2 shotDirectionSub = new Vector2(shotDirXPosSub, shotDirYPosSub) * heavySpeed;

                GameObject tempObjSub = Instantiate(heavyPrefab, newHeavyShot, Quaternion.identity);
                tempObjSub.GetComponent<Rigidbody2D>().velocity = shotDirectionSub;
                newAngle += 60f;
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
