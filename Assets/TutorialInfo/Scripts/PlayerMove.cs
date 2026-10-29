using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the component is created
    [SerializeField] float speed = 5f;
    [SerializeField] Color playerColor = Color.blue;
    
    private Renderer rend;
    void Start()
    {
        Debug.Log("PlayerMove Start!");

        rend=GetComponent<Renderer>();
        rend.material.color = playerColor;
        rend.material.SetColor("_EmissionColor", playerColor);
    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }   
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }

}

