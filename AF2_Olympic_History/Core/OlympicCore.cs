using AF2_Olympic_History.Data;
using AF2_Olympic_History.Data.Tables;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AF2_Olympic_History.Core
{
    class OlympicCore
    {
        // kollekciok 1-3. feladatbol
        private List<Athletes> athletes;
        private List<AthleteEvents> athleteEvents;
        private List<CombinedCollection> combinedCollections;

        private readonly FileReader reader;

        // kiszervezett fajlnevek a feladatokhoz
        private string outPutFolder;
        private const string task3Name = "task3.csv";
        private const string task4Name = "task4.csv";
        private const string task5Name = "task5.csv";
        private const string task6Name = "task6.csv";
        private const string task7Name = "task7.csv";
        private const string task8Name = "task8.csv";
        private const string task9Name = "task9.csv";

        private readonly string exeLocation;

        public OlympicCore()
        {
            this.exeLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            reader = new FileReader(exeLocation);

            // 1-es, 2-es feladat
            athletes = reader.task1GetAthletes();
            athleteEvents = reader.task1GetEvents();

            // 3-as feladat
            CreateOutputDirectory();
            Task3();

            // 4-es feladat
            Task4();

            // 5-os feladat
            Task5();

            // 6-os feladat
            Task6();

            // 7-es feladat
            Task7();

            // 8-as feladat
            Task8();

            // 9-es feladat
            Task9();
        }

        /**
         * ### 3. feladat ###
         * A ket tablat osszefuzom ID menten, majd eventben levo ismetlodesek miatt
         * group by-al szukitem
         * 
         * Ennel a feladatnal irva volt, hogy 3. kollekciot is keszítsunk, igy ezt combinedCollection-be mentem
         * Kessobbi feladatokhoz ez nem volt feltetel igy ott nem mentem csak kozvetlen kiirom
         */
        private void Task3()
        {
            combinedCollections = (List<CombinedCollection>)(from events in athleteEvents
                                                             join athlete in athletes
                                                                 on events.Id equals athlete.Id
                                                             group events by new { athlete.Id, athlete.Name, athlete.Team, events.Sport } into e
                                                             select new CombinedCollection
                                                             {
                                                                 Id = e.Key.Id,
                                                                 Name = e.Key.Name,
                                                                 Team = e.Key.Team,
                                                                 Sport = e.Key.Sport
                                                             }).ToList();

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task3Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(combinedCollections);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task3Name);
        }

        /**
         * ### 4. feladat ###
         * Group by-al szukitjuk ID menten azokat ahol van erme, igy csak 
         * azokat a sportolokat kapjuk akiknek legalabb 1 erme van
         */
        private void Task4()
        {
            string header = "Athletes with at least one medal: ";

            var query = (from events in athleteEvents
                         where events.Medal >= 1
                         group events by events.Id into e
                         select e).Count();

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task4Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField(header);
                csv.NextRecord();
                csv.WriteField(query);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task4Name);
        }

        /**
         * ### 5. feladat ###
         * Group by szukitek versenyzo nevere, majd nevenkent summazzom a medal-ok erteket
         * 
         * Mivel 1-es feladatban "FileReader.cs"-ben a Medal-okat 0-nak vettem amikor "NA", es Bronz, Silver, Gold 1-et vesznek fel igy itt csak Sum-al kijon a medalok szama
         */
        private void Task5()
        {
            var query = from events in athleteEvents
                        group events by new { Id = events.Id, Name = events.Name } into e
                        select new {
                            Id = e.Key.Id,
                            Name = e.Key.Name,
                            Medal = e.Sum(g => g.Medal)
                        };

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task5Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(query);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task5Name);
        }

        /**
         * ### 6. feladat ###
         * Az elozo feladat, viszont csokkeno sorrendbe rendezve az elso ertekkel ter vissza
         * 
         * kimeneti formazashoz hoztam letre a segedosztalyt, mivel ket kulonbozo tipussal ter vissza
         */
        private void Task6()
        {
            resultTask6 query = new resultTask6();
            query = ((from events in athleteEvents
                           group events by new { Id = events.Id, Name = events.Name } into e
                           select new resultTask6
                           {
                                Id = e.Key.Id,
                                Name = e.Key.Name,
                                Medals = e.Sum(g => g.Medal)
                           }).OrderByDescending(g => g.Medals)).FirstOrDefault();

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task6Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("Id");
                csv.WriteField("Name");
                csv.WriteField("Medals");
                csv.NextRecord();
                csv.WriteField(query.Id);
                csv.WriteField(query.Name);
                csv.WriteField(query.Medals);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task6Name);
        }

        /**
         * ### 7. feladat ###
         * Sima query a ket feltetellel
         */
        private void Task7()
        {
            var query = from events in athleteEvents
                        where events.Medal >= 1 && events.Sport == "Swimming"
                        group events by new { Id = events.Id, Name = events.Name } into e
                        select new
                        {
                            Id = e.Key.Id,
                            Name = e.Key.Name
                        };

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task7Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(query);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task7Name);
        }

        /**
         * ### 8. feladat ###
         * egyszeru egy felteteles query csokkeno sorrendben, csak az elso elemmel
         * 
         * custom header kell, mert csak egy elemmel terunk vissza ilyenkor nem irja ki automatikusan
         */
        private void Task8()
        {
            Athletes query = (from athlete in athletes
                        where athlete.Team == "Norway"
                        orderby athlete.Height descending
                        select athlete).FirstOrDefault();

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task8Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<Athletes>();
                csv.NextRecord();
                csv.WriteField(query.ToString(), false);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task8Name);
        }

        /**
         * ### 9. feladat ###
         * athleteAges - kiszamolja minden ID-hoz ki mikor toltotte be a 30-at
         * oldAthletes - ezt hozzafuzi az esemenyekhez ID menten, majd leszukit minden esemenyre ami 30. eleteve utan volt
         *                  majd hozzafuzi az athletes kollekciot amivel group by szukitunk, es abbol irjuk ki az adatokat 
         */
        private void Task9()
        {
            var athleteAges = from athlete in athletes
                              where athlete.Age >= 30
                              select new
                              {
                                    ID = athlete.Id,
                                    AgeOf30 = 2020 - athlete.Age + 30 
                              };

            var oldAthletes = from comingOfAge in athleteAges
                              join events in athleteEvents
                                    on comingOfAge.ID equals events.Id
                              where events.Year >= comingOfAge.AgeOf30
                              join athlete in athletes
                                    on comingOfAge.ID equals athlete.Id
                              group athlete by new { Id = athlete.Id, Name = athlete.Name, Sex = athlete.Sex, Age = athlete.Age } into a 
                              orderby a.Key.Age ascending
                              select new
                              { 
                                  Id = a.Key.Id,
                                  Name = a.Key.Name,
                                  Sex = a.Key.Sex,
                                  Age = a.Key.Age
                              };
                                   

            using (var writer = new StreamWriter(Path.Combine(outPutFolder, task9Name)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(oldAthletes);
                writer.Flush();
            }
            Console.WriteLine("{0} created", task9Name);
        }

        /**
         * Utvonalat hozza letre a kimeneteknek
         * README-ben irtam hova megy
         */
        private void CreateOutputDirectory()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            projectPath = Path.Combine(projectPath, "AF2_Olympic_History");
            outPutFolder = Path.Combine(projectPath, "Output");
            Directory.CreateDirectory(outPutFolder);

            Console.WriteLine("OutPut directory created at {0}", outPutFolder);
        }

        private class resultTask6
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Medals { get; set; }
        }        
    }
}
