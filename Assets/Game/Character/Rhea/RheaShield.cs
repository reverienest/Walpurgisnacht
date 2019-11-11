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

    private SeleneStateInput input;

    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void Start()
    {
        ct = GetComponentInParent<CharacterTargeting>();
        selene = character.GetComponent<Selene>();
        r2d = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 direction = ct.TargetDirection();
        transform.position = ct.transform.position;
        float degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, degree);
        transform.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject bullet = other.collider.gameObject;
        Destroy(bullet);
    }

}
