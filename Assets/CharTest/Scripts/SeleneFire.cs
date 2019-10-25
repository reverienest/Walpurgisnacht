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

private Vector2 startShot;

private Vector2 startHeavyShot;

private Vector2 TarDir;

private CharacterTargeting CharTar;

    // Start is called before the first frame update
    void Start()
    {
        CharTar = GetComponent<CharacterTargeting>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("LightFire"))
        {
            startShot = transform.position;
            StartCoroutine(SelenePrimary(primaryProjectiles, primaryWaves));
        }
        
        if (Input.GetButtonDown("HeavyFire"))
        {
            startShot = transform.position;   
            StartCoroutine(SeleneHeavy(heavyProjectiles));
        }
        TarDir = CharTar.TargetDirection();
    }   

    IEnumerator SelenePrimary(int _primaryProjectiles, int _primaryWaves)
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

                GameObject tempObj = Instantiate(primaryPrefab, transform.position, Quaternion.identity);
                tempObj.GetComponent<Rigidbody2D>().velocity = shotDirection;
                angle += 60f / (tempProjectiles + 1);

            }
            yield return new WaitForSeconds(.4f);
            tempProjectiles = tempProjectiles - 1; 
        }
    }

    IEnumerator SeleneHeavy(int _heavyProjectiles)
    {
        float baseAngle = Mathf.Atan2(TarDir.y, TarDir.x) * Mathf.Rad2Deg;
        float angle = 0f;
        float shotDirXPos = startShot.x + Mathf.Cos(baseAngle * Mathf.PI);
        float shotDirYPos = startShot.y + Mathf.Sin(baseAngle * Mathf.PI);

                Vector2 shotDirection = new Vector2(shotDirXPos, shotDirYPos) * primarySpeed;
                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                tempObj.GetComponent<Rigidbody2D>().velocity = shotDirection;

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < heavyProjectiles + 1; i++)
            {
                float angleStep = 360f / heavyProjectiles;
                float shotDirXPosSub = startHeavyShot.x + Mathf.Cos((angle * Mathf.PI) - 180f);
                float shotDirYPosSub = startHeavyShot.y + Mathf.Sin((angle * Mathf.PI) - 180f);

                Vector2 shotDirectionSub = new Vector2(shotDirXPosSub, shotDirYPosSub) * primarySpeed;

                GameObject tempObjSub = Instantiate(primaryPrefab, startHeavyShot, Quaternion.identity);
                tempObjSub.GetComponent<Rigidbody2D>().velocity = shotDirectionSub;
                angle += angleStep;

            }
    }
}
