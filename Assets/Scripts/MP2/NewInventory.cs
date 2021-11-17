using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventory : MonoBehaviour
{
    public Text m_MagicCrystalUI;
    public int m_MagicCrystalCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("give me monies");
            m_MagicCrystalCount += 100;
        }
        m_MagicCrystalUI.text = m_MagicCrystalCount.ToString();
    }
}
