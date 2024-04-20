using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTestTask.Classes
{
    internal class Warehouse
    {
        public List<Pallet> pallets = new List<Pallet>();

        public Pallet CreatePallet(int id, double width, double height, double depth)
        {
            Pallet pallet = new Pallet
            {
                Id = id,
                Width = width,
                Height = height,
                Depth = depth
            };

            this.pallets.Add(pallet);

            return pallet;
        }

        public Box CreateBox(int id, double width, double height, double depth,
                            double weight, DateOnly? useUntil, DateOnly productionDate)
        {
            Box box = new Box
            {
                Id = id,
                Width = width,
                Height = height,
                Depth = depth,
                Weight = weight,
                UseUntil = useUntil,
                ProductionDate = productionDate
            };

            return box;
        }

        public Box CreateBox(int id, double width, double height, double depth,
                            double weight, DateOnly productionDate)
        {
            Box box = new Box
            {
                Id = id,
                Width = width,
                Height = height,
                Depth = depth,
                Weight = weight,
                ProductionDate = productionDate
            };

            return box;
        }
    }
}
