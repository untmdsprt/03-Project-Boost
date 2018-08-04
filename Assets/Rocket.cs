﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

	Rigidbody rigidBody;
    AudioSource MyAudioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
        MyAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) { return; } // ignore collisions when dead
        switch (collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); // parameterize time
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1); // TODO allow for more than 2 levels
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {  // can thrust while rotating
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            MyAudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            MyAudioSource.Stop();
        }
    }

    private void Rotate() {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
