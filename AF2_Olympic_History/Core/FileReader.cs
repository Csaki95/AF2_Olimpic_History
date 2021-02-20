using System;
using System.Collections.Generic;
using System.IO;
using AF2_Olympic_History.Data.Tables;
using CsvHelper;
using System.Globalization;

namespace AF2_Olympic_History.Data
{
    class FileReader
    {
        private const string athletePath = @"Input\athletes_fixed.csv";
        private const string athleteEventPath = @"Input\athlete_events.csv";
        private readonly string athleteLocation;
        private readonly string athleteEventLocation;

        public FileReader(string exeLocation)
        {
            this.athleteLocation = Path.Combine(exeLocation, athletePath);
            this.athleteEventLocation = Path.Combine(exeLocation, athleteEventPath);
        }

        /**
         * ### 1. feladat ###
         * athletes_fixed beolvasasa
         * 
         * Age, Height, Weight lehetseges "NA" parameterek igy azokat kezzel ellenorzom, ha "NA" akkor 0 ertekkel hozom letre
         */
        public List<Athletes> task1GetAthletes()
        {
            List<Athletes> list = new List<Athletes>();

            if (checkFileExists(athleteLocation)) {
                using (var reader = new StreamReader(athleteLocation))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        Athletes athletes = new Athletes();
                        {
                            athletes.Id = csv.GetField<int>("ID");
                            athletes.Name = csv.GetField<string>("Name");
                            athletes.Sex = csv.GetField<string>("Sex");
                            string age = csv.GetField<string>("Age");
                            athletes.Age = (age == "NA") ? 0 : Int32.Parse(age);
                            string height = csv.GetField<string>("Height");
                            athletes.Height = (height == "NA") ? 0 : Double.Parse(height, CultureInfo.InvariantCulture);
                            string weight = csv.GetField<string>("Weight");
                            athletes.Weight = (weight == "NA") ? 0 : Double.Parse(weight, CultureInfo.InvariantCulture);
                            athletes.Team = csv.GetField<string>("Team");
                            athletes.NOC = csv.GetField<string>("NOC");                                
                        }

                        list.Add(athletes);
                    }
                }
            } else
            {
                System.Environment.Exit(1);
            }

            return list;
        }

        /**
         * ### 1. feladat ###
         * athlete_events beolvasasa
         * 
         * Medal a lehetseges "NA" parameter, es mivel egy feladatban sem kell megkulonboztetni az erme tipusokat igy ha valamilyen erme van akkor 1, "NA" eseten 0
         */
        public List<AthleteEvents> task1GetEvents()
        {
            List<AthleteEvents> list = new List<AthleteEvents>();

            if (checkFileExists(athleteEventLocation))
            {
                using (var reader = new StreamReader(athleteEventLocation))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        AthleteEvents events = new AthleteEvents();
                        {
                            events.Id = csv.GetField<int>("ID");
                            events.Name = csv.GetField<string>("Name");
                            events.Year = csv.GetField<int>("Year");
                            events.Season = csv.GetField<string>("Season");
                            events.City = csv.GetField<string>("City");
                            events.Sport = csv.GetField<string>("Sport");
                            events.Event = csv.GetField<string>("Event");
                            string medal = csv.GetField<string>("Medal");
                            events.Medal = (medal == "NA") ? 0 : 1;
                        }

                        list.Add(events);
                    }
                }
            }
            else
            {
                System.Environment.Exit(1);
            }

            return list;
        }

        private bool checkFileExists(string location)
        {
            if (File.Exists(location))
            {
                return true;
            } else
            {
                Console.WriteLine();
                Console.Error.Write("ERROR: File at " + location + " doesn't exists.\n");
                Console.WriteLine("Please add the athletes_fixed.csv and athlete_events.csv to the /Input folder or to the \\AF2_Olympic_History\\bin\\Release\\netcoreapp3.1\\Input folder");
                Console.WriteLine();
                return false;
            }
        }

    }
}
