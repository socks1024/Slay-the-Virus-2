using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ֿ� ���濨�����̺͵���
public enum ItemCategory{ ChessBoard,Prop};
public enum ResourceCatogory { Nutrition };
[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public int MaxAmount;

    public int price;
    public ResourceCatogory resourceCatogory;//�������ĵ���Դ����
    public int StorageAmount;//�̵��еĿ������

}


[CreateAssetMenu(fileName = "����", menuName = "ScriptableObject/����", order = 1)]
public class ChessBoard:Item
{
    ChessBoard()
    {
        this.Category = ItemCategory.ChessBoard;
        this.MaxAmount = 1;
        this.resourceCatogory = ResourceCatogory.Nutrition;//Ĭ�Ϲ�������Ӫ��
    }
    public BoardData chessboard;//��Դ��board data
    
}


[CreateAssetMenu(fileName = "����", menuName = "ScriptableObject/����", order = 2)]
public class Prop:Item
{
    Prop()
    {
        this.Category = ItemCategory.Prop;
        this.MaxAmount = 99;
        this.resourceCatogory = ResourceCatogory.Nutrition;//Ĭ�Ϲ�������Ӫ��
    }
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