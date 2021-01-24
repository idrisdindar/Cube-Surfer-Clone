using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static float mousex = 0;
    public static float speed = 5;
    public static float min = -2;
    public static float max = 2;
    public static Vector3 newpos;
    public Rigidbody rb;
    public GameObject collectable;

    public static void Move()
    {
        Character.Instance.transform.position += new Vector3(0, 0, 0.1f);
        
        if (Input.GetMouseButton(0))
        {
            var position = Character.Instance.transform.position;
            newpos = position;
            mousex = Input.mousePosition.x;
            newpos.x = (mousex * (((max - min) / 2) / (Screen.width / 2)) + min);
            newpos.x = Mathf.Clamp(newpos.x, min, max);
            position = Vector3.Lerp(position, newpos, Time.deltaTime * speed); 
            Character.Instance.transform.position = position;
        }
    }
}
