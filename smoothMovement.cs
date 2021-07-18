using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class smoothMovement : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public float timer;

	private Rigidbody rb;
	private int count;

	/* You will need to set this to the correct serial port for your Arduino USB serial connection
	Something like COM2 or COM3 on a PC and somethign like "/dev/cu.usbmodem1411" on a Mac.
	*/
	
	SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);


	void Start () {

		sp.Open();
		/*set the read timeout low so unity doesn't freeze, 
		  and catch the exception below in update that unity will throw 
		  when the port isn't open and unity tries to check it */
		sp.ReadTimeout = 1;

		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";

	}
	void FixedUpdate () {

		if (sp.IsOpen == true) {

			try {
				MoveObject (sp.ReadByte ());
				print (sp.ReadByte ());
			}  catch (System.Exception) {

			}
		}

	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 13) 
		{
			winText.text = "You Win!";
		}	
	}
	void MoveObject (int direction) {

		float moveX;
		float moveZ;

		if (direction == 1 ) {
			moveX = 1;
			moveZ = 0;
			Vector3 movement = new Vector3 (moveX, 0.0f, moveZ);
			rb.AddForce (movement * speed);

		}

		if (direction == 2 ) {
			moveX = -1;
			moveZ = 0;
			Vector3 movement = new Vector3 (moveX, 0.0f, moveZ);
			rb.AddForce (movement * speed);
		}

		if (direction == 3 ) {
			moveX = 0;
			moveZ = -1;
			Vector3 movement = new Vector3 (moveX, 0.0f, moveZ);
			rb.AddForce (movement * speed);
		}

		if (direction == 4 ) {
			moveX = 0;
			moveZ = 1;
			Vector3 movement = new Vector3 (moveX, 0.0f, moveZ);
			rb.AddForce (movement * speed);
		}

		if (direction == 0) {
			moveX = 0;
			moveZ = 0;
			Vector3 movement = new Vector3 (moveX, 0f, moveZ);
			rb.AddForce (movement * speed);
		}
	}

}
