using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickReset : MonoBehaviour
{
    [SerializeField]
    private string titleScene = null;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.M))
            SceneManager.LoadScene(titleScene);
    }
}
