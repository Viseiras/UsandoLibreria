using LibreriaBaraja;
using System;
using System.Collections.Generic;
using System.Threading;

namespace usandoBaraja
{
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
		
		public static void Principio()
		{
			Console.Clear();
			Console.WriteLine("\n\n\n\n\n\n\n\t\t\t\tPOR FAVOR PON PANTALLA COMPLETA ANTES DE EMPEZAR");
			Console.ReadLine();

			BannerIncio();
		}

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

		public static void GeneraMonstruos(List<Carta> monstruosmaquina, List<Carta> monstruosjugador)
		{
			monstruosM_N=new List<Carta>();
			monstruosJ_N=new List<Carta>();
			monstruosM_E=new List<Carta>();
			monstruosJ_E=new List<Carta>();
			monstruosM_N_Visibles=new List<Carta>(3);
			monstruosJ_N_Visibles=new List<Carta>(3);
			monstruosM_E_Visibles=new List<Carta>(3);
			monstruosJ_E_Visibles=new List<Carta>(3);

			foreach (Carta c in monstruosmaquina)
			{
				c.setBocaArriba(false);
				//Comprueba si es Especial o Normal
				if(c.getValor()>8)
					monstruosM_E.Add(c);
				else
					monstruosM_N.Add(c);
			}
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
					case 1:
						InicioJuego();
						noquieresalir=false;
					break;
					
					case 0:
						Instrucciones();
						esnum=false;
						
					break;
				}	
			}
		}

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

		public static void MenuJugador()
		{
			int menu=-1;
			bool esnum= false, noelige=true,haentrado=false;
			while(noelige)
			{
				char eleccion='N';
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

				if(SioNo(eleccion))
				{
					menu=99;
				}

				if(menu!=99)
				{
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
				switch(menu)
				{
					case 1:
						//Atacar();
						Ataque();
					break;
					case 2:
						//Habilidad();
					break;
					case 3:
						//Ver cartas
						Console.Clear();
						Console.WriteLine("EN JUEGO:\n");
						JugadorEnJuego();
						Console.WriteLine("EN EL DECK:\n");
						Console.WriteLine("-CARTAS DE MONSTRUO-\n"+Carta.MuestraCartasHorizontal(monstruosJ_N)+"-CARTAS ESPECIALES-\n"+Carta.MuestraCartasHorizontal(monstruosJ_E));
					break;
					case 4:
						//Instrucciones
						Instrucciones();
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
		}
		public static void MaquinaEnJuego()
		{
			Console.Clear();
			vidaM=5000;
			BannerMaquina();
			Console.WriteLine("\nVida:"+vidaM);
			Console.WriteLine("\n\n"+Carta.MuestraCartasHorizontal(monstruosM_E_Visibles));
			Console.WriteLine("\n"+Carta.MuestraCartasHorizontalT(monstruosM_N_Visibles));
		}
		public static void JugadorEnJuego()
		{
			//TODO Fixear que la vida no baja cambiando la declaración de esta y la modificación a un método ajeno
			vidaJ=5000;
			Console.WriteLine(Carta.MuestraCartasHorizontalT(monstruosJ_N_Visibles)+"\n");
			Console.WriteLine(Carta.MuestraCartasHorizontal(monstruosJ_E_Visibles)+"\n\n");
			Console.WriteLine("Vida:"+vidaJ+"\n");
		}
		public static void Juego()
		{
			MaquinaEnJuego();
			JugadorEnJuego();
			BannerJugador();
			MenuJugador(); 
		}
		public static Tuple<int,int> EligeAtacante()
		{
			bool esunmonstruo=false;
			int seleccion;
			Tuple<int,int> stats=new Tuple<int,int>(0,0);
			while(esunmonstruo==false)
			{
				Console.Clear();
				Console.WriteLine("  Carta 1 \t Carta 2\tCarta 3");
				JugadorEnJuego();
				
				Console.WriteLine("Selecciona la carta que quieres usar introduciendo su numero:");
				esunmonstruo=int.TryParse(Console.ReadLine(),out seleccion);
				if(seleccion<0 || seleccion>3)
				{
					Console.WriteLine("Ese Monstruo no se encuentra en la arena");
					esunmonstruo=false;
					Console.ReadLine();
				}
				if(esunmonstruo)
					stats=DameATKyDEF(seleccion,'J');	
			}	
			return stats;
		}
		//TODO Hacer que solo pueda golpear a los adyacentes y crear un método que solo me muestre a los adyacentes(EligeObjetivoMonstruo y EligeObjetivoEspecial?)
		public static Tuple<int,int> EligeObjetivo()
		{
			bool esunmonstruo=false;
			int seleccion;
			Tuple<int,int> stats=new Tuple<int,int>(0,0);
			while(esunmonstruo==false)
			{
				Console.Clear();
				MaquinaEnJuego();
				Console.WriteLine("  Carta 1 \t Carta 2\tCarta 3");

				Console.WriteLine("\nSelecciona la carta que quieres atacar introduciendo su numero:");
				esunmonstruo=int.TryParse(Console.ReadLine(),out seleccion);
				if(seleccion<0 || seleccion>3)
				{
					Console.WriteLine("Ese Monstruo no se encuentra en la arena");
					esunmonstruo=false;
					Console.ReadLine();
				}
				if(esunmonstruo)
					stats=DameATKyDEF(seleccion,'M');	
			}	
			return stats;
		}
		public static void Ataque()
		{
			Tuple<int,int> statsA, statsO;
			int ATKA=0,DEFO=0;
			char eleccion;
			bool seguro=false,esunchar;
			while(seguro==false)
			{
				statsA=EligeAtacante();
				Console.WriteLine("\nEstas son las estadisticas del Monstruo: ATK:"+statsA.Item1+" DEF:"+statsA.Item2);
				Console.WriteLine("Estás seguro de elegir esta carta para tu ataque?(S/N)");
				esunchar=char.TryParse(Console.ReadLine(),out eleccion);
				seguro=SioNo(eleccion);
				ATKA=statsA.Item1;
			}
			seguro=false;
			while(seguro==false)
			{
				statsO=EligeObjetivo();
				Console.WriteLine("Estás seguro de elegir esta carta como tu objetivo?(S/N)");
				esunchar=char.TryParse(Console.ReadLine(),out eleccion);
				seguro=SioNo(eleccion);
				DEFO=statsO.Item2;
			}

			int ganador=ATKA-DEFO;
			if(ganador>=0)
			{
				Console.Write("Carta Enemiga Eliminada");
				if(ganador==0)
					Console.WriteLine(", el enemigo no recibe daño");
				else
				{
					Console.WriteLine(", el enemigo recibe "+ganador+" de daño");
					vidaM-=ganador;
				}
					
			}
			else
			{
				Console.WriteLine("No pudiste eliminar la Carta Enemiga, recibes "+(-ganador)+" de daño");
				vidaJ+=ganador;
			}
		}
		public static Tuple<int,int> DameATKyDEF(int cartaseleccionada,char Personaje)
		{	
			Carta carta=new Carta(ePalo.Diamantes,eValor.As);
			if(Personaje=='J')
				carta=monstruosJ_N_Visibles[cartaseleccionada-1];
			if(Personaje=='M')
				carta=monstruosM_N_Visibles[cartaseleccionada-1];
			int ATK=0, DEF=0;
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
			return Tuple.Create(ATK,DEF);
		}
		public static void Instrucciones()
		{
			BannerInstrucciones();
			Console.WriteLine("\n\nCIRCULO DE DEBILIDADES\n \t↱ ♣ ↴\n\t♥   ♦\n\t⬑ ♠ ↲ \n\t\t♣.Terrestre\n\t\t♦.Volador\n\t\t♠.Acuatico\n\t\t♥.Subterraneo\n\n\tEn este juego peleamos 3 monstruos contra 3 monstruos con cada jugador 3 cartas de efecto sobre la mesa, aquí algunas reglas:\n\n\t\t1.Si tu vida de jugador baja a 0 pierdes\n\n\t\t2.Si te quedas sin cartas en el deck pierdes automaticamente\n\n\t\t3.Las cartas boca abajo son exactamente iguales que las boca arriba pero no puede saberse que carta es\n\n\t\t4.Puedes destruir una carta de Monstruo enemiga si tu carta tiene más ATK que la DEF del enemigo\n\n\t\t5.Al atacar o usar habilidad tu carta se voltea y el rival la puede ver\n\n\t\t6.Tu carta solo puede atacar una vez por turno (exceptuando casos especiales)\n\n\t\t7.Si tu carta ataca a una que tiene más DEF que su ATK tu vida de jugador sufrirá el daño equivalente a la resta del daño\n\n\t\t8.El tipo es irrelevante si el rival tiene más ATK o DEF que tu carta que se encuentra atacando o defendiendo\n\n\t\t9.En caso de empate de ATK y DEF ganará la carta con el tipo favorable");
			Console.ReadLine();
			BannerInstrucciones();
			//Explicación de las cartas de Monstruo Normales
			Console.WriteLine("\nTodas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			Carta c= new Carta(ePalo.Treboles,eValor.As);
			Console.WriteLine(c.MuestraCarta()+"\tATK:100, DEF:500, HABILIDAD: Puedes reemplazar esta carta sin gastar tu turno, CD: 4 Turnos");
			c=new Carta(ePalo.Treboles,eValor.Dos);
			Console.WriteLine(c.MuestraCarta()+"\tATK:300, DEF:600, HABILIDAD: Puedes hacer un ataque de x4 pero se destruye esta carta y gastas turno, CD:0 Turnos");
			c=new Carta(ePalo.Treboles,eValor.Tres);
			Console.WriteLine(c.MuestraCarta()+"\tATK:600, DEF:100, HABILIDAD: Intercambia el ATK y DEFENSA de una carta aliada, CD:5 Turnos");
			c=new Carta(ePalo.Treboles,eValor.Cuatro);
			Console.WriteLine(c.MuestraCarta()+"\tATK:50, DEF:1000, HABILIDAD: Reduce el ATK de una carta enemigo a la mitad durante 1 Turno, CD:3 Turnos");
			c=new Carta(ePalo.Treboles,eValor.Cinco);
			Console.WriteLine(c.MuestraCarta()+"\tATK:500, DEF:500, HABILIDAD: Aumenta la DEF en x3 pero no puedes atacar, dura 2 Turno, CD:5 Turnos");
			Console.ReadLine();
			BannerInstrucciones();
			Console.WriteLine("\nTodas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			c=new Carta(ePalo.Treboles,eValor.Seis);
			Console.WriteLine(c.MuestraCarta()+"\tATK:2000, DEF:50, HABILIDAD: El ATK aumenta x2 durante un ataque pero la carta debe estar boca arriba, CD:2 Turnos");
			c=new Carta(ePalo.Treboles,eValor.Siete);
			Console.WriteLine(c.MuestraCarta()+"\tATK:700, DEF:300, HABILIDAD: Desvela la carta enemiga a la que elijas, CD:2 Turnos");
			c=new Carta((ePalo)0,eValor.Ocho);
			Console.WriteLine(c.MuestraCarta()+"\tATK:1500 DEF:1500 HABILIDAD: Solo puede actuar cada 2 turnos CD:0 Turnos");
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
		public static void BannerJugador()
		{
			Console.WriteLine("     ██╗██╗   ██╗ ██████╗  █████╗ ██████╗  ██████╗ ██████╗\n"+ 
							"     ██║██║   ██║██╔════╝ ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗\n"+
							"     ██║██║   ██║██║  ███╗███████║██║  ██║██║   ██║██████╔╝\n"+
							"██   ██║██║   ██║██║   ██║██╔══██║██║  ██║██║   ██║██╔══██╗\n"+
							"╚█████╔╝╚██████╔╝╚██████╔╝██║  ██║██████╔╝╚██████╔╝██║  ██║\n"+
							" ╚════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═════╝  ╚═════╝ ╚═╝  ╚═╝");
		}
	}

	class ej4 
	{
		static void Main(string[] args)
		{
			//Iniciamos el programa y le damos a elegir que hacemos
			DueloDeMonstruos.Principio();
			DueloDeMonstruos.Menu();

			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			baraja.MezclaBaraja();
			Carta c;
			List<Carta> monstruosjugador=new List<Carta>();
			List<Carta> monstruosmaquina=new List<Carta>();
			//Rellena las manos de los jugadores de la misma baraja para no repetir cartas
			for(int i=0; i<25;i++)
			{
				c = baraja.PideCarta();
				monstruosjugador.Add(c);
				c = baraja.PideCarta();
				monstruosmaquina.Add(c);				
			}	

			DueloDeMonstruos.GeneraMonstruos(monstruosmaquina,monstruosjugador);
			DueloDeMonstruos.Juego();
		}
	}
}