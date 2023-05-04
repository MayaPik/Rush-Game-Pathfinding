using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f,5f)] float speed = 2f;
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

   void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);

    }

    void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
     {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath) {
            coordinates = pathFinder.StartCoords;
        } else {
            coordinates = gridManager.GetCoordsFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());

    }

    void ReturnToStart() {
        transform.position = gridManager.GetPositionFromCoords(pathFinder.StartCoords);

    }

    void FinishPath() {
       enemy.Penalty();
       gameObject.SetActive(false);
    } 

    IEnumerator FollowPath() {
        for (int i =1; i< path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoords(path[i].coordinates);
            float travelPerecnt = 0f;

            transform.LookAt(endPosition);
            while (travelPerecnt < 1f)
            {
            travelPerecnt += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, travelPerecnt);
            yield return new WaitForEndOfFrame();

            }

        }
      FinishPath();
    }
}
