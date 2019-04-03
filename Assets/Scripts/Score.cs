using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    Text vidasJugador1 = null; //Texto donde apareceran las vidas del jugador 1
    Text vidasJugador2 = null; //Texto donde apareceran las vidas del jugador 2
    Text ganadorPlayer = null;  //Texto que aparecera al final de la partida, diciendo el ganador (playe1 o player2)
    public Text ganador = null; //Texto que aparecera al final de la partida, diciendo el ganador (playe1 o player2)
    public Text ganador1 = null; //Texto que aparecera al final de la partida, diciendo el ganador (playe1 o player2)

    public Text creditos; //Texto donde apareceran las reglas y creditos del juego

    public static int vidasP1 = 3; // varible tipo entero donde se almasenaran las vidas del jugado 1
    public static int vidasp2 = 3; // varible tipo entero donde se almasenaran las vidas del jugado 2


    void Start()
    {
        vidasJugador1 = GameObject.Find("VidasP1").GetComponent<Text>(); //Se busca el Gameobject VidasP1 y se le coje el componente "Text"  
        vidasJugador2 = GameObject.Find("VidasP2").GetComponent<Text>(); //Se busca el Gameobject VidasP2 y se le coje el componente "Text"  
        ganadorPlayer = GameObject.Find("GanadorPlayer").GetComponent<Text>(); //Se busca el Gameobject GnadorPlayer y se le coje el componente "Text"  

        //los textos de ganador y creditos al iniciar el juego comiensan desactivados 
        ganador.enabled = false;
        ganador1.enabled = false;
        creditos.enabled = false;
    }

    void Update()
    {
        //Se actualizan los textos VidasP1 y VidasP2
        vidasJugador1.text = "" + vidasP1;
        vidasJugador2.text = "" + vidasp2;

        //Si VidasP1 es menor o igual a 0 entonces el ganador sera player 2, y se activaran los textos de ganador y ganador1
        if (vidasP1 <= 0) 
        {
            ganadorPlayer.text = "Player 2";
            ganador.enabled = true;
            ganador1.enabled = true;
        }
        //Si VidasP2 es menor o igual a 0 entonces el ganador sera player 1, y se activaran los textos de ganador y ganador1
        if (vidasp2 <= 0)
        {
            ganadorPlayer.text = "Player 1";
            ganador.enabled = true;
            ganador1.enabled = true;
        }
    }

    //Public void con el cual se activara el texto "creditos", este metodo se ejecutara desde un boton en la scena
    public void EnableText()
    {
        creditos.enabled = true;
    }
    //Public void con el cual se desactivara el texto "creditos", este metodo se ejecutara desde un boton en la scena
    public void OnenableText()
    {
        creditos.enabled = false;
    }
}
