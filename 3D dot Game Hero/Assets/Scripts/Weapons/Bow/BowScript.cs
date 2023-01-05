using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BowScript : WeaponScript {

    #region Parameters

    [Header("References")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Vector3 arrowSpawnPoint;
    [SerializeField] private Vector3 arrowSpawnRotation;

    [Header("Bow states")]
    [SerializeField] private float arrowPrepTime = 1.5f;
    private bool armed = false;
    private GameObject arrow = null;

    #endregion

    #region IEnumerators

    IEnumerator delayArrowPrepRoutine(float time) {
        yield return new WaitForSeconds(time);
        if (arrow != null) armed = true;
    }

    #endregion

    #region Abstract Methods

    public override void Attack() {
        spawnArrow();
        StartCoroutine(delayArrowPrepRoutine(arrowPrepTime));
    }

    public override void Release() {
        if (armed) shootArrow();
        else dispawnArrow();

        AttackFinished();
    }

    public override void Abort() {
        dispawnArrow();
    }

    public override void Collided() {
    }

    #endregion

    #region Private Methods

    private void shootArrow() {
        armed = false;
        arrow.GetComponent<ArrowScript>().ShootArrow(transform.worldToLocalMatrix.MultiplyVector(transform.forward));
        arrow = null;
        SoundManager.Instance.PlayBowShoot();
    }

    private void spawnArrow() {
        arrow = Instantiate(arrowPrefab);
        arrow.transform.parent = transform;
        arrow.transform.localPosition = new Vector3(0,0,0);
        arrow.transform.localEulerAngles = new Vector3(0,0,0);
        arrow.GetComponent<ArrowScript>().SetLayer(transform.parent.gameObject.layer == (int)Layers.Player ? Layers.Player : Layers.Enemies);
        arrow.GetComponent<ArrowScript>().SetColliderState(false);
    }

    private void dispawnArrow() {
        armed = false;
        if (arrow != null) arrow.GetComponent<ArrowScript>().DestroyArrow();
        arrow = null;
    }

    #endregion

    #region MonoBehaviour Methods

    private void OnEnable() {
        if (armed) dispawnArrow();
        armed = false;
    }

    public override void Start() {
        base.Start();

        usesLeft = int.MaxValue;

        transform.GetComponent<RotationConstraint>().enabled = false;
        transform.localEulerAngles = arrowSpawnRotation;
    }

    #endregion

}
