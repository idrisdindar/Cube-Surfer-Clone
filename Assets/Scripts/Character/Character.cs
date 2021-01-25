using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    #region Fields

    private static Character _instance;
    private bool _canMove = true;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem confettiParticle;

    #endregion
    
    #region Properties

    public static Character Instance { get { return _instance; } }
    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    private void Update()
    {
        if (CanMove)
            CharacterController.Move();
    }

    #endregion
    
    #region Methods

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            gameObject.transform.GetChild(2).GetComponent<TrailRenderer>().emitting = true;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            var offset = new Vector3(1, 3, 0);
            CanMove = false;
            Debug.Log("Level Completed");
            Instantiate(confettiParticle, transform.position + offset, Quaternion.identity);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            ICollectable icollectable = other.GetComponent<ICollectable>();
            
            if(icollectable != null)
                icollectable.Collect();
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            if (transform.childCount > 4)
            {
                /*transform.GetChild(transform.childCount - 1).position = new Vector3(other.transform.position.x, 
                    other.transform.position.y, other.transform.position.z - 1f);*/
                transform.GetChild(transform.childCount - 1).parent = null;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
                gameObject.transform.GetChild(2).position += new Vector3(0, 1.1f, 0);
                gameObject.transform.GetChild(2).GetComponent<TrailRenderer>().emitting = false;
            }
            else
            {
                Instantiate(hitParticle, transform.GetChild(transform.childCount-1).position + Vector3.back, Quaternion.identity);
                gameObject.GetComponent<Animator>().SetTrigger("Fall");
                Debug.Log("Level Fail");
                Character.Instance.CanMove = false;
            }
        }
        else if(other.gameObject.CompareTag("Lava"))
        {
            if (transform.childCount > 4)
            {
                Destroy(transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                gameObject.transform.GetChild(2).position += new Vector3(0, 1.1f, 0);
                gameObject.transform.GetChild(2).GetComponent<TrailRenderer>().emitting = false;
            }
            else
            {
                Debug.Log("Level Fail");
                
                Character.Instance.CanMove = false;
            }
        }
    }

    #endregion
}
