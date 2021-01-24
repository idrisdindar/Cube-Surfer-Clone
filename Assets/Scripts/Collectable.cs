using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour , ICollectable
{
    #region Fields
    
    public GameObject floatingTextPrefab;
    
    #endregion

    #region Properties
    

    #endregion

    #region Methods
    

    public void Collect()
    {
        transform.localPosition = Character.Instance.transform.GetChild(Character.Instance.transform.childCount - 1).position;
        Character.Instance.gameObject.transform.position += new Vector3(0, 1.1f, 0);
        Character.Instance.gameObject.transform.GetChild(2).position += new Vector3(0, -1.1f, 0);
        transform.parent = Character.Instance.transform;
        GetComponent<Collider>().isTrigger = false;
        
        if(floatingTextPrefab)
        {
            ShowFloatingText();
        }
    }

    void ShowFloatingText()
    {
        var offset = new Vector3(1.5f, 0, 0);
        Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity, transform);
    }

    #endregion
    
}
