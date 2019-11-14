using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneFire : MonoBehaviour
{

    [SerializeField]
    private int primaryProjectiles;     //Number of projectiles in first wave of primary spell
    [SerializeField]
    private int primaryWaves;            //Number of waves in primary spell
    [SerializeField]
    private float primarySpeed;         //Speed of projectiles in primary spell

    [SerializeField]
    private int heavyProjectiles;

    [SerializeField]
    public GameObject primaryPrefab;   //Primary shot sprite

    public GameObject heavyPrefab;

    private Vector2 startShot;

    public Vector2 TarDir;

    public CharacterTargeting CharTar;

    public Transform targetTrans;
    public List<IBaseBullet> bulletList;

     // Start is called before the first frame update
    void Start()
    {
        CharTar = GetComponent<CharacterTargeting>();

        bulletList = new List<IBaseBullet>();
    }

        // Update is called once per frame
    void Update()
    {   
        startShot = transform.position;
        TarDir = CharTar.TargetDirection();
        targetTrans = CharTar.target;

        if (Input.GetButtonDown("LightFire"))
        {
            StartCoroutine(SelenePrimary(primaryProjectiles, primaryWaves, startShot));
        }
        
        if (Input.GetButtonDown("HeavyFire"))
        {  
            StartCoroutine(SeleneHeavy(heavyProjectiles));
        }

        if (Input.GetButtonDown("IntrinsicFire"))
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                bool mutateCheck = bulletList[i].CheckMutation();
                if (mutateCheck != true)
                {
                    StartCoroutine(bulletList[i].IntrinsicMutate());
                }
            }   
        }
    }   

    IEnumerator SelenePrimary(int _primaryProjectiles, int _primaryWaves, Vector2 startShot)
    {
        int tempProjectiles = primaryProjectiles;
        float baseAngle = Mathf.Atan2(TarDir.y, TarDir.x) * Mathf.Rad2Deg;

        Debug.Log("Angle = " + baseAngle);
        for (int i = primaryWaves; i > 0; i--)  
       {
            float angle = baseAngle - (60f/2);
            for (int k = 0; k < tempProjectiles; k++)
            {
                float shotDirXPos = Mathf.Cos(angle * Mathf.Deg2Rad);
                float shotDirYPos = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector2 shotDirection = new Vector2(shotDirXPos, shotDirYPos) * primarySpeed;

                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                PrimaryBullet primaryBullet = tempObj.GetComponent<PrimaryBullet>();
                bulletList.Add(primaryBullet);
                primaryBullet.player = this;
                primaryBullet.shotOrigin = startShot;
                tempObj.GetComponent<Rigidbody2D>().velocity = shotDirection;
                angle += 60f / (tempProjectiles + 1);

            }
            yield return new WaitForSeconds(.4f);
            tempProjectiles = tempProjectiles - 1; 
        }
        
    }

    IEnumerator SeleneHeavy(int _heavyProjectiles)
    {
        float angle = Mathf.Atan2(TarDir.y, TarDir.x) * Mathf.Rad2Deg;
        float shotDirXPos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float shotDirYPos = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 shotDirection = new Vector2(shotDirXPos, shotDirYPos) * primarySpeed;
            GameObject tempObj = Instantiate(heavyPrefab, transform.position, Quaternion.identity);
            HeavyBullet heavyBullet = tempObj.GetComponent<HeavyBullet>();
            bulletList.Add(heavyBullet);
            heavyBullet.player = this;
            tempObj.GetComponent<Rigidbody2D>().velocity = shotDirection;

        yield return new WaitForSeconds(2f);

        if(heavyBullet != null && heavyBullet.hasMutated != true)
        {
            heavyBullet.SpawnChildren(heavyBullet.transform.position, angle, heavyProjectiles);
            heavyBullet.DestroyBullet();
        }
    }
}
