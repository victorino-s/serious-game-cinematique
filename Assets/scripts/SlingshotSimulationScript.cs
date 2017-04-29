using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SlingshotSimulationScript : MonoBehaviour {
    public GameObject elasticityConstant_Inputfield;
    public GameObject distance_Inputfield;
    public GameObject label_estimated_velocity;
    public GameObject ShotButton;

    private InputField elasticityConstant_if;
    private InputField elasticDistance_if;
    private Button shot_btn;
    private Text velocity_lbl;

    private float _elasticConstant = 3f;
    private float _elasticDistance = 10f;

    public float ElasticityConstant
    {
        get { return _elasticConstant; }
    }

    public float ElasticDistance
    {
        get { return _elasticDistance; }
    }

    // Use this for initialization
    void Start () {
        elasticityConstant_if = elasticityConstant_Inputfield.GetComponent<InputField>();
        elasticDistance_if = distance_Inputfield.GetComponent<InputField>();
        shot_btn = ShotButton.GetComponent<Button>();
        velocity_lbl = label_estimated_velocity.GetComponent<Text>();

        elasticityConstant_if.onEndEdit.AddListener(EditElasticityConstant);
        elasticDistance_if.onEndEdit.AddListener(EditElasticDistance);
        shot_btn.onClick.AddListener(ShootBall);

        elasticDistance_if.text = ElasticDistance.ToString();
        elasticityConstant_if.text = ElasticityConstant.ToString();
        EditVelocityLabel();
    }

    public void ShootBall()
    {
        Debug.Log("Shot Ball");
        GameObject.Find("Ball").GetComponent<Rigidbody>().isKinematic = false;
        float vel = (_elasticDistance * Mathf.Sqrt(_elasticConstant / GameObject.Find("Ball").GetComponent<Rigidbody>().mass));
        Vector3 dist = GetComponent<SightController>().Direction;
        GameObject.Find("Ball").GetComponent<Rigidbody>().velocity = dist * vel;
        Invoke("ReloadScene", 3f);
    }


    public void ReloadScene()
    {
        Debug.Log("SlingshotSimulationScript.cs ReloadScene fn");
    }

    public void EditElasticDistance(string distance)
    {
        _elasticDistance = float.Parse(distance);
        EditVelocityLabel();
    }

    public void EditElasticityConstant(string value)
    {
        _elasticConstant = float.Parse(value);
        EditVelocityLabel();
    }

    public void EditVelocityLabel()
    {
        float vel = (_elasticDistance * Mathf.Sqrt(_elasticConstant / GameObject.Find("Ball").GetComponent<Rigidbody>().mass));
        velocity_lbl.text = vel.ToString() + " m.s";
    }
    // Update is called once per frame
    void Update () {
	
	}
}
