using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsObj : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layers.Player) {
            PlayerManager.Instance.ReceiveItem(TagsUtils.GetTag(transform.tag));

            Destroy(gameObject);
        }
    }
}
