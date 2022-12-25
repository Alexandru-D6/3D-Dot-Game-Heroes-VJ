using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : WeaponScript {

    #region Parameters

    [Header("References")]
    [SerializeField] private GameObject arrowPrefab;
    private GameObject arrow;

    [Header("Bow states")]
    private bool armed = false;

    #endregion

    #region Abstract Methods

    public override void Attack() {
        if (!armed) {
            spawnArrow();
            armed = true;
        }else {
            shootArrow();
            armed = false;
        }
    }

    public override void Release() {
        // Shot arrow
    }

    public override void Collided() {
    }

    #endregion

    #region Private Methods

    private void shootArrow() {

    }

    private void spawnArrow() {

    }

    private void dispawnArrow() {

    }

    #endregion

    #region MonoBehaviour Methods

    private void OnEnable() {
        armed = false;
        dispawnArrow();
    }

    void Update()
    {
        
    }

    #endregion

}
