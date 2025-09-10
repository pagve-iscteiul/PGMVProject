using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;
using UnityEngine.UI;
using TMPro;

public class Maquete_controller : MonoBehaviour
{
    public TextAsset csvFile;
    public TextAsset csvFile1;
    public TextAsset csvFile2;
    public GameObject pivot;
    public float Critical_polution_value;
    public float High_polution_value;
    public float Medium_polution_value;
    public float Low_polution_value;
    public ParticleSystem smokeEffect;
    public List<GameObject> buildingList;
    public List<GameObject> natureList;
    string[,] grid;
    public GameObject slider_polution;
    public GameObject slider_opacity;
    bool yes = false;
    public int counter;
    Color tempColor;
    

    // Start is called before the first frame update
    void Start()
    {

        counter = 0;
        grid = getCSVGrid(csvFile.text);
        


    }
    static public string[,] getCSVGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        int totalColumns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            totalColumns = Mathf.Max(totalColumns, row.Length);
        }
        string[,] outputGrid = new string[totalColumns + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(',');
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
            }
        }
        
        return outputGrid;
        

    }
    
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(grid[4, 0]);
        //Debug.Log(grid.GetUpperBound(1));
        var PBR_Table = pivot;
        var transparencia = slider_opacity.GetComponent<Slider>().value;
        var poluicao = slider_polution.GetComponent<Slider>().value;


        while (!yes)
        {
            Vector3 lastObject = new Vector3(0,0,0);
            

            for (int y = 0; y < grid.GetUpperBound(1); y++)
            {
                Debug.Log(y);
                Vector3 scale = new Vector3((float)0.1, (float)0.1, (float)0.1);
                Vector3 position = new Vector3(float.Parse(grid[1, y], CultureInfo.InvariantCulture), (float)1, float.Parse(grid[2, y], CultureInfo.InvariantCulture));
                Vector3 Polution = new Vector3((float)0.1, float.Parse(grid[3, y], CultureInfo.InvariantCulture), (float)0.1);

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                GameObject building = buildingList[Random.Range(0, buildingList.Count)];
                GameObject tree = natureList[Random.Range(0, natureList.Count)];
                GameObject newBuilding = Instantiate(building) as GameObject;
                GameObject newTree = Instantiate(tree) as GameObject;

                building.transform.localScale = new Vector3((float)0.03, (float)0.03, (float)0.03);
                tree.transform.localScale = new Vector3((float)0.3, (float)0.3, (float)0.3);

                var cubeRenderer = cube.GetComponent<Renderer>();


                

                if(float.Parse(grid[3,y],CultureInfo.InvariantCulture) >= Critical_polution_value)
                {
                    cubeRenderer.material.SetColor("_Color", Color.red);
                    


                    var main = smokeEffect.main;

                    main.startSize = 0.05f;
                    main.simulationSpace = ParticleSystemSimulationSpace.Custom;
                    main.simulationSpeed = 0.5f;
                    main.customSimulationSpace = cube.transform;
                    main.startLifetime = 1f;

                    var particle = float.Parse(grid[3, y],CultureInfo.InvariantCulture) * 100;

                    main.maxParticles = (int)particle;
                    //main.maxParticles = int.Parse(grid[3, y]);
                    //Debug.Log(float.Parse(grid[3, y]) * 100 );
                    //Debug.Log((int)particle);

                    GameObject mySmoke = Instantiate(smokeEffect.gameObject) as GameObject;
                    
                    mySmoke.transform.parent = cube.transform;
                    mySmoke.transform.position = new Vector3(0, 1, 0);
                    


                }
                else if(float.Parse(grid[3, y],CultureInfo.InvariantCulture) < Critical_polution_value && float.Parse(grid[3, y],CultureInfo.InvariantCulture) >= High_polution_value)
                {
                    cubeRenderer.material.SetColor("_Color", new Color(1.0f,0.64f,0.0f));
                }
                else if(float.Parse(grid[3, y],CultureInfo.InvariantCulture) < High_polution_value && float.Parse(grid[3, y],CultureInfo.InvariantCulture) >= Medium_polution_value)
                {
                    cubeRenderer.material.SetColor("_Color", Color.yellow);
                }
                else
                {
                    cubeRenderer.material.SetColor("_Color", Color.green);
                }

                
                cube.transform.position = position;
                cube.transform.SetParent(PBR_Table.transform);

                newBuilding.transform.SetParent(cube.transform);
                newTree.transform.SetParent(cube.transform);

                if (pivot.name == "pivot")
                {
                    cube.gameObject.tag = "Mesa1";
                    newBuilding.gameObject.tag = "Mesa1";
                    newTree.gameObject.tag = "Mesa1";
                }
                


                newBuilding.transform.position = cube.transform.position + transform.right * (float)2;
                newTree.transform.position = cube.transform.position + -transform.right * (float)3;
                
                newBuilding.transform.localScale = newBuilding.transform.localScale * float.Parse(grid[4, y],CultureInfo.InvariantCulture);
                newTree.transform.localScale = newTree.transform.localScale * ((float)1 - float.Parse(grid[4, y],CultureInfo.InvariantCulture));
                
               
                newBuilding.transform.LookAt(cube.transform.position);
                newTree.transform.LookAt(cube.transform.position);

                if (y != 0)
                {
                    cube.transform.LookAt(lastObject);
                }
                
                lastObject = cube.transform.position;

                
                
                cube.transform.localScale = Polution / 10;





                


            }
            
             PBR_Table.transform.localPosition = new Vector3((float)-0.00189, (float)-0.0057, (float)0.011808);
             PBR_Table.transform.localScale = new Vector3((float)0.03, (float)0.011, (float)0.03);
            
            if (counter != 0)
            {

                PBR_Table.transform.localPosition = new Vector3((float)-0.00425, (float)-0.01033, (float)0.01453);
                PBR_Table.transform.localScale = new Vector3((float)0.070443, (float)0.011, (float)0.051015);
            }

            counter += 1;
            yes = true;
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            
            yes = false;
            GameObject[] destroyObjects = GameObject.FindGameObjectsWithTag("Mesa1");
            foreach (GameObject enemy in destroyObjects)
                GameObject.Destroy(enemy);
             

        }
        GameObject[] transparentObjects = GameObject.FindGameObjectsWithTag("Mesa1");
        for (var x = 0; x < transparentObjects.Length; x++)
        {
            var ObjectFound = transparentObjects[x];
            var obj = ObjectFound.GetComponent<Renderer>();
            obj.material.SetFloat("_Mode", 3.0f);
            tempColor = obj.material.color;
            tempColor.a = transparencia;
            obj.material.color = tempColor;
            if (ObjectFound.transform.localScale.y >= poluicao)
            {
                obj.material.color = Color.red;


            }
        }
        /* string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += ",";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
        */


        Debug.Log(grid.GetUpperBound(1));

    }
}
