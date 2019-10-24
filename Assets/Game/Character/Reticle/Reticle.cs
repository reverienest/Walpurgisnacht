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
        transform.localPosition = ct.TargetDirection() * orbitDistance;
    }
}
