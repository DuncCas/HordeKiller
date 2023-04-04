using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianTarget : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        target = GameObject.FindGameObjectWithTag("Civilian");
    }

    // Update is called once per frame
    void Update()
    {

        //var dir = transform.position - target.transform.position;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.LookAt(target.transform.position);
    }
}
