using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BallPropertiesController : MonoBehaviour
{

    private InputField mass_if;
    private InputField drag_if;

    private Toggle applyGravity_toggle;
    private InputField gravity_value_if;

    public GameObject mass_go;
    public GameObject drag_go;
    public GameObject applyGravity_go;
    public GameObject gravity_if_go;
    // Use this for initialization
    void Start()
    {
        mass_if = mass_go.GetComponent<InputField>();
        drag_if = drag_go.GetComponent<InputField>();
        applyGravity_toggle = applyGravity_go.GetComponent<Toggle>();
        gravity_value_if = gravity_if_go.GetComponent<InputField>();

        mass_if.text = GameObject.Find("Ball").GetComponent<Rigidbody>().mass.ToString();
        drag_if.text = GameObject.Find("Ball").GetComponent<Rigidbody>().drag.ToString();
        applyGravity_toggle.isOn = GameObject.Find("Ball").GetComponent<Rigidbody>().useGravity;
        gravity_value_if.text = Physics.gravity.y.ToString();

        mass_if.onEndEdit.AddListener(editMass);
        drag_if.onEndEdit.AddListener(editDrag);
        applyGravity_toggle.onValueChanged.AddListener(editGravityUse);
        gravity_value_if.onEndEdit.AddListener(ModifyGeneralGravity);
    }

    private void ModifyGeneralGravity(string value)
    {
        Physics.gravity = new Vector3(0f, float.Parse(value), 0f);
    }

    public void editGravityUse(bool value)
    {
        GameObject.Find("Ball").GetComponent<Rigidbody>().useGravity = applyGravity_toggle.isOn;
    }

    private void editDrag(string value)
    {
        GameObject.Find("Ball").GetComponent<Rigidbody>().drag = float.Parse(value);
    }

    private void editMass(string value)
    {
        GameObject.Find("Ball").GetComponent<Rigidbody>().mass = float.Parse(value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
