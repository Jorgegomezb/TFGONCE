using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void loadScene(string scene)
    {
        
        SceneManager.LoadScene(scene);
    }

    public void SaliR()
    {
        Application.Quit();
        // Debug.Log("Salio");
    }
}

/*
 * Incorporamos SceneManagement y usamos la función LoadScene para cargar la función que le indiquemos, el parámetro. 
 * El parametro lo informaremos en cada uno de los botones que formarán el menú.
 */
