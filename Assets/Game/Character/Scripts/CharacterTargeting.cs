using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargeting : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private GameObject reticlePrefab;

    void Start()
    {
        //Create reticle
        GameObject reticle = Instantiate(reticlePrefab, Vector3.zero, Quaternion.identity, transform);
    }

    public Vector2 TargetDirection() { return (target.position - transform.position).normalized; }
}
