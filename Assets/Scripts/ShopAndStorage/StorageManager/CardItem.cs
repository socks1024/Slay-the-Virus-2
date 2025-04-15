using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardItem", menuName = "ScriptableObject/CardItem", order = 3)]
public class CardItem : Item
{
    CardItem()
    {
        this.Category = ItemCategory.Card;
        this.MaxAmount = 99;
        this.resourceCatogory = ResourceCatogory.Nutrition;//Ĭ�Ϲ�������Ӫ��
    }
    //public int amount;
    public CardBehaviour cardBehaviour;
    public string type;
}
