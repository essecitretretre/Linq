using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mazzoni.Alex._5H.LinQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("© – Copyright. - Alex Mazzoni ® - All rights reserved - V^H Linq™.\n");
            //Breve spiegazioni Libreria Linq

            //int[] numeri = new int[] { 1, 2, 3, 4 };

            //List<int> numeriMaggioridiDue = new List<int>();

            ////Nuovo metodo
            //numeriMaggioridiDue = numeri.Where(numero => numero > 2).ToList();

            //// Vecchio metodo
            ////for (int i = 0; i < 4; i++)
            ////    if (numeri[i] > 2)
            ////        numeriMaggioridiDue.Add(numeri[i]);

            //foreach (var a in numeriMaggioridiDue)
            //    Console.WriteLine(a);


            XDocument xmlDocument = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Prima Prova di Linq to XML"),
                new XElement("Studenti",
                    new XElement("Studente", new XAttribute("ID", 1),
                        new XElement("Nome", "Alex"),
                        new XElement("Cognome", "Mazzoni"),
                        new XElement("Voto", "6"),
                        new XElement("Citta", "Rimini")
                    ),
                    new XElement("Studente", new XAttribute("ID", 3),
                        new XElement("Nome", "Piero"),
                        new XElement("Cognome", "Berlino"),
                        new XElement("Voto", "7"),
                        new XElement("Citta", "Riccione")
                    ),
                    new XElement("Studente", new XAttribute("ID", 2),
                        new XElement("Nome", "Mario"),
                        new XElement("Cognome", "Peppino"),
                        new XElement("Voto", "9"),
                        new XElement("Citta", "Firenze")
                    )
                )
            );

            xmlDocument.Save("../../../XML/studenti.xml");

            Console.WriteLine("Visualizza struttura XML");
            XMLParsing("../../../XML/studenti.xml");
            Console.WriteLine("Visualizza Prime Operazioni Fatte"); 
            XMLOperations("../../../XML/studenti.xml");
            Console.WriteLine("Visualizza Operazioni sulle Citta che contengono sono Ri"); 
            XMLOperationsCitta("../../../XML/studenti.xml");
            Console.WriteLine("Visualizza Solo il voto massimo"); 
            XMLOperationsSoloVotoMax("../../../XML/studenti.xml");
            Console.WriteLine("Visualizza In ordine descrescente i voti");
            XMLOperationsVotoMax("../../../XML/studenti.xml");
            Console.WriteLine("Visualizza il Gruppo della citta Rimini");
            XMLOperationsGroup("../../../XML/studenti.xml");
        }

        static void XMLParsing(string Path) 
        {//Questa funzione caricherà il file e ci lavorerà
            XDocument doc = XDocument.Load(Path);

            Console.WriteLine(doc);
        }

        static void XMLOperations(string Path)
        {
            XDocument doc = XDocument.Load(Path);

            var query = from d in doc.Descendants("Studente") //Prima di tutto creo una query che restituisce una raccolta (filtrata) degli elementi "studente" (essendo XML un albero di tag, questi elementi vengono visti come discendenti del documento, da qui l'uso di "doc.Descendants")
                        //FILTRI
                        //where d.Element("Nome").Value == "Mario"    //Cerca Elementi in base al Nome "Mario"
                        //orderby int.Parse(d.Attribute("ID").Value)  //Ordina Elementi in base all'ID (ordine crescente)
                        where d.Element("Citta").Value.Contains("Ri") //Cerca Elementi in base alla citta che contengono le lettere "Ri"
                        //________
                        select new  //notare l'uso del linguaggio simile a quello tipico dei database: from, in , select
                        {
                            Id = d.Attribute("ID").Value,   //sono interessato solo al valore vero e proprio, senza tag
                            Name = d.Element("Nome").Value, //in questo caso vogliamo estrarre tutti gli ID (attributi) ed i Nomi (elementi) contenuti nell'XML
                            Surname = d.Element("Cognome").Value,
                            Mark = d.Element("Voto").Value,
                            City = d.Element("Citta").Value,
                        };

            query.ToList().ForEach(s =>
            {
                Console.WriteLine($"{s.Id}\t{s.Name}\t{s.Surname}\t\t{s.Mark}\t\t{s.City}"); //scrivo in console tutte le voci trovate
            });
        }

        static void XMLOperationsCitta(string Path)
        {
            XDocument doc = XDocument.Load(Path);

            var query = from d in doc.Descendants("Citta") //Prima di tutto creo una query che restituisce una raccolta (filtrata) degli elementi "studente" (essendo XML un albero di tag, questi elementi vengono visti come discendenti del documento, da qui l'uso di "doc.Descendants")

                        select d.Value;

            query.ToList().ForEach(s =>
            {
                Console.WriteLine(s); //scrivo in console tutte le voci trovate
            });
        }

        static void XMLOperationsSoloVotoMax(string Path)
        {
            XDocument doc = XDocument.Load(Path);

            var query = from d in doc.Descendants("Voto") //Query che prende i voti
                        
                        select int.Parse(d.Value);//Prende tutti i Voti e li converte in numeri

            Console.WriteLine(query.Max());//Prende il massimo fra quei n7umeri e lo stampa
        }
        static void XMLOperationsVotoMax(string Path)
        {
            XDocument doc = XDocument.Load(Path);

            var query = from d in doc.Descendants("Studente") //Query che prende i voti
                        orderby int.Parse(d.Element("Voto").Value) descending
                        select new  
                        {
                            Id = d.Attribute("ID").Value, 
                            Name = d.Element("Nome").Value,
                            Surname = d.Element("Cognome").Value,
                            Mark = d.Element("Voto").Value,
                            City = d.Element("Citta").Value,
                        };

            query.ToList().ForEach(s =>
            {
                Console.WriteLine($"{s.Id}\t{s.Name}\t{s.Surname}\t\t{s.Mark}\t\t{s.City}"); //scrivo in console tutte le voci trovate
            });
        }

        static void XMLOperationsGroup(string Path)
        {
            XDocument doc = XDocument.Load(Path);

            var query = from d in doc.Descendants("Studente") //Query che prende i voti
                        group d by d.Element("Citta").Value into gruppo
                        select new
                        {
                            City=gruppo.Key,
                            Count = gruppo.Count()
                        };

            query.ToList().ForEach(s =>
            {
                Console.WriteLine($"Città: {s.City}\t\t Numero studenti: {s.Count}"); //scrivo in console tutte le voci trovate
            });
        }
    }
}
