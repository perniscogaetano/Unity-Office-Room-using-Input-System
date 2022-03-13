using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightOnOff : MonoBehaviour
{
    [SerializeField] GameObject txtToDisplay;
    [SerializeField] List<GameObject> lights;

    private bool PlayerInZone;
    private PlayerInputActions playerInputActions;

    void Awake()  
    {
        PlayerInZone = false;
        txtToDisplay.SetActive(false);

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.LightSwitch.performed += LightSwitch;
        playerInputActions.Player.Click.performed += Click;

    }

    void Click(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
        {
            if (PlayerInZone)
            {
                foreach (GameObject lightorobj in lights)
                {
                    lightorobj.SetActive(!lightorobj.activeSelf);
                }

                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<Animator>().Play("switch");
            }
        }
    }

    // Update is called once per frame
    void LightSwitch(InputAction.CallbackContext context)
    {
        if (PlayerInZone)
        {
            foreach(GameObject lightorobj in lights)
            {
                lightorobj.SetActive(!lightorobj.activeSelf);
            }

            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("switch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            txtToDisplay.SetActive(false);
            PlayerInZone = false;
        }
    }
}
