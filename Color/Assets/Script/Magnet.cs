using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Magnet : MonoBehaviour
{

    #region Singleton class: Magnet

    public static Magnet Instance;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion



    [SerializeField] float magnetForce;
    List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    Transform magnet;





    void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
    }


    private void FixedUpdate()
    {
        foreach (Rigidbody rb in affectedRigidbodies)
        {
            rb.AddForce((magnet.position - rb.position)
                * magnetForce * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag.Equals("Object") || tag.Equals("Obstacle"))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (tag.Equals("Object") || tag.Equals("Obstacle"))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }



    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }

    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }
}
