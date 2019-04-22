using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Taller2 : MonoBehaviour
{

    string Clor;
    /// <summary>
    /// Se da la cantidad De objetos que van estar en la  escena, los aldeanos y Zombies se determine aleatorio
    /// </summary>

    void Start()
    {
        
        GameObject hero = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hero.AddComponent<Hero>().Init();
        int zrnd = Random.Range(10, 21);
        for (int i = 0; i < zrnd; i++)
        {

            int Crnd = Random.Range(1, 4);
            if (Crnd == 1) 
            {
                Clor = "Cyan";
            }
            if (Crnd == 2)
            {
                Clor = "Magenta";
            }
            if (Crnd == 3)
            {
                Clor = "Verde";
            }
            GameObject zCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            zCube.AddComponent<Zombie>().Infectado(Clor);
        }
        int alde = 21 - zrnd;
        
        for (int i = 0; i < alde; i++)
        {

            GameObject aCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            aCube.AddComponent<Aldeano>().ID();

        }
      
    }
}

public class Hero : MonoBehaviour
{
    /// <summary>
    /// se realiza una referencia para los scripts de movimiento y mirar del Heroe (jugador)
    /// Agregamos los componentes necesarios para realizar collisiones y se desactiva el uso de graveda y constraints
    /// </summary>
    Movement movement;
    Look look;
    float speed;
    public void Init()
    {
       
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        gameObject.name = "Hero";
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        movement = gameObject.AddComponent<Movement>();
        look = gameObject.AddComponent<Look>();
        gameObject.AddComponent<Camera>();
        speed = Random.Range(0.2f, 0.7f);

    }
    /// <summary>
    /// llama a los constructores en las clases de Movement y Look
    /// </summary>
    private void Update()
    {
        movement.Move(speed);
        look.Arround();
    }
    /// <summary>
    /// se toman los valores almacendos en el estruc para imprimirlos cada que el heroe colisione con 
    /// el respectivo GameObject
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Zombie>())
        {
            InfoZomb info = collision.gameObject.GetComponent<Zombie>().GetInfo();
            Debug.Log("Waaar quiero comer " + info.gusto);
        }
        if(collision.gameObject.GetComponent<Aldeano>())
        {
            InfoAlde info = collision.gameObject.GetComponent<Aldeano>().GetInfo();
            Debug.Log("soy " +info.name+" y tengo " + info.edad );
        }
    }
}

public class Aldeano : MonoBehaviour
{

    /// <summary>
    /// se realiza una enumeracion para guardar los posibles nombres de los aldeanos 
    /// tambien creamos un espacio para almacenar la edad de los aldeanos
    /// </summary>
    int age;
    public enum Nombres
    {
      Carlos, Sebastian, Eduardo, Daniel, Cata,
      Danilo , Felipe , Tatiana , Juan ,Ronald ,
      Geremias , Rene , Eugenia , Eulari , Gala ,
      Gurtza , Gudula , Hebe , Fara, Fedora

    }

    public Nombres nombres;
    /// <summary>
    /// se le agrega un comoponente  Rigidbody y se desactivan las influencias externas 
    /// le damos un valor random a la edad entre 15 y 100 
    /// y tomamos un nombre ademas de ponerlo en un lugar aleatorio del mapa 
    /// entre las posiciones 10 y -10 en los ejes "x" y "z"
    /// </summary>
    public void ID()
    {
        
        age = Random.Range(15, 101);
        nombres = (Nombres)Random.Range(0,20);
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        this.gameObject.name = nombres.ToString();
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        this.gameObject.transform.position = new Vector3(x, 0, z);
    }
    /// <summary>
    /// almacenamos en el struct del aldeano el nombre 
    /// y su edad
    /// </summary>
    /// <returns></returns>
    public InfoAlde GetInfo()
    {
        InfoAlde infoAlde = new InfoAlde();
        infoAlde.edad = age;
        infoAlde.name = nombres.ToString();
        return infoAlde;
    }
}


public class Zombie : MonoBehaviour
{
  /// <summary>
  /// declaramos 2 enumeradores uno para el estado del zombie y otro para la comida preferida
  /// </summary>
   
    float t;
   
    public enum Gustos
    {
        Cerebelo,
        Muslo,
        Riñon,
        Brazo,
        CostilitasALaBQ
    }
    
    public enum Estado
    {
        Void,
        Moving
    }
   
    Gustos comida;
    Estado estado;
    /// <summary>
    /// le otorgamos un rigidboy como en las otras dos clases para efectuar la colisiones
    /// se determina de que color sera el zombie de manera aleatoria en generador
    /// y se posicionea en un lugar random del mundo entre 10 y menos 10 de los ejes "z" y "x"
    /// </summary>
    public void Infectado(string olor)
    {
        Rigidbody rb;
        comida = (Gustos)Random.Range(0, 5);
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
        if (olor == "Verde")
        {
            
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.green;
            this.gameObject.name = "Zombie";
        }
        if (olor == "Cyan")
        {
           
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.cyan;
            this.gameObject.name = "Zombie";

        }
        if(olor == "Magenta")
        {
           
            Renderer color = this.gameObject.GetComponent<Renderer>();
            color.material.color = Color.magenta;
            this.gameObject.name = "Zombie";

        }
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        this.gameObject.transform.position = new Vector3(x, 0, z);
        
    }
    /// <summary>
    ///almacenamos en el struct del zombie Sus gusto
    /// </summary>
    public InfoZomb GetInfo()
    {
        InfoZomb infoZomb = new InfoZomb();
        infoZomb.gusto = comida.ToString();
        return infoZomb;
    }
    /// <summary>
    /// determinar el tiempo entre cada estado del zombie 
    /// y acceder al siguiente estado de manera aleatorea cada 5 segundos
    /// </summary>
   private void Update()
    {            
       t += Time.deltaTime;
        if (t >= 5)
        {
            estado = (Estado)Random.Range(0, 2);
            this.gameObject.transform.Rotate(0, Random.Range(1f, 361f), 0, 0);
            t = 0;
        }
        switch (estado)
        {
            case Estado.Void:                                                                      
                break;
            case Estado.Moving:    
                
                this.gameObject.transform.Translate(transform.forward*0.02f);                  
                break;
            default:
                break;
        }              
    }
}


