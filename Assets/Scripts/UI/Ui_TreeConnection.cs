using System;
using UnityEditor.Search;
using UnityEngine;

public class Ui_TreeConnection : MonoBehaviour
{
  [SerializeField] private RectTransform rotationPoint;
  [SerializeField] private RectTransform connectionLength;
  [SerializeField] private RectTransform childNodeConnectionPoint;






    public void DirectConnection(NodeDirectionType direction, float length)
    {
        bool shouldBeActive = direction!= NodeDirectionType.None;
        float finalLength = shouldBeActive? length:0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0,0,angle);
        connectionLength.sizeDelta = new Vector2(finalLength,connectionLength.sizeDelta.y);
    }

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.parent as RectTransform,
            childNodeConnectionPoint.position,
            null,
            out var localPosition
        );
        return localPosition;
    }

 
    private float GetDirectionAngle(NodeDirectionType type)
        {
        return type switch
        {
            NodeDirectionType.Up => 90f,
            NodeDirectionType.UpRight => 45f,
            NodeDirectionType.Right => 0f,
            NodeDirectionType.DownRight => -45f,
            NodeDirectionType.Down => -90f,
            NodeDirectionType.DownLeft => -135f,
            NodeDirectionType.Left => 180f,
            NodeDirectionType.UpLeft => 135f,
            _ => 0f,
        };
    }

    }



public enum NodeDirectionType
{
    
    None,
    UpLeft,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
}