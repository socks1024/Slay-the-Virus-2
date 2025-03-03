using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ֿ� ���濨�����̺͵���
public enum ItemCategory{ ChessBoard,Prop};
public abstract class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public int MaxAmount;

}


[CreateAssetMenu(fileName = "����", menuName = "ScriptableObject/����", order = 1)]
public class ChessBoard:Item
{
   
    public BoardData chessboard;//��Դ��board data
    
}


[CreateAssetMenu(fileName = "����", menuName = "ScriptableObject/����", order = 2)]
public class Prop:Item
{
    public int amount;
    
}



/*[System.Serializable]  Ϊ��ʵ�ִ���λ�Ĳֿ�׼���� 
public class InventorySlot
{
    public Item item;
    public int amount;
    public bool isEmpty =>item==null;
    public bool AddMore => !isEmpty && amount < item.MaxAmount;

}*/