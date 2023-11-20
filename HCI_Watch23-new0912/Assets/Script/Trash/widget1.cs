using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;


public class widget1 : MonoBehaviour
{
    public Image imageToShow;

    private Button button;

    private void Start()
    {
        // Get a reference to the button component
        button = GetComponent<Button>();

        // Add an event listener for when the button is clicked
        button.onClick.AddListener(ShowImage);
    }

    private void ShowImage()
    {
        // Set the image to be visible
        imageToShow.gameObject.SetActive(true);
    }

}
