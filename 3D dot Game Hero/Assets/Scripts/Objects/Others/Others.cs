using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Others : MonoBehaviour
{
    [SerializeField] private GameObject destroyedParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Weapon )
        {

            // Exclude weapon List
            if(other.tag.Equals(Tags.Sword.ToString()) || (other.tag.Equals(Tags.Arrow.ToString()) && other.GetComponent<ArrowScript>().GetOriginalLayer() == Layers.Player) || other.tag.Equals(Tags.Bomb.ToString()) || other.tag.Equals(Tags.Boomerang.ToString()))
            {
                Instantiate(destroyedParticles, transform.position, transform.rotation);
                gameObject.SetActive(false);
                gameObject.GetComponent<LootBag>().InstantiateLoot(transform.position);
            }
            
            
        }
    }
}
