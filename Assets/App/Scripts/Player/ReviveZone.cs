using App.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveZone : MonoBehaviour
{
    [SerializeField] List<(GameObject, float)> timers;
    [SerializeField] float ReviveTime;
    // Start is called before the first frame update
    void Awake()
    {
        timers = new List<(GameObject, float)> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(timers.Count > 0)
            UpdateTimer(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player) && other.gameObject.GetComponent<PlayerStat>().IsDead)
        {
            timers.Add((other.gameObject, ReviveTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            DeleteInTuple(other.gameObject);
        }
    }

    void UpdateTimer(float time)
    {
        for(int i = timers.Count - 1; i >= 0 ; i--)
        {
            timers[i] = (timers[i].Item1, timers[i].Item2 - time);
            if (timers[i].Item2 <= 0)
            {
                timers[i].Item1.GetComponent<PlayerStat>().Revive();
                timers.RemoveAt(i);
            }
        }
    }

    void DeleteInTuple(GameObject obj)
    {
        for(int i = 0; i < timers.Count; i++)
        {
            if (obj == timers[i].Item1)
            {
                timers.RemoveAt(i);
            }
        }
    }
}
