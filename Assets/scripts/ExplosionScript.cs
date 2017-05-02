using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioClip))]
public class ExplosionScript : MonoBehaviour {

    public float _ExplosionForce;
    public float _ExplosionRadius;
    public GameObject ExplosionPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Cible")
        {
            if(GetComponent<AudioSource>().clip != null)
            {
                GetComponent<AudioSource>().Play();
            }
            col.GetComponent<Rigidbody>().AddExplosionForce(_ExplosionForce, col.transform.position, _ExplosionRadius);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
