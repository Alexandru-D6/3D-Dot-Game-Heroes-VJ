using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    #region parameters

    [SerializeField] private List<PathCreator> pathsFollowers;
    public EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private PathCreator currentPathFollower;
    [SerializeField] private float speed = 5;
    private float distanceTravelled = 0;

    #endregion

    void Start() {
        currentPathFollower = pathsFollowers[0];

        if (currentPathFollower != null) {
            currentPathFollower.pathUpdated += OnPathChanged;
        }
    }

    private void ChangeToNearestPath(float maxDist) {
        foreach(var x in pathsFollowers) {
            if (x != currentPathFollower) {
                Vector3 point = x.path.GetClosestPointOnPath(transform.position);

                if (Vector3.Distance(point, transform.position) <= maxDist) {

                    if (1 <= Random.Range(0,99)) {
                        currentPathFollower = x;
                        distanceTravelled = currentPathFollower.path.GetClosestDistanceAlongPath(transform.position);
                    }
                }
            }
        }
    }

    void Update() {
        if (currentPathFollower != null) {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = currentPathFollower.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = currentPathFollower.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        ChangeToNearestPath(1.0f);
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = currentPathFollower.path.GetClosestDistanceAlongPath(transform.position);
    }
}
