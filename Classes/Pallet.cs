using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTestTask.Classes
{
    internal class Pallet : WarehouseItem
    {
        private double? weight;
        private double? volume;
        public List<Box> boxes = new List<Box>();

        public void AddBox(Box box)
        {
            if (box.Width > Width || box.Depth > Depth)
            {
                throw new ArgumentException("Box is too large for the pallet!");
            }

            boxes.Add(box);
            //Weight += box.Weight;
        }

        public DateOnly? UseUntil
        {
            get => boxes.Min(b => b.UseUntil);
            set { }

        }

        public new double Weight
        {
            get => boxes.Sum(b => b.Weight) + 30;
            set { }
        }

        public new double Volume
        {
            get => boxes.Sum(b => b.Width) + base.Volume;
            set { }
        }

    }
}
