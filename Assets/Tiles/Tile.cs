using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {get {return isPlaceable;}}
    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        if(gridManager!= null) {
            coordinates = gridManager.GetCoordsFromPosition(transform.position);
            if(!isPlaceable) {
            gridManager.BlockNode(coordinates);

         }
        }
       
    }
    void OnMouseDown() {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
           bool isSuccussful = towerPrefab.CreateTower(towerPrefab, transform.position);
           if (isSuccussful) {
            gridManager.BlockNode(coordinates);
            pathFinder.NotifyReceivers();
           }
        }
    }  
}
