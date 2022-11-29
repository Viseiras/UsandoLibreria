using LibreriaBaraja;
using System;
using System.Collections.Generic;

namespace usandoBaraja
{
	class ej4 
	{
		static void Main(string[] args)
		{
			List<Carta> monstruos=new List<Carta>();
			Carta c;
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			for(int i=0; i<4;i++)
			{
				c = baraja.PideCarta();
				Console.WriteLine(baraja.getTamActual());
				Console.WriteLine(monstruos.Count);
				monstruos.Add(c);
				Console.WriteLine(monstruos.Count);
				//Console.WriteLine(Carta.MuestraCartasHorizontal(monstruos));
			}	


							
		}
	}
}