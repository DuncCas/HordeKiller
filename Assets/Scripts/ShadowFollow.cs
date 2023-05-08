using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour {
    public GameObject Shadow;
    public Transform foot;
    public bool activate;

    // Start is called before the first frame update
    void Start() {
        Instantiate(Shadow, new Vector3(foot.position.x, 0, foot.position.z), gameObject.transform.rotation);
    }

    public void Move() {
            activate = true;
            Shadow.GetComponentInChildren<SpriteRenderer>().enabled = activate;
            Shadow.transform.position = new Vector3(foot.position.x, 0, foot.position.z);
            Debug.Log("I'm moving");
    }

    public void Hide() {
        activate = false;
        Shadow.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }



    // Update is called once per frame
    private void Update() {
        if (activate) {
           // Shadow.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f) * -foot.position.y;
        }
    }
}
