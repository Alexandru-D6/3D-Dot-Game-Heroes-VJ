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

    public GameObject target;

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
        if(Vector3.Distance(transform.position, target.transform.position) > 5)
        {
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
                    transform.Translate(Vector3.right * 1 * Time.deltaTime);
                    anim.SetBool("Running", true);
                    break;
            }
        }
        else
        {
            anim.SetBool("Running", true);
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookPos);
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y - 90.0f, rotation.eulerAngles.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

            transform.Translate(Vector3.right * 2 * Time.deltaTime);
        }
        
    }
}
