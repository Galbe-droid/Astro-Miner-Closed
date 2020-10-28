using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Transform[] guns;
    public GameObject bullet;
    public GameObject muzzle;

    private GameManager gameManagerScript;
    private ButtonSelection buttonSelectionScript;

    //Armamento
    private int gunArray;
    private float gunDelay = 0.5f;
    private float delayTime = 0;
    private bool allowFire = true;
    private bool isDelivering;

    //Deposito
    private float deliveryDelay = 1.5f;
    private float deliveryTime = 0;
    public ParticleSystem transportCargoEffect;

    //Circulo Delimitador para a opção de transferir
    public float radius = 50f;
    Vector3 centerPosition = new Vector3(0, 6, 0);

    // Start is called before the first frame update
    void Start()
    {
        buttonSelectionScript = GameObject.Find("GameManager").GetComponent<ButtonSelection>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        gunArray = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transportCargoEffect.Stop();
        delayTime += Time.deltaTime;
        deliveryTime += Time.deltaTime;

        TransferZone();

        FireTime();
        FireGun();
    }

    void FireTime()
    {
        if(delayTime >= gunDelay)
        {
            allowFire = true;
        }
    }

    void FireGun()
    {
        if(gameManagerScript.isPauseActive == false)
        {
            if (allowFire && !isDelivering)
            {
                if(Input.GetMouseButton(0) && buttonSelectionScript.mouseSelected)
                {
                    Firing();
                }

                if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && buttonSelectionScript.keyboardSelected)
                {
                    Firing();
                }

                if (gunArray >= 2)
                {
                    gunArray = 0;
                }

            }

            if (Input.GetKey(KeyCode.Tab))
            {
                CargoTransfer();
            }
        }
    }

    void Firing()
    {
        Instantiate(bullet, guns[gunArray].transform.position, guns[gunArray].transform.rotation);
        Instantiate(muzzle, guns[gunArray].transform.position, guns[gunArray].transform.rotation);
        gunArray++;
        allowFire = false;
        delayTime = 0;
    }

    void CargoTransfer()
    {
        if (gameManagerScript.isPauseActive == false && !isDelivering && radius > TransferZone())
        {
            while (gameManagerScript.playerCargo > 1 && deliveryDelay < deliveryTime)
            {
                isDelivering = true;
                gameManagerScript.UpdateScore(-5);
                gameManagerScript.RefinaryScore(5);
                deliveryTime = 0;
            }

            if(isDelivering)
            {
                transportCargoEffect.Play();
            }
        }
        isDelivering = false;
    }

    public float TransferZone()
    {
        //Localização do jogador e Limite 
        float distance = Vector3.Distance(transform.position, centerPosition);
        return distance;
    }
}
