using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public bool vulnerable = true;

    [SerializeField]
    private RemoteTrigger hitbox = null;
    [SerializeField]
    private float invulnerabilityDuration = 1;
    [SerializeField]
    private float flashInterval = 0.1f;

    private SharedCharacterController cc;
    private SpriteRenderer sr;

    /// Invulnerability timer
    private float iTimer;

    void Start()
    {
        cc = GetComponent<SharedCharacterController>();
        sr = GetComponent<SpriteRenderer>();

        hitbox.OnTrigger += HandleCollision;
    }

    void Update()
    {
        iTimer -= Time.deltaTime;
    }

    private IEnumerator InvulnerableRoutine()
    {
        hitbox.ColliderEnabled = false;
        iTimer = invulnerabilityDuration;
        while (iTimer > 0)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
        sr.enabled = true;
        hitbox.ColliderEnabled = true;
    }

    private void HandleCollision(RemoteTrigger.ActionType type, Collider2D c)
    {
        if (type == RemoteTrigger.ActionType.EXIT || !c.CompareTag("Bullet"))
            return;

        SharedBullet sb = c.GetComponent<SharedBullet>();
        if (sb)
        {
            if (sb.playerNumber != cc.playerNumber && vulnerable && iTimer <= 0)
            {
                PlayerStatsManager.Instance.TakeDamage(cc.playerNumber);
                if (sb.destroyOnHit)
                    Destroy(sb.gameObject);
                StartCoroutine(InvulnerableRoutine());
            }
        }
    }
}