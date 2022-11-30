using System;
using System.IO;
using System.Collections.Generic;
using LibreriaBaraja;
using System.Runtime.ConstrainedExecution;

//Inicialización del paquete LibreriaBaraja
namespace LibreriaBaraja
{
    //enum llamado ePalo con los cuatro palos de la baraja del Poker
    public enum ePalo
    {
        Treboles,
        Corazones,
        Diamantes,
        Picas
    }

    //enum llamado eValor con los valores del 1 al 13 para cada carta
    public enum eValor
    {
        As = 1,
        Dos = 2,
        Tres = 3,
        Cuatro = 4,
        Cinco = 5,
        Seis = 6,
        Siete = 7,
        Ocho = 8,
        Nueve = 9,
        Diez = 10,
        Jack = 11,
        Queen = 12,
        King = 13

    }

    //clase Carta con atributos palo y valor
    public class Carta
    {
        //atributos
        private ePalo palo;
        private eValor valor;
        private bool bocaarriba;
        // Contructores
        public Carta(ePalo palo, eValor valor)
        {
            this.palo = palo;
            this.valor = valor;
            this.bocaarriba=true;
        }
        //constructor vacío de Carta
        public Carta()
        {

        }

        //setters y getters
        //retorna el palo de la carta
        public int getPalo()
        {
            return (int)palo;
        }
        //retorna el valor de la carta
        public int getValor()
        {
            return (int)valor;
        }
        public bool getBocaArriba()
        {
            return bocaarriba;
        }
        public void setBocaArriba(bool bocaarriba)
        {
            this.bocaarriba=bocaarriba;
        }
        //métodos
        //Muestra la carta visualmente
        public String MuestraCarta()
        {
            //Inicialización de variables del método
            int p = getPalo();
            int v = getValor();
            char simbolo = ' ';//♠', '♦', '♥', '♣'

            if(bocaarriba)
            {
                //Nos convierte el valor numerico de ePalo a un caracter visual del palo
                switch (p)
                {
                    case 0:
                        simbolo = '♣';
                        break;
                    case 1:
                        simbolo = '♥';
                        break;
                    case 2:
                        simbolo = '♦';
                        break;
                    case 3:
                        simbolo = '♠';
                        break;
                }
                if (v > 9) //si el valor es mayor que 9 hay que quitar algun espacio en blanco porque el nº de chars aumenta
                {
                    return "┌───────┐\n" + "│" + v + "     │\n" + "│       │\n" + "│       │\n" + "│       │\n" + "│      " + simbolo + "│\n" + "└───────┘";
                }
                return "┌───────┐\n" + "│" + v + "      │\n" + "│       │\n" + "│       │\n" + "│       │\n" + "│      " + simbolo + "│\n" + "└───────┘";    
            }
            else
                return "┌───────┐\n│║╔╗╔╗╔╗│\n│║║║║║║║│\n│║║║║║║║│\n│║║║║║║║│\n│╚╝╚╝╚╝║│\n└───────┘";
            
        }

        //metodo que necesita como parametro lista de cartas
        public static String MuestraCartasHorizontal(List<Carta> cartas)
        {
            //Declaración de variables de el método
            String lineaCarta = "";

            int p; //palo
            int v; //valor
            char simbolo = ' ';//♠', '♦', '♥', '♣
            bool bocaarriba;

            //bucle en el que se crea la parte superior de la carta tantas veces como numero se le haya pasado al metodo como parametro
            foreach(Carta c in cartas) 
                lineaCarta += "┌───────┐";
            lineaCarta += "\n"; 

            //bucle que nos crea la fila con el valor
            foreach(Carta c in cartas) 
            {
                bocaarriba=c.getBocaArriba();
                if(bocaarriba)
                {
                    v = c.getValor();
                    //Comprueba si es mayor a 2 digitos el valor 
                    if (v > 9)
                        lineaCarta += "│" + v + "     │";
                    else
                        lineaCarta += "│" + v + "      │";    
                }
                else
                    lineaCarta += "│║╔╗╔╗╔╗│";
            }
            lineaCarta += "\n";

            //bucle que nos crea las filas vacías (cartas.Count multiplicado por 4)
            for(int i=0;i<4;i++)
            {
                foreach(Carta c in cartas)
                {
                    bocaarriba=c.getBocaArriba();
                    if(bocaarriba)
                        lineaCarta += "│       │";
                    else
                        lineaCarta += "│║║║║║║║│";
                }  
                lineaCarta+="\n";  
            }
            
            //bucle que nos crea la fila con el palo 
            foreach(Carta c in cartas) 
            {
                bocaarriba=c.getBocaArriba();
                if(bocaarriba)
                {
                        //nos devuelve el palo de la carta para que en simbolo se almacene el del correspondiente palo
                    p = c.getPalo();
                    switch (p)
                    {
                        case 0:
                            simbolo = '♣';
                            break;
                        case 1:
                            simbolo = '♥';
                            break;
                        case 2:
                            simbolo = '♦';
                            break;
                        case 3:
                            simbolo = '♠';
                            break;
                    }
                    lineaCarta += "│      " + simbolo + "│";    
                }
                else
                    lineaCarta += "│╚╝╚╝╚╝║│";
            }
            lineaCarta += "\n";

            //bucle que nos crea la parte final de la carta
            foreach(Carta c in cartas) 
                lineaCarta += "└───────┘";
            lineaCarta += "\n";

            return lineaCarta;
        }
        public static String MuestraCartasHorizontalT(List<Carta> cartas)
        {
            //Declaración de variables de el método
            String lineaCarta = "";

            int p; //palo
            int v; //valor
            char simbolo = ' ';//♠', '♦', '♥', '♣
            bool bc;

            //Carta Monstruo Boca Arriba
            //bucle en el que se crea la parte superior de la carta tantas veces como numero se le haya pasado al metodo como parametro
            foreach(Carta c in cartas) 
                lineaCarta += "┌────────────┐";
            lineaCarta += "\n"; 

            //bucle que nos crea la fila con el valor
            foreach(Carta c in cartas) 
            {
                bc=c.getBocaArriba();
                if(bc)
                {
                    v = c.getValor();
                    //Comprueba si es mayor a 2 digitos el valor 
                    if (v > 9)
                        lineaCarta += "│" + v + "          │";
                    else
                        lineaCarta += "│" + v + "           │";    
                }
                else
                    lineaCarta += "││││││││││││││";
                
            }
            lineaCarta += "\n";

            //bucle que nos crea las filas vacías (cartas.Count multiplicado por 3)
            foreach(Carta c in cartas) 
            {  
                bc=c.getBocaArriba();
                if(bc)
                    lineaCarta += "│            │";
                else
                    lineaCarta += "│─────++─────│";
            }
            lineaCarta += "\n";

            //bucle que nos crea la fila con el palo 
            foreach(Carta c in cartas) 
            {
                bc=c.getBocaArriba();
                if(bc)
                {
                    //nos devuelve el palo de la carta para que en simbolo se almacene el del correspondiente palo
                    p = c.getPalo();
                    switch (p)
                    {
                        case 0:
                            simbolo = '♣';
                            break;
                        case 1:
                            simbolo = '♥';
                            break;
                        case 2:
                            simbolo = '♦';
                            break;
                        case 3:
                            simbolo = '♠';
                            break;
                    }
                    lineaCarta += "│           " + simbolo + "│";
                }
                else
                    lineaCarta += "││││││││││││││";
                
            }
            lineaCarta += "\n";

            //bucle que nos crea la parte final de la carta
            foreach(Carta c in cartas) 
                lineaCarta += "└────────────┘";
            lineaCarta += "\n";

            return lineaCarta;
        }


    } //fin de la clase Carta

    //clase Baraja con un list de cartas como atributo, un contador de cartas pedidas y un tamaño de baraja
    public class Baraja
    {
        //Declaración de los atributos de la clase baraja
        private List<Carta> baraja; //la sintaxis es List<Carta> baraja = new List<Carta>(tamaño);
        private int contPedidos = 0; //contador que nos sirve para evitar que el metodo PideCarta devuelva repetidas.
        private const int TAM = 52; //constante con el tamaño de la baraja

        //constructores
        public Baraja()
        {
            baraja = new List<Carta>(52);
        }

        //métodos
        //Introduce todas las cartas en una baraja de manera ordenada
        public void RellenaBaraja()
        {
            for (int p = 0; p < 4; p++)// Bucle de palos, 4 pasadas
            {
                for (int v = 1; v < 14; v++)// Bucle de cartas, 13 pasadas
                {
                    Carta c = new Carta((ePalo)p, (eValor)v); //creamos una carta 
                    baraja.Add(c); //la añadimos a la List baraja
                }
            }
        }

        //Usa el método MuestraCarta repetidas veces hasta quedarse sin cartas
        public void MuestraCartas()
        {
            for (int i = 0; i < (TAM-contPedidos); i++)
            {
                Console.WriteLine(baraja[i].MuestraCarta()+"\n");
            }
        }
        
        //Nos devuelve una carta y reduce el tamaño restante con cada ejecución
        public Carta PideCarta()
        {
            try
            {
                contPedidos++;
                return baraja[contPedidos];
            }
            catch(Exception)
            {
                Console.WriteLine("Fin de la baraja");
                return null;
            }
        }
        // Método que mezcla las cartas de una baraja. La baraja debe ser rellenada anteriormente
        public void MezclaBaraja()
        {
            try //try que controla que salte un mensaje de error cuandos se llame a este metodo sin haber rellenado la baraja
            {
                contPedidos = 0;
                var random = new Random();
                Carta aux;

                //bucle que mezcla las cartas aleatoriamente pasando por todas
                for (int i = 0; i < (TAM-contPedidos); i++)
                {
                    int a2 = random.Next(1, 52);

                    aux = baraja[i];
                    baraja[i] = baraja[a2];
                    baraja[a2] = aux;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("ERROR: No hay cartas en la baraja");
            }
            
        }
        
        //Devuelve el tamaño actual de la baraja (Tamaño total menos pedidas)
        public int getTamActual()
        {
            return TAM-contPedidos;
        }

    } //fin de la clase Baraja
}