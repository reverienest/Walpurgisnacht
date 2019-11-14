using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseBullet
{
    IEnumerator IntrinsicMutate();

    void DestroyBullet();

    bool CheckMutation();
}

