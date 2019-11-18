using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMutatable
{
    IEnumerator IntrinsicMutate();

    bool CheckMutation();
}

