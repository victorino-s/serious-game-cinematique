using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeaponManager : MonoBehaviour, IDirectionObserver
{
    public GameObject ball_btn_go;
    public GameObject missile_btn_go;


    private Button ballButton;
    private Button missileButton;
    //private Button rocketButton;

    private GameObject weapon_ball_go;
    private GameObject weapon_missile_go;
    //private GameObject weapon_rocketlauncher_go;

    private int _selectedWeapon; // 0: Ball, 1: Missile, 2: Rocket

    // Use this for initialization
    void Start()
    {
        weapon_ball_go = GameObject.Find("Ball");
        weapon_missile_go = GameObject.Find("Missile");

        ballButton = ball_btn_go.GetComponent<Button>();
        missileButton = missile_btn_go.GetComponent<Button>();

        ballButton.onClick.AddListener(ChooseBallWeapon);
        missileButton.onClick.AddListener(ChooseMissileWeapon);

        _selectedWeapon = 0;
        DeactivateWeapon(1);
        DeactivateWeapon(2);
        ChooseBallWeapon();

        transform.GetComponent<SightController>().AddSubscriber(this);
    }

    public void DeactivateWeapons()
    {
        ballButton.interactable = false;
        missileButton.interactable = false;
    }
    public void SetShotDirection(Vector3 axis, float value)
    {
        Vector3 actualRotation = transform.GetComponent<SightController>().SightDirection.eulerAngles;
        if (axis == Vector3.right)
        {
            transform.GetComponent<SightController>().SightDirection = Quaternion.Euler(value, actualRotation.y, actualRotation.z);
            //copy = Quaternion.Euler(value, transform.rotation.y, transform.rotation.z);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis X");

        }
        else if (axis == Vector3.up)
        {
            transform.GetComponent<SightController>().SightDirection = Quaternion.Euler(actualRotation.x, value, actualRotation.z);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis Y");
        }
        else if (axis == Vector3.forward)
        {
            transform.GetComponent<SightController>().SightDirection = Quaternion.Euler(actualRotation.x, actualRotation.y, value);
            Debug.Log("Trigger BallModel.cs SetShotDirection() -> Axis Z");
        }
    }

    public void ChooseMissileWeapon()
    {
        DeactivateWeapon(_selectedWeapon);
        ActivateWeapon(1);
    }

    public void ChooseBallWeapon()
    {
        DeactivateWeapon(_selectedWeapon);
        ActivateWeapon(0);
    }

    public void DeactivateWeapon(int id)
    {
        switch(id)
        {
            case 0:
                transform.GetComponent<BallController>().HideDisplayer();
                weapon_ball_go.SetActive(false);
                break;
            case 1:
                transform.GetComponent<MissileController>().HideDisplayer();
                weapon_missile_go.SetActive(false);
                break;
            case 2:
                Debug.Log("Rocket launcher deactivation not implemented");
                break;
        }
    }

    public void ActivateWeapon(int id)
    {
        switch (id)
        {
            case 0:
                transform.GetComponent<BallController>().ShowDisplayer();
                weapon_ball_go.SetActive(true);
                break;
            case 1:
                transform.GetComponent<MissileController>().ShowDisplayer();
                weapon_missile_go.SetActive(true);
                break;
            case 2:
                break;
        }

        _selectedWeapon = id;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Notify()
    {
        switch(_selectedWeapon)
        {
            case 0:
                weapon_ball_go.transform.rotation = GetComponent<SightController>().SightDirection;
                break;
            case 1:
                weapon_missile_go.transform.rotation = GetComponent<SightController>().SightDirection;
                break;
            case 2:
                break;
        }
    }
}
