﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneFire : MonoBehaviour
{

    [SerializeField]
    private int primaryProjectiles;     //Number of projectiles in first wave of primary spell
    [SerializeField]
    private int primaryWaves;           //Number of waves in primary spell
    [SerializeField]
    private float primarySpeed;         //Speed of projectiles in primary spell
    [SerializeField]
    private int heavyProjectiles;       //Number of split heavy projectiles

    [SerializeField]
    private GameObject primaryPrefab;  
    [SerializeField]
    private GameObject heavyPrefab;

    private Vector2 startShot;
    [HideInInspector]
    private Vector2 tarDir;
    public CharacterTargeting charTar;

    [HideInInspector]
    public List<IBaseBullet> bulletList;

    private SharedCharacterController charCon;
     // Start is called before the first frame update
    void Start()
    {
        charTar = GetComponent<CharacterTargeting>();
        bulletList = new List<IBaseBullet>();
        charCon = GetComponent<SharedCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   
        startShot = transform.position;
        tarDir = charTar.TargetDirection();
 
         if (SpellMap.Instance.GetSpellDown(charCon.playerNumber, SpellType.PRIM))       
        {
            StartCoroutine(SelenePrimary(startShot));
        }
     
        if (SpellMap.Instance.GetSpellDown(charCon.playerNumber, SpellType.HEAVY))
        {  
            StartCoroutine(SeleneHeavy());
        }

        if (SpellMap.Instance.GetSpellDown(charCon.playerNumber, SpellType.INTRIN))
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

    IEnumerator SelenePrimary(Vector2 startShot)
    {
        int tempProjectiles = primaryProjectiles;
        float baseAngle = Mathf.Atan2(tarDir.y, tarDir.x) * Mathf.Rad2Deg;

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

    IEnumerator SeleneHeavy()
    {
        float angle = Mathf.Atan2(tarDir.y, tarDir.x) * Mathf.Rad2Deg;
        Vector2 shotDirection = tarDir * primarySpeed;
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