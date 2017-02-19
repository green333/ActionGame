using UnityEngine;
using System.Collections;

public class WeaponGun : Weapon {

    // 使用している武器のタイプを返す。
    public override USE_WEAPON_TYPE GetWeaponType()
    {
        return USE_WEAPON_TYPE.GUN;
    }
}
