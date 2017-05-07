using UnityEngine;
using System.Collections;

public class CharacterStatus {

    /// <summary> ベースパラメータ </summary>
    public PlayerBaseMaster.Param param { get; set; }
    /// <summary> 武器パラメータ </summary>
    public WeaponMaster.Param weaponParam { get; set; }
    /// <summary> 経験値 </summary>
    public int exp { get; set; }

}
