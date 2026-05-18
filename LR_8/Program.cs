using System;
using System.Collections.Generic;

namespace AngolaShortestPath
{
    public class City(string name, double latitude, double longitude)
    {
        public string Name { get; } = name;
        public double Latitude { get; } = latitude;
        public double Longitude { get; } = longitude;
        public List<Edge> Neighbors { get; } = new List<Edge>();

        public void AddNeighbor(City target, double distance)
        {
            Neighbors.Add(new Edge(target, distance));
            target.Neighbors.Add(new Edge(this, distance));
        }
    }

    public class Edge
    {
        public City Target { get; }
        public double Distance { get; }

        public Edge(City target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }

    public class PathFinder
    {
        // Haversine
        public static double Heuristic(City a, City b)
        {
            double R = 6371;
            double lat1 = a.Latitude * Math.PI / 180;
            double lat2 = b.Latitude * Math.PI / 180;
            double dLat = (b.Latitude - a.Latitude) * Math.PI / 180;
            double dLon = (b.Longitude - a.Longitude) * Math.PI / 180;

            double aVal = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                          Math.Cos(lat1) * Math.Cos(lat2) *
                          Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(aVal), Math.Sqrt(1 - aVal));
            return R * c;
        }

        public static List<City>? FindShortestPath(City start, City goal)
        {
            var openSet = new PriorityQueue<City, double>();
            var cameFrom = new Dictionary<City, City>();
            var gScore = new Dictionary<City, double>();
            var fScore = new Dictionary<City, double>();

            openSet.Enqueue(start, 0);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }

                foreach (var edge in current.Neighbors)
                {
                    var neighbor = edge.Target;
                    double tentativeGScore = gScore[current] + edge.Distance;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);

                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }

            return null;
        }

        private static List<City> ReconstructPath(Dictionary<City, City> cameFrom, City current)
        {
            var path = new List<City> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }
            path.Reverse();
            return path;
        }
    }

    class Program
    {
        static void Main()
        {
            var benguela = new City("Benguela", -12.5783, 13.4073);
            var huambo = new City("Huambo", -12.7761, 15.7392);
            var kuito = new City("Kuito", -12.3833, 16.9333);
            var luanda = new City("Luanda", -8.8390, 13.2894);
            var lubango = new City("Lubango", -14.9172, 13.4925);
            var lobito = new City("Lobito", -12.3464, 13.5456);
            var luena = new City("Luena", -11.7833, 19.9167);
            var malanje = new City("Malanje", -9.5403, 16.3410);
            var menongue = new City("Menongue", -14.6585, 17.6910);
            var namibe = new City("Namibe", -15.1961, 12.1522);
            var ndalatando = new City("N'dalatando", -9.3000, 14.9167);
            var ondjiva = new City("Ondjiva", -17.0667, 15.7333);
            var saurimo = new City("Saurimo", -9.6608, 20.3916);
            var sumbe = new City("Sumbe", -11.2061, 13.8437);
            var uige = new City("Uíge", -7.6087, 15.0613);

            luanda.AddNeighbor(ndalatando, 240);
            luanda.AddNeighbor(sumbe, 330);
            luanda.AddNeighbor(uige, 345);

            uige.AddNeighbor(malanje, 250);
            uige.AddNeighbor(ndalatando, 195);

            ndalatando.AddNeighbor(malanje, 185);
            ndalatando.AddNeighbor(huambo, 400);
            malanje.AddNeighbor(saurimo, 450);
            malanje.AddNeighbor(kuito, 325);

            saurimo.AddNeighbor(luena, 250);

            sumbe.AddNeighbor(lobito, 175);
            lobito.AddNeighbor(benguela, 35);
            lobito.AddNeighbor(huambo, 320);

            benguela.AddNeighbor(lubango, 340);
            benguela.AddNeighbor(huambo, 345);

            huambo.AddNeighbor(kuito, 145);
            huambo.AddNeighbor(ondjiva, 620);
            kuito.AddNeighbor(luena, 410);
            kuito.AddNeighbor(menongue, 340);

            lubango.AddNeighbor(namibe, 175);
            lubango.AddNeighbor(ondjiva, 415);
            lubango.AddNeighbor(huambo, 350);
            lubango.AddNeighbor(menongue, 480);

            menongue.AddNeighbor(ondjiva, 342);

            var cityRegistry = new Dictionary<string, City>(StringComparer.OrdinalIgnoreCase)
            {
                { "Luanda", luanda },
                { "Huambo", huambo },
                { "Lubango", lubango },
                { "Benguela", benguela },
                { "Lobito", lobito },
                { "Malanje", malanje },
                { "Kuito", kuito },
                { "Uíge", uige },
                { "Namibe", namibe },
                { "Luena", luena },
                { "Saurimo", saurimo },
                { "Sumbe", sumbe },
                { "Menongue", menongue },
                { "N'dalatando", ndalatando },
                { "Ondjiva", ondjiva }
            };

            Console.WriteLine("Available cities: " + string.Join(", ", cityRegistry.Keys));
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter departure city: ");
            string startInput = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Enter destination city: ");
            string endInput = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine();

            if (startInput == null)
            {
                Console.WriteLine($"Error: '{startInput}' is not a recognized city in our map.");
                return;
            }

            if (endInput == null)
            {
                Console.WriteLine($"Error: '{endInput}' is not a recognized city in our map.");
                return;
            }

            if (!cityRegistry.TryGetValue(startInput, out City? startCity))
            {
                Console.WriteLine($"Error: '{startInput}' is not a recognized city in our map.");
                return;
            }

            if (!cityRegistry.TryGetValue(endInput, out City? endCity))
            {
                Console.WriteLine($"Error: '{endInput}' is not a recognized city in our map.");
                return;
            }

            var path = PathFinder.FindShortestPath(startCity, endCity);

            if (path != null)
            {
                Console.WriteLine($"Shortest path from {startCity.Name} to {endCity.Name}:");
                double totalDistance = 0;
                for (int i = 0; i < path.Count; i++)
                {
                    Console.Write(path[i].Name);
                    if (i < path.Count - 1)
                    {
                        var edge = path[i].Neighbors.Find(e => e.Target == path[i + 1]);
                        totalDistance += edge?.Distance ?? 0;
                        Console.Write($" --({edge?.Distance ?? 0} km)--> ");
                    }
                }
                Console.WriteLine($"\n\nTotal Driving Distance: {totalDistance} km");
            }
            else
            {
                Console.WriteLine($"No transit connection available between {startCity.Name} and {endCity.Name}.");
            }
        }
    }
}