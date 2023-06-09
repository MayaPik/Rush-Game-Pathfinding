using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //this is how you make it visable when you attach it to mono behivor;

    public class Node //pure c#- can't attach to game object;
    {
        public Vector2Int coordinates;
        public bool isWalkable;
        public bool isExplored;
        public bool isPath;
        public Node connectedTo;

        public Node(Vector2Int coordinates, bool isWalkable)
        {
            this.coordinates = coordinates;
            this.isWalkable = isWalkable;
        }
    }
