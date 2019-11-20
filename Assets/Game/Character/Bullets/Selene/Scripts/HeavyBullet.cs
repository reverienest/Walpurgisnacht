using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : MonoBehaviour, IMutatable
{
    public SeleneFire player;
    private Transform target;
    [SerializeField]
    private GameObject heavyPrefab = null;
    [SerializeField]
    private float spinSpeed = 180;

    [HideInInspector]
    public Vector2 startHeavyShot;
    [SerializeField]
    private float heavySpeed = 0;

    private float deltaTarget;
    private Vector2 newTarDir;

    [HideInInspector]
    public bool hasMutated;

    private Rigidbody2D rb;

    [HideInInspector]
    public Vector2 TargetDir() { return (target.position - transform.position).normalized;}
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        target = player.GetComponent<CharacterTargeting>().target;
        startHeavyShot = transform.position;
        newTarDir = TargetDir();
    }
    IEnumerator IMutatable.IntrinsicMutate()
    {
        hasMutated = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        rb.velocity = newTarDir * heavySpeed;
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

    void OnDestroy()
    {
        player.bulletList.Remove(this);
    }
}
