using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerPanel : MonoBehaviour
{
    public GameObject Toggle;
    public GameObject Slider;
    public FollowerController Follower;

    // Start is called before the first frame update
    void Start()
    {
        Toggle.GetComponent<Toggle>().onValueChanged.AddListener((bool on) => { GameManager.Instance.OnFollowerAddSub?.Invoke(on); });
        SliderSetup();
    }

    void SliderSetup()
    {
        Slider s = Slider.GetComponent<Slider>();
        s.minValue = 0f;
        s.maxValue = 1f;
        s.value = Follower.LeaderDistance;
        s.onValueChanged.AddListener((float val) => { Follower.LeaderDistance = val; });
    }
}
