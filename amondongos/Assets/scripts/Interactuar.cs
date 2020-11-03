using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{

    [SerializeField] GameObject[] puertas;
    [SerializeField] GameObject puertaActual;
    [SerializeField] GameObject pressE;

    [SerializeField] float [] distance;



    // Start is called before the first frame update
    void Start()
    {
        puertas=GameObject.FindGameObjectsWithTag("Puerta");
        pressE=GameObject.FindGameObjectWithTag("PressE");
        distance=new float[100];
        // pressE.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {

        int aux=0;

        for (int i = 0; i < puertas.Length; i++)
        {
            distance[i]=Vector3.Distance(gameObject.transform.position,puertas[i].transform.position);
            
            
           
           if(distance[i]<=2&&puertaActual==null)
           {
               aux=i;
               puertaActual=puertas[i];
               pressE.SetActive(true);
               break;
               
           }
           else if(distance[aux]>2&&puertaActual!=null)
           {
               puertaActual=null;
               pressE.SetActive(false);
           }

           

        }

        if(Input.GetKeyDown(KeyCode.E)&&puertaActual!=null)
        {
            AbrirPuerta();
        }

        
        
    }

    void AbrirPuerta()
    {
        puertaActual.GetComponent<Puerta>().Open();
    }
}
