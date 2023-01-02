using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLogic : MonoBehaviour {

    #region parameters

    [Header("References")]
    [SerializeField] private DragonAnimations animator;

    [Header("Paths configuration")]
    [SerializeField] private List<PathCreator> pathsFollowers;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private float speed = 5;
    private PathCreator currentPathFollower;
    private PathCreator lastPathFollower = null;
    private float distanceTravelled = 0;

    [Header("Other")]
    [SerializeField] private float rotationSpeed = 3.0f;

    [Header("States")]
    private bool isFollowingPath = true;
    private bool isFlying = false;
    private bool isAttacking = false;
    private bool skipNextFly = false;
    private bool flamethrower = false;

    [Header("Flamethrower")]
    [SerializeField] private float flamethrowerDelay = 1.0f;
    [SerializeField] private float flyTime = 10.0f;
    private bool canFlameThrower = true;

    [Header("Foot Kick")]
    [SerializeField] private float footKickDelay = 1.0f;
    [SerializeField] private Collider playerDetector;
    [SerializeField] private Collider rightCollider;
    [SerializeField] private Collider leftCollider;
    private bool canFootKick = true;

    [Header("Probabilities")]
    [Range(0,100)]
    [SerializeField] private float changePathProbability = 2;
    [Range(0,100)]
    [SerializeField] private float startFlyingProbability = 30;
    [Range(0,100)]
    [SerializeField] private float groundFlamethrowerProbability = 60;
    [Range(0,100)]
    [SerializeField] private float simpleFlamethrowerProbability = 50;
    [Range(0,100)]
    [SerializeField] private float leftFootKickProbability = 50;

    #endregion

    IEnumerator delayedActiveFollowRoutine(float time) {
        yield return new WaitForSeconds(time);

        if (isFlying) animator.forceFlying();
        else animator.toIdle();

        isFlying = false;
        flamethrower = false;
        canFlameThrower = true;
        isFollowingPath = true;

        animator.SetFlamethrower(false);

        playerDetector.enabled = true;
    }

    IEnumerator delayedFlamethrowerRoutine(float time) {
        yield return new WaitForSeconds(time);

        canFlameThrower = true;
    }

    IEnumerator delayedFootKickRoutine(float time) {
        yield return new WaitForSeconds(time);

        canFootKick = true;
        isAttacking = false;
        isFollowingPath = true;

        animator.toIdle();
        animator.enableRunning(true);
    }

    private void OnTriggerEnter(Collider other) {
        if (canFootKick && !isAttacking && (other.tag == Tags.Player.ToString() || other.gameObject.layer == (int)Layers.Weapon)) {
            isFollowingPath = false;
            isAttacking = true;
        }
    }

    private void ChangeToNearestPath(float maxDist) {
        foreach(var x in pathsFollowers) {
            // The conditions to change the path are:
            //      1.- Not be the same path
            //      2.- Meanwhile the last path is not the same as the next one or if the next one is the main path
            // There will be a probability of 2% that this change will occur

            if (x != currentPathFollower && (lastPathFollower == null || x != lastPathFollower || x == pathsFollowers[0])) {
                Vector3 point = x.path.GetClosestPointOnPath(transform.position);

                if (Vector3.Distance(point, transform.position) <= maxDist && Random.Range(0,100) < changePathProbability) {
                    lastPathFollower = currentPathFollower;
                    currentPathFollower = x;
                    distanceTravelled = currentPathFollower.path.GetClosestDistanceAlongPath(transform.position);
                }
            }
        }
    }

    private void LookAt(float rotY) {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f,rotY,0.0f), rotationSpeed);
    }

    private void HandleFlyingFlamethrower() {
        if (!skipNextFly && currentPathFollower == pathsFollowers[0] && Mathf.Abs(currentPathFollower.path.length - (distanceTravelled % currentPathFollower.path.length)) < 0.5f) {
            if (Random.Range(0,100) < startFlyingProbability) {
                isFlying = true;
                playerDetector.enabled = false;
                animator.enableFlying(true);
            }else if (Random.Range(0,100) < groundFlamethrowerProbability){
                animator.toIdle();
            }else {
                return;
            }

            isFollowingPath = false;
            skipNextFly = true;
            flamethrower = true;
            StartCoroutine(delayedActiveFollowRoutine(flyTime));

        } else if (skipNextFly && currentPathFollower == pathsFollowers[0]) {
            skipNextFly = Mathf.Abs(currentPathFollower.path.length - (distanceTravelled % currentPathFollower.path.length)) <= 0.5f;
        }
    }

    private void PathRoutine() {
        if (currentPathFollower != null) {
            if (isFollowingPath) animator.enableRunning(true);

            distanceTravelled += speed * Time.deltaTime;
            transform.position = currentPathFollower.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = currentPathFollower.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            HandleFlyingFlamethrower();
        }

        ChangeToNearestPath(0.2f);
    }

    private void FlamethrowerAttack() {
        if (!canFlameThrower) return;

        if (isFlying) {
            if (Random.Range(0, 100) < simpleFlamethrowerProbability) animator.Breath_Fs();
            else animator.Breath_Fw();
        }else {
            if (Random.Range(0, 100) < simpleFlamethrowerProbability) animator.Breath_Gs();
            else animator.Breath_Gw();
        }
        canFlameThrower = false;
    }

    private void AttackRoutine() {
        if (!canFootKick) return;

        if (Random.Range(0, 100) < leftFootKickProbability) {
            animator.AttackClawL();
            leftCollider.enabled = true;
        } else {
            animator.AttackClawR();
            rightCollider.enabled = true;
        }

        canFootKick = false;
    }

    void Start() {
        currentPathFollower = pathsFollowers[0];

        if (currentPathFollower != null) {
            currentPathFollower.pathUpdated += OnPathChanged;
        }
    }

    void Update() {
        if (isFollowingPath) PathRoutine();
        if (isFlying || flamethrower) LookAt(0.0f);
        if (flamethrower) FlamethrowerAttack();
        if (isAttacking) AttackRoutine();
    }


    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = currentPathFollower.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void OnFlamethrowerFinished() {
        StartCoroutine(delayedFlamethrowerRoutine(flamethrowerDelay));
        animator.SetFlamethrower(false);
    }

    public void OnFlamethrowerStarted() {
        animator.SetFlamethrower(true);
    }

    public void OnFootKickFinished() {
        StartCoroutine(delayedFootKickRoutine(footKickDelay));
        leftCollider.enabled = false;
        rightCollider.enabled = false;
    }
}
