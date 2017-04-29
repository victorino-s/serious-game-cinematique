using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallModel : MonoBehaviour
{
    private List<IDirectionObserver> _subscribers = new List<IDirectionObserver>();

    private Quaternion _shotDirection;

    public float elasticity_constant_value = 1f;
    public void AddSubscriber(IDirectionObserver sub)
    {
        _subscribers.Add(sub);
    }

    public void Notify()
    {
        foreach(IDirectionObserver subscriber in _subscribers)
        {
            subscriber.Notify();
        }
    }
    public Quaternion BallShotDirection
    {
        get { return transform.rotation; }
        set
        {
            transform.rotation = value;
            Notify();
        }
    }

    public void SetShotDirection(Vector3 axis, float value)
    {
        if (axis == Vector3.right)
        {
            BallShotDirection = Quaternion.Euler(value, transform.eulerAngles.y, transform.eulerAngles.z);
             //copy = Quaternion.Euler(value, transform.rotation.y, transform.rotation.z);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis X");

        }
        else if (axis == Vector3.up)
        {
            BallShotDirection = Quaternion.Euler(transform.eulerAngles.x, value, transform.eulerAngles.z);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis Y");
        }
        else if(axis == Vector3.forward)
        {
            BallShotDirection = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, value);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis Z");
        }
    }
    void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
