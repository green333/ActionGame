using UnityEngine;
using System.Collections;

/*
 *  武器の規定クラス
 * 
 */
public abstract class Weapon{

    // 使用する武器タイプ
    // この定数の変数は作らず、継承先のgetWeaponTypeで自身のタイプを返すようにする
    public enum USE_WEAPON_TYPE
    {
        BIG_SHIELD,   // 盾(特大)
        JAPANEASE_SWORD,// 日本刀
        BIG_SWORD,    // 大剣
        GUN,            // 銃
        SICKLE,         // 鎌
    }

    // 武器パラメーター
    // TODO:現時点ではstructだげ、変数が増えていけばclassに変える必要があるので注意。
    public struct Parameter
    {
        public int attack;
        public int defence;
    }
    protected Parameter parameter;

    // 使用している武器のタイプを取得する
    public abstract USE_WEAPON_TYPE GetWeaponType();

    // 予ダメ算出関数
    // TODO:仮関数()
    static public int CalcDamage(int enemyDef) { return 0; }

}
