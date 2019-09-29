using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public int health;
    public int numHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        UISingleton.Instance.Setup(hearts, fullHeart, emptyHeart);
        UISingleton.Instance.Player1Health = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
