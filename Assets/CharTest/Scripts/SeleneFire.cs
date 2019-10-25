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
private float startAngle = 120f, endAngle = 240f;
public GameObject primaryPrefab;   //Primary shot sprite

private Vector2 startShot;
private const float radius = 1F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("LightFire"))
        {
            startShot = transform.position;
            StartCoroutine(SelenePrimary(primaryProjectiles, primaryWaves));
        }

    }   

    IEnumerator SelenePrimary(int _primaryProjectiles, int _primaryWaves)
    {
        float angleStep = 120f / primaryProjectiles;
        float angle = 0f;
        //for (int i = primaryWaves; i > 0; i--)
       // {
            for (int k = 0; k < primaryProjectiles; k++)
            {
                float shotDirXPos = startShot.x + Mathf.Sin((angle * Mathf.PI) / 120f) * radius;
                float shotDirYPos = startShot.y + Mathf.Cos((angle * Mathf.PI) / 120f) * radius;

                Vector2 shotVector = new Vector2(shotDirXPos, shotDirYPos);
                Vector2 shotDirection = (shotVector - startShot).normalized * primarySpeed;

                GameObject tempObj = Instantiate(primaryPrefab, startShot, Quaternion.identity);
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDirection.x, shotDirection.y);
                angle += angleStep;

            }

           // primaryProjectiles = primaryProjectiles - 1; 
           yield return new WaitForSeconds(10f);
        //}
    }

    private void SeleneHeavy(int _heavyProjectiles)
    {
        //bullet that continues down a path and explodes
    }
}
