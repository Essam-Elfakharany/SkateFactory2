using SkateFactory2.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateFactory2.Business
{
    public class Skateboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public EColor Color { get; set; }
        public ESkateType SkateType { get; set; }
        public EBrakeType BrakeType { get; set; }
        public EShapeType ShapeType { get; set; }
    }
}
