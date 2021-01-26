using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float mousex = 0;
    public static float speed = 5;
    public static float min = -10.0f;
    public static float max = 10.0f;
    public static Vector3 newpos;
    public static float playerSpeed = 8.0f;
    bool move = true;

    public GameObject failPanel;

    public GameObject floattingTextPrefabs;
    //  public Rigidbody rb;


    private void Update()
    {
        if (move)
        {
            MovePlayer();
        }

    }
    private void MovePlayer()
    {
        transform.Translate(0, 0, 1f * playerSpeed * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            newpos = this.transform.position;
            mousex = Input.mousePosition.x;
            newpos.x = (mousex * (((max - min) / 2) / (Screen.width / 2)) + min);
            newpos.x = Mathf.Clamp(newpos.x, min, max);
            this.transform.position = Vector3.Lerp(this.transform.position, newpos, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectable")
        {
            other.transform.localPosition = transform.GetChild(transform.childCount - 1).position; // collect objesini karakterin altina ekliyorum
            transform.position += new Vector3(0, 2.2f, 0); // ilk bastaki playerimi bir kup boyutunda yukari kaldiriyorum
            other.transform.parent = transform; // altima ekledigim objenin parentina oyuncum yapiyorum
            other.isTrigger = false; //eklenen objenin trigerini kapatiyorum
            FloattingTextPrefabs();
        }
        else if (other.gameObject.tag == "lav")
        {
            if (transform.childCount > 3)
            {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                transform.position += new Vector3(0, -2.2f, 0);
                transform.GetChild(transform.childCount - 1).parent = transform;
            }
           

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            if (transform.childCount > 3)
            {
                transform.GetChild(transform.childCount - 1).parent = null;
                Debug.Log("Gitti");

            }
            else
            {
                move = false;
                failPanel.SetActive(true);
                Debug.Log("Fail");
            }
        }
        

    }
   
    void FloattingTextPrefabs()
    {
        var editPosition = new Vector3(-1.0f, 0, 0);
         Instantiate(floattingTextPrefabs, transform.position + editPosition, Quaternion.identity, transform);
        
    }
}
