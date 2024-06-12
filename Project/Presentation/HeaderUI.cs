namespace Cinema;

public static class DisplayHeaderUI{

	//font used : https://patorjk.com/software/taag/#p=display&f=Jacky&t=test

	public static void HeaderMain(){
		Console.WriteLine("   ____    _____      __      _    _____   __    __    _____   _____       _____        _____  ");
		Console.WriteLine("  / ___)  (_   _)    /  \\    / )  / ___/   ) )  ( (   (_   _) (_   _)     (_   _)      / ___/  ");
		Console.WriteLine(" / /        | |     / /\\ \\  / /  ( (__    ( (    ) )    | |     | |         | |       ( (__    ");
		Console.WriteLine("( (         | |     ) ) ) ) ) )   ) __)    \\ \\  / /     | |     | |         | |        ) __)   ");
		Console.WriteLine("( (         | |    ( ( ( ( ( (   ( (        \\ \\/ /      | |     | |   __    | |   __  ( (      ");
		Console.WriteLine(" \\ \\___    _| |__  / /  \\ \\/ /    \\ \\___     \\  /      _| |__ __| |___) ) __| |___) )  \\ \\___  ");
		Console.WriteLine("  \\____)  /_____( (_/    \\__/      \\____\\     \\/      /_____( \\________/  \\________/    \\____\\ ");                                                                                 
	}
	
	public static void HeaderSnack()
	{
		Console.WriteLine("  _____      __      _     ____       ____    __   ___  ");
		Console.WriteLine(" / ____\\    /  \\    / )   (    )     / ___)  () ) / __) ");
		Console.WriteLine("( (___     / /\\ \\  / /    / /\\ \\    / /      ( (_/ /    ");
		Console.WriteLine(" \\___ \\    ) ) ) ) ) )   ( (__) )  ( (       ()   (     ");
		Console.WriteLine("     ) )  ( ( ( ( ( (     )    (   ( (       () /\\ \\    ");
		Console.WriteLine(" ___/ /   / /  \\ \\/ /    /  /\\  \\   \\ \\___   ( (  \\ \\   ");
		Console.WriteLine("/____/   (_/    \\__/    /__(  )__\\   \\____)  ()_)  \\_\\  ");
		Console.WriteLine("_________________________________________________________________");
		Console.WriteLine("Your current order:\n");
		if (SnackMenuLogic.OrderedSnacks.Count!= 0)
		{
			foreach (Tuple<string,int> snack in SnackMenuLogic.OrderedSnacks)
			{
				Console.WriteLine($"{snack.Item1} [{snack.Item2}x]");
			}
		}
	}

	public static void AdminHeader()
	{
		Console.WriteLine("   ____     ______       __    __      _____      __      _  ");
		Console.WriteLine("  (    )   (_  __ \\      \\ \\  / /     (_   _)    /  \\    / ) ");
		Console.WriteLine("  / /\\ \\     ) ) \\ \\     () \\/ ()       | |     / /\\ \\  / /  ");
		Console.WriteLine(" ( (__) )   ( (   ) )    / _  _ \\       | |     ) ) ) ) ) )  ");
		Console.WriteLine("  )    (     ) )  ) )   / / \\/ \\ \\      | |    ( ( ( ( ( (   ");
		Console.WriteLine(" /  /\\  \\   / /__/ /   /_/      \\_\\    _| |__  / /  \\ \\/ /   ");
		Console.WriteLine("/__(  )__\\ (______/   (/          \\)  /_____( (_/    \\__/    ");
															 
	}

	public static void LoginHeader()
	{
		
		Console.WriteLine(" _____         ____        _____     _____      __      _  ");
		Console.WriteLine("(_   _)       / __ \\      / ___ \\   (_   _)    /  \\    / ) ");
		Console.WriteLine("  | |        / /  \\ \\    / /   \\_)    | |     / /\\ \\  / /  ");
		Console.WriteLine("  | |       ( ()  () )  ( (  ____     | |     ) ) ) ) ) )  ");
		Console.WriteLine("  | |   __  ( ()  () )  ( ( (__  )    | |    ( ( ( ( ( (   ");
		Console.WriteLine("__| |___) )  \\ \\__/ /    \\ \\__/ /    _| |__  / /  \\ \\/ /   ");
		Console.WriteLine("\\________/    \\____/      \\____/    /_____( (_/    \\__/    ");
	}

	public static void RegistrationHeader(){
		Console.WriteLine(" ______      _____      _____     _____    _____   ________    _____   ______    ");
		Console.WriteLine("(   __ \\    / ___/     / ___ \\   (_   _)  / ____\\ (___  ___)  / ___/  (   __ \\   ");
		Console.WriteLine(" ) (__) )  ( (__      / /   \\_)    | |   ( (___       ) )    ( (__     ) (__) )  ");
		Console.WriteLine("(    __/    ) __)    ( (  ____     | |    \\___ \\     ( (      ) __)   (    __/   ");
		Console.WriteLine(" ) \\ \\  _  ( (       ( ( (__  )    | |        ) )     ) )    ( (       ) \\ \\  _  ");
		Console.WriteLine("( ( \\ \\_))  \\ \\___    \\ \\__/ /    _| |__  ___/ /     ( (      \\ \\___  ( ( \\ \\_)) ");
		Console.WriteLine(" )_) \\__/    \\____\\    \\____/    /_____( /____/      /__\\      \\____\\  )_) \\__/  ");
	}

	public static void TicketsHeader(){
		Console.WriteLine(" ________    _____     ____    __   ___    _____   ________    _____  ");
		Console.WriteLine("(___  ___)  (_   _)   / ___)  () ) / __)  / ___/  (___  ___)  / ____\\ ");
		Console.WriteLine("    ) )       | |    / /      ( (_/ /    ( (__        ) )    ( (___   ");
		Console.WriteLine("   ( (        | |   ( (       ()   (      ) __)      ( (      \\___ \\  ");
		Console.WriteLine("    ) )       | |   ( (       () /\\ \\    ( (          ) )         ) ) ");
		Console.WriteLine("   ( (       _| |__  \\ \\___   ( (  \\ \\    \\ \\___     ( (      ___/ /  ");
		Console.WriteLine("   /__\\     /_____(   \\____)  ()_)  \\_\\    \\____\\    /__\\    /____/	 ");
	}

	public static void UserHeader(){
		Console.WriteLine(" __    __    _____    _____   ______    ");
		Console.WriteLine(" ) )  ( (   / ____\\  / ___/  (   __ \\   ");
		Console.WriteLine("( (    ) ) ( (___   ( (__     ) (__) )  ");
		Console.WriteLine(" ) )  ( (   \\___ \\   ) __)   (    __/   ");
		Console.WriteLine("( (    ) )      ) ) ( (       ) \\ \\  _  ");
		Console.WriteLine(" ) \\__/ (   ___/ /   \\ \\___  ( ( \\ \\_)) ");
		Console.WriteLine(" \\______/  /____/     \\____\\  )_) \\__/  ");
	}                                 

}