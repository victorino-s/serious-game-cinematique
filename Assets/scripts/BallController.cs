using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Globalization;

public class BallController : MonoBehaviour {

    public BallModel _model;
    public GameObject PanelDisplayer;

    public GameObject IF_Mass;
    public GameObject IF_Drag;
    public GameObject IF_Gravity_val;
    public GameObject Checkbox_Gravity;
    public GameObject LBL_Velocity;
    public GameObject IF_Elasticity_Constant;
    public GameObject IF_Elastic_Distance;
    public GameObject BTN_Launch;


    private InputField if_mass_value;
    private InputField if_drag_value;
    private InputField if_gravity_value;
    private InputField if_elasticity_constant;
    private InputField if_elastic_distance;
    private Toggle checkbox_use_gravity;
    private Text lbl_estimated_velocity;
    private Button btn_slingshot;

   // private float _mass = 1f;
    private float _drag = 0f;
    private float _elasticity = 45f;
    private float _elastic_distance = 2f;
    private float _estimated_velocity;


    // Use this for initialization
    void Start () {
        if_mass_value = IF_Mass.GetComponent<InputField>();
        if_drag_value = IF_Drag.GetComponent<InputField>();
        if_gravity_value = IF_Gravity_val.GetComponent<InputField>();
        if_elasticity_constant = IF_Elasticity_Constant.GetComponent<InputField>();
        if_elastic_distance = IF_Elastic_Distance.GetComponent<InputField>();

        lbl_estimated_velocity = LBL_Velocity.GetComponent<Text>();
        checkbox_use_gravity = Checkbox_Gravity.GetComponent<Toggle>();
        btn_slingshot = BTN_Launch.GetComponent<Button>();

        if_mass_value.onEndEdit.AddListener(EditBallMass);
        if_drag_value.onEndEdit.AddListener(EditBallDrag);
        if_gravity_value.onEndEdit.AddListener(EditGeneralGravity);
        if_elasticity_constant.onEndEdit.AddListener(EditElasticityConstant);
        if_elastic_distance.onEndEdit.AddListener(EditElasticDistance);

        checkbox_use_gravity.onValueChanged.AddListener(ChangeBallGravityActivation);
        btn_slingshot.onClick.AddListener(LaunchSlingshot);

        // Var affectations

        if_mass_value.text = _model.GetComponent<Rigidbody>().mass.ToString();
        _model.GetComponent<Rigidbody>().mass = 1f;

        
        _model.GetComponent<Rigidbody>().drag = 0.0f; // Ball drag value
        if_drag_value.text = _model.GetComponent<Rigidbody>().drag.ToString();

        if_gravity_value.text = Physics.gravity.y.ToString();

        if_elasticity_constant.text = _elasticity.ToString();

        if_elastic_distance.text = _elastic_distance.ToString();

        checkbox_use_gravity.isOn = _model.GetComponent<Rigidbody>().useGravity;
        RefreshEstimatedVelocity();

    }

    private void LaunchSlingshot()
    {
        _model.transform.GetComponent<Rigidbody>().isKinematic = false;
        //RefreshEstimatedVelocity();
        Vector3 direction = GetComponent<SightController>().Direction;
        _model.transform.rotation = GetComponent<SightController>().SightDirection;
        
        GameObject.Find("Ball").GetComponent<Rigidbody>().velocity = direction.normalized * _estimated_velocity;
        btn_slingshot.interactable = false;
        GetComponent<WeaponManager>().DeactivateWeapons();
        // Load Reload scene button
    }

    private void ChangeBallGravityActivation(bool state)
    {
        _model.GetComponent<Rigidbody>().useGravity = checkbox_use_gravity.isOn;
    }

    private void EditElasticDistance(string value)
    {
        float valueEntered;
        if(float.TryParse(value,System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_elastic_distance.colors;
            c.normalColor = Color.white;
            _elastic_distance = valueEntered;
            if_elastic_distance.colors = c;
        }
        else
        {
            ColorBlock c = if_elastic_distance.colors;
            c.normalColor = Color.red;
            _elastic_distance = 10f;
            if_elastic_distance.colors = c;
            if_elastic_distance.text = "10";
        }
        RefreshEstimatedVelocity();
    }

    private void EditElasticityConstant(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            if (valueEntered >= 0f)
            {
                ColorBlock c = if_elasticity_constant.colors;
                c.normalColor = Color.white;
                _elasticity = valueEntered;
                if_elasticity_constant.colors = c;
            }
            else
            {
                ColorBlock c = if_elasticity_constant.colors;
                c.normalColor = Color.red;
                _elasticity = 0f;
                if_elasticity_constant.colors = c;
                if_elasticity_constant.text = "0";
            }
        }
        else
        {
            ColorBlock c = if_elasticity_constant.colors;
            c.normalColor = Color.red;
            _elasticity = 0f;
            if_elasticity_constant.colors = c;
            if_elasticity_constant.text = "0";
        }
        RefreshEstimatedVelocity();                
    }

    private void EditGeneralGravity(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_gravity_value.colors;
            c.normalColor = Color.white;
            Physics.gravity = new Vector3(0f, valueEntered, 0f);
            if_gravity_value.colors = c;
        }
        else
        {
            ColorBlock c = if_gravity_value.colors;
            c.normalColor = Color.red;
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            if_gravity_value.colors = c;
            if_gravity_value.text = "-9.81";
        }
    }

    private void EditBallDrag(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_drag_value.colors;
            c.normalColor = Color.white;
            _drag = valueEntered;
            if_drag_value.colors = c;
        }
        else
        {
            ColorBlock c = if_drag_value.colors;
            c.normalColor = Color.red;
            _drag = 0.45f;
            if_drag_value.colors = c;
            if_drag_value.text = "0.45";
        }
    }

    private void EditBallMass(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_mass_value.colors;
            c.normalColor = Color.white;
            _model.GetComponent<Rigidbody>().mass = valueEntered;
            if_mass_value.colors = c;
        }
        else
        {
            ColorBlock c = if_mass_value.colors;
            c.normalColor = Color.red;
            _model.GetComponent<Rigidbody>().mass = 1f;
            if_mass_value.colors = c;
            if_mass_value.text = _model.GetComponent<Rigidbody>().mass.ToString();
        }
        RefreshEstimatedVelocity();
    }

    public void RefreshEstimatedVelocity()
    {
        //_estimated_velocity = _elastic_distance * Mathf.Sqrt(_elasticity / _model.GetComponent<Rigidbody>().mass);
        _estimated_velocity = _elastic_distance * _elasticity;
        lbl_estimated_velocity.text = _estimated_velocity.ToString() + " m.s";
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void HideDisplayer()
    {
        PanelDisplayer.SetActive(false);
    }

    public void ShowDisplayer()
    {
        PanelDisplayer.SetActive(true);
    }
}
