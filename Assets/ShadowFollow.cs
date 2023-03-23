using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public bool activate;
    public GameObject shadow;
    public Transform foot;


    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        shadow.GetComponent<SpriteRenderer>().enabled=activate;
        
    }

    // Update is called once per frame
    private void LateUpdate() {
        shadow.GetComponent<SpriteRenderer>().enabled = activate;
        shadow.transform.position = new Vector3(foot.position.x, 0, foot.position.z);
    }
}
