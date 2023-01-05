using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDropMov : MonoBehaviour
{

    [SerializeField] float height;
    [SerializeField] int incrementrot;
    [SerializeField] float cont;
    [SerializeField] float weight;
    [SerializeField] float freq;
    // Start is called before the first frame update
    void Start() {
        SoundManager.Instance.PlayMinecraftDropBlock();
    }

    // Update is called once per frame
    void Update()
    {
        incrementrot ++;
        if (incrementrot > 360) incrementrot = 0; 
        transform.rotation = Quaternion.AngleAxis(incrementrot, Vector3.up);
        cont += freq/100;
        float angle = Mathf.Sin(cont); 
        transform.position = new Vector3(transform.position.x,height+(angle*weight), transform.position.z);
        if(cont > 6.28f)cont= 0;    
        
    }
}

