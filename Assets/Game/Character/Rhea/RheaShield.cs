using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheaShield : MonoBehaviour
{
    [SerializeField]
    private float shieldDuration = 2;

    private CharacterTargeting ct;

    void Start()
    {
        ct = GetComponentInParent<CharacterTargeting>();
        Destroy(this.gameObject, shieldDuration);
    }

    void Update()
    {
        transform.right = ct.TargetDirection();
    }
}
