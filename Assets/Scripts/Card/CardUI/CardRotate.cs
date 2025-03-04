using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotate : MonoBehaviour
{
    /// <summary>
    /// 卡牌的可旋转性
    /// </summary>
    public bool rotateable{ get{return GetComponent<CardUI>().UIState == UIStates.DRAG;} }

    /// <summary>
    /// 卡牌的方块要组成的形状，通过一组向量表示每个方块相对原点的位置
    /// </summary>
    List<Vector2> CardShape
    { 
        get{return GetComponent<CardBehaviour>().CardShape;} 
        set{GetComponent<CardBehaviour>().CardShape = value;}
    }

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    List<Vector2> ConditionsShape
    { 
        get{return GetComponent<CardBehaviour>().ConditionsShape;} 
        set{GetComponent<CardBehaviour>().ConditionsShape = value;}
    }

    /// <summary>
    /// 卡牌的方块要组成的默认形状
    /// </summary>
    List<Vector2> OriginalCardShape{ get{return GetComponent<CardBehaviour>().cardData.CardShape;} }

    /// <summary>
    /// 卡牌特效格子的默认形状
    /// </summary>
    List<Vector2> OriginalConditionsShape{ get{return GetComponent<CardBehaviour>().cardData.ConditionsShape;} }

    /// <summary>
    /// 格子的根物体
    /// </summary>
    GameObject blocks{ get{return GetComponent<CardSwitchMode>().blockMode;} }

    /// <summary>
    /// 旋转卡牌的方块模式
    /// </summary>
    /// <param name="degree">旋转角度</param>
    public void RotateCard(float degree)
    {
        if(rotateable)
        {
            for (int i = 0; i < CardShape.Count; i++)
            {
                CardShape[i] = MathHelper.Rotate2DVector(CardShape[i], degree);
            }

            for (int i = 0; i < ConditionsShape.Count; i++)
            {
                ConditionsShape[i] = MathHelper.Rotate2DVector(ConditionsShape[i], degree);
            }

            blocks.transform.Rotate(new Vector3(0,0,degree));
        }
        
    }

    /// <summary>
    /// 把旋转过的卡牌归位
    /// </summary>
    public void Homing()
    {
        CardShape = DeepCopy.DeepCopyValueTypeList<Vector2>(OriginalCardShape);
        ConditionsShape = DeepCopy.DeepCopyValueTypeList<Vector2>(OriginalConditionsShape);
        blocks.transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GetComponent<CardUI>().UIState == UIStates.DRAG)
        {
            RotateCard(90);
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     foreach (Vector2 cardShape in CardShape)
    //     {
    //         Gizmos.DrawCube(transform.position + new Vector3(cardShape.x,cardShape.y,0),new Vector3(1,1,1));
    //     }
    // }

}
