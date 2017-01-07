using UnityEngine;
using System.Collections;

public class PowerupShield_S : MonoBehaviour {
    public Boundary boundary;
    public float speed;
    public int health;
    public static float moveHorizontal;
    public static Vector2 movement;
    public static bool moveDown;

    // Use this for initialization
    void Start () {

	moveDown = true;

	}
	
	// Update is called once per frame
	void Update () {

        if (Enemy_S.levelComplete == false)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            movement = new Vector2(moveHorizontal, 0);
            GetComponent<Rigidbody2D>().velocity = movement * speed;
        }

        else
            endingAnimation();

        //Lock the position in the screen by putting a boundaries
        GetComponent<Rigidbody2D>().position = new Vector2
            (
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
            );
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Excute if the object tag was equal to one of these
        if (other.tag == "EnemyShot")
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }	
    }

    void endingAnimation()
    {

        PowerupShield_S.moveHorizontal = 0;
        PowerupShield_S.movement = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().velocity = movement * 0;
        boundary.yMin = -7;
        boundary.yMax = 20;
        if (GetComponent<Rigidbody2D>().position.y > boundary.yMin && moveDown == true)
            transform.Translate(Vector2.down * 5 * Time.deltaTime);

        if (GetComponent<Rigidbody2D>().position.y == boundary.yMin)
            moveDown = false;

        if (moveDown == false)
            transform.Translate(Vector2.up * 15 * Time.deltaTime);
    }
}
