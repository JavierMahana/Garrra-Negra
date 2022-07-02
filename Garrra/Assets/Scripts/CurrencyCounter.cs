using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
    public static int fishValue = 0;
    Text fishes;
    // Start is called before the first frame update
    void Start()
    {
        fishes = GetComponent<Text>();
        fishes.text = "" + fishValue;
    }

    // Update is called once per frame
    void Update()
    {
        fishes.text = "" + fishValue;
    }
}
