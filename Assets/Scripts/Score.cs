using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro; 
/// ui text�� �ҷ��������� �κ�
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