using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]

public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] Color defultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

        void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying) {
        DisplayCoordinates();
        UpdateObjectName();
        }
        SetLabelColor();
    }

    void SetLabelColor() {

        if (gridManager == null) {return ;}

        Node node = gridManager.GetNode(coordinates);

        if (node == null) {return ;}

        if(!node.isWalkable) {
        label.color = blockedColor;
        label.text = "Blocked";
        }
        else if (node.isPath) {
        label.color = pathColor;
        label.text = "Path";
        }
        else if (node.isExplored) {
        label.color = exploredColor;
        label.text = "Explored";
        }
        else {
        label.color = defultColor;
        label.text = "none";
        }

    }
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y=  Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;

    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
