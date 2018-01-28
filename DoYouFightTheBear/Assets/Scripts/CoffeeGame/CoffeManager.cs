using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeManager : MonoBehaviour {

    public GameManager gameManager;
    public float coffeeTimer = 25;

    bool countdown;
    public TextMesh textMesh;

    Rigidbody[] bodies;
    public Transform bodyHolder;
    public Transform camera;

    public GameObject bear;
    public Transform camRot;

    private void Start()
    {
        bodies = bodyHolder.GetComponentsInChildren<Rigidbody>();
		bear.SetActive (false);
    }

    private void Update()
    {

        if (countdown)
        {
            coffeeTimer -= Time.deltaTime;
            textMesh.text = "" + Mathf.RoundToInt(coffeeTimer);
        }


        if (coffeeTimer < 0 && countdown)
        {
            countdown = false;
            textMesh.text = "Done";
            End();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBear();
        }
    }

    public void SetCountdown(bool cd)
    {
        countdown = cd;
    }

    void End()
    {
        Debug.Log("here");
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
            //camera.localRotation = Quaternion.Lerp(camera.localRotation, camRot.rotation, Time.time * .02f);

            timer += Time.deltaTime;
            yield return null;
        }
        bear.SetActive(true);
        bear.GetComponent<Rigidbody>().velocity = Vector3.up * 200;
        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].isKinematic = false;
            bodies[i].velocity = (Vector3.up + -Vector3.forward) * 20;
            textMesh.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(.6f);
        gameManager.StartShowdown();
    }


}
