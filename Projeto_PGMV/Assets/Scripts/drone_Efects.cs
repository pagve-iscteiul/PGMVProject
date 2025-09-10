using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone_Efects : MonoBehaviour
{
    [SerializeField] float speed=100;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int i=0;
    // Update is called once per frame
    void Update()
    {
        i=i+1;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            transform.Rotate(10,0,0);
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            transform.Rotate(-10,0,0);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            transform.Rotate(-10,0,0);
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            transform.Rotate(10,0,0);
        }
        
        if(i==25)
            transform.Translate((float)0.002,(float)0,0);
        if(i==50)
            transform.Translate((float)0,(float)0.002,0);
        if(i==75)
            transform.Translate((float)-0.002,(float)0,0);
        if(i==100)
            transform.Translate((float)0,(float)-0.002,0);
        if(i==125)
            transform.Translate((float)0,(float)-0.002,0);
        if(i==150)
            transform.Translate((float)-0.002,(float)0,0);
        if(i==175)
            transform.Translate((float)0,(float)0.002,0);
        if(i==200)
            transform.Translate((float)0.002,(float)0,0);
        
        
        if(i==200)
            i=0;
        

    }
}
