using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaFire : MonoBehaviour
{
    [HideInInspector]
    public Vector2 startShot;
    private Vector2 tarDir;
    private CharacterTargeting charTar;
    private SharedCharacterController charCon;

    [SerializeField]
    private GameObject primaryPrefab = null;
    [SerializeField]
    private GameObject heavyPrefab = null;

    [SerializeField]
    private float primarySpeed = 5;
    [SerializeField]
    private float heavySpeed = 3;
    [SerializeField]
    private int primaryProjectiles = 15;
    [SerializeField]
    private int heavyProjectiles = 3;

    // Start is called before the first frame update
    void Start()
    {
        charTar = GetComponent<CharacterTargeting>();
        charCon = GetComponent<SharedCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        startShot = transform.position;
        tarDir = charTar.TargetDirection();
    }

    public IEnumerator RheaPrimary(Vector2 startShot)
    {
        for(int i = 0; i < primaryProjectiles; i++)
        {
            float angle = (Random.Range(-0.15f * Mathf.PI, 0.15f * Mathf.PI) + Mathf.Atan2(tarDir.y, tarDir.x)) * Mathf.Rad2Deg;

            float shotDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float shotDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 shotVelocity = new Vector2(shotDirX, shotDirY) * primarySpeed;

            GameObject temp = Instantiate(primaryPrefab, startShot, Quaternion.identity);
            temp.GetComponent<SharedBullet>().playerNumber = charCon.playerNumber;
            temp.GetComponent<Rigidbody2D>().velocity = shotVelocity;
            temp.transform.right = shotVelocity;
            
            yield return new WaitForSeconds(.1f);
            if(Random.Range(1,3) == 1)
            {
                AudioManager.instance.Play("RheaLastWordSequence");
            }
        }
        AudioManager.instance.Stop("RheaLastWordSequence");
    }

    public void RheaHeavy(Vector2 startShot)
    {
        float angle = (Mathf.Atan2(tarDir.y, tarDir.x) * Mathf.Rad2Deg) -  (60f/2);

        for(int i = 0; i < heavyProjectiles; i++)
        {
            float shotDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float shotDirY = Mathf.Sin(angle * Mathf.Rad2Deg);

            Vector2 shotVelocity = new Vector2(shotDirX, shotDirY) * heavySpeed;

            GameObject temp = Instantiate(heavyPrefab, startShot, Quaternion.identity);
            temp.GetComponent<SharedBullet>().playerNumber = charCon.playerNumber;
            temp.GetComponent<Rigidbody2D>().velocity = shotVelocity;
            temp.transform.right = shotVelocity;

            angle += 60 / (heavyProjectiles + 1);
        }
    }

    public void SPSpell()
    {
        StartCoroutine(RheaPrimary(startShot));
    }

}
