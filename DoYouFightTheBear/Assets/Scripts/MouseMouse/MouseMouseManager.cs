using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMouseManager : MonoBehaviour {

    public GameManager gameManager;
    public GameObject adPrefab;
    int adAmount;
    public Transform screen;
    public float spawnTime;

    Rigidbody[] bodies;
    public Transform bodyHolder;
    public Transform camera;

    public GameObject bear;
    public Transform camRot;
    public GameObject breakable;

    private void Start()
    {
        bodies = bodyHolder.GetComponentsInChildren < Rigidbody>();
        bear.SetActive(false);
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
        {
            Destroy(ad);
            SpawnBear();
        }
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
            camera.localRotation = Quaternion.Lerp(camera.localRotation, camRot.rotation , Time.time * .02f);

            timer += Time.deltaTime;
            yield return null;
        }
        bear.SetActive(true);
        bear.GetComponent<Rigidbody>().velocity = bear.transform.forward * 200;
        yield return new WaitForSeconds(.2f);
        breakable.SetActive(false);
        bear.GetComponent<Rigidbody>().velocity = bear.transform.forward * 200;
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].isKinematic = false;
            bodies[i].velocity = (Vector3.right + Vector3.up) * 10;
        }

        yield return new WaitForSeconds(1f);
        gameManager.StartShowdown();

    }

}
