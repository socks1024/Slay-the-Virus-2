using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float expand = 1.2f;
    public float lessen = 0.7f;

    public GameObject[] RedDots;
    public Transform[] RedDotPos;
    public DungeonMissionData[] missionDatas;
    public DungeonMissionData tutorial;

   

    private bool EnterTutorial;
    private Vector3 originalscale;

    private void Start()
    {
        for(int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(false);
            RedDots[i].transform.parent.GetChild(RedDots[i].transform.childCount + 1).gameObject.SetActive(false);
        }

        EnterTutorial = SaveSystem.Instance.getSave().TutorialClear[2];
        originalscale = RedDots[0].transform.localScale;
    }

    public void SetActive()
    {
        if (!EnterTutorial)
        {
            Debug.Log(SaveSystem.Instance.getSave().ClearLevels[2]);
            int randpos = Random.Range(0, RedDotPos.Length);
            RedDots[0].transform.position = RedDotPos[randpos].position;
            RedDots[0].GetComponent<EnterBattle>().missionData = tutorial;
            RedDots[0].SetActive(true);
            return;
        }



        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;

        bool[] pos = new bool[RedDotPos.Length];
        bool[] levelchosen = new bool[missionDatas.Length];


        bool AllClear = true;
        int LevelClearCount = 0;
        for (int i = 0; i < levelclear.Length; i++)
        {
            if (levelclear[i] == false)
            {
                AllClear = false;
            }
            else
            {
                LevelClearCount++;
            }
        }

        if (LevelClearCount < 1)
            LevelClearCount = 1;

        if (LevelClearCount == 6)
            LevelClearCount = 5;

        if (AllClear == true)
        {
            //for (int i = 0; i < RedDots.Length; i++)
            //{
            //    for (int k = 0; k < levelclear.Length; k++)
            //    {
            //        if (levelclear[k] == false && levelchosen[k] == false)
            //        {
            //            int randpos = Random.Range(0, pos.Length);

            //            while (pos[randpos] == true)//重了就重row一下
            //            {
            //                randpos = Random.Range(0, pos.Length);
            //            }

            //            RedDots[i].transform.position = RedDotPos[randpos].position;
            //            RedDots[i].gameObject.GetComponent<EnterBattle>().missionData = missionDatas[k];
            //            RedDots[i].gameObject.SetActive(true);

            //            levelchosen[k] = true;
            //            pos[randpos] = true;
            //            break;
            //        }
            //    }
            //}

            for (int i = 0; i < RedDots.Length; i++)
            {
                int randomlevel = Random.Range(0, LevelClearCount + 1);
                while (levelchosen[randomlevel] == true)
                {
                    randomlevel = Random.Range(0, LevelClearCount + 1);
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
                RedDots[i].transform.localScale = new Vector3(originalscale.x * lessen, originalscale.y * lessen);
                RedDots[i].gameObject.SetActive(true);
            }
        }
        else
        {

            for (int i = 0; i < RedDots.Length - 1; i++)
            {
                int randomlevel = Random.Range(0, LevelClearCount + 1);
                while (levelchosen[randomlevel] == true)
                {
                    randomlevel = Random.Range(0, LevelClearCount + 1);
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
                if (SaveSystem.Instance.getSave().ClearLevels[randomlevel] == true)
                {
                    RedDots[i].transform.localScale = new Vector3(originalscale.x * lessen, originalscale.y * lessen);
                }
                else
                {
                    RedDots[i].transform.localScale = new Vector3(originalscale.x * expand, originalscale.y * expand);
                }
                RedDots[i].gameObject.SetActive(true);
            }

            int randomlevel2 = 0;


                for(int i = 0; i < missionDatas.Length; i++)
                {
                    if(levelchosen[i] == false&& SaveSystem.Instance.getSave().ClearLevels[i] == false)
                    {
                    randomlevel2 = i;
                    break;
                    }
                }
            
            int randpos2 = Random.Range(0, pos.Length);
            while (pos[randpos2] == true)
            {
                randpos2 = Random.Range(0, pos.Length);
            }

            RedDots[RedDots.Length - 1].transform.position = RedDotPos[randpos2].position;
            RedDots[RedDots.Length - 1].transform.localScale = new Vector3(originalscale.x * expand, originalscale.y * expand);
            RedDots[RedDots.Length - 1].gameObject.GetComponent<EnterBattle>().missionData = missionDatas[randomlevel2];
            RedDots[RedDots.Length - 1].gameObject.SetActive(true);
            //RedDots[RedDots.Length - 1].transform.localScale = new Vector3(RedDots[RedDots.Length - 1].transform.localScale.x*1.2f, RedDots[RedDots.Length - 1].transform.localScale.y*1.2f);

        }
    }


    public void ReturnMain()
    {
        for(int i = 0; i < RedDots.Length;i++)
        {
            RedDots[i].gameObject.SetActive(false);
        }
    }

}
