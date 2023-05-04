using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords {get{return startCoords;}}
    [SerializeField] Vector2Int endCoords;
    public Vector2Int EndCoords {get{return endCoords;}}

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Dictionary<Vector2Int,Node> reached = new Dictionary<Vector2Int,Node>();
    Queue<Node> front = new Queue<Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int,Node>();

    void Awake() {

        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null) {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            endNode = grid[endCoords];
        }

    }
    void Start()
    {
        GetNewPath();
    }
    
    public List<Node> GetNewPath() {
        return GetNewPath(startCoords);
    }
     public List<Node> GetNewPath(Vector2Int coordinates) {
        gridManager.ResetNodes();
        BFS(coordinates);
        return BuildPath();
    }


void ExploreNeighbors() {

    List<Node> neighbors = new List<Node>();

    foreach (Vector2Int direction in directions)
    {
        Vector2Int neighborsCoords = currentSearchNode.coordinates + direction;

        if (grid.ContainsKey(neighborsCoords)) {

            Node neighbor = grid[neighborsCoords];
            if (!neighbor.isExplored && neighbor.isWalkable) { // only add non-explored and non-blocked nodes
                neighbors.Add(neighbor);
            }
        }
    }

    foreach (Node neighbor in neighbors)
    {
        if (!reached.ContainsKey(neighbor.coordinates)) {
            neighbor.connectedTo = currentSearchNode;
            reached.Add(neighbor.coordinates, neighbor);
            front.Enqueue(neighbor);
        }
    }

}
    void BFS(Vector2Int coordinated) {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        front.Clear();
        reached.Clear();
        bool isRunning = true;

        front.Enqueue(grid[coordinated]);
        reached.Add(coordinated, grid[coordinated]);

        while(front.Count > 0 && isRunning) 
        {
            currentSearchNode = front.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == endCoords) {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null) 
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates) {

        if (grid.ContainsKey(coordinates)) {
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = true;

            if (newPath.Count <=1) {
                GetNewPath();
                return true;
            }
        return false;
        }
    return false;
    }

 
    public void NotifyReceivers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
