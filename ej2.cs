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
			//Declaración de contadores
			int contT=0, contD=0, contP=0, contC=0;
			//Declaración de variables
			List<Carta> aux = new List<Carta>();
			Carta c;
			bool noGanado = true;

			//
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			baraja.MezclaBaraja();

			Console.WriteLine("Cuantas cartas vas a usar por ronda? Por defecto [5]");
			bool numeroCorrecto = int.TryParse(Console.ReadLine(),out int numC);
			if(!numeroCorrecto)
			{
				Console.WriteLine("Valor incorrecto, introduciendo por defecto");
				numC=5;
			}

			Console.WriteLine("Cuantas veces se deben repetir para ganar? Por defecto [3]");
			bool fallosCorrectos = int.TryParse(Console.ReadLine(),out int fallos);
			if(!fallosCorrectos)
			{
				Console.WriteLine("Valor incorrecto, introduciendo por defecto");
				fallos=3;
			}

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
				catch(Exception)
				{

				}
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
				if(baraja.getTamActual()<numC)
				{
					noGanado=false;
					Console.WriteLine("Te has quedado sin cartas");
				}
			}	
		}	
	}
}
