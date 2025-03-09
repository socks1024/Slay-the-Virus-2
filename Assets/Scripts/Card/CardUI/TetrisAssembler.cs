using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisAssembler : MonoBehaviour
{
    /// <summary>
    /// 卡牌的方块要组成的形状，通过一组向量表示每个方块相对原点的位置
    /// </summary>
    List<Vector2> CardShape;

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    List<Vector2> ConditionsShape;

    /// <summary>
    /// 方块配图
    /// </summary>
    Sprite BlockTex;

    /// <summary>
    /// 特殊点位配图
    /// </summary>
    Sprite ConditionTex;

    /// <summary>
    /// 所有方块的根Transform
    /// </summary>
    Transform tetrisRoot;

    /// <summary>
    /// 卡牌核心组件
    /// </summary>
    CardBehaviour card;

    void Start()
    {
        card = GetComponent<CardBehaviour>();
        CardShape = card.CardShape;
        ConditionsShape = card.ConditionsShape;
        BlockTex = card.cardData.BlockTex;
        tetrisRoot = transform.Find("BlockMode");

        AssembleBlocks();
        card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
    }

    /// <summary>
    /// 将方块组合成卡牌的形状
    /// </summary>
    void AssembleBlocks()
    {
        if (CardShape == null)
        {
            print("null shape");
        }
        foreach (Vector2 v in CardShape)
        {
            GameObject block = new GameObject("block" + v.ToString());
            block.transform.SetParent(tetrisRoot, false);
            block.AddComponent<Image>();
            block.AddComponent<CardDrag>();
            block.AddComponent<CardHover>();
            block.AddComponent<CardPress>();
            block.AddComponent<CardSetTarget>();

            block.transform.localPosition = new Vector2(v.x, v.y);
            block.transform.localScale *= 0.01f;
            block.GetComponent<Image>().sprite = BlockTex;
        }

        foreach (Vector2 v in ConditionsShape)
        {
            GameObject block = new GameObject("block" + v.ToString());
            block.transform.SetParent(tetrisRoot, false);
            block.AddComponent<Image>();

            block.transform.localPosition = new Vector2(v.x, v.y);
            block.transform.localScale *= 0.01f;
            block.GetComponent<Image>().sprite = ConditionTex;
        }
    }
}
