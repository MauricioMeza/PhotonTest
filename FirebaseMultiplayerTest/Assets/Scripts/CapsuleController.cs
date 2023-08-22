using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using Random = UnityEngine.Random;

public class CapsuleController : NetworkBehaviour
{
    [SerializeField] GameObject capsule;
    [SerializeField] float speed;
    NetworkCharacterControllerPrototype _cc;
    // Start is called before the first frame update
    void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        capsule.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public override void FixedUpdateNetwork(){
        if (GetInput(out NetworkInputData data)){
            data.direction.Normalize();
            _cc.Move(data.direction*Runner.DeltaTime*speed);
        }   
    }
}
