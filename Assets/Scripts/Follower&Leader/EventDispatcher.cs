using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    public GameObject Leader;
    public GameObject Follower;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnFollowerAddSub += FollowerAddSub;        
    }

    void FollowerAddSub(bool add)
    {
        Leader.GetComponent<LeaderController>().FollowerAddSub(add);
        Follower.SetActive(add);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnFollowerAddSub -= FollowerAddSub;
    }
}
