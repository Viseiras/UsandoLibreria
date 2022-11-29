/*Crear una aplicación de cliente que haga uso del módulo de librería que baraje la baraja y vaya sacando una a una las cartas mostrando qué carta se ha sacado ( una cadena en modo texto que lo indique)(método siguienteCarta o algo así que devuelve un objeto de la clase carta)*/


using LibreriaBaraja;
using System;

namespace usandoBaraja
{
	class ej1
	{
		static void Main(string [] args)
		{
			Baraja b = new Baraja();
			b.RellenaBaraja();
			Console.WriteLine("Baraja Rellenada");

			b.MezclaBaraja();
			Console.WriteLine("Baraja Mezclada");

			string s="";
			Carta c=b.PideCarta();

			while(s!="n")
			{
				try
				{
					Console.WriteLine(c.MuestraCarta());
						
					Console.WriteLine("Quieres seguir viendo la baraja? s/n");
					s=Console.ReadLine();
					c=b.PideCarta();
				}
				catch(Exception)
				{
					Console.WriteLine("Introduce n para acabar:");
					s=Console.ReadLine();
				}
			}
		}
	}
}