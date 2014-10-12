using Assignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new FundaApi();

            Console.WriteLine("Ophalen gegevens...");
            var amsterdam = api.GetTop10("koop", "/amsterdam");

            Console.WriteLine("Top 10 Objecten in Amsterdam");
            Console.WriteLine("----------------------------");

            amsterdam.ToList().ForEach(x =>
            {
                Console.WriteLine("{0}\t{1}\t{2}", x.MakelaarId, x.MakelaarNaam, x.ObjectCount);
            });
            Console.WriteLine();

            Console.WriteLine("Ophalen gegevens...");
            var amsterdamMetTuin = api.GetTop10("koop", "/amsterdam/tuin");

            Console.WriteLine("Top 10 Objecten met tuin in Amsterdam");
            Console.WriteLine("-------------------------------------");

            amsterdamMetTuin.ToList().ForEach(x =>
            {
                Console.WriteLine("{0}\t{1}\t{2}", x.MakelaarId, x.MakelaarNaam, x.ObjectCount);
            });

            Console.ReadKey();


            //// http://partnerapi.funda.nl/feeds/Aanbod.svc/[key]/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25
            //// http://partnerapi.funda.nl/feeds/Aanbod.svc/json/005e7c1d6f6c4f9bacac16760286e3cd/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25

            //// Wat zijn de makelaars met de meeste objecten voor een bepaalde zoekopdracht.
            //// Resultaat is paged dus er zijn eventueel meerdere requests nodig om het totaal op te halen.
            //// Groeperen op  makelaar.
            //// Sorteren op aantal object.
            //// Toon in een tabelletje het resultaat.



            //var query_type = "type=koop";
            //var query_search = "zo=/amsterdam"; // + "/tuin";

            //var page = "page=1";
            //var pageSize = "pagesize=25";

            //        Paging": {
            //    "AantalPaginas": 57,
            //    "HuidigePagina": 1,
            //    "VolgendeUrl": "/~/koop/amsterdam/tuin/p2/",
            //    "VorigeUrl": null
            //},


        }


    }
}
