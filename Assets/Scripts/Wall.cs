using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactable
{
    private void Update()
    {
        if (m_Health <= 0 )
        {
            Destroy(gameObject);
        }
    }


}
