using SkateFactory2.Models.Enums;

namespace SkateFactory2.Models
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
