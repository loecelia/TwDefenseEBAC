using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class AdministradorToques : MonoBehaviour
{

    public InputActionAsset inputs;
    private InputAction toque;
    private InputAction posicionToque;
    private Camera mainCam;

    public delegate void PlataformaTocada(GameObject plataforma);
    public event PlataformaTocada EnPlataformaTocada;

    private void OnEnable()
    {
        TouchSimulation.Enable();
        inputs.Enable();
        toque = inputs.FindAction("Toque");
        posicionToque = inputs.FindAction("PosicionToque");
        toque.performed += Toque; 
    }

    private void OnDisable()
    {
        inputs.Disable();
        TouchSimulation.Disable();
        toque.performed -= Toque; 

    }
    
    
    // Start is called before the first frame update
    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Toque(InputAction.CallbackContext obj)
    {
        Vector2 poseToque2D = posicionToque.ReadValue<Vector2>();
        Vector3 poseToque3D = new Vector3(poseToque2D.x, poseToque2D.y, mainCam.farClipPlane);
        Ray rayoPantalla = mainCam.ScreenPointToRay(poseToque3D);
        Debug.Log($"la pantalla fue tocada en la posicion: {poseToque2D}");
        RaycastHit hit;
        if(Physics.Raycast(rayoPantalla, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.tag == "Plataforma")
            {
                Debug.Log("Plataforma tocada");
                if(EnPlataformaTocada != null)
                {
                    EnPlataformaTocada(hit.transform.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("no hubo un hit del raycast");
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
