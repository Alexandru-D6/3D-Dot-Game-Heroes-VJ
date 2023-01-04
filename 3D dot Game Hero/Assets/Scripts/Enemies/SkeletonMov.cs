using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMov : MonoBehaviour
{
    #region Parameters
    public int rutina;
    public float crono;
    public Animator anim;
    public Quaternion angle;
    public float grado;
    public bool attacking;
    public bool stay_Attacking;

    public GameObject target;

    public NavMeshAgent agent;
    [SerializeField] float attack_distance;
    [SerializeField] float distance_search_again;
    [SerializeField] float vision_radio;
    [SerializeField] SkeletonRayCast checkerWall;
    [SerializeField] SkeletonWeaponManager SkeletonWeaponManager;
    bool runactive = false;
    [SerializeField] GameObject runningparticles;
    private bool disabled = false;

    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        checkerWall= GetComponent<SkeletonRayCast>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!target.GetComponent<PlayerManager>().isDead())Comportamiento_Skeleton();

        if (Random.Range(0,500) < 10) SoundManager.Instance.PlaySkeletonSounds();
    }

    public void Comportamiento_Skeleton()
    {
        if (disabled)
        {
            this.enabled = false;
        }
        else if (Vector3.Distance(transform.position, target.transform.position) > vision_radio)
        {
            agent.enabled= false;
            crono += 1 * Time.deltaTime;
            if(crono >= 4)
            {
                rutina = Random.Range(0, 2);
                crono = 0;
            }
            switch (rutina)
            {
                case 0:
                    anim.SetBool("Running", false);
                    if (runactive)
                    {
                        runningparticles.SetActive(false);
                        runactive = false;
                    }
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    if (!runactive)
                    {
                        runningparticles.SetActive(true);
                        runactive = true;
                    }
                    anim.SetBool("Running", true);
                    break;
            }
        }
        else
        {

            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (!attacking && ((distance > attack_distance && !stay_Attacking) || checkerWall.isSeeingTheObjective(target.transform.position) || (distance > distance_search_again && stay_Attacking)))
            {
                stay_Attacking = false;
                agent.enabled = true;
                agent.SetDestination(target.transform.position);
                anim.SetBool("Running", true);
                if (!runactive)
                {
                    runningparticles.SetActive(true);
                    runactive = true;
                }

            }
            else
            {
                if (runactive)
                {
                    runningparticles.SetActive(false);
                    runactive = false;
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                if (!attacking)
                {
                    runningparticles.SetActive(false);
                    anim.SetTrigger("Aim");
                    attacking = true;
                    agent.enabled = false;
                    stay_Attacking= true;
                }
                
            }
        }
        
    }

    public void Shoot()
    {
        if (disabled)
        {
            this.enabled = false;
        }
        else
        {
            Debug.Log("Shoot");
            anim.SetBool("Shoot", false);
            SkeletonWeaponManager.UseCurrentWeapon();
            StartCoroutine(ExecuteAfterTime());
        }
    }

    public void End_Attack() {
        if (disabled)
        {
            this.enabled = false;
        }
        else
        {
            attacking = false;
            Debug.Log("Finish");
            
        }

    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(1f);

        if (!disabled)
        {
            SkeletonWeaponManager.ReleaseCurrentWeapon();
            anim.SetBool("Shoot",true);

        }
        else this.enabled = false;
        // Code to execute after the delay
    }


    public void Hitted()
    {
        disabled = true;
        attacking = false;
        if (runactive)
        {
            runningparticles.SetActive(false);
            runactive = false;
        }
        agent.enabled = false;
        anim.SetBool("Shoot", false);
        SkeletonWeaponManager.AbortAttack();

    }

    public void isEnabled()
    {
        disabled = false;
        
    }
 
}
