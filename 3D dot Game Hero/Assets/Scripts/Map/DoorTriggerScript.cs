using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour {

    enum Position { North, East, South, West };

    [SerializeField] private Position triggerPosition;
    [SerializeField] private GameObject room;

    [SerializeField] private RoomManager roomManager;
    [SerializeField] private float delay = 0.5f;

    IEnumerator delayedPlayerPresence(float time) {
        yield return new WaitForSeconds(time);

        if (roomManager != null) roomManager.ChangePlayerPresence();
    }

    private Vector2 getDirection() {
        switch(triggerPosition) {
            case Position.North:
                return new Vector2(0.0f,1.0f);
            case Position.East:
                return new Vector2(1.0f,0.0f);
            case Position.South:
                return new Vector2(0.0f,-1.0f);
            case Position.West:
                return new Vector2(-1.0f,0.0f);
        }
        return new Vector2(0.0f,0.0f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals(Tags.Player.ToString())) {
            SceneManagement.Instance.ChangeRoom(getDirection(), room);

            StartCoroutine(delayedPlayerPresence(delay));
        }
    }

}
