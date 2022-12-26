using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
        Comportamiento_Skeleton();
    }

    public void Comportamiento_Skeleton()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > vision_radio)
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
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
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
            if ((distance > attack_distance && !attacking && !stay_Attacking) || checkerWall.isSeeingTheObjective(target.transform.position) || ((distance > distance_search_again && !attacking && stay_Attacking)))
            {
                stay_Attacking = false;
                agent.enabled = true;
                agent.SetDestination(target.transform.position);
                anim.SetBool("Running", true);

            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                if (!attacking)
                {
                    anim.SetBool("Running", false);
                    anim.SetBool("Aim", true);
                    attacking = true;
                    agent.enabled = false;
                    stay_Attacking= true;
                }

            }
        }
        
    }

    public void Final_Anim()
    {
        
        anim.SetBool("Aim", false);
// anim.SetBool(,false);
        Debug.Log("Shoot");
    }
}
