using System.Drawing;

namespace Dessins.Events
{
    public class ChangeColor : DrawingEvent
    {
        public override string Type { get { return "Color"; } }
        public string Color { get; set; }
       

        public ChangeColor(string color)
        {
            Color = color;
        }
    }
}
