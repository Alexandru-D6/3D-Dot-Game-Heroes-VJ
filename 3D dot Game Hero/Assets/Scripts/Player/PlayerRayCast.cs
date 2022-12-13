using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 20f, layerMask)) 
        {
            if (hitInfo.transform.tag.Equals("Enderman")) 
            {
                transform.GetComponent<EndermanMov>().alertEnderman();
                Debug.Log("visto");
            }
        }
    }
}
