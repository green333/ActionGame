using UnityEngine;
using System.Collections;

public class Herbs : Item
{
    public void Init(POSSESSOR possessor)
    {
        itemPossessor = possessor;
        itemName = "薬草";
        itemDetail = "体力を回復すると思う";
    }

    protected override void ItemTriggerEnter()
    {
        LogExtensions.OutputInfo("薬草ゲッチュ");
        itemPossessor = POSSESSOR.PLAYER;
    }
}
