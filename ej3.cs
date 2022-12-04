using System;
using LibreriaBaraja;
using System.Collections.Generic;

/*Realizar una aplicación de cliente que simule un pequeño juego entre dos jugadores. Cada jugador sacará n cartas de la baraja (1→ tam_baraja, valor por defecto 10) y se irán comparando de una en una en turno rotatorio. Si son del mismo palo se comparan las cartas y se lleva un punto el que tenga la carta más alta. Si son de distinto palo gana el punto quien sacó la primera carta. El turno se pasa siempre al jugador que pierde. El turno inicial se sortea.*/

namespace usandoBaraja
{
	//Cremos una clase para facilitar el juego
	class Jogo
	{
		//Cremos la variable mano que nos eneseña las cartas que tiene el usuario en la mano en ese momento
		private List<Carta> mano;
		//Creamos la variable turno que se va pasando de un usuario a otro cada vez que uno gana
		private bool turno;

		//Constructor general de Jogo
		public Jogo(bool turno)
		{
			this.turno=turno;
			mano=new List<Carta>();
		}

		//Rellena la Mano con la carta que le introduzcamos 
		public void RellenaMano(Carta c)
		{
			mano.Add(c);
		}

		//Nos devuelve la mano del jugador
		public List<Carta> getMano()
		{
			return mano;
		}

		//Método que muestra la mano por pantalla
		public void MuestraMano()
		{
			Console.WriteLine(Carta.MuestraCartasHorizontal(mano));
		}

		//Método que nos devuelve el turno actual del jugador
		public bool getTurno()
		{
			return turno;
		}

		//Método que nos permite modificar a quien le toca el turno
		public void setTurno(bool turno)
		{
			this.turno=turno;
		}
	}

	class ej3
	{
		static void Main(string [] args)
		{
			//Creamos una baraja , la rellenamos y la mezclamos
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			baraja.MezclaBaraja();
			Carta c;

			//Inicializamos un random para que el turno incial sea aleatorio
			Random random = new Random();
			int turno=random.Next(0, 2);
			Jogo j1; Jogo j2;
			//Decimos quien empieza según los turnos (sería como quien saca primero)
			if(turno==0)
			{
				j1= new Jogo(true);
				j2= new Jogo(false);
			}
			else
			{
				j1= new Jogo(false);
				j2= new Jogo(true);
			}


			int numC;
			//Le pedimos al usuario un numero de cartas para usar en el programa, si el valor es incorrecto ponemos por defecto 10
			Console.WriteLine("Introduce el numero de cartas a usar en cada jugador: [10]");
		 	bool esvalido = int.TryParse(Console.ReadLine(), out numC);

		 	if(numC<0 || numC>25)
		 		numC=10;

	 		if(!esvalido)
		 	{
		 		numC=10;
		 	}

			//Rellena las manos de los dos jugadores
			Console.WriteLine("Rellenando Baraja...");
			try
			{
				for(int i=0; i<numC;i++)
				{
					c = baraja.PideCarta();
					j1.RellenaMano(c);
					c = baraja.PideCarta();
					j2.RellenaMano(c);
				}
			}
			//Atrapamos la excepción para que el programa no explote
			catch(Exception)
			{
				
			}
			Console.WriteLine("\n\nIniciamos el Juego:\n\n");

			//Vamos mostrando las dos manos y tiramos una moneda para indicar quien empieza (Tiene el turno inicial)
			Console.WriteLine("Mano del jugador 1");
			j1.MuestraMano();
			Console.WriteLine("Mano del jugador 2");
			j2.MuestraMano();
			Console.WriteLine("Vamos a tirar una moneda: ");
			Console.ReadLine();
			if(j1.getTurno())
				Console.WriteLine("Empieza el jugador 1");
			else
				Console.WriteLine("Empieza el jugador 2");

			int contJ1=0, contJ2=0;
			//Va calculando que carta es mayor a la otra y quien gana
			for(int i=0; i<numC; i++)
			{
				List<Carta> muestra=new List<Carta>(); 
				muestra.Add(j1.getMano()[i]);
				muestra.Add(j2.getMano()[i]);
				Console.WriteLine("Jugador 1 Jogador 2");
				//Muestra las cartas de forma horizontal
				Console.WriteLine(Carta.MuestraCartasHorizontal(muestra));

				//Comprueba si el palo de las dos cartas es el mismo en ese caso
				if(j1.getMano()[i].getPalo()==j2.getMano()[i].getPalo())
				{
					//Comprueba cual es el valor mayor si es el del jugador 1 gana 
					if(j1.getMano()[i].getValor()>j2.getMano()[i].getValor())
					{
						Console.WriteLine("Ha ganado el jugador 1");
						j1.setTurno(false);
						j2.setTurno(true);
						contJ1++;
					}
					//Si no ha sido el jugador 1 el que tiene el valor mayor gana el jugador 2
					else
					{
						Console.WriteLine("Ha ganado el jugador 2");
						j2.setTurno(false);
						j1.setTurno(true);
						contJ2++;
					}
				}
				//Si los palos son diferentes gana el que tenga el turno en ese momento
				else
				{
					if(j1.getTurno())
					{
						Console.WriteLine("Ha ganado el jugador 1");
						//Cambiamos el turno una vez ha ganado el jugador 1
						j1.setTurno(false);
						j2.setTurno(true);
						contJ1++;
					}
					else
					{
						Console.WriteLine("Ha ganado el jugador 2");
						//Cambiamos el turno una vez ha ganado el jugador 1
						j2.setTurno(false);
						j1.setTurno(true);
						contJ2++;
					}
				}
				Console.WriteLine("\t"+contJ1+"-"+contJ2);
				Console.ReadLine();
			}
			//Mostramos por pantalla los puntos
			Console.WriteLine("El Jugador 1 ha tenido "+contJ1+" puntos");
			Console.WriteLine("El Jogador 2 ha tenido "+contJ2+" puntos");
			//Mostramos el ganador o el empate (casi siempre será empate)
			if(contJ1>contJ2)
				Console.WriteLine("HA GANADO EL JUGADOR 1");
			else if(contJ2>contJ1)
				Console.WriteLine("JOGADOR 2 GANHOU");
			else
				Console.WriteLine("EMPATE");
		}
	}
}