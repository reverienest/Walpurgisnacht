using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTextManager : Singleton<SplashTextManager>
{

    [SerializeField]
    private GameObject splashTextPrefab = null;


    public void Splash(string text, SplashText.EndAction callback = null)
    {
        GameObject splashTextGO = Instantiate(splashTextPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        SplashText splashText = splashTextGO.GetComponent<SplashText>();
        splashText.Init(text, callback);
    }
}
