using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hints : MonoBehaviour
{
    [SerializeField]private List<Sprite> hintImages = new List<Sprite>();
    [SerializeField]private List<string> hintText = new List<string>();

    [SerializeField] private Image hintImageBox;
    [SerializeField] private TMP_Text hintTextBox;

    [SerializeField] private float hintTimer;
    [SerializeField] private float hintResetTimer;

    // Update is called once per frame
    void Update()
    {
        hintTimer+=Time.deltaTime;
        if(hintTimer>hintResetTimer)
        {
            hintTimer = 0;

            hintImageBox.sprite = hintImages[Random.Range(0, hintImages.Count)];
            hintTextBox.text= hintText[Random.Range(0, hintText.Count)];

        }


    }
}
