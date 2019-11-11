using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : MonoBehaviour, IBaseBullet
{
    // Start is called before the first frame update
        public void IntrinsicMutate()
    {
        //Info here
    }
    
    public void DestroyBullet()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }
}
