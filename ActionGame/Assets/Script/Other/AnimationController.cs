using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;

    public void Init()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(string name, float frame, int layer = 0)
    {
        if (anim == null) return;

        anim.Play(name, layer, frame);
    }

    public void SetAnimation(string name)
    {
        if (anim == null) return;

        anim.Play(name);
    }

    public bool GetIsEndAnimation(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1.0f;
    }

    public float GetNormalizedTime(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime;
    }
}
