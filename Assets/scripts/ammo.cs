using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ammo : MonoBehaviour
{
    public int number = 6;
    public TMP_Text numberText;

    void Start()
    {
        UpdateNumber();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DecreaseNumber();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetNumber();
        }
    }

    void DecreaseNumber()
    {
        if (number > 0)
        {
            number--;
            UpdateNumber();
        }
    }

    void ResetNumber()
    {
        number = 6;
        UpdateNumber();
    }

    void UpdateNumber()
    {
        numberText.text = number + "/6";
    }
}
//attach it to a game object and drag an UI text into numberText
//the number value is the ammount of ammo
//and add another text next to it with "/6"