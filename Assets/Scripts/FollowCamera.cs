using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed;

    public float offset;
    Vector3 cameraPos;

    public Vector2 safeArea; // (X,Y) valori per i quali la telecamera non si muove



    // Start is called before the first frame update
    void Awake() {
        followTarget.position += Vector3.up * offset;
    }

    // Update is called once per frame
    void Update() {
        // Follow brutto
        // transform.position = followTarget.position - offset;

        // Follow morbido
        cameraPos = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * followSpeed);

        if (Mathf.Abs(transform.position.x - followTarget.position.x) < safeArea.x)
            cameraPos.x = transform.position.x;

        transform.position = cameraPos;
    }
}
