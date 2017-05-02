using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShotDirectionAngle_InputField : MonoBehaviour {

    private InputField angleField;
    public Vector3 angleAxis;
	// Use this for initialization
	void Start () {
        angleField = GetComponent<InputField>();
        angleField.onEndEdit.AddListener(SetNewDirectionAngle);
	}

    public void SetNewDirectionAngle(string textValue)
    {
        Debug.Log("Trigger If");
        float angleValue = float.Parse(textValue);
        GameObject.Find("GameController").GetComponent<WeaponManager>().SetShotDirection(angleAxis, angleValue);
       // GameObject.Find("Ball").GetComponent<BallModel>().SetShotDirection(angleAxis, angleValue);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
