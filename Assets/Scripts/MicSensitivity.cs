using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicSensitivity : MonoBehaviour
{
    public Pop popScript;

    int sampleData = 512;
    private AudioSource mic;

    [Range(-100f, 0f)]
    public float dBLevel;
    public float curLeveldB;

    public int currMicSelected = 0;

    [SerializeField] private GameObject Holder;
    [SerializeField] private Slider visualizer, sensSlider;
    [SerializeField] private Dropdown micDropdown;

    [SerializeField] private CameraControls camControl;

    // Start is called before the first frame update
    void Start()
    {
        // Microphone Setup
        mic = GetComponent<AudioSource>();
        mic.clip = Microphone.Start(Microphone.devices[currMicSelected], true, 999, 44100);

        //Mic Sensativity Range Setup
        visualizer.minValue = -100.0f;
        visualizer.maxValue = 0.0f;

        sensSlider.minValue = -100.0f;
        sensSlider.maxValue = 0.0f;

        //Mic Device Dropdown Setup
        List<Dropdown.OptionData> dropdownData = new List<Dropdown.OptionData>();
        foreach (string mic in Microphone.devices) 
        {
            Dropdown.OptionData micData = new Dropdown.OptionData();
            micData.text = mic;
            dropdownData.Add(micData);
        }
        micDropdown.AddOptions(dropdownData);

        ToggleVisabillity(true);
    }

    // Update is called once per frame
    void Update()
    {
        dBLevel = sensSlider.value;
        visualizer.value = curLeveldB;

        float[] spectrum = new float[512];
        int micPos = Microphone.GetPosition(Microphone.devices[currMicSelected]) - (sampleData + 1);
        if (micPos < 0)
        {
            return;
        }

        mic.clip.GetData(spectrum, micPos);

        for (int i = 0; i < spectrum.Length; i++)
        {
            curLeveldB = LinearToDb(Mathf.Abs(spectrum[i]));

            if (curLeveldB >= dBLevel)
            {
                popScript.StartCoroutine("OpenMouth");
                popScript.iSpeak = true;
            }
        }
    }

    private float LinearToDb(float linear)
    {
        float dB;
        dB = 20.0f * Mathf.Log10(linear);
        return dB;
    }

    public void ToggleVisabillity(bool close)
    {
        if (!close)
        {
            Holder.SetActive(false);
        }
        else 
        {
            Holder.SetActive(!Holder.activeSelf);
        }
    }

    public void UpdateCurrentMicrophone() 
    {
        currMicSelected = micDropdown.value;
    }
}
