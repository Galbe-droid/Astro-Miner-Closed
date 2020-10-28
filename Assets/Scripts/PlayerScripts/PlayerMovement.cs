using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Opções selecionadas
    private ButtonSelection buttonSelectionScript;

    //forças
    [SerializeField] private float horsePower = 10;
    [SerializeField] private float rotationPower = 500;
    [SerializeField] private float brakePower = 200;

    //Circulo Delimitador 
    float radius = 150f;
    Vector3 centerPosition = new Vector3(0, 6, 0);

    public Rigidbody magnetRb;
    private Rigidbody playerRb;
    public Camera cam;
    private GameManager gameManagerScript;

    public GameObject spark;
     
    // Start is called before the first frame update
    void Start()
    {
        buttonSelectionScript = GameObject.Find("GameManager").GetComponent<ButtonSelection>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManagerScript.gameStart == true && !gameManagerScript.isPauseActive)
        {
            playerRb = GetComponent<Rigidbody>();
            magnetRb = GetComponent<Rigidbody>();
            cam = FindObjectOfType<Camera>();
            Movement();
        }
    }

    void Movement()
    {
        //movimento da nave

        float horizontalMove = Input.GetAxis("Horizontal");

        float verticalMove = Input.GetAxis("Vertical");

        //Opção de movimento 
        if(buttonSelectionScript.physicsSelected)
        {
            //movimentação por fisica
            playerRb.AddRelativeForce(Vector3.forward * verticalMove * horsePower, ForceMode.Acceleration);
        }
        if(buttonSelectionScript.arcadeSelected)
        {
            //movimentação por deslocação
            transform.Translate(Vector3.forward * verticalMove * horsePower * Time.deltaTime, Space.Self);
        }

        //opção de rotação 
        if(buttonSelectionScript.keyboardSelected)
        {
            if (buttonSelectionScript.physicsSelected)
            {
                playerRb.AddRelativeTorque(Vector3.right * horizontalMove * rotationPower, ForceMode.Acceleration);
            }

            if (buttonSelectionScript.arcadeSelected)
            {
                transform.Rotate(Vector3.up * horizontalMove * rotationPower * Time.deltaTime, Space.Self);
            }
        }

        if(buttonSelectionScript.mouseSelected)
        {
            //Movimentação para os lados
            if (buttonSelectionScript.physicsSelected)
            {
                playerRb.AddRelativeForce(Vector3.right * horizontalMove * rotationPower, ForceMode.Acceleration);
            }

            if (buttonSelectionScript.arcadeSelected)
            {
                transform.Translate(Vector3.right * horizontalMove * rotationPower * Time.deltaTime, Space.Self);
            }

            //Movimento de mira
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        //Localização do jogador e Limite 
        float distance = Vector3.Distance(playerRb.transform.position, centerPosition);

        if (distance > radius)
        {
            Vector3 radiusLimit = playerRb.transform.position - centerPosition;
            radiusLimit *= radius / distance;
            playerRb.transform.position = centerPosition + radiusLimit;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(spark, transform.position, transform.rotation);
    }
}
