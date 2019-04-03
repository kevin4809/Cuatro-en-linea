using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public int width;  //Definimos el numero de prefabs que queremos instanciar, en forma horizontal 
    public int height; //Definimos el numero de prefabs que queremos instanciar, en forma vertical 
    public GameObject puzzelePiece; //El Gameobject que deseamos Instanciar 
    private GameObject[,] grid; //Aqui almazenaremos los Gameobjects instanciados 

    public bool Click; //bool con el cual controlaremos, el color que hay en cada click 
    int contador; //contador con el cual controlaremos el numero de esferas del mismo color en Horizontal, Vertical y Diagonal  

    void Start()
    {
        grid = new GameObject[width, height];
        for (int x = 0; x < width; x++) //x es es igual a 0. Si x es menor que width se entrara adentro del for, y el valor de x aumentara  
        {
            for (int y = 0; y < height; y++) //y es es igual a 0. Si y es menor que height se entrara adentro del for, y el valor de y aumentara  
            {
                GameObject go = GameObject.Instantiate(puzzelePiece) as GameObject; //Se instancia el gameobject 

                //Asignamos la posicion el la cual se van a instanciar los Gameobjects 
                Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;

                grid[x, y] = go;  //se almacena la posicion de los Gameobjects en "x" y "y"
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);//El color por default de la esfera al instanciarse sera blanco 
            }
           
        }

    }

    void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Se dectecta los posicion del mouse en la scena    
        UpdatePickedPiece(mPosition); //Se llama el metodo UpdatePickedPiece 

        //acedemos a la variable tipo static del script Score, y decimos que si VidasP1 es menor o igual a 0, se ejecute el metodo Destruir()
        if (Score.vidasP1 <= 0)
        {
            Destruir(); // se encarga de destruir todos los gameobjects de la Scena 
        }
        //acedemos a la variable tipo static del script Score, y decimos que si VidasP2 es menor o igual a 0, se ejecute el metodo Destruir()
        if (Score.vidasp2 <= 0)
        { 
            Destruir(); // se encarga de destruir todos los gameobjects de la Scena 
        }
    }
    void UpdatePickedPiece(Vector3 position)
    {
        int x = (int)(position.x + 0.5f); //posicion del mouse "x"
        int y = (int)(position.y + 0.5f); //posicion del mouse "y"

        if (Click == true) //Si el bool Click es True entonces entramos al if 
        {
            //si "x" es mayor igual que 0, "y" es mayor igual que 0, "x" es menor que width, "y" es menor que height y apretamos el boton primario del mouse, se esntrara en el if 
            if (x >= 0 && y >= 0 && x < width && y < height && Input.GetMouseButtonDown(0))
            {
                GameObject go = grid[x, y];
                if (go.GetComponent<Renderer>().material.color == Color.white) //si el color del gameobject en el cual tenemos el mouse es blanco, podremos entrar al if 
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black); //pintamos el Gameobject en el cual tenemos el mouse de color negro 
                    Click = false; // el bool click se coloca en falso 
                    Comprobar(position); //llamamos el metodo comprobar(position)
                }
            }
        }
        else //Si click no es true entonces 
        {
            //si "x" es mayor igual que 0, "y" es mayor igual que 0, "x" es menor que width, "y" es menor que height y apretamos el boton primario del mouse, se esntrara en el if 
            if (x >= 0 && y >= 0 && x < width && y < height && Input.GetMouseButtonDown(0))
            {
                GameObject go = grid[x, y];
                if (go.GetComponent<Renderer>().material.color == Color.white) //si el color del gameobject en el cual tenemos el mouse es blanco, podremos entrar al if 
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);  //pintamos el Gameobject en el cual tenemos el mouse de color Azul 
                    Click = true; // el bool Click se coloca en verdadero 
                    Comprobar(position); //llamamos el metodo Comprobar
                }
            }
        }
        
    }


    void Comprobar(Vector3 position)
    {
        int x = (int)(position.x + 0.5f); //posicion del mouse en "x"
        int y = (int)(position.y + 0.5f); //posicion del mouse en "y"

        //si "x" es mayor igual que 0, "y" es mayor igual que 0, "x" es menor que width y "y" es menor que height entramos al if 
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];
            Color blue = go.GetComponent<Renderer>().material.color; //Accedemos al componente "Renderer" de los Gameobjects Instanciados, y almacenamos la informacion en blue
            Color black = go.GetComponent<Renderer>().material.color; //Accedemos al componente "Renderer" de los Gameobjects Instanciados, y alamacenamos la informacion en black

            if (blue == Color.blue) //Si el color es azul entramos al if 
            {
                for (int i = 0; i < width; i++) //"i" es igual a 0, si "i" menor que "width" entramos en el for, y "i"aumenta su valor 
                {
                    Color horizontal = grid[i, y].GetComponent<Renderer>().material.color; //horizontal Toma el componente "renderer" de los Gameobject en posicion Horizontal 

                    if (blue == horizontal) //si hay gameObject en horizontal de color azul entramos al if
                    {
                        contador++;//El contador ira aumentando su valor  
                    }
                    else //si no hay Gameobjects de color azul en horizontal 
                      contador = 0; // el contador regresa a 0

                    if (contador == 4) //si el contador es igual a 4 entramos al if 
                    {

                        Score.vidasp2 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP2
                        contador = 0; // contador regresa a 0
                        Borrar(); //llamamos el metodo borrar()
                    }
                }

                for (int i = 0; i < height; i++)  //"i" es igual a 0, si "i" menor que "width" entramos en el for, y "i"aumenta su valor 
                {
                    Color vertical = grid[x, i].GetComponent<Renderer>().material.color; //vertical Toma el componente "renderer" de los Gameobject en posicion Vertical 

                    if (blue == vertical)//si hay gameObject en horizontal de color azul entramos al if
                    {
                        contador++; //El contador ira aumentando su valor  
                    }
                    else //si no hay Gameobjects de color azul en Vertical 
                        contador = 0; // el contador regresa a 0

                    if (contador == 4) //si el contador es igual a 4 entramos al if 
                    {
                        Score.vidasp2 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP2
                        contador = 0; // contador regresa a 0
                        Borrar(); //llamamos el metodo borrar() 
                    }
                }

                for (int i = -width; i < width; i++) //"i" es igual a "-width" si "i" es menor que "width" entramos a el for y el valor de "i" ira aumentando 
                {
                    //si "x" + "i" mayor igual que 0, "y" + "i" mayor igual que 0, "x" + "i" menor que "width" y "y" + "i" menor que height  
                    if (x + i >= 0 && y + i >= 0 && x + i < width && y + i < height)  
                    {
                        Color diagonal = grid[x + i, y + i].GetComponent<Renderer>().material.color; //diagonal Toma el componente "renderer" de los Gameobject en posicion Diagonal 

                        if (blue == diagonal)//si hay gameObjects en Diagonal de color azul entramos al if
                        {
                            contador++; //El contador ira aumentando su valor  
                        }
                        else //si no hay Gameobjects de color azul en Diagonal 
                            contador = 0; // el contador regresa a 0

                        if (contador == 4)//si el contador es igual a 4 entramos al if
                        {
                            Score.vidasp2 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP1
                            contador = 0;  // contador regresa a 0
                            Borrar(); //llamamos el metodo borrar()
                        }
                    }
                }
                for (int i = -width; i < width; i++) //"i" es igual a "-width" si "i" es menor que "width" entramos a el for y el valor de "i" ira aumentando 
                {
                    //si "x" - "i" mayor igual que 0, "y" + "i" mayor igual que 0, "x" - "i" menor que "width" y "y" + "i" menor que height  
                    if (x - i >= 0 && y + i >= 0 && x - i < width && y + i < height)
                    {
                        Color diagonal = grid[x - i, y + i].GetComponent<Renderer>().material.color; //diagonal Toma el componente "renderer" de los Gameobject en posicion Diagonal 

                        if (blue == diagonal) //si hay gameObjects en Diagonal de color azul entramos al if
                        {
                            contador++; //El contador ira aumentando su valor  
                        }
                        else //si no hay Gameobjects de color azul en Diagonal 
                            contador = 0; // el contador regresa a 0

                        if (contador == 4) //si el contador es igual a 4 entramos al if
                        {
                            Score.vidasp2 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP2
                            contador = 0; // contador regresa a 0
                            Borrar();  //llamamos el metodo borrar()
                        }
                    }
                }
            }
            if (black == Color.black)
            {

                for (int i = -width; i < width; i++)  //"i" es igual a "-width" si "i" es menor que "width" entramos a el for y el valor de "i" ira aumentando 
                {
                    //si "x" - "i" mayor igual que 0, "y" + "i" mayor igual que 0, "x" - "i" menor que "width" y "y" + "i" menor que height  
                    if (x -  i >= 0 && y + i >= 0 && x - i < width && y + i < height)
                    {
                        Color diagonal = grid[x - i, y + i].GetComponent<Renderer>().material.color; //diagonal Toma el componente "renderer" de los Gameobject en posicion Diagonal 

                        if (black == diagonal) //si hay gameObjects en Diagonal de color negro entramos al if
                        {
                            contador++; //El contador ira aumentando su valor  
                        }
                        else //si no hay Gameobjects de color negro en Diagonal 
                            contador = 0; // el contador regresa a 0

                        if (contador == 4) //si el contador es igual a 4 entramos al if
                        {
                            Score.vidasP1 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP1
                            contador = 0; // contador regresa a 0
                            Borrar();  //llamamos el metodo borrar()
                        }
                    }
                }
                for (int i = -width; i < width; i++)  //"i" es igual a "-width" si "i" es menor que "width" entramos a el for y el valor de "i" ira aumentando 
                {
                    //si "x" + "i" mayor igual que 0, "y" + "i" mayor igual que 0, "x" + "i" menor que "width" y "y" + "i" menor que height 
                    if (x + i >= 0 && y + i >= 0 && x + i < width && y + i < height)
                    {
                        Color diagonal = grid[x + i, y + i].GetComponent<Renderer>().material.color; //diagonal Toma el componente "renderer" de los Gameobject en posicion Diagonal 

                        if (black == diagonal) //si hay gameObjects en Diagonal de color negro entramos al if
                        {
                            contador++;  //El contador ira aumentando su valor  
                        }
                        else  //si no hay Gameobjects de color negro en Diagonal 
                            contador = 0; // el contador regresa a 0

                        if (contador == 4) //si el contador es igual a 4 entramos al if
                        {
                            Score.vidasP1 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP1
                            contador = 0;  // contador regresa a 0
                            Borrar();  //llamamos el metodo borrar()
                        }
                    }
                }
                for (int i = 0; i < width; i++)  //"i" es igual a 0, si "i" menor que "width" entramos en el for, y "i"aumenta su valor 
                {
                    Color horizontal = grid[i, y].GetComponent<Renderer>().material.color; //horizontal Toma el componente "renderer" de los Gameobject en posicion Horizontal 

                    if (black == horizontal) //si hay un gameObject en horizontal de color negro entramos al if
                    {
                        contador++; //El contador ira aumentando su valor  
                    }
                    else //si no hay Gameobjects de color negro  en horizontal 
                        contador = 0; // contador regresa a 0

                    if (contador == 4) //si el contador es igual a 4 entramos al if
                    {
                        Score.vidasP1 -= 1; //Accedemos al script Score y le restamos 1 al valor de vidasP1 
                        contador = 0; // contador regresa a 0 
                        Borrar();  //llamamos el metodo borrar()

                    }

                }

                for (int i = 0; i < height; i++) //"i" es igual a 0, si "i" menor que "width" entramos en el for, y "i"aumenta su valor 
                {
                    Color vertical = grid[x, i].GetComponent<Renderer>().material.color;  //vertical Toma el componente "renderer" de los Gameobject en posicion Vertical
                    
                    if (black == vertical) //si hay un gameObject en Vertical de color negro entramos al if
                    {
                        contador++; //El contador ira aumentando su valor  
                    }
                    else //si no hay Gameobjects de color negro  en Vertical 
                        contador = 0;  // contador regresa a 0

                    if (contador == 4) //si el contador es igual a 4 entramos al if
                    {
                        Score.vidasP1 -= 1;  //Accedemos al script Score y le restamos 1 al valor de vidasP1 
                        contador = 0; // contador regresa a 0  
                        Borrar(); //llamamos el metodo borrar()
                    }
                }
   
            }

        }

    }

    void Borrar() //Metodo con el cual pintamos todos los GameObject instanciados de blanco
    {
        for (int x = 0; x < width; x++) //x es es igual a 0. Si x es menor que width se entrara adentro del for, y el valor de x aumentara  
        {
            for (int y = 0; y < height; y++) //y es es igual a 0. Si y es menor que height se entrara adentro del for, y el valor de y aumentara  
            {

                GameObject go = grid[x, y];
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white); //el color del GameObject regresa a blanco
            }

        }
    }

    void Destruir() //metdo con el cual destruimos todos los GameObjects en la Scena
    {
        for (int x = 0; x < width; x++) //x es es igual a 0. Si x es menor que width se entrara adentro del for, y el valor de x aumentara  
        {
            for (int y = 0; y < height; y++) //y es es igual a 0. Si y es menor que height se entrara adentro del for, y el valor de y aumentara  
            {

                GameObject go = grid[x, y];
                Destroy(go.gameObject); //Se destrullen todos los GameObjects en la Scena
            }

        }
    }

}
