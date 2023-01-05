using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private Puzzle3Manager puzzle3;
        // Use this for initialization

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == Tags.Wall.ToString() && Vector3.Distance(transform.position, other.transform.position) <= 0.4f)
            {
                other.gameObject.transform.position = transform.position;
                other.gameObject.GetComponent<ObsMangEnd>().Blocked();
                puzzle3.aButtonHaveBeenPressed();
            }
        }
    }
}