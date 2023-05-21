using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public TMP_Text valueInput;
    public Slider sliderInput;
    public string name;
    public AudioMixer musique;
    public AudioMixer effets;
        
        public void SetValue(float value)
        {
            switch (name)
            {
                case "Musique":
                    Debug.Log("Musique");
                    musique.SetFloat("Musique", value);
                    break;
                case "Effets":
                    Debug.Log("Effets");
                    effets.SetFloat("Effets", value);
                    break;
            }
            
            valueInput.text = value.ToString() + "db";
            Debug.Log(value);
        }

        

        void Start()
        {
            valueInput.text = sliderInput.value.ToString() + "db";
        }
}
