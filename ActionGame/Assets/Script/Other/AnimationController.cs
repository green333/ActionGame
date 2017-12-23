using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;

    public void Init()
    {
        if (GetComponent<Animator>() == null)
        {
            LogExtensions.OutputError(gameObject.name + "にアニメーターがセットされていません。");
            return;
        }
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(string name, float frame, int layer = 0)
    {
        anim.Play(name, layer, frame);
    }

    public void SetAnimation(string name)
    {
        anim.Play(name);
    }

    // Is系の関数にGetは不要、IsEndAnimation()でOK
    // IsBeginAnimationも欲しい
    public bool IsEndAnimation(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1.0f;
    }

    public float GetNormalizedTime(int layer = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layer).normalizedTime;
    }
}
