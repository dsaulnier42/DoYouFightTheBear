using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeManager : MonoBehaviour {

    public float coffeeTimer;

    bool countdown;
    public TextMesh textMesh;

    private void Update()
    {

        if (countdown)
        {
            coffeeTimer -= Time.deltaTime;
            textMesh.text = "Coffee \nTime In\n" + Mathf.RoundToInt(coffeeTimer);
        }


        if (coffeeTimer < 0)
        {
            textMesh.text = "Coffee";
            End();
        }
    }

    public void SetCountdown(bool cd)
    {
        countdown = cd;
    }

    void End()
    {
        Debug.Log("La Bear");
    }


}
