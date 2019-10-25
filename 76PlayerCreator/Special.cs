using System;
namespace SpecialDefualt
{
    public class Special
    {
        public string mainColor { get; set; }
        public float maxPoints { get; set; }
        public float assignedPoints { get; set; }
        public float totPointsUsedByCards { get; set; }
        public float branch { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }


        public override string ToString()
        {
            return string.Format(name);
        }
    }

    public class Rootobject
    {
        public Special[] special { get; set; }
    }
}
