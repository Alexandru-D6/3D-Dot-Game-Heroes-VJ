using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BombScript : WeaponScript {

#region Parameters

    [Header("Bomb Reference")]
    [SerializeField] private GameObject sceneObjects;
    [SerializeField] private GameObject player;
    [SerializeField] private FollowAnchor followAnchorScript;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explotionParticles;
    [SerializeField] private GameObject explotionCollider;

    [Header("Bomb Parameters")]
    [Range(0.0f,1.0f)]
    [SerializeField] private float attackDelay = 0.25f;
    [Range(0.0f,5.0f)]
    [SerializeField] private float explotionDelay = 0.1f;
    [SerializeField] private Vector3 bombSpawnRotation;
    [SerializeField] private Vector3 throwVelocity;

#endregion

#region IEnumerators

    IEnumerator delayedAttackRoutine(float time) {
        yield return new WaitForSeconds(time);

        ActuallyAttack();
        AttackFinished();
    }

    IEnumerator delayedColliderRoutine(float time) {
        yield return new WaitForSeconds(time);

        transform.GetComponent<Collider>().enabled = true;
    }

    IEnumerator delayedExplotionRoutine(float time) {
        yield return new WaitForSeconds(time);

        Explode();
    }

    IEnumerator delayedDestroyRoutine(float time) {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

#endregion

#region Abstract Methods

    public override void Attack() {
        StartCoroutine(delayedAttackRoutine(attackDelay));
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

    #region Public Methods

    public void StartCoroutines() {
        StartCoroutine(delayedColliderRoutine(0.1f));
        StartCoroutine(delayedExplotionRoutine(explotionDelay));
        SoundManager.Instance.PlayMinecraftTNT();
    }

    #endregion

    #region Private Methods

    private void ActuallyAttack() {

        GameObject newBomb = Instantiate(bombPrefab, transform.position, transform.rotation, sceneObjects.transform);
        newBomb.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        newBomb.GetComponent<FollowAnchor>().enabled = false;

        Vector3 _throw = player.transform.forward;
        _throw += Vector3.up;
        _throw = new Vector3(_throw.x * throwVelocity.x, _throw.y * throwVelocity.y, _throw.z * throwVelocity.z);

        newBomb.GetComponent<Rigidbody>().velocity = _throw + player.GetComponent<Rigidbody>().velocity;
        newBomb.GetComponent<Collider>().enabled = false;
        newBomb.GetComponent<BombScript>().StartCoroutines();
    }

    private void Explode() {
        Instantiate(explotionParticles, transform.position, transform.rotation, sceneObjects.transform);
        explotionCollider.SetActive(true);

        StartCoroutine(delayedDestroyRoutine(0.5f));
    }

    #endregion

#region MonoBehaviour Methods

    public override void Start() {
        base.Start();

        // TODO: handle the uses when more bomb are gifted
        usesLeft = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        sceneObjects = GameObject.FindGameObjectWithTag("SceneObjects");

        transform.GetComponent<RotationConstraint>().enabled = false;
        transform.localEulerAngles = bombSpawnRotation;
    }

    void Update() {
    }

#endregion

}
