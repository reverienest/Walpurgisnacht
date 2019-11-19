using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaLastWord : MonoBehaviour
{
    [SerializeField]
    private float beamInterval = 5;
    [SerializeField]
    private GameObject beamPrefab = null;
    [SerializeField]
    private float bulletInterval = 0.1f;
    [SerializeField]
    private float bulletSpeed = 2;
    [SerializeField]
    private GameObject bulletPrefab = null;

    private CharacterTargeting ct;
    private SharedCharacterController cc;

    private float beamTimer;
    private float bulletTimer;

    void Start()
    {
        ct = GetComponent<CharacterTargeting>();
        cc = GetComponent<SharedCharacterController>();
    }

    void OnEnable()
    {
        beamTimer = beamInterval;
        bulletTimer = bulletInterval;
    }

    void Update()
    {
        beamTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;

        if (beamTimer <= 0)
        {
            beamTimer = beamInterval;
            Beam beam = GameObject.Instantiate(beamPrefab, transform.position, Quaternion.identity).GetComponent<Beam>();
            beam.GetComponent<SharedBullet>().playerNumber = cc.playerNumber;
            int opposingPlayerNumber = cc.playerNumber == 0 ? 1 : 0;
            beam.origin = transform;
            beam.target = MatchManager.Instance.Players[opposingPlayerNumber].transform;
        }
        if (bulletTimer <= 0)
        {
            bulletTimer = bulletInterval;

            Vector2 tarDir = ct.TargetDirection();
            float angle = (Random.Range(-0.25f * Mathf.PI, 0.25f * Mathf.PI) + Mathf.Atan2(tarDir.y, tarDir.x)) * Mathf.Rad2Deg;

            float shotDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float shotDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 shotVelocity = new Vector2(shotDirX, shotDirY) * bulletSpeed;

            GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            go.GetComponent<SharedBullet>().playerNumber = cc.playerNumber;
            go.GetComponent<Rigidbody2D>().velocity = shotVelocity;
            go.transform.right = shotVelocity;
        }
    }
}
