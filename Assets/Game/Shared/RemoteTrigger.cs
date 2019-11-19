using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTrigger : MonoBehaviour
{
    private Collider2D col;
    public bool ColliderEnabled
    {
        get { return col.enabled; }
        set { col.enabled = value; }
    }

    public enum ActionType
    {
        ENTER,
        STAY,
        EXIT,
    }
    public delegate void TriggerAction(ActionType type, Collider2D collider);
    public event TriggerAction OnTrigger;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D c) { OnTrigger(ActionType.ENTER, c); }
    void OnTriggerStay2D(Collider2D c) { OnTrigger(ActionType.STAY, c); }
    void OnTriggerExit2D(Collider2D c) { OnTrigger(ActionType.EXIT, c); }
}
