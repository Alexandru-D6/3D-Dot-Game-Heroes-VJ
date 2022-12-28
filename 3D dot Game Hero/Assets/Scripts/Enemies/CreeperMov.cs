using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreeperMov : MonoBehaviour
{
    #region Parameters
    [SerializeField] int rutina;
    private float crono;
    [SerializeField] Animator anim;
    [SerializeField] Quaternion angle;
    private float grado;
    [SerializeField] bool exploding;
    
    [SerializeField] GameObject target;
    [SerializeField] GameObject explosion;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float initial_explode_distance;
    [SerializeField] float after_exploding_distance;
    [SerializeField] float vision_radio;
    [SerializeField] ParticleSystem particle;
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
        ComportamientoCreeper();
    }

    public void ComportamientoCreeper()
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

           
            if (Vector3.Distance(transform.position, target.transform.position) > initial_explode_distance && !exploding)
            {
                agent.enabled = true;
                agent.SetDestination(target.transform.position);
                anim.SetBool("Running", true);
                if (!runactive)
                {
                    runningparticles.SetActive(true);
                    runactive = true;
                }

            }
            else if (Vector3.Distance(transform.position, target.transform.position) > after_exploding_distance && exploding)
            {
                agent.enabled = true;
                agent.SetDestination(target.transform.position);
                anim.SetBool("Explosion", false);
                anim.SetBool("Running", true);
                if (!runactive)
                {
                    runningparticles.SetActive(true);
                    runactive = true;
                }
                exploding = false;
                
            }
            else
            {
                if (!exploding)
                {
                    
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    anim.SetBool("Running", false);
                    if (runactive)
                    {
                        runningparticles.SetActive(false);
                        runactive = false;
                    }
                    anim.SetBool("Explosion", true);
                    exploding = true;
                    agent.enabled = false;
                }
                

            }
        }
        
    }

    public void Final_Anim()
    {
        anim.SetBool("Explosion", false);
        exploding = false;
        explosion.SetActive(true);
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
