using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed,walkSpeed,jumpForce,smoothTime;

    [SerializeField] Item[] items;

    int itemIndex;
    int previousItemIndex = -1;


    int previousPing = -1;


    float verticalLookRotation;
    [SerializeField] bool grounded;

    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    [SerializeField] TMP_Text Ping,fps;

    // [SerializeField] GameObject menu;
    // bool menuOpen;



    float deltaTime;



    

void Awake()
{
    rb= GetComponent<Rigidbody>();
    PV = GetComponent<PhotonView>();
    Ping = GameObject.FindGameObjectWithTag("ping").GetComponent<TMP_Text>();
    fps = GameObject.FindGameObjectWithTag("fps").GetComponent<TMP_Text>();
    // menu = GameObject.FindGameObjectWithTag("menu").gameObject;


}

    // Start is called before the first frame update
    void Start()
    {
        // menuOpen=false;
        // menu.SetActive(false);

        if(PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);

        }

        

        Cursor.lockState=CursorLockMode.Locked;
    }



    void Update()
    {
        if(fps!=null)
        {
            deltaTime+=(Time.unscaledDeltaTime-deltaTime)*0.1f;
            fps.text=("FPS: ")+(1.0f/deltaTime).ToString("f0");


        }

        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if(menuOpen==true)
        //     {
        //     menu.SetActive(false);
        //     menuOpen=false;
        //     Cursor.visible=false;
        //     Cursor.lockState=CursorLockMode.Locked;


        //     }
        //     else
        //     {
        //     menu.SetActive(true);
        //     menuOpen=true;
        //     Cursor.lockState=CursorLockMode.None;




        //     }
        // }





        if(Ping!=null)
        {
        if(PhotonNetwork.GetPing()!=previousPing)
        {
        Ping.text=("Ping: "+PhotonNetwork.GetPing());
        previousPing=PhotonNetwork.GetPing();
        }
        }

        

        if(!PV.IsMine)
        {
            return;
        }
           
        //    if(menuOpen==false)
        // {

        Look();
        Move();
        Jump();
        // }

        for(int i=0; i<items.Length;i++)
        {
            if(Input.GetKeyDown((i+1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel")>0f)
        {
            if(itemIndex>=items.Length-1)
            {
                EquipItem(0);
            }
            else
            {
            EquipItem(itemIndex+1);

            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel")<0f)
        {
            if(itemIndex<=0)
            {
                EquipItem(items.Length-1);
            }
            else
            {
                EquipItem(itemIndex-1);

            }

        }


    }

    void Move()
    {
        Vector3 moveDiR = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount,moveDiR*(Input.GetKey(KeyCode.LeftShift)? sprintSpeed : walkSpeed),ref smoothMoveVelocity,smoothTime);

    }

    

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
            Debug.Log("i am jumping");
        }
    }

    void EquipItem(int _index)
    {

        if(_index==previousItemIndex)
        return;

        itemIndex=_index;

        items[itemIndex].ItemGameObject.SetActive(true);

        if(previousItemIndex!=-1)
        {
            items[previousItemIndex].ItemGameObject.SetActive(false);
        }


        previousItemIndex=itemIndex;
    }


    void Look()
    {
        

        
        transform.Rotate(Vector3.up*Input.GetAxisRaw("Mouse X")*mouseSensitivity);

        verticalLookRotation+=Input.GetAxisRaw("Mouse Y")*mouseSensitivity;
        verticalLookRotation=Mathf.Clamp(verticalLookRotation,-90f,90f);

        cameraHolder.transform.localEulerAngles=Vector3.left*verticalLookRotation;
        
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    

    void FixedUpdate()
    {

        if(!PV.IsMine)
        {
            return;
        }
            
        rb.MovePosition(rb.position+transform.TransformDirection(moveAmount)*Time.fixedDeltaTime);
    }
}
