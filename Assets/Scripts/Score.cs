using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro; 
/// ui text를 불러오기위한 부분
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class score : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = ((int)elapsedTime).ToString();
    }
}