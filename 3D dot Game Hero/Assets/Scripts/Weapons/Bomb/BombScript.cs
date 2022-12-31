using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : WeaponScript {

#region Parameters

    [Header("Bomb Reference")]
    [SerializeField] private GameObject sceneObjects;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject player;
    [SerializeField] private FollowAnchor followAnchorScript;

    //[Header("Bomb Parameters")]

#endregion

#region IEnumerators

#endregion

#region Abstract Methods

    public override void Attack() {
        // Create a new gameObject in the same position 
        //transform.parent = sceneObjects.transform;
        //followAnchorScript.enabled = false;

        //Set velocity
    }

    public override void Release() {
        // DO NOTHING
    }

    public override void Abort() {
        // DO NOTHING
    }

    public override void Collided() {
        // DO NOTHING
    }

    #endregion

    #region Virtual Methods

    public override void AttackFinished() {
        base.AttackFinished();

        DecrementUses();

        if (GetLeftUses() == 0) {
            weaponManager.DestroyWeapon((Tags)System.Enum.Parse(typeof(Tags), GetName()));
        }
    }

    #endregion

    #region Private Methods

#endregion

#region MonoBehaviour Methods

    public override void Start() {
        base.Start();

        // TODO: handle the uses when more bomb are gifted
        usesLeft = 3;

        playerHand = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        sceneObjects = GameObject.FindGameObjectWithTag("SceneObjects");
    }

    void Update() {
    }

#endregion

}
