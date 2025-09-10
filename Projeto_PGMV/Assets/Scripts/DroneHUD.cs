using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DroneHUD : MonoBehaviour
{
    [SerializeField] GameObject Drone;
    [SerializeField] GameObject Altitude;
    [SerializeField] GameObject Orientacão;
    [SerializeField] GameObject AirBreakLight;
    [SerializeField] GameObject AirBreakText;

    [SerializeField] GameObject PolutionLevel;

    [SerializeField] GameObject CameraDrone;
    [SerializeField] GameObject Slider;
    [SerializeField] float smooth;
    // Start is called before the first frame update
    
    TextMeshProUGUI ABT;
    Image ABL;
    TextMeshProUGUI Alt;
    TextMeshProUGUI Orient;
    TextMeshProUGUI Pl;
    Camera Cm;
    bool AirBreakON = false;
    float zoom;
    GameObject target;
    void Start()
    {
        ABT=AirBreakText.GetComponent<TextMeshProUGUI>();
        ABL=AirBreakLight.GetComponent<Image>();
        Alt=Altitude.GetComponent<TextMeshProUGUI>();
        Orient=Orientacão.GetComponent<TextMeshProUGUI>();
        Cm=CameraDrone.GetComponent<Camera>();
        Pl=PolutionLevel.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.Z)){
            AirBreakON=!AirBreakON;
            if(AirBreakON){
                ABT.text="Air Break: ON";
                ABL.color=UnityEngine.Color.green;
            }else{
                ABT.text="Air Break: OFF";
                ABL.color=UnityEngine.Color.red;
            }
            

        }
        Alt.text= "Altitude: "+ Mathf.Round(Drone.transform.position.y/3 * 1000.0f) / 1000.0f+" m";

        var v = Drone.transform.forward;
        if(Vector3.Angle(v, Vector3.forward) <= 45.0)
            Orient.text="Orientacão: Norte";
        if(Vector3.Angle(v, Vector3.right) <= 45.0)
            Orient.text="Orientacão: Este";
        if(Vector3.Angle(v, Vector3.back) <= 45.0)
            Orient.text="Orientacão: Sul";
        if(Vector3.Angle(v, Vector3.left) <= 45.0)
            Orient.text="Orientacão: Oeste";

        zoom=Slider.GetComponent<Slider>().value;

        Cm.fieldOfView = Mathf.Lerp(Cm.fieldOfView,zoom,Time.deltaTime*smooth);

        RaycastHit hitInfo = new RaycastHit();

        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Camera.main.transform.position+new Vector3(Screen.width/2,Screen.height/2,0)), out hitInfo);

        if(hit){
                target = hitInfo.collider.gameObject;
        }
        if(target.transform.parent.tag=="Mesa1")
            Pl.text="Polution Level: " + target.transform.localScale.y*10;
            
    }
}
