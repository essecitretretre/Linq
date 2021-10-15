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
            Console.WriteLine("© – Copyright. - Alex Mazzoni ®, V^H, Primo Esercizio Linq™");

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
                        new XElement("Voto", "8"),
                        new XElement("Mail", "username@domain.li")
                    ),
                    new XElement("Studente", new XAttribute("ID", 2),
                        new XElement("Nome", "Piero"),
                        new XElement("Cognome", "Berlino"),
                        new XElement("Voto", "6.5"),
                        new XElement("Mail", "username@domain.li")
                    )
                )
            );

            xmlDocument.Save("../../../XML/studenti.xml");


            XMLParsing("../../../XML/studenti.xml");
        }

        static void XMLParsing(string Path) 
        {//Questa funzione caricherà il file e ci lavorerà
            XDocument doc = XDocument.Load(Path);

            Console.WriteLine(doc);
        }
    }
}
