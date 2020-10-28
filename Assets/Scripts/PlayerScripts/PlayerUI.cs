using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //Ui de transferencia
    public Image transferUI;
    private GameManager gameManagerScript;
    private PlayerGun playerGunScript;

    //UI de localização 
    public RectTransform pointerLocation;
    public GameObject magnet;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerGunScript = GameObject.Find("Player").GetComponent<PlayerGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.gameStart == true)
        {
            
            IsTransferReady();
            PointerMagnetLocation();
        }
    }

    void IsTransferReady()
    {
        if (playerGunScript.radius > playerGunScript.TransferZone())
        {
            transferUI.gameObject.SetActive(true);
        }
        else
        {
            transferUI.gameObject.SetActive(false);
        }
    }

    void PointerMagnetLocation()
    {
        pointerLocation.gameObject.SetActive(true);

        Vector3 objScreenPos = cam.WorldToScreenPoint(magnet.transform.position);

        Vector3 dir = (objScreenPos - pointerLocation.position).normalized;

        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;

        pointerLocation.localEulerAngles = new Vector3(pointerLocation.localEulerAngles.x, pointerLocation.localEulerAngles.y, angle);
    }
}
