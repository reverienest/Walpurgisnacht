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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (health > numHealth)
        {
            health = numHealth;
        }
        for (int i  = 0; i < hearts.Length; i++)
        {
            if (i< health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false; 
            }
        }
    }
}
