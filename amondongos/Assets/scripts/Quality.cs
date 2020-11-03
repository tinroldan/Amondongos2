using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quality : MonoBehaviour
{
    [SerializeField] TMP_Text qualityText;
    private string qualityName;
    private int qualityIndex;

    void Start()
    {
        qualityIndex=3;
        ApplyQuality();
    }

    public void NextQuality()
    {
        qualityIndex++;
        ApplyQuality();

    }

    public void PreviousQuality()
    {
        qualityIndex--;
        ApplyQuality();

    }

    private void ApplyQuality()
    {
        qualityIndex = Mathf.Clamp(qualityIndex,0,5);
        switch(qualityIndex)
        {
            case 0:
            qualityName="Very Low";
            break;

            case 1:
            qualityName="Low";
            break;

            case 2:
            qualityName="Medium";
            break;

            case 3:
            qualityName="High";
            break;

            case 4:
            qualityName="Very High";
            break;

            case 5:
            qualityName="Ultra";
            break;
        }
        qualityText.text=qualityName;
        QualitySettings.SetQualityLevel(qualityIndex,true);
        
    }


}
