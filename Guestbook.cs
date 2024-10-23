using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Log
{

    public class Guestbook
    {
        private readonly string filename = @"guestbook.json";   //Skapar en read only variabel "filename" för en json-fil
        private readonly List <Inlagg> loggar = []; //Skapar en array för objekt av klassen Inlagg

        //Konstruktor som kontrollerae om json-filen finns mha en if-sats och därefter deserializerar loggarna i filen(om filen finns)
        public Guestbook()
        {
            if(File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                loggar = JsonSerializer.Deserialize<List<Inlagg>>(jsonString)!;
            }
        }

        //Metod som skapar en logg
        public Inlagg AddLog(string log)
        {
            string [] nameAndMessage = log.Split('-');  //Delar upp inmatningen mha '-'
            int i = 2;

            //While-loop som körs så länge inmatningen inte är uppdelad till två delar eller om del ett eller två är null eller tomma
            while (nameAndMessage.Length != 2 || string.IsNullOrWhiteSpace(nameAndMessage[0]) || string.IsNullOrWhiteSpace(nameAndMessage[1]))
            {
                VisaGästbok();  //Skiver ut gästbok                       
                Console.Write("Fel format! Loggen måste fyllas i enligt formatet 'Namn - Meddelande'(Försök: " + i++ +"):");    //Felmeddelande som visar hur många felaktiga inmatningar som gjorts
                log = Console.ReadLine()!;  //Läser av ny inmatning
                nameAndMessage = log.Split('-');    //Delar ny inmatning
            }

            //Skapar ett nytt objekt av klassen Inlagg och tilldelar egenskaper till objektet med den uppdelade inmatningen
            Inlagg obj = new()
            {
                Name = nameAndMessage[0].Trim(),  
                Message = nameAndMessage[1].Trim()
            };

            //Lägger till loggen i arrayen och serializerar denna till json-filen
            loggar.Add(obj);
            Serialize();
            return obj; //Returnerar det nya objektet
        }

        //Metod som raderar logg med angivet index
        public int DelLog (int index)
        {
            loggar.RemoveAt(index); //Tar bort loggen från arrayen
            Serialize();    //Tar bort loggen från json-filen
            return index;   //Returnerar inmatat index
        }

        //Metod som returnerar alla loggar från arrayen
        public List<Inlagg> GetLoggar()
        {
            return loggar;
        }

        //Metod som serializerar alla loggar från arrayen
        private void Serialize()
        {
            var jsonString = JsonSerializer.Serialize(loggar);
            File.WriteAllText(filename, jsonString);  
        }

        //Metod som rensar konsolen och skriver ut gästboken, menyn och loggarna från arrayen mha en foreach-loop
        public void VisaGästbok()
        {
            Console.Clear();
            Console.WriteLine("VÄLKOMMEN TILL GÄSTBOKEN! :-) \n");
            Console.WriteLine("1. LÄGG TILL LOGG");
            Console.WriteLine("2. TA BORT LOGG");
            Console.WriteLine("X. AVSLUTA\n");

            int i = 0;
            foreach (Inlagg meddelande in loggar)
            {
                Console.WriteLine("[" + i++ + "] " + meddelande.Name + " - " + meddelande.Message);
            }
        }
    }    
}