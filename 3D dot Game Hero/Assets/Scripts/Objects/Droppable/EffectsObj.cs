using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsObj : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Player)
        {
                
            if (this.gameObject.tag == Tags.Coin.ToString())
            {
                other.GetComponent<PlayerManager>().ReceiveItem(Tags.Coin);
            }
            else if (this.gameObject.tag == Tags.Hamburguer.ToString())
            {
                other.GetComponent<PlayerManager>().ReceiveItem(Tags.Hamburguer);
            }
            Destroy(this.gameObject);
        }
    }
}
