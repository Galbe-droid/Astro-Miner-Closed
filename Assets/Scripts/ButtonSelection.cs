using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    Color greyDisable = new Color(0.5f, 0.5f, 0.5f);
    Color whiteActive = new Color(1, 1, 1);

    //Mouse ou Keys rotação
    public Button mouseRotation;
    public Button keyboardRotation;

    public bool mouseSelected;
    public bool keyboardSelected;

    //Movimentação por fisica ou Arcade
    public Button physicsMovement;
    public Button arcadeMovement;

    public bool physicsSelected;
    public bool arcadeSelected;

    //Habilitar ou desabilitar planeta
    public Toggle planetOn;
    public GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        mouseSelected = true;
        keyboardSelected = false;

        physicsSelected = true;
        arcadeSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotationSelected();
        MovementSelected();
    }

    void RotationSelected()
    {
        if (mouseSelected)
        {
            keyboardRotation.image.color = greyDisable;
            mouseRotation.image.color = whiteActive;
        }
        else if (keyboardSelected)
        {
            keyboardRotation.image.color = whiteActive;
            mouseRotation.image.color = greyDisable;
        }
    }

    void MovementSelected()
    {
        if(physicsSelected)
        {
            physicsMovement.image.color = whiteActive;
            arcadeMovement.image.color = greyDisable;
        }
        else if(arcadeSelected)
        {
            physicsMovement.image.color = greyDisable;
            arcadeMovement.image.color = whiteActive;
        }
    }

    public void KeyboardRotation()
    {
        mouseSelected = false;
        keyboardSelected = true;
    }

    public void MouseRotation()
    {
        mouseSelected = true;
        keyboardSelected = false;        
    }

    public void PhysicsMovement()
    {
        physicsSelected = true;
        arcadeSelected = false;
    }

    public void ArcadeMovement()
    {
        physicsSelected = false;
        arcadeSelected = true;
    }

    public void IsPlanetOn()
    {
        if(planetOn.isOn == true)
        {
            planet.gameObject.SetActive(true);
        }
        else
        {
            planet.gameObject.SetActive(false);
        }
    }
}
