using UnityEngine;
using System.Collections;
 
public class WeaponJapaneseSword : Weapon
{
    // 使用している武器のタイプを返す。
    public override USE_WEAPON_TYPE GetWeaponType()
    {
        return USE_WEAPON_TYPE.JAPANEASE_SWORD;
    }
}
