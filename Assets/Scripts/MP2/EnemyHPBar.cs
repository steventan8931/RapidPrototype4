using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image m_HPBar;
    public NewEnemyAI m_Enemy;

    private void LateUpdate()
    {
        m_HPBar.fillAmount = (m_Enemy.currentHp / m_Enemy.maxHp);
        transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
