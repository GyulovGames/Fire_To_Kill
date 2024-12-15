using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.EventSystems;

public class ddddd : MonoBehaviour
{

    public Camera cam;
    private void Update()
{
    if ( Input.GetMouseButton(0))
    {
        // Get the mouse position in world space
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction from the object to the mouse position
        Vector3 direction = mousePosition - transform.position;
        // Calculate the angle from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Set the rotation of the object
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
}
