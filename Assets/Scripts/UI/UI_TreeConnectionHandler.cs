using UnityEngine;
using System;



[Serializable]
public class UI_TreeConnectionDetails
{
    public UI_TreeConnectionHandler childNode;
    public NodeDirectionType direction;
    [Range(100f,350f)] public float length;
}


public class UI_TreeConnectionHandler : MonoBehaviour
{
    private RectTransform rect=> GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectionDetails[] connectionDetails;
    [SerializeField] private Ui_TreeConnection[] connections;




    private void OnValidate()
    {
        if(connectionDetails.Length <=0)return;
        if(connectionDetails.Length != connections.Length)
        {
            Debug.Log("Amount of details should be same as amount of connetions. - "+ gameObject.name);
            return;
        }
        UpdateConnections();
    }

    private void UpdateConnections()
    {
        

        for(int i =0; i < connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i];
            var connection = connections[i];

            Vector2 targetPosition = connection.GetConnectionPoint(rect);
            connection.DirectConnection(detail.direction,detail.length);
            detail.childNode?.SetPosition(targetPosition);
            
        } 
    }

    public void SetPosition(Vector2 position )=>rect.anchoredPosition = position;
}
