using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
   public static ResourceManager Instance { get; private set; }
    

    [SerializeField]
    private int Nutrition;  //Ӫ��
    [SerializeField]
    private int Health,MaxHealth;   //Ѫ�������Ѫ��


    private void Awake()
    {
        Instance = this;
    }
    public int nutrition
    {
        get { return Nutrition; }
    }
    public void AddNutrition(int amount){
        Nutrition += amount;
    }
    public bool RemoveNutrition(int amount)
    {
        if (Nutrition >= amount)
        {
            Nutrition -= amount;
            return true;
        }
        else return false;
    }

    public int health
    {
        get { return Health; }
    }
    public void AddHealth(int amount)  //���ᳬ�����Ѫ��
    {
        Health += amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }
    public bool RemoveHealth(int amount)
    {
        if (Health >= amount)
        {
            Health -= amount;
            return true;
        }
        else return false;
    }
}
