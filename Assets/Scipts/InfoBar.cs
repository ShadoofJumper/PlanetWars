using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text cooldownText;
    private int gameMaxHealth;

    private void Start()
    {
        gameMaxHealth = SceneController.instance.PlanetsHealth;
    }

    public void UpdateHP(int hp)
    {
        hpText.text = hp.ToString();
        UpdateHPColor(hp);
    }

    private void UpdateHPColor(int hp)
    {
        if (hp >= gameMaxHealth * 0.8)
        {
            hpText.color = Color.green;
        }
        else if (   hp < gameMaxHealth * 0.8
                &&  hp >= gameMaxHealth * 0.5)
        {
            hpText.color = Color.yellow;
        }
        else
        {
            hpText.color = Color.red;
        }
    }

    public void UpdateCoolDown(int timeCd)
    {
        cooldownText.text = timeCd.ToString();
    }
}
