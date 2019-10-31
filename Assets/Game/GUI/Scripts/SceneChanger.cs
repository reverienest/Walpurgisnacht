using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string sceneName = null;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
