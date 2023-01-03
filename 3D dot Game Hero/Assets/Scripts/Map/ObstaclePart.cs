using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePart : MonoBehaviour
{
    [SerializeField] private ObstacleParts namePart;
    [SerializeField] private GameObject wholeObstacle;
 
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player.ToString())
        {
            wholeObstacle.GetComponent<ObstacleManager>().PushedFrom(namePart);
        }
        else if(other.tag != Tags.Wall.ToString() && other.tag != Tags.Obstacle.ToString() && other.tag !=Tags.Vase.ToString())
        {
            wholeObstacle.GetComponent<ObstacleManager>().blockAll();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.Player.ToString())
        {
            wholeObstacle.GetComponent<ObstacleManager>().PushedFrom(namePart);
        }

        else if (other.tag != Tags.Wall.ToString() && other.tag != Tags.Obstacle.ToString() && other.tag != Tags.Vase.ToString())
        {
            wholeObstacle.GetComponent<ObstacleManager>().blockAll();
        }
    }
    private void OnTriggerExit()
    {
        wholeObstacle.GetComponent<ObstacleManager>().blockAll();
    }
}
