using UnityEngine;
using System.Collections;

public class WeaponBigSword : Weapon{

    // 使用している武器のタイプを返す。
    public override USE_WEAPON_TYPE GetWeaponType()
    {
        return USE_WEAPON_TYPE.BIG_SWORD;
    }
}
