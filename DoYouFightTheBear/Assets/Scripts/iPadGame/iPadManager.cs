﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iPadManager : MonoBehaviour {

    public GameManager gameManager;
    public GameObject iPadPrefab;
    int iPadAmount;
    public float spawnTime;

    Rigidbody[] bodies;
    public Transform bodyHolder;
    public Transform camera;

    public GameObject bear;
    public Transform camRot;
	bool firstIpadWasPlacedCorrectly;

    private void Start()
    {
        bodies = bodyHolder.GetComponentsInChildren<Rigidbody>();
		Invoke ("OffsetSpawn", 1);
    }

	void OffsetSpawn(){
		iPadAmount = Random.Range(5, 7);

		for (int i = 0; i < iPadAmount; i++)
		{
			GameObject newAd = Instantiate(iPadPrefab,Vector3.one,Quaternion.identity);
			newAd.GetComponent<iPad>().Setup(this);
			Debug.Log ("wow " + newAd.name);
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
		if(!firstIpadWasPlacedCorrectly){
			Invoke ("SpawnBear", 30);
			firstIpadWasPlacedCorrectly = true;
		}

        if (iPadAmount > 1)
        {
            iPadAmount--;
        }
        else
            SpawnBear();
    }

    void SpawnBear()
    {
        StartCoroutine(BearAttack());
    }

    IEnumerator BearAttack()
    {

        float timer = 0;

        while (timer < .9f)
        {
            camera.localRotation = Quaternion.Lerp(camera.localRotation, camRot.rotation, Time.time * .01f);

            timer += Time.deltaTime;
            yield return null;
        }
        bear.SetActive(true);
        bear.GetComponent<Rigidbody>().velocity = bear.transform.forward * 300;
        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].isKinematic = false;
        }
        bear.GetComponent<Rigidbody>().velocity = bear.transform.forward * 300;
        yield return new WaitForSeconds(.8f);
        gameManager.StartShowdown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBear();
        }
    }
}
