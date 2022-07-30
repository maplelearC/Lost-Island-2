using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private Vector3 mouseWorldPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private bool canClicks = false;

    void Update()
    {
        canClicks = ObjectAtMousePosition();

        if (canClicks && Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }
    
    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport=clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
                case "Item":
                    var item = clickObject.GetComponent<Item>();
                    item?.ItemClicked();
                break;
        }
    }
    
    /// <summary>
    /// 检测鼠标点击范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
