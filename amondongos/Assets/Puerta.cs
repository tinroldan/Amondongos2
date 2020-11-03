using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Puerta : MonoBehaviourPun
{

    public bool open;
    float time;

    [SerializeField] GameObject puertaL,puertaR;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if(time>0)
        {
            time-=Time.deltaTime;
        }

        if(time<=0)
        {
            if(open==false)
            {
                animator.SetBool("Open",true);

            }
            open=true;

        }
        
    }

    public void Open()
    {
        if(open==true)
        {
            time=10;
            open=false;
            animator.SetBool("Open",false);

        }
        
    }


}
