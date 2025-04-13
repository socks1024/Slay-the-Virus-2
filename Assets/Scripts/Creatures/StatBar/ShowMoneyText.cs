using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMoneyText : MonoBehaviour
{
    /// <summary>
    /// 玩家的引用
    /// </summary>
    public PlayerBehaviour player;

    TextMeshProUGUI tmp;

    void Start()
    {
        player.OnNutritionChange += OnNutritionChange;
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = player.Nutrition.ToString();
    }

    void OnNutritionChange(int nutrition)
    {
        tmp.text = nutrition.ToString();
    }
}
