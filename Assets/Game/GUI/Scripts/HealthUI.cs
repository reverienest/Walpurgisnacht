using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 0;

    [SerializeField]
    private Image[] healthIcons = new Image[3];

    void Start()
    {
        PlayerStatsManager.Instance.OnHealthChanged += UpdateUI;
        PlayerStatsManager.Instance.OnWardRefill += PartiallyRefillWard;
        foreach (var healthIcon in healthIcons)
            healthIcon.material = Instantiate(healthIcon.material);
    }

    private static Color ModifyAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    private void UpdateUI(int playerNumber, int newHealth, int newWards)
    {
        if (playerNumber != this.playerNumber)
            return;

        for (int i = 0; i < PlayerStatsManager.Instance.MaxHealth; ++i)
        {
            if (i < newWards)
            {
                healthIcons[i].color = ModifyAlpha(healthIcons[i].color, 1);
                healthIcons[i].material.SetFloat("_t", 1);
            }
            else if (i < newHealth)
            {
                healthIcons[i].color = ModifyAlpha(healthIcons[i].color, 1);
                healthIcons[i].material.SetFloat("_t", 0);
            }
            else
            {
                healthIcons[i].color = ModifyAlpha(healthIcons[i].color, 0);
            }
        }
    }

    private void PartiallyRefillWard(int playerNumber, int currentWardValue, float refillValue)
    {
        if (playerNumber != this.playerNumber)
            return;
       
        healthIcons[currentWardValue].color = ModifyAlpha(healthIcons[currentWardValue].color, 1);
        healthIcons[currentWardValue].material.SetFloat("_t", refillValue * 0.43f + 0.1f);
    }

    void OnDestroy() {
        foreach (var healthIcon in healthIcons)
            Destroy(healthIcon.material);
    }
}
