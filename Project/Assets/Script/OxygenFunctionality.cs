using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OxygenFunctionality : MonoBehaviour
{
    private float oxygenAmount;
    private float roundedOxygenAmount;
    public float maxOxygen = 100f;
    public float oxygenTick = 0.001f;
    public Image oxygenProgressBar;
    public TMP_Text NumberDisplay;
    public GameObject LoseText;
    public ClimberHand[] handsList;
    [SerializeField] float killTime = 5f;

    [SerializeField] AudioClip oxygenLowSound;
    [SerializeField] AudioClip oxygenRefilledSound;
    [SerializeField] AudioClip oxygenEmptySound;
    [SerializeField] AudioClip oxygenEmptyAmbient;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceAmbient;

    bool killed = false;
    bool begunFadeOut = false;
    bool oxygenLow = false;

    // Start is called before the first frame update
    void Start()
    {
        oxygenAmount = maxOxygen;
        if (LoseText) LoseText.SetActive(false);
        handsList = FindObjectsOfType<ClimberHand>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOxygenBar();
        CountdownReset();
    }

    void UpdateOxygenBar()
    {
        oxygenAmount -= oxygenTick * Time.timeScale;
        //Debug.Log("Oxygen: " + oxygenAmount.ToString());

        if (oxygenAmount < 0.2f * maxOxygen)
        {
            if (!oxygenLow)
            {
                audioSourceAmbient.clip = oxygenLowSound;
                audioSourceAmbient.Play();
                oxygenLow = true;
            }
        }
        else
        {
            oxygenLow = false;
        }

        if(oxygenAmount > 0.0f)
        {
            if (oxygenProgressBar) 
                oxygenProgressBar.rectTransform.localScale = new Vector3(oxygenProgressBar.rectTransform.localScale.x, oxygenAmount / maxOxygen, oxygenProgressBar.rectTransform.localScale.z);
            
            roundedOxygenAmount = Mathf.RoundToInt(oxygenAmount);

            if (NumberDisplay) 
                NumberDisplay.text = roundedOxygenAmount.ToString() + " / " + maxOxygen.ToString();
        }
        else
        {
            KillPlayer();
        }
    }

    public void GainOxygen(float oxygen)
    {
        oxygenAmount = Mathf.Clamp(oxygenAmount + oxygen, 0f, maxOxygen);
        audioSource.PlayOneShot(oxygenRefilledSound);
    }

    public void KillPlayer()
    {
        if (killed) return;
        killed = true;

        if (NumberDisplay) NumberDisplay.text = "0 / " + maxOxygen.ToString();
        if (LoseText) LoseText.SetActive(true);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        for (int i = 0; i < handsList.Length; i++)
        {
            handsList[i].enabled = false;
        }

        audioSource.PlayOneShot(oxygenEmptySound);
        audioSourceAmbient.clip = oxygenEmptyAmbient;
        audioSourceAmbient.Play();
    }

    void CountdownReset()
    {
        if (!killed) return;

        killTime -= Time.deltaTime;

        if (!begunFadeOut && killTime < 2f)
        {
            OVRScreenFade screenFade = FindObjectOfType<OVRScreenFade>();
            if (screenFade) screenFade.FadeOut();

            begunFadeOut = true;
        }

        if (killTime < 0f) SceneManager.LoadScene("SampleScene");
    }
}
