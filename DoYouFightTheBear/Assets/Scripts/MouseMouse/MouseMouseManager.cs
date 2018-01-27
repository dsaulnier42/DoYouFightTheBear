using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMouseManager : MonoBehaviour {

    public GameObject adPrefab;
    int adAmount;
    public Transform screen;
    public float spawnTime;


    private void Start()
    {
        adAmount = Random.Range(5, 7);

        for (int i = 0; i < adAmount; i++)
        {
            GameObject newAd = Instantiate(adPrefab,screen);
            newAd.GetComponent<AD>().Setup(this);
        }
        StartCoroutine(SpawnAd());
    }

    IEnumerator SpawnAd()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject newAd = Instantiate(adPrefab, screen);
        newAd.GetComponent<AD>().Setup(this);
        StartCoroutine(SpawnAd());
        adAmount++;
    }

    void NewAd()
    {
        GameObject newAd = Instantiate(adPrefab, screen);
        newAd.GetComponent<AD>().Setup(this);
        adAmount++;
    }

    public void CloseAd(GameObject ad)
    {
        if (adAmount > 1)
        {
            adAmount--;
            Destroy(ad);
        }
        else
            Debug.Log("LA BEAR");
    }

}
