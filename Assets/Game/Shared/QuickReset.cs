using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickReset : MonoBehaviour
{
    [SerializeField]
    private string titleSceneName = null;

    private static QuickReset instance;

    void Start()
    {
        // We only want one of these things in existance at a time
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.M))
            SceneManager.LoadScene(titleSceneName);
    }
}
