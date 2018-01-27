using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD : MonoBehaviour {

    bool jump;


    MouseMouseManager mouseManager;

    public void Setup(MouseMouseManager mouseMouseManager)
    {
        mouseManager = mouseMouseManager;
        jump = Random.Range(0,2) == 1;
        SetRandomPosistion();
    }

    private void Update()
    {
        transform.position += new Vector3(Random.Range(-.8f , .8f), Random.Range(-.8f, .8f)) * Time.deltaTime;
    }

    void SetRandomPosistion()
    {
        transform.localPosition = new Vector3(Random.Range(-7f,7f), Random.Range(-7f, 7f));
    }

    public void Close()
    { 
        mouseManager.CloseAd(this.gameObject);
    }



}
