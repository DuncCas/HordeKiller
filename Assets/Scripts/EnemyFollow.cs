using UnityEngine;
using UnityEngine.AI;
public class EnemyFollow : MonoBehaviour
{
    [Tooltip("Navmesh for the enemy")]
    public NavMeshAgent nav;
    GameObject _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    void Update() { 
        if (_player) {
            nav.destination = _player.transform.position;
        }
    }
}
