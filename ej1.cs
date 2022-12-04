/*Crear una aplicación de cliente que haga uso del módulo de librería que baraje la baraja y vaya sacando una a una las cartas mostrando qué carta se ha sacado ( una cadena en modo texto que lo indique)(método siguienteCarta o algo así que devuelve un objeto de la clase carta)*/


using LibreriaBaraja;
using System;

namespace usandoBaraja
{
	//Iniciamos el programa
	class ej1
	{
		static void Main(string [] args)
		{
			//Creamos una baraja nueva donde le introducimos todas las cartas
			Baraja b = new Baraja();
			b.RellenaBaraja();
			Console.WriteLine("Baraja Rellenada");

			//Mezclamos las cartas en una disposición aleatoria
			b.MezclaBaraja();
			Console.WriteLine("Baraja Mezclada");

			string s="";
			//Pedimos una carta al principio ya que queremos mostrar al menos una siempre antes de preguntar
			Carta c=b.PideCarta();

			while(s!="n")
			{
				try
				{
					Console.WriteLine(c.MuestraCarta());
						
					//Vamos mostrando la baraja una a una a menos que nos diga que No ('N')
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