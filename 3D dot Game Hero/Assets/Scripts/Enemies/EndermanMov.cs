using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndermanMov : MonoBehaviour
{
    #region Parameters
    [SerializeField] int rutina;
    private float crono;
    [SerializeField] Animator anim;
    [SerializeField] Quaternion angle;
    private float grado;
    [SerializeField] bool attacking;
    [SerializeField] Collider ownCollider;
    [SerializeField] GameObject particles;

    [SerializeField] GameObject target;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float attack_distance;
    [SerializeField] bool alert = false;
    [SerializeField] float delay_attack = 3;
    [SerializeField] float range = 30; //radius of sphere
    private bool enablebody = true;
    private bool teleporting = false;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

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
        Comportamiento_Enderman();
    }

    public void Comportamiento_Enderman()
    {
        if (!alert)
        {
            agent.enabled = false;
            crono += 1 * Time.deltaTime;
            if (crono >= 3)
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
            if (!attacking && 3 > delay_attack)
            {   
              
                if (teleporting)
                {  agent.enabled = true;
                    if (runactive)
                    {
                        runningparticles.SetActive(false);
                        runactive = false;
                    }
                    //done with path
                    if (enablebody)
                    {
                        agent.speed = 7.0f;
                        enablebody = false;
                        enableOrDisableBody(enablebody);
                        Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                    }
                    if (agent.remainingDistance <= agent.stoppingDistance) //done with path
                    {
                        Vector3 point;
                        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                        {
                            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                            agent.SetDestination(point);
                        }
                    }
                }                
                delay_attack += 1 * Time.deltaTime;   
            }
            else
            {  
                if (!enablebody)
                {
                    agent.speed = 3.5f;
                    Instantiate(particles,new Vector3(transform.position.x, transform.position.y +2f, transform.position.z), Quaternion.identity);
                    teleporting = false;
                    enablebody = true;
                    enableOrDisableBody(enablebody);
                }
            }
            if (Vector3.Distance(transform.position, target.transform.position) > attack_distance && !attacking && !teleporting)
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
            else if ((Vector3.Distance(transform.position, target.transform.position) <= attack_distance) && !attacking && !teleporting)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                if (3 <= delay_attack)
                {   
                    anim.SetBool("Running", false);
                    if (runactive)
                    {
                        runningparticles.SetActive(false);
                        runactive = false;
                    }
                    anim.SetBool("Attack", true);
                    attacking = true;
                    agent.enabled = false;
                    delay_attack = 0;
                    if (1 == ((int)Random.Range(0, 2)))
                    {
                        teleporting = true; 

                        SoundManager.Instance.PlayEnderTeleport();
                    }
                }
            }      
        }
    }

    public void Final_Anim()
    {

        anim.SetBool("Attack", false);
        attacking = false;
    }
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range-1, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void enableOrDisableBody(bool enable)
    {
        ownCollider.enabled = enable;
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }

    }

    public void alertEnderman()
    {
        alert = true;
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
