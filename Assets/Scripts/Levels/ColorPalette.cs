using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour {

    private void Awake()
    {

        string currentObjectTag = this.tag;
        switch (currentObjectTag)
        {
            case "YellowChanger":
                gameObject.GetComponent<Renderer>().material.color = Constants.Yellow;
                break;
            case "GreenChanger":
                gameObject.GetComponent<Renderer>().material.color = Constants.Green;
                break;
            case "BlueChanger":
                gameObject.GetComponent<Renderer>().material.color = Constants.Blue;
                break;
            case "RedChanger":
                gameObject.GetComponent<Renderer>().material.color = Constants.Red;
                break;
        }
    }
}
