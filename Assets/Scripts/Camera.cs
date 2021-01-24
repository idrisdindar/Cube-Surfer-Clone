using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Character;
    public Transform target;
    public Vector3 offset;
    
    private void Update()
    {
        target = Character.gameObject.transform.GetChild(Character.gameObject
            .transform.childCount - 1);
        transform.position = target.position + offset;
    }
}
