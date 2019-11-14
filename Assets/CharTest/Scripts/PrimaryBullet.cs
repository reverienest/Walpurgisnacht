using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryBullet : MonoBehaviour, IBaseBullet 
{
    public SeleneFire player;

    [SerializeField]

    void Start()
    {
        player = GetComponent<SeleneFire>();
    }
    public void IntrinsicMutate()
    {
        //Stops the bullets
        //Begins to orbit them around their fired position
        //(In Fire class, remove bullets from screen)
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
        player.bulletList.Remove(this);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DestroyBullet();

        }
    }
}
