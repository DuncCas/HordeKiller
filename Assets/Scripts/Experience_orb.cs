using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Experience_orb : MonoBehaviour {

    public Transform orbGfx;
    public AudioClip audioOrb;
    private float rotationSpeed = 2f;
    private float scaleSpeed = 2;

    [Tooltip("Timer to trigger check of the player distance")]
    public float maxTimeToCheck = 10f;
    [Tooltip("The max distance from player to trigger exp deactivation/repositioning")]
    public float maxDistanceFromPlayer;
    GameObject _player;
    float _TimeToCheck;

    void Start() {
        _player= GameObject.FindGameObjectWithTag("Player");
        _TimeToCheck = 0;

        var sequence = DOTween.Sequence()
           .Append(orbGfx.DOLocalRotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360).SetRelative())
           .Join(orbGfx.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), scaleSpeed, 10, 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void Update() {
        if (gameObject.activeInHierarchy) {
            if (_TimeToCheck >= maxTimeToCheck) {
                checkDistance();
                _TimeToCheck = 0;
            }
            _TimeToCheck += Time.deltaTime;
        }
    }
    

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            AudioSource.PlayClipAtPoint(audioOrb, transform.position);
            
            _player.GetComponent<PlayerHandling>().IncreaseExp();
            gameObject.SetActive(false);
        }
    }


    private void checkDistance() {
        float distance= Vector3.Distance(transform.position, _player.transform.position);
        if (distance>= maxDistanceFromPlayer) {
            gameObject.SetActive(false);
        }
    }
}

