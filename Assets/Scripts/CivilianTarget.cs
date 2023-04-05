using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianTarget : MonoBehaviour
{
    public GameObject target;
    public Transform targetTransform;
    public float hideDistance;

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
        var dir = transform.position - target.transform.position;

        if (dir.magnitude < hideDistance)
        {
            SetChildrenActive(false);
        }
        else {
            SetChildrenActive(true);
            transform.LookAt(target.transform.position);
        }
        
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in targetTransform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
