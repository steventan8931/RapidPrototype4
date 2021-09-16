using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuddyWaypointScr : MonoBehaviour
{
    public Image waypointImg;
    public Transform buddyLoc;
    public Text dist;
    public Vector3 offset;
    public GameObject player;
    // Update is called once per frame

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float minX = waypointImg.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = waypointImg.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(buddyLoc.position + offset);

        //if buddy is out of camera and behind player
        if (Vector3.Dot((buddyLoc.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        waypointImg.transform.position = pos;
        dist.text = ((int)Vector3.Distance(buddyLoc.position, player.transform.position)).ToString();
        
    }
}
