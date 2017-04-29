using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SightController : MonoBehaviour
{

    private List<IDirectionObserver> _subscribers = new List<IDirectionObserver>();
    public GameObject _SightModel;


    public void AddSubscriber(IDirectionObserver observer)
    {
        _subscribers.Add(observer);
    }

    public void Notify()
    {
        foreach (IDirectionObserver sub in _subscribers)
        {
            sub.Notify();
        }
    }
    public Quaternion SightDirection
    {
        get { return _SightModel.transform.rotation; }
        set
        {
            _SightModel.transform.rotation = value;
            Notify();
        }
    }
    public Vector3 Direction
    {
        get { return (GameObject.Find("SightHead").transform.position - GameObject.Find("SightBody").transform.position).normalized; }
    }
    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Ball").GetComponent<BallModel>().AddSubscriber(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void Notify()
    {
        _SightModel.transform.rotation = GameObject.Find("Ball").GetComponent<BallModel>().BallShotDirection;
    }*/
}
