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
        float baseAngle = Mathf.Atan2(TarDir.y, TarDir.x) * Mathf.Rad2Deg;
        float angle = 0f;

        Debug.Log("Angle = " + baseAngle);
        for (int i = primaryWaves; i > 0; i--)
       {
            for (int k = 0; k < primaryProjectiles + 1; k++)
            {
                float angleStep = ((baseAngle + 30) - (baseAngle - 30)) / primaryProjectiles;
                float shotDirXPos = startShot.x + Mathf.Sin((angle * Mathf.PI) - 60f);
                float shotDirYPos = startShot.y + Mathf.Cos((angle * Mathf.PI) - 60f);

                Vector2 shotVector = new Vector2(shotDirXPos, shotDirYPos);
                Vector2 shotDirection = (shotVector - startShot).normalized * primarySpeed;

                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDirection.x, shotDirection.y);
                angle += angleStep;

            }

            yield return new WaitForSeconds(.4f);
            primaryProjectiles = primaryProjectiles - 1; 

        }
    }

    IEnumerator SeleneHeavy(int _heavyProjectiles)
    {
        float baseAngle = Mathf.Atan2(TarDir.y, TarDir.x) * Mathf.Rad2Deg;
        float angle = 0f;
        float shotDirXPos = startShot.x + Mathf.Sin(baseAngle * Mathf.PI);
        float shotDirYPos = startShot.y + Mathf.Cos(baseAngle * Mathf.PI);

         Vector2 shotVector = new Vector2(shotDirXPos, shotDirYPos);
                Vector2 shotDirection = (shotVector - startShot).normalized * primarySpeed;
                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDirection.x, shotDirection.y);

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < heavyProjectiles + 1; i++)
            {
                float angleStep = 360f / heavyProjectiles;
                float shotDirXPosSub = startHeavyShot.x + Mathf.Sin((angle * Mathf.PI) - 180f);
                float shotDirYPosSub = startHeavyShot.y + Mathf.Cos((angle * Mathf.PI) - 180f);

                Vector2 shotVectorSub = new Vector2(shotDirXPosSub, shotDirYPosSub);
                Vector2 shotDirectionSub = (shotVectorSub - startHeavyShot).normalized * primarySpeed;

                GameObject tempObjSub = Instantiate(primaryPrefab, startHeavyShot, Quaternion.identity);
                tempObjSub.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDirectionSub.x, shotDirectionSub.y);
                angle += angleStep;

            }
    }
}
