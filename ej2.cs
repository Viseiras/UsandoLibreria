using System;
using LibreriaBaraja;
using System.IO;
using System.Collections.Generic;

namespace usandoBaraja
{
	class ej2
	{
		static void Main(string[] args)
		{
			//Declaración de contadores y variables del tipo int
			int contT=0, contD=0, contP=0, contC=0, numC, fallos;
			//Declaración de variables
			List<Carta> aux = new List<Carta>();
			Carta c;
			bool noGanado = true;

			//Creamos una baraja que rellenamos y Mezclamos
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			baraja.MezclaBaraja();

			//Le preguntamos al usuario cuantas cartas quiere usar por ronda (si no elige nada o introduce un valor no valido se introduce el 5)
			Console.WriteLine("Cuantas cartas vas a usar por ronda? Por defecto [5]");
			bool numeroCorrecto = int.TryParse(Console.ReadLine(),out numC);
			if(!numeroCorrecto || numC > 52)
			{
				Console.WriteLine("Valor incorrecto, introduciendo por defecto");
				numC=5;
			}

			//Le preguntamos cuantas deben coincidir para ganar (si no elige nada o introduce un valor no valido se introduce el 3)
			Console.WriteLine("Cuantas veces se deben repetir para ganar? Por defecto [3]");
			bool fallosCorrectos = int.TryParse(Console.ReadLine(),out fallos);
			if(!fallosCorrectos || fallos>numC)
			{
				if(fallos>numC)
					Console.WriteLine("No puede haber más coincidencias que cartas sobre la mesa");
				Console.WriteLine("Valor incorrecto, introduciendo por defecto");
				fallos=3;
			}

			//Comprueba que no ha ganado y en caso de haber perdido de nuevo continua , si gana sale del bucle
			while(noGanado)
			{
				//Llenamos la lista auxiliar
				for(int i=0;i<numC;i++)
				{
					c= baraja.PideCarta();
					aux.Add(c);
				}

				try
				{
					//Contamos cuantas cartas repetidas hay en cada palo
					foreach(Carta b in aux)
					{
						Console.WriteLine(b.MuestraCarta());
						switch(b.getPalo())
						{
					 		case 0:
								contT++;	
								break;
							case 1:
								contC++;	
								break;
							case 2:
								contD++;	
								break;
							case 3:
								contP++;	
								break;
						}
					}
				}
				//Recoge la excepción pero no hace nada con ella, simplemente que no explote el programa 
				catch(Exception)
				{

				}

				//Muestra las repeticiones de cada palo en las cartas mostradas
				Console.WriteLine("Han salido:\nTreboles: "+contT+"\nDiamantes: "+contD+"\nPicas: "+contP+"\nCorazones: "+contC);
				if(contT>=fallos||contD>=fallos||contP>=fallos||contC>=fallos)
				{
					Console.WriteLine("\nHas ganado\n\nSaliendo del juego...");
					noGanado=false;
				}	
				else
				{
					Console.WriteLine("\nHas perdido");
					Console.WriteLine("\nTe quedan "+baraja.getTamActual()+" cartas");
				}
				contT=0; contD=0; contP=0; contC=0; aux.Clear();
				Console.ReadLine();	
				//Comprobamos si se ha acabado la baraja
				if(baraja.getTamActual()<numC)
				{
					noGanado=false;
					Console.WriteLine("Te has quedado sin cartas");
				}
			}	
		}	
	}
}
