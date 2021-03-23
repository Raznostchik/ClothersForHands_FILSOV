using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersForHands_FILSOV.EF;

namespace ClothersForHands_FILSOV.EF
{
    public partial class Materials
    {
        public string GetTypeAndName { get => $"Тип материала: {TypeMaterials.TypeMaterial} | Наименование материала:  {Name}"; }

        public string GetMinCount { get => $"Минимальное количество: {MinimumQuantity} шт."; }

        public string GetCount { get => $"Остаток: {QuantityOnStorage} шт."; }
        public string GetColor
        {
            get
            {
                if (QuantityOnStorage <= MinimumQuantity)
                {
                    return "#f19292";
                }
                else if (QuantityOnStorage <= MinimumQuantity * 3)
                {
                    return "#ffba01";
                }
                else
                {
                    return "#fff";
                }
            }
        }
    }
}
