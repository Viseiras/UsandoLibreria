using LibreriaBaraja;
using System;
using System.Collections.Generic;
using System.Threading;

namespace usandoBaraja
{
	//Inicio de la clase DueloDeMonstruos
	class DueloDeMonstruos
	{
		//J-M (Jugador-Máquina) y N-E(Normal-Especial)
		private static List<Carta> monstruosM_N;
		private static List<Carta> monstruosJ_N;
		private static List<Carta> monstruosM_E;
		private static List<Carta> monstruosJ_E;
		private static List<Carta> monstruosM_N_Visibles;
		private static List<Carta> monstruosJ_N_Visibles;
		private static List<Carta> monstruosM_E_Visibles;
		private static List<Carta> monstruosJ_E_Visibles;
		private static int vidaM;
		private static int vidaJ;
		private static int turnospasados=0;

		//Aviso de poner pantalla completa y printeo del BannerInicio
		public static void Principio()
		{
			Console.Clear();
			Console.WriteLine("\n\n\n\n\n\n\n\t\t\t\tPOR FAVOR PON PANTALLA COMPLETA ANTES DE EMPEZAR");
			Console.ReadLine();

			BannerIncio();
		}

		//Escibe iniciando juego y pone 3 puntos después de confirmar que entramos al juego
		public static void InicioJuego()
		{
			Console.Clear();
			Console.Write("Iniciando Juego");
			for(int i=0;i<3;i++)
			{
				Thread.Sleep(800);
				Console.Write(".");
			}
			Console.Clear();
		}

		//Introduce toda la vida y cartas que se van a emplear por los jugadores (Máquina y Jugador)
		public static void GeneraMonstruos(List<Carta> monstruosmaquina, List<Carta> monstruosjugador)
		{
			//Listas que recogen todas las cartas
			monstruosM_N=new List<Carta>();
			monstruosJ_N=new List<Carta>();
			monstruosM_E=new List<Carta>();
			monstruosJ_E=new List<Carta>();
			//Listas que recogen las cartas que se van a visualizar en la arena en ese momento
			monstruosM_N_Visibles=new List<Carta>(3);
			monstruosJ_N_Visibles=new List<Carta>(3);
			monstruosM_E_Visibles=new List<Carta>(3);
			monstruosJ_E_Visibles=new List<Carta>(3);

			vidaM=5000;
			vidaJ=5000;

			//Introduce todas las cartas de la máquina en las listas correspondientes, separando por Especial y Normal
			foreach (Carta c in monstruosmaquina)
			{
				c.setBocaArriba(false);
				//Comprueba si es Especial o Normal
				if(c.getValor()>8)
					monstruosM_E.Add(c);
				else
					monstruosM_N.Add(c);
			}
			//Introduce todas las cartas de la jugador en las listas correspondientes, separando por Especial y Normal
			foreach (Carta c in monstruosjugador)
			{
				//Comprueba si es Especial o Normal
				if(c.getValor()>8)
					monstruosJ_E.Add(c);
				else
					monstruosJ_N.Add(c);
			}

			//Rellenamos las cartas visibles
			for(int i=0;i<3;i++)
			{
				monstruosJ_E_Visibles.Add(monstruosJ_E[monstruosJ_E.Count-1]);
				monstruosJ_E.RemoveAt(monstruosJ_E.Count-1);
				monstruosJ_N_Visibles.Add(monstruosJ_N[monstruosJ_N.Count-1]);
				monstruosJ_N.RemoveAt(monstruosJ_N.Count-1);
				monstruosM_E_Visibles.Add(monstruosM_E[monstruosM_E.Count-1]);
				monstruosM_E.RemoveAt(monstruosM_E.Count-1);
				monstruosM_N_Visibles.Add(monstruosM_N[monstruosM_N.Count-1]);
				monstruosM_N.RemoveAt(monstruosM_N.Count-1);
			}
		}

		//Elegimos el menu de inicio al prinicipio del programa por si queremos ver las reglas en lugar de empezar a jugar
		public static void Menu()
		{
			int menu=-1;
			bool esnum= false, noquieresalir=true;
			while(noquieresalir)
			{
				while(esnum==false)
				{
					Console.WriteLine("\n\n\tELIGE UNA OPCIÓN:\n\n\n\t\t1.INICIO\n\t\t0.INSTRUCCIONES");	
					esnum= int.TryParse(Console.ReadLine(),out menu);
					if(menu>1 || menu<0)
					{
						Console.WriteLine("Valor incorrecto, vuelve a intentar");
						esnum=false;
					}
				}
				switch(menu)
				{
					//Inicia el programa
					case 1:
						InicioJuego();
						noquieresalir=false;
					break;
					
					//Muestra las instrucciones
					case 0:
						Instrucciones();
						esnum=false;
						
					break;
				}	
			}
		}

		//Método para reducir unas cuantas líneas repetidas que simplemente comprueba si el char introducido es un 'S'
		public static bool SioNo(char eleccion)
		{
			bool esunsi=false;
			if(Char.IsUpper(eleccion)==false)
			{
				eleccion=Char.ToUpper(eleccion);
			}
			if(eleccion=='S')
				esunsi= true;
			return esunsi;
		}


		//Menu que muestra y manda las ordenes de las acciones que realizará el jugador
		public static void MenuJugador()
		{
			int menu=-1;
			bool esnum= false, noelige=true,haentrado=false;
			//Comprueba si el jugador a elegido alguna acción (O si esta accion consume un turno)
			while(noelige)
			{
				char eleccion='N';
				//Nos permite volver al menu principal o seguir en la pestaña actual eligiendo
				if(haentrado)
				{
					Console.WriteLine("¿Quieres volver al tablero principal[S]?(S/N)");
					bool esunchar;
					esunchar=char.TryParse(Console.ReadLine(),out eleccion);
				
					
					if(esunchar==false)
					{
						if(eleccion!='N')
							eleccion='S';
					}
					
				}

				//Menu es la opción 99 para prevenir errores y obviamente no puede ser triggerado a proposito por el usuario
				if(SioNo(eleccion))
				{
					menu=99;
				}

				if(menu!=99)
				{
					//Nos muestra el menu principal donde realizaremos las acciones
					while(esnum==false)
					{
						Console.WriteLine("\n\n\t¿QUÉ QUIERES HACER?:\n\n\n\t\t1.ATACAR (gasta el turno) \n\t\t2.USAR HABILIDAD (según que carta puede gastar el turno)\n\t\t3.VER MIS CARTAS\n\t\t4.CONSULTAR LAS INSTRUCCIONES\n\t\t0.PASAR TURNO");
						esnum= int.TryParse(Console.ReadLine(),out menu);
						if(menu>4 || menu<0)
							esnum=false;

						if(esnum==false)
							Juego();
						
					}	
					haentrado=true;
				}
				noelige=false;
				//Propio Menu que nos permite interactuar con el programa 
				switch(menu)
				{
					case 1:
						//Atacar();
						Ataque('J');
					break;
						
					case 2:
						noelige=Habilidad('J');
						menu=99;
					break;
					case 3:
						//Ver cartas
						CartasReserva('J','X');
						noelige=true;
					break;
					case 4:
						//Instrucciones
						Instrucciones();
						noelige=true;
					break;

					case 99:
						//Vuelve al tablero principal
						Juego();
						haentrado=false;
					break;

					case 0:
						//Pasa turno
					break;
				}
			}
			//Esta variable está Work In Progress, servirá para que los estados temporales tengan una duración establecida y funcionaría como un reloj interno(posibilidad de pasarlo a long o Int64 para prevenir errores)
			turnospasados++;
		}

		//Menu que elige por la máquina y que te lo muestra por pantalla para hacerlo más interactivo y parezca que estás jugando contra alguien de verdad (Muy parecido al menu del jugador pero sin posibilidad de inputs)
		public static void MenuMaquina()
		{
			int menu=-1;
			bool noelige=true;

			while(noelige)
			{

				Console.WriteLine("\n\n\t¿QUÉ QUIERES HACER?:\n\n\n\t\t1.ATACAR (gasta el turno) \n\t\t2.USAR HABILIDAD (según que carta puede gastar el turno)\n\t\t3.VER MIS CARTAS\n\t\t4.CONSULTAR LAS INSTRUCCIONES\n\t\t0.PASAR TURNO");
				//Apunte del funcionamiento del Random, el valor menor (izquierda) que generará puede ser mayor o igual a este, mientras que el valor mayor (derecha) solo puede ser menor, así que en este caso que queremos que sea entre 1 y 3 debemos indicar que es Next(1,4)
				Random random= new Random();
				menu=random.Next(1,4);
				Thread.Sleep(1000);
				Console.WriteLine(menu);
				Thread.Sleep(3000);

				noelige=false;
				switch(menu)
				{
					case 1:
						//Atacar();
						Ataque('M');
					break;
					case 2:
						//Habilidad();
					break;
					case 3:
						//Ver cartas
						CartasReserva('M','X');
						noelige=true;
					break;
					case 4:
						//Instrucciones
						Instrucciones();
						noelige=true;
					break;
					case 0:
						//Pasa turno
					break;
				}
			}
		}

		//Muestra todas las cartas que no tenemos en el campo por si acaso queremos verlas para hacer alguna estrategia
		public static void CartasReserva(char Personaje, char Modo)
		{
			int pasada=0;
			//Comprueba que personaje a realizado la acción en este caso el jugador
			if(Personaje=='J')
			{
				Console.Clear();
				Console.WriteLine("EN JUEGO:\n");
				JugadorEnJuego('X');
				Console.WriteLine("EN EL DECK:\n");
				//Printea las cartas de Monstruos Normales que tenemos en nuestro Deck
				Console.WriteLine("-CARTAS DE MONSTRUO-\n"+Carta.MuestraCartasHorizontal(monstruosJ_N));
				//Modo 'H' se refiere a si es activado mediante una habilidad y el modo U es solo para la habilidad de la carta 1 (puedes cambiar una carta 1 por otra carta de Monstruo)
				if(Modo=='H' || Modo=='U')
				{
					for(int i=0;i<monstruosJ_N.Count;i++)
					{
						if(i<10)
							Console.Write("  Carta "+(i+1));
						else
							Console.Write(" Carta "+(i+1));
						pasada=i;
					}
				}	
				//Aquí se le suma 1 porque empieza en 0 y queremos que se cuente bien el numeros
				pasada++;
				//Printea las cartas de Monstruos Especiales que tenemos en nuestro Deck
				Console.WriteLine("\n-CARTAS ESPECIALES-\n"+Carta.MuestraCartasHorizontal(monstruosJ_E));
				if(Modo=='H')
				{
					for(int i=0;i<monstruosJ_E.Count;i++)
						Console.Write(" Carta "+((i+1)+pasada));
				}
					
			}
			//Mismo que arriba pero sin inputs de verdad
			if(Personaje=='M')
			{
				Console.Clear();
				Console.WriteLine("EN JUEGO:\n");
				MaquinaEnJuego('X');
				Console.WriteLine("EN EL DECK:\n");
				Console.WriteLine("-CARTAS DE MONSTRUO-\n"+Carta.MuestraCartasHorizontal(monstruosM_N));
				if(Modo=='H' || Modo=='U')
				{
					for(int i=0;i<monstruosM_N.Count;i++)
					{
						if(i<10)
							Console.Write("  Carta "+(i+1));
						else
							Console.Write(" Carta "+(i+1));
						pasada=i;
					}
				}
				pasada++;
				Console.WriteLine("-CARTAS ESPECIALES-\n"+Carta.MuestraCartasHorizontal(monstruosM_E));
				if(Modo=='H')
				{
					for(int i=0;i<monstruosM_E.Count;i++)
						Console.Write(" Carta "+((i+1)+pasada));
				}
				Thread.Sleep(5000);
			}
		}
		
		//Muestra la mano de la máquina en Juego y el numero de la carta en caso de que queramos actacar o usar alguna habilidad
		public static void MaquinaEnJuego(char Modo)
		{
			Console.Clear();
			BannerMaquina();
			//Muestra las cartas en caso de usar una Habilidad 
			if(Modo=='H')
				Console.WriteLine(" Carta 4  Carta 5  Carta 6\n");
			Console.WriteLine("\nVida:"+vidaM);
			Console.WriteLine("\n\n"+Carta.MuestraCartasHorizontal(monstruosM_E_Visibles));
			Console.WriteLine("\n"+Carta.MuestraCartasHorizontalT(monstruosM_N_Visibles));
			//Muestra las cartas en caso de usar una habilidad o atacar
			if(Modo=='A' || Modo=='H')
				Console.WriteLine("  Carta 1 \t Carta 2\tCarta 3");
		}

		//Nos deja visualizar los monstruos adyacentes y cuales podemos atacar
		public static void MaquinaSiendoAtacada(int atacante)
		{
			Console.Clear();
			BannerMaquina();
			Console.WriteLine("\nVida:"+vidaM);
			//Hacemos una lista secundaria para que solo nos muestre las cartas adyacentes es decir en la casilla de enfrene o las de los lados en 1
			List<Carta> monstruosM_N_Visibles_PA=new List<Carta>();
			for(int i=0;i<3;i++)
			{
				if(i==atacante-1 || i==atacante || i==atacante+1)
					monstruosM_N_Visibles_PA.Add(monstruosM_N_Visibles[i]);
			}
			
			Console.WriteLine("\n"+Carta.MuestraCartasHorizontalT(monstruosM_N_Visibles_PA));
		}

		//TODO este método y el de MaquinaEnJuego pueden ser resumidos en uno introduciendo la variable char Personaje como hacemos en otros métodos
		//Muestra la mano del jugador en Juego y el numero de la carta en caso de que queramos actacar o usar alguna habilidad
		public static void JugadorEnJuego(char Modo)
		{
			//TODO Fixear que la vida no baja cambiando la declaración de esta y la modificación a un método ajeno
			if(Modo=='A' || Modo=='H')
				Console.WriteLine("  Carta 1 \t Carta 2\tCarta 3");			
			Console.WriteLine(Carta.MuestraCartasHorizontalT(monstruosJ_N_Visibles)+"\n");
			Console.WriteLine(Carta.MuestraCartasHorizontal(monstruosJ_E_Visibles)+"\n\n");
			if(Modo=='H')
				Console.WriteLine(" Carta 4  Carta 5  Carta 6\n");
			Console.WriteLine("Vida:"+vidaJ+"\n");

		}

		
		//TODO este método y el de MaquinaSiendoAtacada pueden ser resumidos en uno introduciendo la variable char Personaje como hacemos en otros métodos
		//Nos muestra los monstruos adyacentes al del rival y a cual de los nuestros puede atacar
		public static void JugadorSiendoAtacado(int atacante)
		{
			Console.Clear();
			BannerJugador();
			Console.WriteLine("\nVida:"+vidaJ);
			//Hacemos una lista secundaria para que solo nos muestre las cartas adyacentes es decir en la casilla de enfrene o las de los lados en 1
			List<Carta> monstruosJ_N_Visibles_PA=new List<Carta>();
			//Añade a una Lista las cartas adyacentes a nuestro atacante (Es decir a la carta con la que estamos atacando)
			for(int i=0;i<3;i++)
			{
				if(i==atacante-1 || i==atacante || i==atacante+1)
					monstruosJ_N_Visibles_PA.Add(monstruosJ_N_Visibles[i]);
			}
			
			Console.WriteLine("\n"+Carta.MuestraCartasHorizontalT(monstruosJ_N_Visibles_PA));
			
		}

		//El método que engloba la estructura principal del juego donde vemos el tablero principal y comprobamos si alguno de los dos ha ganado
		public static void Juego()
		{
			bool haganado=false;

			
			while(haganado==false)
			{
				MaquinaEnJuego('X');
				JugadorEnJuego('X');
				BannerJugador();
				
				
				
				if(vidaM<=0)
				{
					Victoria('J');
					haganado=true;
				}
				if(vidaJ<=0)
				{
					Victoria('M');	
					haganado=true;
				}
				if(haganado==false)
					MenuJugador(); 
				if(haganado==false)
					MenuMaquina();
				
			}
		}

		//Con este método elegimos la carta aliada que atacará a los enemigos, se distingue por Máquina 'M' y Jugador 'J'
		public static Tuple<int,int,int> EligeAtacante(char Personaje)
		{
			bool esunmonstruo=false;
			int seleccion;
			Tuple<int,int,int> stats=new Tuple<int,int,int>(0,0,0);
			if(Personaje=='J')
			{
				while(esunmonstruo==false)
				{
					Console.Clear();
					JugadorEnJuego('A');
					
					Console.WriteLine("Selecciona la carta que quieres usar introduciendo su numero:");
					esunmonstruo=int.TryParse(Console.ReadLine(),out seleccion);
					if(seleccion<0 || seleccion>3)
					{
						Console.WriteLine("Ese Monstruo no se encuentra en la arena");
						esunmonstruo=false;
						Console.ReadLine();
					}
					if(esunmonstruo)
						stats=DameATKyDEF(seleccion,'J','X');	
				}	
			}

			if(Personaje=='M')
			{
				Console.Clear();
				MaquinaEnJuego('A');
				Console.WriteLine("Selecciona la carta que quieres usar introduciendo su numero:");
				Random random= new Random();
				seleccion=random.Next(1,4);
				Thread.Sleep(1000);
				Console.WriteLine(seleccion);
				Thread.Sleep(2000);
				stats=DameATKyDEF(seleccion,'M','X');
			}
			
			return stats;
		}

		//En este método elegimos el objetivo de nuestra carta con las cartas adyacentes
		public static Tuple<int,int,int> EligeObjetivo(int atacante, char Personaje)
		{
			bool esunmonstruo=false;
			int seleccion=0;
			Tuple<int,int,int> stats=new Tuple<int,int,int>(0,0,0);
			
			//TODO Esto puede ser un poco confuso y sería más apropiado cambiar el personaje a J para demostrar que el que efectua la acción es el Jugador (Ahora mismo la lógica es que el objetivo es la máquina)
			if(Personaje=='M')
			{
				while(esunmonstruo==false)
				{
					Console.Clear();
					MaquinaSiendoAtacada(atacante);
					string cartasdisponibles="";
					if(atacante==0)
						cartasdisponibles="  Carta 1 \t Carta 2";
					if(atacante==1)
						cartasdisponibles="  Carta 1 \t Carta 2\tCarta 3";
					if(atacante==2)
						cartasdisponibles="  Carta 2\tCarta 3";

					Console.WriteLine(cartasdisponibles);

					Console.WriteLine("\nSelecciona la carta que quieres atacar introduciendo su numero:");
					esunmonstruo=int.TryParse(Console.ReadLine(),out seleccion);
					//Comprobamos si la carta de ataque es la de la izquierda
					if(atacante==0)
					{
						//Comprobamos que el atacante sea una carta posible
						if(seleccion<0 || seleccion>3)
						{
							Console.WriteLine("Ese Monstruo no se encuentra en la arena");
							esunmonstruo=false;
							Console.ReadLine();
						}
						//Y comprobamos si se encuentra en rango
						else if(seleccion==3)
						{
							Console.WriteLine("Ese Monstruo no se encuentra fuera del alcance del tuyo");
							esunmonstruo=false;
							Console.ReadLine();
						}
					}
					//Comprobamos si la carta de ataque es la del centro
					if(atacante==1)
					{
						if(seleccion<0 || seleccion>3)
						{
							Console.WriteLine("Ese Monstruo no se encuentra en la arena");
							esunmonstruo=false;
							Console.ReadLine();
						}
					}
					//Comprobamos si la carta de ataque es la de la derecha
					if(atacante==2)
					{
						if(seleccion<0 || seleccion>3)
						{
							Console.WriteLine("Ese Monstruo no se encuentra en la arena");
							esunmonstruo=false;
							Console.ReadLine();
						}
						else if(seleccion==1)
						{
							Console.WriteLine("Ese Monstruo no se encuentra fuera del alcance del tuyo");
							esunmonstruo=false;
							Console.ReadLine();
						}
					}
					
					if(esunmonstruo)
						stats=DameATKyDEF(seleccion,'M','X');	
				}	
			}
			//Método adaptado para la máquina (mucho más simple y sin comprobaciones)
			if(Personaje=='J')
			{
				Console.Clear();
				JugadorSiendoAtacado(atacante);
				string cartasdisponibles="";
				Random random = new Random();
				if(atacante==0)
				{
					cartasdisponibles="  Carta 1 \t Carta 2";
					seleccion = random.Next(1,3);
				}
				if(atacante==1)
				{
					cartasdisponibles="  Carta 1 \t Carta 2\tCarta 3";
					seleccion = random.Next(1,4);
				}
				if(atacante==2)
				{
					cartasdisponibles="  Carta 2\tCarta 3";
					seleccion = random.Next(2,4);
				}

				Console.WriteLine(cartasdisponibles);
				Console.WriteLine("\nSelecciona la carta que quieres atacar introduciendo su numero:");
				Thread.Sleep(1000);
				Console.WriteLine(seleccion);
				Thread.Sleep(2000);

				stats=DameATKyDEF(seleccion,'J','X');	
			}
			
			return stats;
		}

		//TODO hay habilidades sin introducir por ejemplo todas las cartas especiales
		//Menu de habilidades donde ejecutamos la habilidad de la carta (es como un Nexo que nos va redirigeindo a las habilidades)
		public static bool Habilidad(char Personaje)
		{
			bool nohapasadoturno=true;
			if(Personaje=='J')
			{
				nohapasadoturno=EligeCarta('J');
				EligeReceptor();
			}
			if(Personaje=='M')
			{
				nohapasadoturno=EligeCarta('M');
				EligeReceptor();
			}
			return nohapasadoturno;
		}

		//Este método se encuentra incompleto como el de Habilidad ya que falta introducir cartas de efecto pasivo
		public static bool EligeCarta(char Personaje)
		{
			bool esunmonstruo=false, nohapasadoturno=true;
			int seleccion;
			Tuple<int,int,int> stats=new Tuple<int,int,int>(0,0,0);
			if(Personaje=='J')
			{
				while(esunmonstruo==false)
				{
					Console.Clear();
					JugadorEnJuego('H');
					
					Console.WriteLine("Selecciona la carta que quieres usar introduciendo su numero:");
					esunmonstruo=int.TryParse(Console.ReadLine(),out seleccion);
					if(seleccion<0 || seleccion>3)
					{
						Console.WriteLine("Ese Monstruo no se encuentra en la arena");
						esunmonstruo=false;
						Console.ReadLine();
					}
					if(esunmonstruo)
						nohapasadoturno=DameHabilidad(seleccion,'J');	
				}	
			}

			if(Personaje=='M')
			{
				Console.Clear();
				MaquinaEnJuego('H');
				Console.WriteLine("Selecciona la carta que quieres usar introduciendo su numero:");
				Random random= new Random();
				seleccion=random.Next(1,4);
				Thread.Sleep(1000);
				Console.WriteLine(seleccion);
				Thread.Sleep(2000);
				nohapasadoturno=DameHabilidad(seleccion,'M');	
			}
			return nohapasadoturno;
		}

		//Nos devuelve si ha pasado turno (por eso es de tipo bool) y además nos da / ejecuta la habilidad de la carta usada
		public static bool DameHabilidad(int seleccion, char Personaje)
		{
			Random random = new Random();
			int seleccionH=0;
			bool mesirve=false,nohapasadoturno=true;
			Carta c=new Carta(ePalo.Diamantes,eValor.As);
			if(Personaje=='J')
			{
				if(seleccion>0&&seleccion<4)
					c= monstruosJ_N_Visibles[seleccion-1];
				if(seleccion>3&&seleccion<7)
					c= monstruosJ_E_Visibles[seleccion-1];
			}
			if(Personaje=='M')
			{
				if(seleccion>0&&seleccion<4)
					c= monstruosM_N_Visibles[seleccion-1];
				if(seleccion>3&&seleccion<7)
					c= monstruosM_E_Visibles[seleccion-1];
			}
			
			int skill=c.getValor();

			//Switch que nos redirige a las habilidades de las cartas
			switch(skill)
			{

				//Sustituye la carta Nº1 por cualquiera en el DECK que se del tipo Monstruo Normal
				case 1:	
					if(Personaje=='J')
					{
						while(mesirve==false)
						{
							CartasReserva('J','U');
							Console.WriteLine("¿Por cual Carta quieres sustituirla?");
							mesirve=int.TryParse(Console.ReadLine(),out seleccionH);
							if(seleccionH>monstruosJ_N.Count || seleccionH<0)
							{
								mesirve=false;
								Console.WriteLine("Ese no es un monstruo que puedas cambiar");
								Console.ReadLine();
							}	
						}
						monstruosJ_N_Visibles[seleccion-1]=monstruosJ_N[seleccionH-1];
						monstruosJ_N.RemoveAt(seleccionH-1);
					}
					if(Personaje=='M')
					{
							CartasReserva('M','U');
							Console.WriteLine("¿Por cual Carta quieres sustituirla?");
							seleccionH=random.Next(1,monstruosM_N.Count);
							Thread.Sleep(1000);
							Console.WriteLine(seleccionH);
							Thread.Sleep(2000);
							monstruosM_N_Visibles[seleccion-1]=monstruosM_N[seleccionH-1];
							monstruosM_N.RemoveAt(seleccionH-1);
					}
					nohapasadoturno=true;
				break;
				//Realiza un ataque con la carta Nº2 que causa 1200 de daño 
				case 2:
					Tuple<int,int,int> statsA= new Tuple<int,int,int>(0,0,0), statsO= new Tuple<int,int,int>(0,0,0);
					int ATKA, DEFO, ganador=0;
					if(Personaje=='J')
					{						
						statsA=DameATKyDEF(seleccion,'J','X');
						statsO=EligeObjetivo(seleccion-1,'M');	
						ATKA=statsA.Item1*4;
						DEFO=statsO.Item2;
						ganador=ATKA-DEFO;
					}

					if(Personaje=='M')
					{
						statsA=DameATKyDEF(seleccion,'M','X');
						statsO=EligeObjetivo(seleccion-1,'J');	
						ATKA=statsA.Item1*4;
						DEFO=statsO.Item2;
						ganador=ATKA-DEFO;
					}
					if(Personaje=='J')
					{
						if(ganador>=0)
						{
							Console.Write("Carta Enemiga Eliminada");
							if(ganador==0)
							{
								Console.WriteLine(", el enemigo no recibe daño");
								Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
								EliminaCarta(statsO.Item3,'M');
							}
							else
							{
								Console.WriteLine(", el enemigo recibe "+ganador+" de daño");
								vidaM-=ganador;
								Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
								EliminaCarta(statsO.Item3,'M');
							}
						}
						else
						{
							Console.WriteLine("No pudiste eliminar la Carta Enemiga, recibes "+(-ganador)+" de daño");
							HaActuado(statsO.Item3,'M');
							vidaJ+=ganador;
						}
						Console.WriteLine("\nTu Carta ha sido eliminada al usar la habilidad");
						EliminaCarta(statsA.Item3,'J');
					}
					if(Personaje=='M')
					{
						if(ganador>=0)
						{
							Console.Write("Carta Aliada Eliminada");
							if(ganador==0)
							{
								Console.WriteLine(", no recibes daño");
								Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
								EliminaCarta(statsO.Item3,'J');
							}
							else
							{
								Console.WriteLine(", recibes "+ganador+" de daño");
								vidaJ-=ganador;
								Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
								EliminaCarta(statsO.Item3,'J');
							}
						}
						else
						{
							Console.WriteLine("Tu carta no pudo ser eliminada, el enemigo recibe "+(-ganador)+" de daño");
							vidaM+=ganador;
						}
						Console.WriteLine("\nLa Carta Enemiga ha sido eliminada al usar la habilidad");
						EliminaCarta(statsA.Item3,'M');
						Thread.Sleep(4000);
					}	
					nohapasadoturno=false;
				break;
				//TODO Acabar de añadir habilidades
				//A partir de aquí Work In Progress
				case 3:
					
				break;
				case 4:
				break;
				case 5:
				break;
				case 6:
				break;
				case 7:
				break;
				case 8:
				break;
				//Añadir cartas especiales 
				case 9:
				break;
				case 10:
				break;
				case 11:
				break;
				case 12:
				break;
				case 13:
				break;
			}
			return nohapasadoturno;
		}

		//TODO acabar este método porque de momento no hace nada
		//Este método es para cartas de efecto o habilidades especiales que la habilidad sepa a que enemigo debe enfocarse
		public static void EligeReceptor()
		{

		}

		//Desecadena todas las reacciones y opciones relacionadas con el Ataque
		public static void Ataque(char Personaje)
		{
			Tuple<int,int,int> statsA, statsO=new Tuple<int,int,int>(0,0,0);
			int ATKA=0,DEFO=0,atacante=0, ganador=0;
			char eleccion='N';
			bool seguro=false,esunchar,esunchara=false;
			if(Personaje=='J')
			{
				while(seguro==false)
				{
					esunchar=false;
					statsA=EligeAtacante('J');
					//Te muestra las estadisticas del monstruo y te hace confirmar que quieres emplear a ese (solo por si quieres cambiarlo a mitad)
					Console.WriteLine("\nEstas son las estadisticas del Monstruo: ATK:"+statsA.Item1+" DEF:"+statsA.Item2);
					Console.WriteLine("Estás seguro de elegir esta carta para tu ataque?(S/N)");
					while(esunchar==false)
						esunchar=char.TryParse(Console.ReadLine(),out eleccion);
					seguro=SioNo(eleccion);
					ATKA=statsA.Item1;
					atacante=statsA.Item3-1;
				
					if(seguro)
					{
						//Elegimos la carta que recibirá nuestro ataque
						statsO=EligeObjetivo(atacante,'M');
						Console.WriteLine("Estás seguro de elegir esta carta como tu objetivo?(S/N)");
						while(esunchara==false)
							esunchara=char.TryParse(Console.ReadLine(),out eleccion);
						seguro=SioNo(eleccion);
						DEFO=statsO.Item2;
					}
				}
				//Nos desvela la carta enemiga cuando la atacamos (por si acaso no muere poder ver que carta es)
				HaActuado(statsO.Item3,'M');

				ganador=ATKA-DEFO;
				MaquinaEnJuego('X');
			}

			//Mismo funcionamiento pero para la máquina
			if(Personaje=='M')
			{
				statsA=EligeAtacante('M');
				atacante=statsA.Item3-1;
				HaActuado(statsA.Item3,'M');
				statsO=EligeObjetivo(atacante,'J');
				ATKA=statsA.Item1;
				DEFO=statsO.Item2;
				ganador=ATKA-DEFO;
			}

			//Nos calcula el daño que causamos / recibimos por no poder eliminar la carta y nos lo muestra por pantalla
			if(Personaje=='J')
			{
				if(ganador>=0)
				{
					Console.Write("Carta Enemiga Eliminada");
					if(ganador==0)
					{
						Console.WriteLine(", el enemigo no recibe daño");
						Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
						EliminaCarta(statsO.Item3,'M');
					}
					else
					{
						Console.WriteLine(", el enemigo recibe "+ganador+" de daño");
						vidaM-=ganador;
						Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
						EliminaCarta(statsO.Item3,'M');
					}
				}
				else
				{
					Console.WriteLine("No pudiste eliminar la Carta Enemiga, recibes "+(-ganador)+" de daño");
					vidaJ+=ganador;
				}
			}

			//Nos calcula el daño que causa / recibe la máquina por no poder eliminar la carta y nos lo muestra por pantalla
			if(Personaje=='M')
			{
				if(ganador>=0)
				{
					Console.Write("Carta Aliada Eliminada");
					if(ganador==0)
					{
						Console.WriteLine(", no recibes daño");
						Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
						EliminaCarta(statsO.Item3,'J');
					}
					else
					{
						Console.WriteLine(", recibes "+ganador+" de daño");
						vidaJ-=ganador;
						Console.WriteLine("Como la carta ha sido eliminada se cambiará por otra de la baraja del mimso tipo");
						EliminaCarta(statsO.Item3,'J');
					}
				}
				else
				{
					Console.WriteLine("Tu carta no pudo ser eliminada, el enemigo recibe "+(-ganador)+" de daño");
					vidaM+=ganador;
				}
				Thread.Sleep(4000);
			}
			
			
		}

		//Voltea la carta que se haya empleado en el campo cuando sea atacada o ataque
		public static void HaActuado(int cartaafligida, char Personaje)
		{
			if(Personaje=='M')
				monstruosM_N_Visibles[cartaafligida-1].setBocaArriba(true);
			if(Personaje=='J')
				monstruosJ_N_Visibles[cartaafligida-1].setBocaArriba(true);
		}

		//Borra la carta de la lista principal al introducir una nueva en los monstruos visibles
		public static void EliminaCarta(int cartaafligida,char Personaje)
		{
			if(Personaje=='M')
			{
				//Compueba si quedan cartas
				if(monstruosM_N.Count>0)
				{
					monstruosM_N_Visibles[cartaafligida-1]=monstruosM_N[monstruosM_N.Count-1];
					monstruosM_N.RemoveAt(monstruosM_N.Count-1);
				}
				//Si te quedas sin cartas mueres porque no puedes continuar el juego
				else
				{
					vidaM=0;
					Console.WriteLine("Ganas, el enemigo se ha quedado sin cartas");
				}
			}
			if(Personaje=='J')
			{
				if(monstruosJ_N.Count>0)
				{
					monstruosJ_N_Visibles[cartaafligida-1]=monstruosJ_N[monstruosJ_N.Count-1];
					monstruosJ_N.RemoveAt(monstruosJ_N.Count-1);
				}
				else
				{
					vidaJ=0;
					Console.WriteLine("Pierdes, te has quedado sin cartas");
				}
			}
		}

		//Devuelve una tupla que contiene el Ataque de la carta, su defensa y la posición en la que se encuentra en ese momento
		public static Tuple<int,int,int> DameATKyDEF(int cartaseleccionada,char Personaje,char Modo)
		{	
			Carta carta=new Carta(ePalo.Diamantes,eValor.As);
			if(Personaje=='J')
				carta=monstruosJ_N_Visibles[cartaseleccionada-1];
			if(Personaje=='M')
				carta=monstruosM_N_Visibles[cartaseleccionada-1];
			int ATK=0, DEF=0;
			if(Modo=='H')
			{
				switch((eValor)carta.getValor())
				{
					case eValor.As:
						ATK=500;DEF=100;
					break;
					case eValor.Dos:
						ATK=600;DEF=300;
					break;
					case eValor.Tres:
						ATK=100;DEF=600;
					break;
					case eValor.Cuatro:
						ATK=1000;DEF=50;
					break;
					case eValor.Cinco:
						ATK=500;DEF=500;
					break;
					case eValor.Seis:
						ATK=50;DEF=2000;
					break;
					case eValor.Siete:
						ATK=300;DEF=700;
					break;
					case eValor.Ocho:
						ATK=1500;DEF=1500;
					break;
				}
			}
			else
			{
				switch((eValor)carta.getValor())
				{
					case eValor.As:
						ATK=100;DEF=500;
					break;
					case eValor.Dos:
						ATK=300;DEF=600;
					break;
					case eValor.Tres:
						ATK=600;DEF=100;
					break;
					case eValor.Cuatro:
						ATK=50;DEF=1000;
					break;
					case eValor.Cinco:
						ATK=500;DEF=500;
					break;
					case eValor.Seis:
						ATK=2000;DEF=50;
					break;
					case eValor.Siete:
						ATK=700;DEF=300;
					break;
					case eValor.Ocho:
						ATK=1500;DEF=1500;
					break;
				}
			}
			
			return Tuple.Create(ATK,DEF,cartaseleccionada);
		}

		//Nos printea el menu de Instrucciones 
		public static void Instrucciones()
		{
			BannerInstrucciones();
			Console.WriteLine("\n\nCIRCULO DE DEBILIDADES\n \t↱ ♣ ↴\n\t♥   ♦\n\t⬑ ♠ ↲ \n\t\t♣.Terrestre\n\t\t♦.Volador\n\t\t♠.Acuatico\n\t\t♥.Subterraneo\n\n\tEn este juego peleamos 3 monstruos contra 3 monstruos con cada jugador 3 cartas de efecto sobre la mesa, aquí algunas reglas:\n\n\t\t1.Si tu vida de jugador baja a 0 pierdes\n\n\t\t2.Si te quedas sin cartas en el deck pierdes automaticamente\n\n\t\t3.Las cartas boca abajo son exactamente iguales que las boca arriba pero no puede saberse que carta es\n\n\t\t4.Puedes destruir una carta de Monstruo enemiga si tu carta tiene más ATK que la DEF del enemigo\n\n\t\t5.Al atacar o usar habilidad tu carta se voltea y el rival la puede ver\n\n\t\t6.Tu carta solo puede atacar una vez por turno (exceptuando casos especiales)\n\n\t\t7.Si tu carta ataca a una que tiene más DEF que su ATK tu vida de jugador sufrirá el daño equivalente a la resta del daño\n\n\t\t8.El tipo es irrelevante si el rival tiene más ATK o DEF que tu carta que se encuentra atacando o defendiendo\n\n\t\t9.En caso de empate de ATK y DEF ganará la carta con el tipo favorable");
			Console.ReadLine();
			BannerInstrucciones();
			//Explicación de las cartas de Monstruo Normales
			Console.WriteLine("\nTodas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			Carta c= new Carta(ePalo.Treboles,eValor.As);
			Console.WriteLine(c.MuestraCarta()+"\tATK:100, DEF:500, HABILIDAD: Puedes reemplazar esta carta sin gastar tu turno");
			c=new Carta(ePalo.Treboles,eValor.Dos);
			Console.WriteLine(c.MuestraCarta()+"\tATK:300, DEF:600, HABILIDAD: Puedes hacer un ataque de x4 pero se destruye esta carta y gastas turno");
			c=new Carta(ePalo.Treboles,eValor.Tres);
			Console.WriteLine(c.MuestraCarta()+"\tATK:600, DEF:100, HABILIDAD: Intercambia el ATK y DEFENSA de una carta aliada durante el turno de actual");
			c=new Carta(ePalo.Treboles,eValor.Cuatro);
			Console.WriteLine(c.MuestraCarta()+"\tATK:50, DEF:1000, HABILIDAD: Reduce el ATK de una carta enemigo a la mitad durante 1 Turno");
			c=new Carta(ePalo.Treboles,eValor.Cinco);
			Console.WriteLine(c.MuestraCarta()+"\tATK:500, DEF:500, HABILIDAD: Aumenta la DEF en x3 pero no puedes atacar");
			Console.ReadLine();
			BannerInstrucciones();
			Console.WriteLine("\nTodas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			c=new Carta(ePalo.Treboles,eValor.Seis);
			Console.WriteLine(c.MuestraCarta()+"\tATK:2000, DEF:50, HABILIDAD: El ATK aumenta x2 durante un ataque pero la carta debe estar boca arriba");
			c=new Carta(ePalo.Treboles,eValor.Siete);
			Console.WriteLine(c.MuestraCarta()+"\tATK:700, DEF:300, HABILIDAD: Desvela la carta enemiga a la que elijas");
			c=new Carta((ePalo)0,eValor.Ocho);
			Console.WriteLine(c.MuestraCarta()+"\tATK:1500 DEF:1500 HABILIDAD: Solo puede actuar cada 2 turnos");
			Console.ReadLine();
			BannerInstrucciones();
			//Explicación de las cartas de Monstruo Especiales
			Console.WriteLine("\nTodas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS ESPECIALES");
			c=new Carta((ePalo)0,eValor.Nueve);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: El ataque enemigo falla si ataca al monstruo que tiene enlazado esta carta, Carta Trampa");
			c=new Carta((ePalo)0,eValor.Diez);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: Todos los monstruos en tu lado del estadio ganan 100 de ATK y DEF, Carta de Estadio");
			c=new Carta((ePalo)0,eValor.Jack);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: Te permite realizar 2 acciones en el mismo turno, Carta de Efecto");
			c=new Carta((ePalo)0,eValor.Queen);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: Añade 500 a tu vida de jugador al usarla. Carta de Efecto");
			c=new Carta((ePalo)0,eValor.King);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: Destruye una carta en el campo, Carta de Efecto");
			Console.ReadLine();
		}
		//Método que printea el Banner de Inicio del juego
		public static void BannerIncio()
		{
			Console.Clear();
		  	Console.WriteLine("\t\t\t\t\t /$$$$$$$  /$$$$$$ /$$$$$$$$ /$$   /$$ /$$    /$$ /$$$$$$$$ /$$   /$$ /$$$$$$ /$$$$$$$   /$$$$$$\n"+ 
							"\t\t\t\t\t| $$__  $$|_  $$_/| $$_____/| $$$ | $$| $$   | $$| $$_____/| $$$ | $$|_  $$_/| $$__  $$ /$$__  $$\n"+
							"\t\t\t\t\t| $$  \\ $$  | $$  | $$      | $$$$| $$| $$   | $$| $$      | $$$$| $$  | $$  | $$  \\ $$| $$  \\ $$\n"+
							"\t\t\t\t\t| $$$$$$$   | $$  | $$$$$   | $$ $$ $$|  $$ / $$/| $$$$$   | $$ $$ $$  | $$  | $$  | $$| $$  | $$\n"+
							"\t\t\t\t\t| $$__  $$  | $$  | $$__/   | $$  $$$$ \\  $$ $$/ | $$__/   | $$  $$$$  | $$  | $$  | $$| $$  | $$\n"+
							"\t\t\t\t\t| $$  \\ $$  | $$  | $$      | $$\\  $$$  \\  $$$/  | $$      | $$\\  $$$  | $$  | $$  | $$| $$  | $$\n"+
							"\t\t\t\t\t| $$$$$$$/ /$$$$$$| $$$$$$$$| $$ \\  $$   \\  $/   | $$$$$$$$| $$ \\  $$ /$$$$$$| $$$$$$$/|  $$$$$$/\n"+
							"\t\t\t\t\t|_______/ |______/|________/|__/  \\__/    \\_/    |________/|__/  \\__/|______/|_______/  \\______/");
		  	Console.WriteLine( "\t\t\t\t\t\t\t\t\t\t  /$$$$$$\n"+ 
							"\t\t\t\t\t\t\t\t\t\t /$$__  $$\n"+
							"\t\t\t\t\t\t\t\t\t\t| $$  \\ $$\n"+
							"\t\t\t\t\t\t\t\t\t\t| $$$$$$$$\n"+
							"\t\t\t\t\t\t\t\t\t\t| $$__  $$\n"+
							"\t\t\t\t\t\t\t\t\t\t| $$  | $$\n"+
							"\t\t\t\t\t\t\t\t\t\t| $$  | $$\n"+
							"\t\t\t\t\t\t\t\t\t\t|__/  |__/");
		  	Console.WriteLine( " \t   /$$$$$$$  /$$   /$$ /$$$$$$$$ /$$        /$$$$$$        /$$$$$$$  /$$$$$$$$       /$$      /$$  /$$$$$$  /$$   /$$  /$$$$$$  /$$$$$$$$ /$$$$$$$  /$$   /$$  /$$$$$$   /$$$$$$\n"+ 
							"\t  | $$__  $$| $$  | $$| $$_____/| $$       /$$__  $$      | $$__  $$| $$_____/      | $$$    /$$$ /$$__  $$| $$$ | $$ /$$__  $$|__  $$__/| $$__  $$| $$  | $$ /$$__  $$ /$$__  $$\n"+
							"\t  | $$  \\ $$| $$  | $$| $$      | $$      | $$  \\ $$      | $$  \\ $$| $$            | $$$$  /$$$$| $$  \\ $$| $$$$| $$| $$  \\__/   | $$   | $$  \\ $$| $$  | $$| $$  \\ $$| $$  \\__/\n"+
							"\t  | $$  | $$| $$  | $$| $$$$$   | $$      | $$  | $$      | $$  | $$| $$$$$         | $$ $$/$$ $$| $$  | $$| $$ $$ $$|  $$$$$$    | $$   | $$$$$$$/| $$  | $$| $$  | $$|  $$$$$$ \n"+
							"\t  | $$  | $$| $$  | $$| $$__/   | $$      | $$  | $$      | $$  | $$| $$__/         | $$  $$$| $$| $$  | $$| $$  $$$$ \\____  $$   | $$   | $$__  $$| $$  | $$| $$  | $$ \\____  $$\n"+
							"\t  | $$  | $$| $$  | $$| $$      | $$      | $$  | $$      | $$  | $$| $$            | $$\\  $ | $$| $$  | $$| $$\\  $$$ /$$  \\ $$   | $$   | $$  \\ $$| $$  | $$| $$  | $$ /$$  \\ $$\n"+
							"\t  | $$$$$$$/|  $$$$$$/| $$$$$$$$| $$$$$$$$|  $$$$$$/      | $$$$$$$/| $$$$$$$$      | $$ \\/  | $$|  $$$$$$/| $$ \\  $$|  $$$$$$/   | $$   | $$  | $$|  $$$$$$/|  $$$$$$/|  $$$$$$/\n"+
							"\t  |_______/  \\______/ |________/|________/ \\______/       |_______/ |________/      |__/     |__/ \\______/ |__/  \\__/ \\______/    |__/   |__/  |__/ \\______/  \\______/  \\______/" );
		}

		//Método que printea el Banner de las instrucciones
		public static void BannerInstrucciones()
		{
			Console.Clear();
			Console.WriteLine( "\t\t\t /$$$$$$ /$$   /$$  /$$$$$$  /$$$$$$$$ /$$$$$$$  /$$   /$$  /$$$$$$   /$$$$$$  /$$$$$$  /$$$$$$  /$$   /$$ /$$$$$$$$  /$$$$$$ \n"+
								"\t\t\t|_  $$_/| $$$ | $$ /$$__  $$|__  $$__/| $$__  $$| $$  | $$ /$$__  $$ /$$__  $$|_  $$_/ /$$__  $$| $$$ | $$| $$_____/ /$$__  $$\n"+
								"\t\t\t  | $$  | $$$$| $$| $$  \\__/   | $$   | $$  \\ $$| $$  | $$| $$  \\__/| $$  \\__/  | $$  | $$  \\ $$| $$$$| $$| $$      | $$  \\__/\n"+
								"\t\t\t  | $$  | $$ $$ $$|  $$$$$$    | $$   | $$$$$$$/| $$  | $$| $$      | $$        | $$  | $$  | $$| $$ $$ $$| $$$$$   |  $$$$$$ \n"+
								"\t\t\t  | $$  | $$  $$$$ \\____  $$   | $$   | $$__  $$| $$  | $$| $$      | $$        | $$  | $$  | $$| $$  $$$$| $$__/    \\____  $$\n"+
								"\t\t\t  | $$  | $$\\  $$$ /$$  \\ $$   | $$   | $$  \\ $$| $$  | $$| $$    $$| $$    $$  | $$  | $$  | $$| $$\\  $$$| $$       /$$  \\ $$\n"+
								"\t\t\t /$$$$$$| $$ \\  $$|  $$$$$$/   | $$   | $$  | $$|  $$$$$$/|  $$$$$$/|  $$$$$$/ /$$$$$$|  $$$$$$/| $$ \\  $$| $$$$$$$$|  $$$$$$/\n"+
								"\t\t\t|______/|__/  \\__/ \\______/    |__/   |__/  |__/ \\______/  \\______/  \\______/ |______/ \\______/ |__/  \\__/|________/ \\______/" );
		}

		//Método que printea el banner de la máquina
		public static void BannerMaquina()
		{
			Console.WriteLine(" __   __  _______  _______  __   __  ___   __    _  _______\n"+ 
							"|  |_|  ||   _   ||       ||  | |  ||   | |  |  | ||   _   |\n"+
							"|       ||  |_|  ||   _   ||  | |  ||   | |   |_| ||  |_|  |\n"+
							"|       ||       ||  | |  ||  |_|  ||   | |       ||       |\n"+
							"|       ||       ||  |_|  ||       ||   | |  _    ||       |\n"+
							"| ||_|| ||   _   ||      | |       ||   | | | |   ||   _   |\n"+
							"|_|   |_||__| |__||____||_||_______||___| |_|  |__||__| |__|");
		}

		//Método que printea el Banner del jugador
		public static void BannerJugador()
		{
			Console.WriteLine("     ██╗██╗   ██╗ ██████╗  █████╗ ██████╗  ██████╗ ██████╗\n"+ 
							"     ██║██║   ██║██╔════╝ ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗\n"+
							"     ██║██║   ██║██║  ███╗███████║██║  ██║██║   ██║██████╔╝\n"+
							"██   ██║██║   ██║██║   ██║██╔══██║██║  ██║██║   ██║██╔══██╗\n"+
							"╚█████╔╝╚██████╔╝╚██████╔╝██║  ██║██████╔╝╚██████╔╝██║  ██║\n"+
							" ╚════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═════╝  ╚═════╝ ╚═╝  ╚═╝");
		}

		//Método que nos muestra un Banner u otro según si ganas o pierdes
		public static void Victoria(char Personaje)
		{
			Console.Clear();
			//En este caso gana el jugador
			if(Personaje=='J')
			{
				Console.WriteLine(" ____  ____       _       ______       ______       _       ____  _____       _       ______      ___\n"+    
								"|_   ||   _|     / \\    .' ____ \\    .' ___  |     / \\     |_   \\|_   _|     / \\     |_   _ `.  .'   `.  \n"+
								"  | |__| |      / _ \\   | (___ \\_|  / .'   \\_|    / _ \\      |   \\ | |      / _ \\      | | `. \\/  .-.  \\ \n"+
								"  |  __  |     / ___ \\   _.____`.   | |   ____   / ___ \\     | |\\ \\| |     / ___ \\     | |  | || |   | | \n"+
								" _| |  | |_  _/ /   \\ \\_| \\____) |  \\ `.___]  |_/ /   \\ \\_  _| |_\\   |_  _/ /   \\ \\_  _| |_.' /\\  `-'  / \n"+
								"|____||____||____| |____|\\______.'   `._____.'|____| |____||_____|\\____||____| |____||______.'  `.___.'  ");
			}
			//Aquí gana la máquina
			else if(Personaje=='M')
			{
				Console.WriteLine(" ____  ____       _       ______     _______  ________  _______     ______   _____  ______      ___    \n"+
								"|_   ||   _|     / \\    .' ____ \\   |_   __ \\|_   __  ||_   __ \\   |_   _ `.|_   _||_   _ `.  .'   `.  \n"+
								"  | |__| |      / _ \\   | (___ \\_|    | |__) | | |_ \\_|  | |__) |    | | `. \\ | |    | | `. \\/  .-.  \\ \n"+
								"  |  __  |     / ___ \\   _.____`.     |  ___/  |  _| _   |  __ /     | |  | | | |    | |  | || |   | | \n"+
								" _| |  | |_  _/ /   \\ \\_| \\____) |   _| |_    _| |__/ | _| |  \\ \\_  _| |_.' /_| |_  _| |_.' /\\  `-'  / \n"+
								"|____||____||____| |____|\\______.'  |_____|  |________||____| |___||______.'|_____||______.'  `.___.'  ");
			}

			Console.ReadLine();
			Console.Clear();
			//Nos saca el Banner final y acaba el programa (si es que no hacemos nada después)
			Console.WriteLine(" ██████  ██████   █████   ██████ ██  █████  ███████     ██████   ██████  ██████           ██ ██    ██  ██████   █████  ██████  \n"+
							"██       ██   ██ ██   ██ ██      ██ ██   ██ ██          ██   ██ ██    ██ ██   ██          ██ ██    ██ ██       ██   ██ ██   ██ \n"+
							"██   ███ ██████  ███████ ██      ██ ███████ ███████     ██████  ██    ██ ██████           ██ ██    ██ ██   ███ ███████ ██████  \n"+
							"██    ██ ██   ██ ██   ██ ██      ██ ██   ██      ██     ██      ██    ██ ██   ██     ██   ██ ██    ██ ██    ██ ██   ██ ██   ██ \n"+
							" ██████  ██   ██ ██   ██  ██████ ██ ██   ██ ███████     ██       ██████  ██   ██      █████   ██████   ██████  ██   ██ ██   ██ ");
		}
	}

	//Inicio del programa
	class ej4 
	{
		static void Main(string[] args)
		{
			//Iniciamos el programa y le damos a elegir que hacemos
			DueloDeMonstruos.Principio();
			DueloDeMonstruos.Menu();

			Baraja baraja = new Baraja();
			Carta c;
			List<Carta> monstruosjugador=new List<Carta>();
			List<Carta> monstruosmaquina=new List<Carta>();

			baraja.RellenaBaraja();
			baraja.MezclaBaraja();
			//Rellena las manos de los jugadores de la misma baraja para no repetir cartas
			for(int i=0; i<25;i++)
			{
				c = baraja.PideCarta();
				monstruosjugador.Add(c);
				c = baraja.PideCarta();
				monstruosmaquina.Add(c);				
			}	
			//Genera los monstruos que emplearemos en el juego y cuales son visibles
			DueloDeMonstruos.GeneraMonstruos(monstruosmaquina,monstruosjugador);
			//Empieza el juego
			DueloDeMonstruos.Juego();
		}
	}
}