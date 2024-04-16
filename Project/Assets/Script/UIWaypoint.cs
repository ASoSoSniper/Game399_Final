using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaypoint : MonoBehaviour
{
    public Image waypointImage;
    public Transform waypointTransform;

    // Update is called once per frame
    void Update()
    {
        float minX = waypointImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = waypointImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 position = Camera.main.WorldToScreenPoint(waypointTransform.position);  

        if (Vector3.Dot(waypointTransform.position - transform.position, transform.forward) < 0)
        {
            if (position.x < Screen.width / 2)
            {
                position.x = maxX;
            }
            else
            {
                position.x = minX;
            }
        }

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        waypointImage.transform.position = position;
    }
}
