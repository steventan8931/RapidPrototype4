using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCanvasTransition : MonoBehaviour
{
    public RectTransform m_OnScreenPos;
    public RectTransform m_OffScreenPos;

    public RectTransform m_BuidlSection;

    public bool m_OnScreen = false;
    public float m_TransitionSpeed = 1.0f;

    private void Update()
    {
        if (m_OnScreen)
        {
            m_BuidlSection.position = Vector3.Lerp(m_BuidlSection.position, m_OnScreenPos.position, Time.deltaTime * m_TransitionSpeed);
        }
        else
        {
            m_BuidlSection.position = Vector3.Lerp(m_BuidlSection.position, m_OffScreenPos.position, Time.deltaTime * m_TransitionSpeed);
        }
    }
}
