using System;


namespace Log
{
    class Program
    {
        static void Main(string[] args)
        {
            Guestbook guestbook = new();    //Instansierar ett nytt objekt av klassen Guestbook

            //While-loop som skriver ut gästboken och kontrollerar de olika menyvalen mha en switch-stas så länge som konsolapplikationen körs
            while(true)
            {
                guestbook.VisaGästbok();    //Skriver ut gästboken i konsolen
                int inp = (int) Console.ReadKey(true).Key;  //Läser av knapptryck från användaren

                switch (inp)
                {
                    //Läser inmatning från användaren för att skapa en logg av denna 
                    case '1':
                        Console.Write("Ange namn och meddelande i formatet 'Namn - Meddelande': ");
                        string namnOchMeddelande = Console.ReadLine()!; //Skapaer en variabel av användarens inmatning

                        //If-sats som kontrollerae att inmatat värde inte är null eller tomt
                        if(!string.IsNullOrEmpty(namnOchMeddelande)) 
                        {
                            try
                            {
                                guestbook.AddLog(namnOchMeddelande);    //Lägger till logg i gästboken
                            }
                            catch(Exception)
                            {
                                Console.Write("Något gick fel. Vänligen tryck på valfri tangent för att gå vidare.");   //Skriver ut felmeddelande om programmet stöter på undantag när AddLog körs
                                Console.ReadKey();
                            }
                        }
                        break;

                    //Läser inmatning från användaren för att redera vald logg från gästboken
                    case '2':  
                        Console.Write("Ange index för att redera logg: ");
                        string index = Console.ReadLine()!;

                        //If-sats som kontrollerar att inmatat värde inte är null eller tomt 
                        if(!string.IsNullOrEmpty(index))
                        {
                            try 
                            {
                                guestbook.DelLog(Convert.ToInt32(index));   //Raderar logg med inmatat index
                            }
                            catch(Exception) 
                            {
                                Console.WriteLine("Kunde inte hitta angivet index. Vänligen tryck på valfri tangent för att gå vidare.");   //Skriver ut ett felmeddelande om programmet stöter på ett undantag när DelLog körs
                                Console.ReadKey();
                            }
                        }
                        break;


                    case 88:
                        Environment.Exit(0);    //Avslutar konsolapplikationen
                        break;
                }
            }
        }
    }
}
