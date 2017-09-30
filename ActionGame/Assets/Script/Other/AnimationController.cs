using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;

    public void Init()
    {
        anim = GetComponent<Animator>();
        // ここでnullチェックしよう
    }

    public void SetAnimation(string name, float frame, int layer = 0)
    {
        // このチェックはいらない
        if (anim == null) return;
        anim.Play(name, layer, frame);
    }

    public void SetAnimation(string name)
    {
        // このチェックはいらない
        if (anim == null) return;

        anim.Play(name);
    }

    // Is系の関数にGetは不要、IsEndAnimation()でOK
    // IsBeginAnimationも欲しい
    public bool GetIsEndAnimation(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1.0f;
    }

    public float GetNormalizedTime(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime;
    }
}
