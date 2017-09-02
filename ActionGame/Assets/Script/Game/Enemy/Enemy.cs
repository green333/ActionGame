using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 全エネミーの親クラスとなる
/// 
/// 敵成長マスタパラメーターは保持するが、敵基本マスタパラメーターは保持しない。
/// 生成される敵は各々レベルが違うためパラメーターが変わるから保持する必要性があるが、
/// 敵基本は敵の名前の敵が落とすアイテム情報を管理しており、これはレベルによって変わることはないため、
/// ここでは管理しない
/// </summary>
public class Enemy : MonoBehaviour
{

    /// <summary>　敵のレベル毎の成長データ </summary>
    protected EnemyGrowthMaster.Param m_growthStatus;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
  
    }
       
    /// <summary>
    /// 敵を生成させる
    /// </summary>
    /// <param name="growthParam"></param>
    public void Spawn(EnemyGrowthMaster.Param growthParam)
    {
        m_growthStatus = growthParam;
    }
}
