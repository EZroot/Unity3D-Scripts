using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public GameObject cameraPivot;
	public int speed = 15;

	private Vector3 dragOrigin;

	void Start()
	{
		//Default iso
		//There is a Call order here
		cameraPivot.transform.rotation = Quaternion.Euler(new Vector3(30,0,0));
		transform.rotation = Quaternion.Euler (0, 45, 0);
	}

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
			transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

		if (Input.GetKey(KeyCode.E) && GameManager.Instance.mode == GameManager.GameState.Play)
		{
			transform.Rotate (Vector3.up * -100 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Q) && GameManager.Instance.mode == GameManager.GameState.Play)
		{
			transform.Rotate (Vector3.up * 100 * Time.deltaTime);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) { //forward
			Camera.main.orthographicSize--;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f) { //backwards
			Camera.main.orthographicSize++;
		}

		if (Input.GetMouseButtonDown (2)) 
		{
				dragOrigin = Input.mousePosition;
				return;
		}

			if (!Input.GetMouseButton(2)) return;

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * speed * 0.5f, 0, pos.y * speed * 0.5f);

			transform.Translate(move);  
			
    }
}
