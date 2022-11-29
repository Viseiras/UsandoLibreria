/*Crear una aplicación de cliente obtenga n cartas (1→ tam_baraja, valor por defecto 5) de una baraja previamente barajada. Si m cartas (2→ n, valor por defecto 3) son del mismo palo debe indicarlo en pantalla. Si no son del mismo palo, mostrar un mensaje indicando que vuelva a intentarlo con n cartas nuevas hasta que lo consiga. Si se acaba la baraja pierdes el juego.
*/

using System;
using LibreriaBaraja;
using System.IO;
using System.Collections.Generic;

namespace usandoBaraja
{
	class BarajaCliente
	{
		//private int TAM=5;
		private Carta carta;
		private List<Carta> barajaObjeto;

		public BarajaCliente(Baraja baraja,int TamBaraja)
		{
			barajaObjeto=new List<Carta>(TamBaraja);
			
			for(int i=0;i<TamBaraja;i++)
			{
				carta=baraja.PideCarta();
				barajaObjeto.Add(carta);
			}
		}



		public bool compruebaPalos(int fallos)
		{
			int contT=0, contD=0, contP=0, contC=0;
			ePalo palitroc;
			try
			{
				for(int i=0;i<=barajaObjeto.Count;i++)
				{
					palitroc=(ePalo)barajaObjeto[i].getPalo();
					switch(palitroc)
					{
						case ePalo.Treboles:
							contT++;	
							break;
						case ePalo.Corazones:
							contC++;	
							break;
						case ePalo.Diamantes:
							contD++;	
							break;
						case ePalo.Picas:
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
				return true;
			else
				return false;
		}
	}

	class ej2
	{
		static void Main(string[] args)
		{
			bool derrotado=true;
			Console.WriteLine("Introduce con las cartas que quieres jugar: ");
			bool numCv = int.TryParse(Console.ReadLine(),out int numC);
			if(numCv)
			{
				Console.WriteLine("Introduce con las cartas que se pueden repetir: ");
				bool fallosv = int.TryParse(Console.ReadLine(),out int fallos);
				if(fallosv)
				{
					Baraja baraja = new Baraja();
					baraja.RellenaBaraja();
					baraja.MezclaBaraja();
					while(derrotado)
					{
						BarajaCliente bc = new BarajaCliente(baraja,numC);
						foreach(Carta c in bc)
						{
							c.MuestraCarta();
						}
						if(bc.compruebaPalos(fallos))
						{
							Console.WriteLine("Has ganado");
							derrotado=false;
						}
						else
						{
							Console.WriteLine("\nQuedan "+baraja.getTamActual()+" cartas en la Baraja");
							Console.WriteLine("Has perdido");
							Console.ReadLine();
						}
					}		
				}
				else
					Console.WriteLine("Introduce un valor númerico: ");

			}
			else
				Console.WriteLine("Introduce un dato númerico");
		}
	}


}