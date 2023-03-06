using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject Panel;
    public InputAsset InputAsset;

    // Start is called before the first frame update
    void Start()
    {        
        foreach (InputBinding input in InputAsset.inputs)
        {
            Instantiate(TextPrefab, Panel.transform).GetComponent<TextMeshProUGUI>().text = string.Format("{0} : {1} {2}", input.InputType.ToString(), input.InputKey1.ToString(), input.InputKey2.ToString());
        }

        GetComponent<Button>().onClick.AddListener(() => { Panel.SetActive(!Panel.activeSelf); });
    }
}
