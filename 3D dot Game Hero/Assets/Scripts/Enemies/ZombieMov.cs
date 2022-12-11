using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMov : MonoBehaviour
{
    #region Parameters
    public int rutina;
    public float crono;
    public Animator anim;
    public Quaternion angle;
    public float grado;
    public bool attacking;

    public GameObject target;

    public NavMeshAgent agent;
    public float attack_distance;
    public float vision_radio;
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
            /*if (Vector3.Distance(transform.position, target.transform.position) > 1 && !attacking) 
            { 
                anim.SetBool("Running", true);
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;

                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                anim.SetBool("Attack", false);
            }
            else
            {
                anim.SetBool("Running", false);
                anim.SetBool("Attack", true);
                attacking = true;
            }*/

            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);

            agent.enabled = true;
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) > attack_distance && !attacking)
            {
                anim.SetBool("Running", true);
            }
            else
            {
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
}
