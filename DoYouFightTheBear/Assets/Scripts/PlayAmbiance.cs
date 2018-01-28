using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAmbiance : MonoBehaviour {
	public AudioClip officeSound;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		source.PlayOneShot(officeSound);
	}
}
