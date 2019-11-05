using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField]
    private float orbitDistance = 0.0f;

    private CharacterTargeting ct;

    void Start()
    {
        ct = GetComponentInParent<CharacterTargeting>();
    }

    void Update()
    {
        Vector2 targetDirection = ct.TargetDirection();
        transform.localPosition = targetDirection * orbitDistance;
        transform.right = targetDirection;
    }
}
