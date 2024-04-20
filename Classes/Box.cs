using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTestTask.Classes
{
    internal class Box : WarehouseItem
    {
        private DateOnly? useUntil;
        private DateOnly? productionDate;

        public DateOnly? UseUntil
        {
            get => useUntil ?? productionDate?.AddDays(100);
            set => useUntil = value;
        }

        public DateOnly? ProductionDate
        {
            get => productionDate;
            set
            {
                productionDate = value;
                if (useUntil == null)
                {
                    useUntil = productionDate?.AddDays(100);
                }
            }

        }
    }
}
