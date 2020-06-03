using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text cooldownText;

    public void UpdateHP(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void UpdateCoolDown(int timeCd)
    {
        cooldownText.text = timeCd.ToString();
    }
}
