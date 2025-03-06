using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仓库 保存卡牌棋盘和道具
public enum ItemCategory{ ChessBoard,Prop,Card};
public enum ResourceCatogory { Nutrition };
[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public int MaxAmount;

    public int price;
    public ResourceCatogory resourceCatogory;//购买消耗的资源种类
    public int StorageAmount;//商店中的库存数量
    public int sellprice;//售卖的价格
    public int weight;//抽奖的权重，最小1

}


[CreateAssetMenu(fileName = "棋盘", menuName = "ScriptableObject/棋盘", order = 1)]
public class ChessBoard:Item
{
    ChessBoard()
    {
        this.Category = ItemCategory.ChessBoard;
        this.MaxAmount = 1;
        this.resourceCatogory = ResourceCatogory.Nutrition;//默认购买消耗营养
    }
    public Board chessboard;//来源于board data
    
}


[CreateAssetMenu(fileName = "道具", menuName = "ScriptableObject/道具", order = 2)]
public class Prop:Item
{
    Prop()
    {
        this.Category = ItemCategory.Prop;
        this.MaxAmount = 99;
        this.resourceCatogory = ResourceCatogory.Nutrition;//默认购买消耗营养
    }
    public int amount;
    
}

[CreateAssetMenu(fileName = "卡牌", menuName = "ScriptableObject/卡牌", order = 3)]
public class Card : Item
{
    Card()
    {
        this.Category = ItemCategory.Card;
        this.MaxAmount = 99;
        this.resourceCatogory = ResourceCatogory.Nutrition;//默认购买消耗营养
    }
    public int amount;

}




/*[System.Serializable]  为了实现带槽位的仓库准备的 
public class InventorySlot
{
    public Item item;
    public int amount;
    public bool isEmpty =>item==null;
    public bool AddMore => !isEmpty && amount < item.MaxAmount;

}*/