using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェード処理
/// </summary>
public class FadeManager
{
    private const float DEFAULT_FADE_SPEED = 0.01f;
    private const string FADE_CANVAS_PATH = "Prefab/CanvasFade";

    private Image fadeImg;
    private float alpha;

    public enum FADE_STATUS : int
    {
        NONE = 0,
        FADE_IN,
        FADE_OUT,
        FADE_FINISH,
    }
    private FADE_STATUS status = FADE_STATUS.NONE;
    public FADE_STATUS Status { get { return status; } }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FadeManager()
    {
        GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(FADE_CANVAS_PATH));
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        fadeImg = obj.GetComponentInChildren<Image>();
    }

    /// <summary>
    /// Update
    /// </summary>
    public void FadeUpdate()
    {
        switch(this.status)
        {
            case FADE_STATUS.FADE_IN:
                FadeIn();
                break;
            case FADE_STATUS.FADE_OUT:
                FadeOut();
                break;
            case FADE_STATUS.FADE_FINISH:
                break;
        }
    }

    /// <summary>
    /// フェードモード設定
    /// </summary>
    /// <param name="status"></param>
    public void SetFadeMode(FADE_STATUS status)
    {
        this.status = status;
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    private void FadeIn()
    {
        if (this.alpha < 1.0f)
        {
            this.alpha += DEFAULT_FADE_SPEED;
            this.fadeImg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        } else {
            SetFadeMode(FADE_STATUS.FADE_FINISH);
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    private void FadeOut()
    {
        if (this.alpha > 0.0f)
        {
            this.alpha -= DEFAULT_FADE_SPEED;
            this.fadeImg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        } else {
            SetFadeMode(FADE_STATUS.FADE_FINISH);
        }
    }
}
