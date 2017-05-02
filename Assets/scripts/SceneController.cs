using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public GameObject tpsView_obj;
    public GameObject lat_view_obj;
    public GameObject top_view_obj;
    public GameObject reloadScene_obj;

    private Button btn_tps_view;
    private Button btn_lat_view;
    private Button btn_top_view;
    private Button btn_reload_scene;
    // Use this for initialization
    void Start () {
        btn_tps_view = tpsView_obj.GetComponent<Button>();
        btn_lat_view = lat_view_obj.GetComponent<Button>();
        btn_top_view = top_view_obj.GetComponent<Button>();
        btn_reload_scene = reloadScene_obj.GetComponent<Button>();

        btn_reload_scene.onClick.AddListener(ReloadScene);
	}

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
