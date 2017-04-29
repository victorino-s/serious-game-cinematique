using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Globalization;

public class MissileController : MonoBehaviour {

    
    public MissileModel _model;
    public GameObject PanelDisplayer;

    public GameObject IF_Mass;
    public GameObject IF_Drag;
    public GameObject IF_Gravity_val;
    public GameObject IF_Acceleration;
    public GameObject IF_Time;
    public GameObject Checkbox_Gravity;
    public GameObject BTN_Launch;

    private InputField if_mass_value;
    private InputField if_drag_value;
    private InputField if_gravity_value;
    private InputField if_acceleration_value;
    private InputField if_time_value;
    private Toggle checkbox_use_gravity;
    private Button btn_missile;

    private float _acceleration = 5f;
    private float _time = 1f;
    private bool missileActivated = false;
    public GameObject MissileFlames;
    // Use this for initialization
    void Start () {
        if_mass_value = IF_Mass.GetComponent<InputField>();
        if_drag_value = IF_Drag.GetComponent<InputField>();
        if_gravity_value = IF_Gravity_val.GetComponent<InputField>();
        if_acceleration_value = IF_Acceleration.GetComponent<InputField>();
        if_time_value = IF_Time.GetComponent<InputField>();

        checkbox_use_gravity = Checkbox_Gravity.GetComponent<Toggle>();
        btn_missile = BTN_Launch.GetComponent<Button>();

        if_mass_value.onEndEdit.AddListener(EditMissileMass);
        if_drag_value.onEndEdit.AddListener(EditMissileDrag);
        if_gravity_value.onEndEdit.AddListener(EditGeneralGravity);
        if_acceleration_value.onEndEdit.AddListener(EditMissileAcceleration);
        if_time_value.onEndEdit.AddListener(EditMissileAccTime);

        checkbox_use_gravity.onValueChanged.AddListener(ChangeMissileGravityActivation);
        btn_missile.onClick.AddListener(LaunchMissile);

        if_mass_value.text = _model.GetComponent<Rigidbody>().mass.ToString();
        _model.GetComponent<Rigidbody>().drag = 0.048f; // Init drag value to missile drag in air
        if_drag_value.text = _model.GetComponent<Rigidbody>().drag.ToString();

        if_gravity_value.text = Physics.gravity.y.ToString();

        if_acceleration_value.text = _acceleration.ToString();
        if_time_value.text = _time.ToString();
        checkbox_use_gravity.isOn = _model.GetComponent<Rigidbody>().useGravity;

        MissileFlames.SetActive(false);
    }

    private void LaunchMissile()
    {
        _model.transform.rotation = GetComponent<SightController>().SightDirection;
        _model.GetComponent<Rigidbody>().isKinematic = false;
        MissileFlames.SetActive(true);
        missileActivated = true;
        Invoke("ResetAcceleration", _time);
        btn_missile.interactable = false;
        GetComponent<WeaponManager>().DeactivateWeapons();

        _model.GetComponent<AudioSource>().Play();
    }

    public void ResetAcceleration()
    {
        _model.GetComponent<AudioSource>().Stop();
        missileActivated = false;
        MissileFlames.SetActive(false);
    }
    public void ChangeMissileGravityActivation(bool state)
    {
        _model.GetComponent<Rigidbody>().useGravity = checkbox_use_gravity.isOn;
    }

    public void EditMissileAccTime(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_time_value.colors;
            c.normalColor = Color.white;
            _time = valueEntered;
            if_time_value.colors = c;
        }
        else
        {
            ColorBlock c = if_time_value.colors;
            c.normalColor = Color.red;
            _time = 1f;
            if_time_value.colors = c;
            if_time_value.text = _time.ToString();
        }
    }

    public void EditMissileAcceleration(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_acceleration_value.colors;
            c.normalColor = Color.white;
            _acceleration = valueEntered;
            if_acceleration_value.colors = c;
        }
        else
        {
            ColorBlock c = if_acceleration_value.colors;
            c.normalColor = Color.red;
            _acceleration = 5f;
            if_acceleration_value.colors = c;
            if_acceleration_value.text = _acceleration.ToString();
        }
    }

    public void EditMissileDrag(string value)
    {
        float valueEntered;
        if (float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valueEntered))
        {
            ColorBlock c = if_drag_value.colors;
            c.normalColor = Color.white;
            _model.GetComponent<Rigidbody>().drag = valueEntered;
            if_drag_value.colors = c;
        }
        else
        {
            ColorBlock c = if_drag_value.colors;
            c.normalColor = Color.red;
            _model.GetComponent<Rigidbody>().drag = 0.048f;
            if_drag_value.colors = c;
            if_drag_value.text = _model.GetComponent<Rigidbody>().drag.ToString();
        }
    }

    public void EditGeneralGravity(string value)
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

    public void EditMissileMass(string value)
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
    }

    // Update is called once per frame
    void FixedUpdate () {
	    if(missileActivated)
        {
            Vector3 accPosition = GameObject.Find("MissileAccelerationPoint").transform.position;
            //_model.GetComponent<Rigidbody>().velocity += GetComponent<SightController>().Direction.normalized * _acceleration * Time.deltaTime;
            _model.GetComponent<Rigidbody>().AddForceAtPosition(GetComponent<SightController>().Direction.normalized * _acceleration, accPosition, ForceMode.Force);
        }
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
