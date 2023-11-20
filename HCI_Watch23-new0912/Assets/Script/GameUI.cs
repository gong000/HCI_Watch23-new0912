using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Image CircleImg;
    [SerializeField] Text txtProgress;
    [SerializeField] [Range(0,1)] float progress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CircleImg.fillAmount = progress;
        txtProgress.text = Mathf.Floor(progress * 100).ToString();
    }
}
