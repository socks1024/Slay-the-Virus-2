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
    [SerializeField] Transform tetrisRoot;

    /// <summary>
    /// 卡牌核心组件
    /// </summary>
    CardBehaviour card;

    /// <summary>
    /// 方块预制体
    /// </summary>
    [SerializeField] GameObject BlockPrefab;

    [Header("方块配图")]
    [SerializeField] Sprite AttackTex;
    [SerializeField] Sprite DefendTex;
    [SerializeField] Sprite HealTex;
    [SerializeField] Sprite SkillTex;
    [SerializeField] Sprite ExpandTex;
    [SerializeField] Sprite TrashTex;

    [Header("星星配图")]
    [SerializeField] Sprite TriggerSquareTex;
    [SerializeField] Sprite AffectSquareTex;
    [SerializeField] Sprite ExpandSquareTex;

    void Start()
    {
        card = GetComponent<CardBehaviour>();
        CardShape = card.CardShape;
        ConditionsShape = card.ConditionsShape;

        switch (card.AbilityType)
        {
            case CardAbilityType.ATTACK:
                BlockTex = AttackTex;
                break;
            case CardAbilityType.DEFEND:
                BlockTex = DefendTex;
                break;
            case CardAbilityType.HEAL:
                BlockTex = HealTex;
                break;
            case CardAbilityType.SKILL:
                BlockTex = SkillTex;
                break;
            case CardAbilityType.EXPAND:
                BlockTex = ExpandTex;
                break;
            case CardAbilityType.TRASH:
                BlockTex = TrashTex;
                break;
        }

        switch (card.ConditionType)
        {
            case CardConditionType.TRIGGER:
                ConditionTex = TriggerSquareTex;
                break;
            case CardConditionType.AFFECT:
                ConditionTex = AffectSquareTex;
                break;
            case CardConditionType.EXPAND:
                ConditionTex = ExpandSquareTex;
                break;
        }

        AssembleBlocks();
        
        card.GetComponent<CardUI>().Mode = CardMode.CARD;
    }

    /// <summary>
    /// 将方块组合成卡牌的形状
    /// </summary>
    void AssembleBlocks()
    {
        foreach (Vector2 v in CardShape)
        {
            GameObject block = Instantiate(BlockPrefab);
            block.transform.SetParent(tetrisRoot, false);

            block.transform.localPosition = new Vector2(v.x, v.y);
            block.transform.localScale *= 0.01f;
            block.GetComponent<Image>().sprite = BlockTex;
        }

        foreach (Vector2 v in ConditionsShape)
        {
            GameObject block = Instantiate(BlockPrefab);
            block.transform.SetParent(tetrisRoot, false);

            block.transform.localPosition = new Vector2(v.x, v.y);
            block.transform.localScale *= 0.01f;
            block.GetComponent<Image>().sprite = ConditionTex;

            block.GetComponent<Image>().raycastTarget = false;
        }
    }
}
