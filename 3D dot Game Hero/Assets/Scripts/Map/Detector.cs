using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private Puzzle3Manager puzzle3;
        private bool wasDetected = false;
        // Use this for initialization

        public void SetDetectedToFalse() {
            wasDetected = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == Tags.Wall.ToString() && Vector3.Distance(transform.position, other.transform.position) <= 0.4f)
            {
                other.gameObject.transform.position = transform.position;
                other.gameObject.GetComponent<ObsMangEnd>().Blocked();
                if (!wasDetected) {
                    puzzle3.aButtonHaveBeenPressed();
                    wasDetected = true;
                }
            }
        }
    }
}