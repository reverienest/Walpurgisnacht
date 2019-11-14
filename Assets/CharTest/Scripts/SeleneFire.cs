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

    [SerializeField]
    public GameObject heavyPrefab;

    private Vector2 startShot;

    private Vector2 startHeavyShot;

    private Vector2 TarDir;

    private CharacterTargeting CharTar;

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
        if (Input.GetButtonDown("LightFire"))
        {
            StartCoroutine(SelenePrimary(primaryProjectiles, primaryWaves, transform.position));
        }
        
        if (Input.GetButtonDown("HeavyFire"))
        {
            startShot = transform.position;   
            StartCoroutine(SeleneHeavy(heavyProjectiles));
        }
        TarDir = CharTar.TargetDirection();
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
                //float angleStep = (baseAngle - 60f / 2) / primaryProjectiles;
                float shotDirXPos = Mathf.Cos(angle * Mathf.Deg2Rad);
                float shotDirYPos = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector2 shotDirection = new Vector2(shotDirXPos, shotDirYPos) * primarySpeed;

                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                bulletList.Add(tempObj.GetComponent<PrimaryBullet>());
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
        float shotDirYPos = Mathf.Sin(angle * Mathf .Deg2Rad);

            Vector2 shotDirection = new Vector2(shotDirXPos, shotDirYPos) * primarySpeed;
            GameObject tempObj = Instantiate(heavyPrefab, transform.position, Quaternion.identity);
            bulletList.Add(tempObj.GetComponent<HeavyBullet>());
            tempObj.GetComponent<Rigidbody2D>().velocity = shotDirection;

        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < heavyProjectiles + 1; i++)
            {
                float shotDirXPosSub = Mathf.Cos((angle * Mathf.Deg2Rad) - 180f);
                float shotDirYPosSub = Mathf.Sin((angle * Mathf.Deg2Rad) - 180f);

                Vector2 shotDirectionSub = new Vector2(shotDirXPosSub, shotDirYPosSub) * primarySpeed;

                GameObject tempObjSub = Instantiate(heavyPrefab, startHeavyShot, Quaternion.identity);
                tempObjSub.GetComponent<Rigidbody2D>().velocity = shotDirectionSub;
                angle += 60f;
            }
    }
}
