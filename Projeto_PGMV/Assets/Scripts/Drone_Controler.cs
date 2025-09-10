using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Controler : MonoBehaviour

{
    [SerializeField] float speed;
    [SerializeField] float turn_rate;
    [SerializeField] float up_speed;
    //[SerializeField] bool use_physics = true;
    Rigidbody rb;
    bool freeze = false;

    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

    bool firstPerson = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera1.active=true;
        camera2.active=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            firstPerson=!firstPerson;
            if(firstPerson){
                camera1.active=true;
                camera2.active=false;
            }
            else{
                camera1.active=false;
                camera2.active=true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            freeze=!freeze;
            if(freeze){
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }else{
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }


    private void FixedUpdate()
    {
        //rb.isKinematic = !use_physics;
        //rb.useGravity = use_physics;
        //if(!use_physics)
        //{
        
        //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turn_rate * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.up * Input.GetAxis("Jump") * up_speed * Time.deltaTime, Space.Self);
        //}
        //else
        //{
        rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical")*speed, ForceMode.Force);
            //rb.AddRelativeTorque(Vector3.up * Input.GetAxis("Horizontal")*turn_rate, ForceMode.Force);
        
        rb.AddRelativeForce(Vector3.up * Input.GetAxis("Jump") * up_speed, ForceMode.Force);
        rb.AddRelativeForce(Vector3.up * rb.mass * 7 * up_speed, ForceMode.Force);
        
        //}
        /*if(transform.position.y<-10)
        {
            transform.position = Vector3.zero;
        }*/
        
    }
}
