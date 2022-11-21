using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {
    public float xSpeed;
    public float ySpeed;
    public float clampLeft;
    public float clampRight;

    private float cameraX;
    private int xdirection = 1;
    private int ydirection = 1;
    //private float[] speeds = new float[] { 0f, 0f, 0f, 0f, 0.1f, 0.1f, 0.1f, 0.2f, 0.2f };

    // Use this for initialization
    void Start () {
        cameraX = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        //ySpeed = speeds[Random.Range(0, 8)];
        transform.Translate(new Vector3(xSpeed * Time.deltaTime * xdirection, ySpeed * Time.deltaTime * ydirection, 0));

        if (transform.position.x >= clampRight | transform.position.x <= clampLeft) {
            xdirection = xdirection * -1;
        }

        if (transform.position.y >= 1.3 | transform.position.y <= -2)
        {
            ydirection = ydirection * -1;
        }

    }
}
