using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class PlayerTag : MonoBehaviourPun
{
    [SerializeField] private TMP_Text nameTag;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine){return;}

        SetName();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetName()
    {
        nameTag.text=photonView.Owner.NickName;
    }
}
