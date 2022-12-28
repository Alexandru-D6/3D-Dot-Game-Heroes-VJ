using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ParticlesChest : MonoBehaviour
{
    [SerializeField] ParticleSystem shine;
    [SerializeField] GameObject shinelarge;
    [SerializeField] GameObject lightchest;
    void Awake()
    {
        shine.Stop();
        shinelarge.SetActive(false);
        lightchest.SetActive(false);
    }

    IEnumerator delayEndParticles()
    {
        yield return new WaitForSeconds(2.5f);
        shine.Stop();
        shinelarge.SetActive(false);
        lightchest.SetActive(false);
    }

    public void chestIsOpened()
    {
        shine.Play();
        shinelarge.SetActive(true);
        lightchest.SetActive(true);
        delayEndParticles();
    }

}
