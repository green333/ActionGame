using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

    private Canvas m_canvas = null;
    private Slider m_hpSlider = null;
    private int m_damgeAccumu = 0;
    public void Initialize(int hp,Vector3 pos)
    {
        m_canvas    = GetComponentInChildren<Canvas>() as Canvas;
        m_hpSlider  = GetComponentInChildren<Slider>() as Slider;
        m_hpSlider.maxValue = hp;
        m_hpSlider.value    = hp;
        m_hpSlider.transform.position = pos;
        m_canvas.worldCamera = Camera.main;
    }

    public void SubHPValue(int value)
    {
        m_damgeAccumu += value;
    }

    // Use this for initialization-
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	     if(m_damgeAccumu <= 0) { return; }
         if(--m_hpSlider.value < 0)
        {
            m_hpSlider.value = 0;
        }
        --m_damgeAccumu;
    }
}
