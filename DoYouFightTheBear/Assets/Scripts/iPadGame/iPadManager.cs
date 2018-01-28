using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iPadManager : MonoBehaviour {

    public GameObject iPadPrefab;
    int iPadAmount;
    public float spawnTime;


    private void Start()
    {
        iPadAmount = Random.Range(5, 7);

        for (int i = 0; i < iPadAmount; i++)
        {
            GameObject newAd = Instantiate(iPadPrefab,Vector3.zero,Quaternion.identity);
            newAd.GetComponent<iPad>().Setup(this);
        }
        StartCoroutine(SpawnAd());
    }

    IEnumerator SpawnAd()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject newAd = Instantiate(iPadPrefab, Vector3.zero, Quaternion.identity);
        newAd.GetComponent<iPad>().Setup(this);
        iPadAmount++;
        StartCoroutine(SpawnAd());
    }

    public void Stacked()
    {
        if (iPadAmount> 1)
        {
            iPadAmount--;
        }
        else
            Debug.Log("LA BEAR");
    }
}
