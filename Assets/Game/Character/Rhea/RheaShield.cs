using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaShield : MonoBehaviour
{
    private RheaShield rs;

    [SerializeField]
    private GameObject character;

    private CharacterTargeting ct;

    private Selene selene;

    private EdgeCollider2D ec;

    private Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        ct = GetComponentInParent<CharacterTargeting>();
        selene = character.GetComponent<Selene>();
        r2d = GetComponent<Rigidbody2D>();
        ec = GetComponent<EdgeCollider2D>();
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 direction = ct.TargetDirection();
        transform.localPosition = selene.transform.localPosition;
        float degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, degree);
        transform.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
