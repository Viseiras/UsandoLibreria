using System;
using LibreriaBaraja;
using System.Collections.Generic;

/*Realizar una aplicación de cliente que simule un pequeño juego entre dos jugadores. Cada jugador sacará n cartas de la baraja (1→ tam_baraja, valor por defecto 10) y se irán comparando de una en una en turno rotatorio. Si son del mismo palo se comparan las cartas y se lleva un punto el que tenga la carta más alta. Si son de distinto palo gana el punto quien sacó la primera carta. El turno se pasa siempre al jugador que pierde. El turno inicial se sortea.*/

namespace usandoBaraja
{
	class Jogo
	{
		private List<Carta> mano;
		private bool turno;

		public Jogo(bool turno)
		{
			this.turno=turno;
			mano=new List<Carta>();
		}

		public void RellenaMano(Carta c)
		{
			mano.Add(c);
		}

		public List<Carta> getMano()
		{
			return mano;
		}

		public void MuestraMano()
		{
			Console.WriteLine(Carta.MuestraCartasHorizontal(mano));
		}
		public bool getTurno()
		{
			return turno;
		}
		public void setTurno(bool turno)
		{
			this.turno=turno;
		}
	}

	class ej3
	{
		static void Main(string [] args)
		{
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			baraja.MezclaBaraja();
			Carta c;

			Random random = new Random();
			int turno=random.Next(0, 2);
			Jogo j1; Jogo j2;
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
			Console.WriteLine("Introduce el numero de cartas a usar en cada jugador: [10]");
		 	bool esvalido = int.TryParse(Console.ReadLine(), out numC);

		 	if(numC<0 || numC>25)
		 		numC=10;

	 		if(!esvalido)
		 	{
		 		numC=10;
		 	}

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
			catch(Exception)
			{
				
			}
			Console.WriteLine("\n\nIniciamos el Juego:\n\n");

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
			for(int i=0; i<numC; i++)
			{
				List<Carta> muestra=new List<Carta>(); 
				muestra.Add(j1.getMano()[i]);
				muestra.Add(j2.getMano()[i]);
				Console.WriteLine("Jugador 1 Jogador 2");
				Console.WriteLine(Carta.MuestraCartasHorizontal(muestra));

				if(j1.getMano()[i].getPalo()==j2.getMano()[i].getPalo())
				{
					if(j1.getMano()[i].getValor()>j2.getMano()[i].getValor())
					{
						Console.WriteLine("Ha ganado el jugador 1");
						j1.setTurno(false);
						j2.setTurno(true);
						contJ1++;
					}
					else
					{
						Console.WriteLine("Ha ganado el jugador 2");
						j2.setTurno(false);
						j1.setTurno(true);
						contJ2++;
					}
				}
				else
				{
					if(j1.getTurno())
					{
						Console.WriteLine("Ha ganado el jugador 1");
						j1.setTurno(false);
						j2.setTurno(true);
						contJ1++;
					}
					else
					{
						Console.WriteLine("Ha ganado el jugador 2");
						j2.setTurno(false);
						j1.setTurno(true);
						contJ2++;
					}
				}
				Console.WriteLine("\t"+contJ1+"-"+contJ2);
				Console.ReadLine();
			}
			Console.WriteLine("El Jugador 1 ha tenido "+contJ1+" puntos");
			Console.WriteLine("El Jogador 2 ha tenido "+contJ2+" puntos");
			if(contJ1>contJ2)
				Console.WriteLine("HA GANADO EL JUGADOR 1");
			else if(contJ2>contJ1)
				Console.WriteLine("JOGADOR 2 GANHOU");
			else
				Console.WriteLine("EMPATE");
		}
	}
}