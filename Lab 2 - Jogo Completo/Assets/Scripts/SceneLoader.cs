using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que fará a carga da Cena de acordo com um index
/// PCBJ - Minami
/// </summary>


public class SceneLoader : MonoBehaviour
{   
    /*
    * Método que passa parâmetro inteiro para LoadScene referente
    * à pagina (cena) desejada
    */
   public void LoadOnClick(int sceneIndex)
   {
       SceneManager.LoadScene(sceneIndex);  // Método Unity para carga de cena
   }
}
