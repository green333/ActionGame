using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary> 所持者 </summary>
    public enum POSSESSOR : int
    {
        NONE = 0,
        PLAYER,
        ENEMY
    };

    protected POSSESSOR itemPossessor;
    protected string itemName;
    protected string itemDetail;

    protected virtual void ItemTriggerEnter()
    {
        LogExtensions.OutputWarn("オーバーライドされてません！ : ItemTriggerEnter");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        ItemTriggerEnter();
    }
}
