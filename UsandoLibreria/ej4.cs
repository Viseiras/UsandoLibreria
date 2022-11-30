using LibreriaBaraja;
using System;
using System.Collections.Generic;

namespace usandoBaraja
{
	class DueloDeMonstruos
	{
		public static void GameStart()
		{
			Console.Clear();
			Console.WriteLine("\n\n\n\n\n\n\n\t\t\t\tPOR FAVOR PON PANTALLA COMPLETA ANTES DE EMPEZAR");
			Console.ReadLine();


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
		public static void Menu()
		{
			Console.WriteLine("\n\n\n\t\t1.INICIO\n\t\t0.INSTRUCCIONES");
			bool esnum= int.TryParse(Console.ReadLine(),out int menu);

			switch(menu)
			{
				case 1:

				break;
				
				case 0:
					Instrucciones();
				break;
			}
		}
		public static void Instrucciones()
		{
			Banner();
			Console.WriteLine("\n\nCIRCULO DE DEBILIDADES\n \t↱ ♣ ↴\n\t♥   ♦\n\t⬑ ♠ ↲ \n\t\t♣.Terrestre\n\t\t♥.Acuatico\n\t\t♦.Volador\n\t\t♠.Subterraneo");
			Console.ReadLine();
			Banner();
			Console.WriteLine("Todas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			Carta c= new Carta((ePalo)0,(eValor)1);
			Console.WriteLine(c.MuestraCarta()+"\tATK:100, DEF:500, HABILIDAD: Puedes reemplazar esta carta sin gastar tu turno, CD: 4 Turnos");
			c=new Carta((ePalo)0,(eValor)2);
			Console.WriteLine(c.MuestraCarta()+"\tATK:300, DEF:600, HABILIDAD: Puedes hacer un ataque de x4 pero se destruye esta carta y gastas turno, CD:0 Turnos");
			c=new Carta((ePalo)0,(eValor)3);
			Console.WriteLine(c.MuestraCarta()+"\tATK:600, DEF:100, HABILIDAD: Intercambia el ATK y DEFENSA de una carta aliada, CD:5 Turnos");
			c=new Carta((ePalo)0,(eValor)4);
			Console.WriteLine(c.MuestraCarta()+"\tATK:50, DEF:1000, HABILIDAD: Reduce el ATK de una carta enemigo a la mitad durante 1 Turno, CD:3 Turnos");
			c=new Carta((ePalo)0,(eValor)5);
			Console.WriteLine(c.MuestraCarta()+"\tATK:500, DEF:500, HABILIDAD: Aumenta la DEF en x3 pero no puedes atacar, dura 2 Turno, CD:5 Turnos");
			Console.ReadLine();
			Banner();
			Console.WriteLine("Todas las cartas con el mismo numero hacen lo mismo (El tipo no importa)\n\nCARTAS DE MONSTRUO");
			c=new Carta((ePalo)0,(eValor)6);
			Console.WriteLine(c.MuestraCarta()+"\tATK:2000, DEF:50, HABILIDAD: El ATK aumenta x2 durante un ataque pero la carta debe estar boca arriba, CD:2 Turnos");
			c=new Carta((ePalo)0,(eValor)7);
			Console.WriteLine(c.MuestraCarta()+"\tATK:700, DEF:300, HABILIDAD: Desvela la carta enemiga a la que elijas, CD:2 Turnos");
			c=new Carta((ePalo)0,(eValor)8);
			Console.WriteLine(c.MuestraCarta()+"\tATK:1000 DEF:1000 HABILIDAD: Solo puede actuar cada 2 turnos CD:0 Turnos");
			c=new Carta((ePalo)0,(eValor)9);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: El ataque enemigo falla si ataca al monstruo que tiene enlazado ,Carta Trampa");
			c=new Carta((ePalo)0,(eValor)13);
			Console.WriteLine(c.MuestraCarta()+"\tHABILIDAD: Destruye una carta en el campo, Carta de Efecto");
		}
		public static void Banner()
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
	}

	class ej4 
	{
		static void Main(string[] args)
		{
			List<Carta> monstruos=new List<Carta>();
			Carta c;
			Baraja baraja = new Baraja();
			baraja.RellenaBaraja();
			for(int i=0; i<3;i++)
			{
				c = baraja.PideCarta();
				monstruos.Add(c);				
			}	
			DueloDeMonstruos.GameStart();
			DueloDeMonstruos.Menu();
			//Console.WriteLine(Carta.MuestraCartasHorizontal(monstruos));
		}
	}
}