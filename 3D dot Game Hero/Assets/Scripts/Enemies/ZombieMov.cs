using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMov : MonoBehaviour
{
    #region Parameters
    private int rutina;
    private float crono;
    [SerializeField] Animator anim;
    private Quaternion angle;
    private float grado;
    private bool attacking;

    [SerializeField] GameObject target;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float attack_distance;
    [SerializeField] float vision_radio;
    private bool runactive = false;
    [SerializeField] GameObject runningparticles;
    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Zombie();
    }

    public void Comportamiento_Zombie()
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
                    anim.SetBool("Running", true);
                    if (!runactive)
                    {
                        runningparticles.SetActive(true);
                        runactive = true;
                    }
                    break;
            }
        }
        else
        {

            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);

            
            if (Vector3.Distance(transform.position, target.transform.position) > attack_distance && !attacking)
            {
                agent.enabled = true;
                agent.speed = 1.5f;
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
                if (!attacking)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    anim.SetBool("Running", false);
                    anim.SetBool("Attack", true);
                    attacking = true;
                    agent.enabled = false;
                }

            }
        }
        
    }

    public void Final_Anim()
    {
        anim.SetBool("Attack", false);
        attacking = false;
    }

    public void Hitted()
    {
        attacking = false;
        if (runactive)
        {
            runningparticles.SetActive(false);
            runactive = false;
        }
        agent.enabled = false;

    }
}
