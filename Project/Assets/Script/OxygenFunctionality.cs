using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenFunctionality : MonoBehaviour
{
    private float oxygenAmount;
    public float maxOxygen = 100f;
    public float oxygenTick = 0.001f;
    public Image oxygenProgressBar;


    // Start is called before the first frame update
    void Start()
    {
        oxygenAmount = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOxygenBar();
    }

    void UpdateOxygenBar()
    {
        oxygenAmount -= oxygenTick * Time.timeScale;

        if(oxygenAmount > 0.0f)
        {
            oxygenProgressBar.rectTransform.localScale = new Vector3(oxygenProgressBar.rectTransform.localScale.x, oxygenAmount / maxOxygen, oxygenProgressBar.rectTransform.localScale.z);
        }
    }
}
