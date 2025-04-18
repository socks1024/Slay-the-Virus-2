using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ActionLib
{
    /// <summary>
    /// 直接变更生命值
    /// </summary>
    /// <param name="target">要变更生命值的目标</param>
    /// <param name="amount">变更量</param>
    public static void DirectlyChangeHealthAction(CreatureBehaviour target, int amount)
    {
        target.takeDamage.Health += amount;
    }

    #region battle related actions

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="target">要受到伤害的目标</param>
    /// <param name="source">攻击的来源</param>
    /// <param name="damage">伤害数值</param>
    public static void DamageAction(CreatureBehaviour target, CreatureBehaviour source, int damage)
    {
        damage += source.buffOwner.GetBuffAmount("Strength");

        damage -= source.buffOwner.GetBuffAmount("Paralyze");
        if (damage < 0) damage = 0;

        if (source.buffOwner.HasBuff("Weakness")) damage *= 2;

        if (source is PlayerBehaviour)
        {
            if ((source as PlayerBehaviour).HasRelic("StrongMedicine"))
            {
                damage += damage / 2;
            }
        }

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, AnimEffectType.DAMAGED, () => {
            target.takeDamage.GetDamage(damage);
        });

        if (source.buffOwner.HasBuff("Counter")) CounterAction(source, target, source.buffOwner.GetBuffAmount("Counter"));

        if (source is PlayerBehaviour)
        {
            if ((source as PlayerBehaviour).HasRelic("BloodyKnife"))
            {
                ApplyBuffAction(target, source, "Wound", 1);
            }
        }
    }

    /// <summary>
    /// 造成多次伤害
    /// </summary>
    /// <param name="target">要受到伤害的目标</param>
    /// <param name="source">攻击的来源</param>
    /// <param name="damage">伤害数值</param>
    /// <param name="times">伤害数值</param>
    public static void MultiAttackAction(CreatureBehaviour target, CreatureBehaviour source, int damage, int times)
    {
        for (int i = 0; i < times; i++)
        {
            DamageAction(target, source, damage);
        }
    }

    /// <summary>
    /// 受到治疗
    /// </summary>
    /// <param name="target">要受到治疗的目标</param>
    /// <param name="source">治疗的来源</param>
    /// <param name="heal">治疗数值</param>
    public static void HealAction(CreatureBehaviour target, CreatureBehaviour source, int heal)
    {
        if (source is PlayerBehaviour && (source as PlayerBehaviour).HasRelic("IdolSign")) heal += 1;
        
        // 治疗动画
        AnimationManager.Instance.PlayAnimEffect(target.transform.position, AnimEffectType.HEALED, () => {
            target.takeDamage.Health += heal;
        });
    }

    /// <summary>
    /// 给予特定Buff
    /// </summary>
    /// <param name="target">要获得buff目标</param>
    /// <param name="source">给予buff的来源</param>
    /// <param name="buffName">buff的名字</param>
    /// <param name="amount">buff的层数</param>
    public static void ApplyBuffAction(CreatureBehaviour target, CreatureBehaviour source, string buffName, int amount)
    {
        // 给予负面BUFF动画
        // 获得正面BUFF动画
        AnimEffectType type = AnimEffectType.POSITIVE_BUFF;

        if (DungeonBuffLib.buffPrefabs[buffName].Type == BuffType.NEGATIVE)
        {
            type = AnimEffectType.NEGATIVE_BUFF;
        }

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, type, () => {
            target.buffOwner.GainBuff(DungeonBuffLib.GetBuff(buffName, amount));
        });
    }

    /// <summary>
    /// 下回合给予特定Buff
    /// </summary>
    /// <param name="target">要获得buff目标</param>
    /// <param name="source">给予buff的来源</param>
    /// <param name="buffName">buff的名字</param>
    /// <param name="amount">buff的层数</param>
    public static void ApplyBuffNextTurnAction(CreatureBehaviour target, CreatureBehaviour source, string buffName, int amount)
    {
        AnimEffectType type = AnimEffectType.POSITIVE_BUFF;

        if (DungeonBuffLib.buffPrefabs[buffName].Type == BuffType.NEGATIVE)
        {
            type = AnimEffectType.NEGATIVE_BUFF;
        }

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, type, () => {
            ApplyBuffNextTurnBuff buff = DungeonBuffLib.GetBuff("ApplyBuffNextTurn", amount) as ApplyBuffNextTurnBuff;
            
            buff.source = source;
            buff.newBuffID = buffName;

            target.buffOwner.GainBuff(buff);
        });
    }

    /// <summary>
    /// 给予所有敌人特定Buff
    /// </summary>
    /// <param name="source">给予buff的来源</param>
    /// <param name="buffName">buff的名字</param>
    /// <param name="amount">buff的层数</param>
    public static void ApplyBuffToAllEnemyAction(CreatureBehaviour source, string buffName, int amount)
    {
        foreach (EnemyBehaviour enemy in DungeonManager.Instance.battleManager.enemyGroup.enemies)
        {
            ApplyBuffAction(enemy, source, buffName, amount);
        }
    }

    /// <summary>
    /// 给予随机敌人特定Buff
    /// </summary>
    /// <param name="source">给予buff的来源</param>
    /// <param name="buffName">buff的名字</param>
    /// <param name="amount">buff的层数</param>
    public static void ApplyBuffToRandomEnemyAction(CreatureBehaviour source, string buffName, int amount)
    {
        ApplyBuffAction(DungeonManager.Instance.battleManager.enemyGroup.GetRandomEnemy(), source, buffName, amount);
    }

    /// <summary>
    /// 获得格挡
    /// </summary>
    /// <param name="target">获得格挡的对象</param>
    /// <param name="source">获得格挡的来源</param>
    /// <param name="amount">格挡的数量</param>
    public static void GainBlockAction(CreatureBehaviour target, CreatureBehaviour source, int amount)
    {
        amount += source.buffOwner.GetBuffAmount("Tenacity");

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, AnimEffectType.NONE, () => {
            target.takeDamage.Block += amount;
        });

        if (source is PlayerBehaviour)
        {
            if ((source as PlayerBehaviour).HasRelic("ElectricArmor"))
            {
                ApplyBuffAction(target, source, "Counter", 1);
            }
        }
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount">抽牌量</param>
    public static void DrawCardAction(int amount)
    {
        DungeonManager.Instance.battleManager.cardFlow.DrawCards(amount);
    }

    /// <summary>
    /// 消耗卡牌
    /// </summary>
    /// <param name="card">要消耗的卡牌</param>
    public static void ExhaustCardAction(CardBehaviour card)
    {
        DungeonManager.Instance.battleManager.cardFlow.ExhaustCard(card);
    }

    /// <summary>
    /// 攻击全体敌人
    /// </summary>
    /// <param name="source">伤害来源</param>
    /// <param name="amount">伤害量</param>
    public static void DamageAllAction(CreatureBehaviour source, int amount)
    {
        for (int i = 0; i < DungeonManager.Instance.battleManager.enemyGroup.enemies.Count; i++)
        {
            DamageAction(DungeonManager.Instance.battleManager.enemyGroup.enemies[i], source, amount);
        }
    }

    /// <summary>
    /// 通过受伤状态被伤害
    /// </summary>
    /// <param name="owner">主人</param>
    /// <param name="amount">量</param>
    public static void WoundAction(CreatureBehaviour owner, int amount)
    {
        AnimationManager.Instance.PlayAnimEffect(owner.transform.position, AnimEffectType.WOUND, () => {
            if ((owner as PlayerBehaviour).HasRelic("ConceptShield"))
            {
                owner.takeDamage.GetDamage(amount);
            }
            else
            {
                owner.takeDamage.Health -= amount;
            }
        });
    }

    /// <summary>
    /// 反击
    /// </summary>
    /// <param name="target">反击目标</param>
    /// <param name="source">反击来源</param>
    /// <param name="amount">反击量</param>
    public static void CounterAction(CreatureBehaviour target, CreatureBehaviour source, int amount)
    {
        AnimationManager.Instance.PlayAnimEffect(target.transform.position, AnimEffectType.COUNTER, () => {
            target.takeDamage.GetDamage(amount);
        });
    }

    /// <summary>
    /// 将一张卡牌加入手牌和卡组
    /// </summary>
    /// <param name="p_card_ID">卡牌ID</param>
    /// <param name="amount">卡牌数量</param>
    public static void AddCardToHandAndDeck(string p_card_ID, int amount)
    {
        AddCardToHand(p_card_ID, amount);

        CardBehaviour p_card = CardLib.GetCard(p_card_ID);

        for (int i = 0; i < amount; i++)
        {
            PlayerAddCardToDeck(p_card);
        }
    }

    /// <summary>
    /// 将一张垃圾牌加入玩家手牌
    /// </summary>
    /// <param name="p_card_ID">卡牌ID</param>
    /// <param name="amount">卡牌数量</param>
    public static void PlayerGainVirusCard(string p_card_ID, int amount)
    {
        if (DungeonManager.Instance.Player.buffOwner.HasBuff("GasMask"))
        {
            DungeonManager.Instance.Player.buffOwner.GetBuff("GasMask").Amount -= 1;
        }
        else
        {
            AddCardToHand(p_card_ID, amount);
        }
    }

    /// <summary>
    /// 将一张垃圾牌加入玩家抽牌堆
    /// </summary>
    /// <param name="p_card_ID">卡牌ID</param>
    /// <param name="amount">卡牌数量</param>
    public static void AddVirusCardToDrawPile(string p_card_ID, int amount)
    {
        DungeonManager.Instance.battleManager.cardFlow.drawPile.AddCard(MonoBehaviour.Instantiate(CardLib.GetCard(p_card_ID)));
    }

    /// <summary>
    /// 将一张卡牌加入手牌
    /// </summary>
    /// <param name="p_card_ID">卡牌ID</param>
    /// <param name="amount">卡牌数量</param>
    public static void AddCardToHand(string p_card_ID, int amount)
    {
        CardBehaviour p_card = CardLib.GetCard(p_card_ID);

        for (int i = 0; i < amount; i++)
        {
            CardBehaviour card = MonoBehaviour.Instantiate(p_card);
            DungeonManager.Instance.battleManager.cardFlow.AddCardToHand(card);
        }
    }

    /// <summary>
    /// 将一张卡牌从战斗中和牌组中移除
    /// </summary>
    /// <param name="card">要移除的卡牌</param>
    public static void RemoveCardFromBattle(CardBehaviour card)
    {
        PlayerRemoveCardFromDeck(card.Id);

        DungeonManager.Instance.battleManager.board.RemoveCard(card);
        
        card.removedFromBattleAndDeck = true;
        MonoBehaviour.Destroy(card.gameObject);
    }

    /// <summary>
    /// 开启棋盘上随机格子
    /// </summary>
    /// <param name="amount">开启的数量</param>
    public static void EnableRandomSquareAction(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            List<Square> s = DungeonManager.Instance.battleManager.board.AllDisabledSquares;
            int randIndex = Random.Range(0,s.Count);
            s[randIndex].IsActive = true;
        }
    }

    /// <summary>
    /// 禁用棋盘上随机格子
    /// </summary>
    /// <param name="amount">禁用的数量</param>
    public static void DisableRandomSquareAction(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            List<Square> s = DungeonManager.Instance.battleManager.board.AllEnabledSquares;
            int randIndex = Random.Range(0,s.Count);
            s[randIndex].IsActive = false;
        }
    }

    /// <summary>
    /// 对随机目标造成伤害
    /// </summary>
    /// <param name="source">攻击的来源</param>
    /// <param name="damage">伤害数值</param>
    public static void RandomDamageAction(CreatureBehaviour source, int damage)
    {
        DamageAction(DungeonManager.Instance.battleManager.enemyGroup.GetRandomEnemy(), source, damage);
    }

    /// <summary>
    /// 去除最高的Debuff
    /// </summary>
    /// <param name="target"></param>
    public static void ClearHighestDebuffAction(CreatureBehaviour target)
    {
        target.buffOwner.GetHighestDebuff().Amount = 0;
    }

    /// <summary>
    /// 从消耗堆中恢复随机卡牌
    /// </summary>
    /// <param name="amount">卡牌数量</param>
    public static void GetRandomCardFromExhaustPile(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DungeonManager.Instance.battleManager.cardFlow.RestoreRandomCardFromExhaustPile();
        }
    }

    /// <summary>
    /// 召唤敌人
    /// </summary>
    /// <param name="enemyID">敌人的ID</param>
    public static void SummonEnemyAction(string enemyID)
    {
        DungeonManager.Instance.battleManager.enemyGroup.AddEnemyToBattle(EnemyLib.GetEnemy(enemyID));
    }

    #endregion

    #region Dungeon Related Actions

    /// <summary>
    /// 改变玩家的金钱
    /// </summary>
    /// <param name="variation">变化量</param>
    public static void PlayerChangeMoney(int variation)
    {
        if (DungeonManager.Instance.Player.HasRelic("NutritionDetector"))
        {
            if (variation > 0)
            {
                variation = (int)(1.25f * variation);
            }
        }

        DungeonManager.Instance.Player.Nutrition += variation;
    }

    /// <summary>
    /// 给予玩家新的遗物
    /// </summary>
    /// <param name="p_Relic">遗物预制体</param>
    public static void PlayerGainRelic(RelicBehaviour p_Relic)
    {
        DungeonManager.Instance.Player.p_Relics.Add(p_Relic);
    }

    /// <summary>
    /// 给予玩家新的卡牌并添加至卡组
    /// </summary>
    /// <param name="card">卡牌预制体</param>
    public static void PlayerAddCardToDeck(CardBehaviour card)
    {
        DungeonManager.Instance.Player.p_Deck.Add(card);
    }

    /// <summary>
    /// 从卡组中移除一张特定卡牌
    /// </summary>
    /// <param name="ID">卡牌ID</param>
    public static void PlayerRemoveCardFromDeck(string ID)
    {
        foreach (CardBehaviour card in DungeonManager.Instance.Player.p_Deck)
        {
            if (card.Id == ID)
            {
                DungeonManager.Instance.Player.p_Deck.Remove(card);
                return;
            }
        }
    }

    #endregion
}
