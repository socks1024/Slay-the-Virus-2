using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仓库 保存卡牌棋盘和道具
public enum ItemCategory{ ChessBoard,Prop};
public abstract class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public int MaxAmount;

}


[CreateAssetMenu(fileName = "棋盘", menuName = "ScriptableObject/棋盘", order = 1)]
public class ChessBoard:Item
{
   
    public BoardData chessboard;//来源于board data
    
}


[CreateAssetMenu(fileName = "道具", menuName = "ScriptableObject/道具", order = 2)]
public class Prop:Item
{
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