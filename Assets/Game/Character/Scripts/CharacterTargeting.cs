using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargeting : MonoBehaviour
{
    public Transform target;

    /// <summary>
    /// Returns direction of target as normalized Vector2
    /// </summary>
    public Vector2 TargetDirection() { return (target.position - transform.position).normalized; }
}
