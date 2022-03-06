using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numTentativas;          // armazena as tentativas válidas naquela rodada
    private int maxTentativas;          // número máximo de tentativas
    int score = 0;                      // armazena a pontuação
    public GameObject letra;            // prefab da letra
    public GameObject centro;           // objeto de texto no centro

    private string palavra= "";         // palavra a ser descoberta
    // private string [] bancoPalavras = new string [] {"carro", "elefante", "futebol"}; // array de palavras possíveis (usado no lab)
    private int tamanhoPalavra;         // tamanho da palavra
    char[] letras;                      // letras da palavra a ser descoberta
    bool[] letrasCertas;                // letras descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centro");
        InitGame();
        InitLetras();
        numTentativas = 0;
        maxTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTeclado();
    }
    
    void InitLetras()
    {
        int numLetras = tamanhoPalavra;
        for (int i=0; i<numLetras; i++){
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);     // nomeia na hierarquia a GameObject com letra-(iésima+1), i =1..numLetras
            l.transform.SetParent(GameObject.Find("Canvas").transform);     // posiciona como filho do GameObject Canvas
        }
    }

    void InitGame()
    {
        // palavra = "Elefante";                       // palavra a ser descoberta (utilizado no Lab1 - Parte A)
        // int numeroAleatorio = Random.Range(0,bancoPalavras.Length);  // sorteamos uma posição do array (usado no lab)
        // palavra = bancoPalavras[numeroAleatorio];  // definimos a palavra como a palavra da posição sorteada (usado no lab)
        palavra = SorteiaPalavra();
        tamanhoPalavra = palavra.Length;           // determina no número de letras da palavra
        palavra = palavra.ToUpper();                // transforma a palavra em caixa alta
        letras = new char[tamanhoPalavra];         // instanciamos o array car das letras da palavra
        letrasCertas = new bool[tamanhoPalavra];   // instaciamos o bool char do indicador de letras corretas
        letras = palavra.ToCharArray();            // copia a palavra para o array char
    }

    void checkTeclado()
    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraCodificada = System.Convert.ToInt32(letraTeclada);

            if(letraCodificada>=97 && letraCodificada<= 122)
            {   
                numTentativas++;
                UpdateNumTentativas();
                if (numTentativas > maxTentativas)
                {
                    SceneManager.LoadScene("Lab1_forca");
                }
                for (int i = 0; i <= tamanhoPalavra; i++){
                    if (!letrasCertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if (letras[i] == letraTeclada)
                        {   
                            letrasCertas[i] = true;
                            GameObject.Find("letra" + (i+1)).GetComponent<Text>().text = letraTeclada.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            VerificaPalavra();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxTentativas;
    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }

    void VerificaPalavra()
    {
        bool condicao = true;
        for (int i = 0; i < tamanhoPalavra; i++)
        {
            condicao = condicao && letrasCertas[i];
        }

        if (condicao)
        {
            PlayerPrefs.SetString("palavra", palavra);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    string SorteiaPalavra()
    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras1", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length + 1);
        return (palavras[palavraAleatoria]);
    }
}
