using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public GameObject[] RedDots;
    public Transform[] RedDotPos;
    public DungeonMissionData[] missionDatas;

    private void Start()
    {
        for(int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(false);
            RedDots[i].transform.parent.GetChild(RedDots[i].transform.childCount + 1).gameObject.SetActive(false);
        }
    }

    public void SetActive()
    {
        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;

        bool[] pos = new bool[RedDotPos.Length];
        bool[] levelchosen = new bool[missionDatas.Length];


        bool AllClear = true;
        for (int i = 0; i < levelclear.Length; i++)
        {
            if (levelclear[i] == false)
            {
                AllClear = false;
            }
        }

        if (AllClear == false)
        {
            for (int i = 0; i < RedDots.Length; i++)
            {
                for (int k = 0; k < levelclear.Length; k++)
                {
                    if (levelclear[k] == false && levelchosen[k] == false)
                    {
                        int randpos = Random.Range(0, pos.Length);

                        while (pos[randpos] == true)//重了就重row一下
                        {
                            randpos = Random.Range(0, pos.Length);
                        }

                        RedDots[i].transform.position = RedDotPos[randpos].position;
                        RedDots[i].gameObject.GetComponent<EnterBattle>().missionData = missionDatas[k];
                        RedDots[i].gameObject.SetActive(true);

                        levelchosen[k] = true;
                        pos[randpos] = true;
                        break;
                    }
                }
            }
        }
        else
        {
           
            for (int i = 0; i < RedDots.Length; i++)
            {
                int randomlevel = Random.Range(0, missionDatas.Length);
                while (levelchosen[randomlevel] == true)
                {
                    randomlevel = Random.Range(0, missionDatas.Length);
                }

                int randpos = Random.Range(0, pos.Length);

                while (pos[randpos] == true)//重了就重row一下
                {
                    randpos = Random.Range(0, pos.Length);
                }

                levelchosen[randomlevel] = true;
                pos[randpos] = true;

                RedDots[i].transform.position = RedDotPos[randpos].position;

                RedDots[i].gameObject.GetComponent<EnterBattle>().missionData = missionDatas[randomlevel];
                RedDots[i].gameObject.SetActive(true);
            }
        }
    }

}
