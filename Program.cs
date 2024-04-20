using System;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using MonopolyTestTask.Classes;
using System.Runtime.CompilerServices;

namespace Test
{
    public class Program
    {
        static void Main()
        {
            Warehouse warehouse = new Warehouse();
            Console.Write("The following date is a starting point.\n" +
                "It will be inceased or decreased for each of the pallets.\n" +
                "Enter the production date for one of the pallets: ");
            DateOnly productionDate;

            while (true)
            {
                if (DateOnly.TryParse(Console.ReadLine(), out productionDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect format!");
                }
            }

            warehouse.CreatePallet(0, 15.5, 10.3, 2.2);
            warehouse.CreatePallet(1, 20, 30, 10);
            warehouse.CreatePallet(2, 34, 355, 2);
            warehouse.CreatePallet(3, 4, 33, 8);
            warehouse.CreatePallet(4, 111, 14, 40);
            warehouse.pallets[0].AddBox(warehouse.CreateBox(0, 3.5, 2.3, 1.1, 15, productionDate));
            warehouse.pallets[0].AddBox(warehouse.CreateBox(1, 2, 3, 2, 40, productionDate.AddDays(+32), productionDate.AddDays(-20)));
            warehouse.pallets[1].AddBox(warehouse.CreateBox(2, 12, 10, 1, 15.3, productionDate.AddDays(34), productionDate.AddDays(-160)));
            warehouse.pallets[2].AddBox(warehouse.CreateBox(3, 1, 22.6, 1.1, 150, productionDate.AddDays(-66)));
            warehouse.pallets[4].AddBox(warehouse.CreateBox(4, 100, 3, 33, 18.2, productionDate.AddDays(12), productionDate.AddDays(-79)));

            warehouse.pallets = warehouse.pallets.OrderBy(p => p.UseUntil).ToList();
            var groups = warehouse.pallets.GroupBy(p => p.UseUntil);

            while (true)
            {
                Console.Write("Press \"Q\" to exit\n" +
                    "\"C\" to create a new pallet\n" +
                    "\"S\" to show the pallets\n");
                var key = Console.ReadLine();

                if (key == "q" || key == "Q")
                {
                    break;
                }
                else if (key == "c" || key == "C")
                {
                    PalletCreation(warehouse);
                }
                else if (key == "s" || key == "S")
                {
                    PalletShow(warehouse, groups);
                }
                else
                {
                    Console.WriteLine("Incorrect choice. Please, choose the command from the list.");
                }

            }

        }

        static void PalletCreation(Warehouse warehouse)
        {
            int id = Enumerable.Range(0, warehouse.pallets.Max(p => p.Id) + 2).
                    Except(warehouse.pallets.Select(p => p.Id)).First();

            Console.Write("\nEnter the width: ");
            double width = double.Parse(Console.ReadLine());
            Console.Write("\nEnter the height: ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("\nEnter the depth: ");
            double depth = double.Parse(Console.ReadLine());

            warehouse.CreatePallet(id, width, height, depth);
        }

        static void PalletShow(Warehouse warehouse, IEnumerable<IGrouping<DateOnly?, Pallet>> groups)
        {
            warehouse.pallets = warehouse.pallets.OrderBy(p => p.UseUntil).ToList();
            groups = warehouse.pallets.GroupBy(p => p.UseUntil);

            foreach (var group in groups)
            {
                var pallet = group.OrderBy(g => g.Weight).ToList();

                if (group.Key != null)
                {
                    Console.WriteLine($"===== Срок годности: {group.Key} =====");
                }
                else
                {
                    Console.WriteLine("===== Срок годности: без срока годности =====");
                }

                foreach (var pal in pallet)
                {
                    Console.WriteLine($"Pallet id: {pal.Id}\n" +
                            $"Pallet weight: {pal.Weight}\n" +
                            $"Pallet height: {pal.Height}\n" +
                            $"Pallet width: {pal.Width}\n" +
                            $"Pallet depth: {pal.Depth}\n" +
                            $"Pallet volume: {pal.Volume}\n" +
                            $"Pallet use until: {pal.UseUntil}\n");
                }
            }

            List<Pallet> threePallets = new List<Pallet>();
            for (int i = 0; i < 3; i++)
            {
                threePallets.Add(warehouse.pallets[warehouse.pallets.Count - 1 - i]);
            }

            threePallets = threePallets.OrderBy(p => p.Volume).ToList();

            Console.WriteLine("***** 3 pallets with the latest expiration date, ordered by volume *****");
            foreach (var pallet in threePallets)
            {
                Console.WriteLine($"pallet id: {pallet.Id}\n" +
                            $"pallet weight: {pallet.Weight}\n" +
                            $"pallet height: {pallet.Height}\n" +
                            $"pallet width: {pallet.Width}\n" +
                            $"pallet depth: {pallet.Depth}\n" +
                            $"pallet volume: {pallet.Volume}\n" +
                            $"pallet use until: {pallet.UseUntil}\n");
            }
        }

    }

}
