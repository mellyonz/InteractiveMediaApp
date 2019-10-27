using System;
namespace SpecialDefualt
{
    public class Special
    {
        public string mainColor { get; set; }
        public float assignedPoints { get; set; }
        public float toPointsUsedByCards { get; set; }
        public float branch { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public float[] Data
        {
            get { return new float[] { assignedPoints, toPointsUsedByCards}; }
        }
        public string[] Info
        {
            get { return new string[] { name, mainColor, image }; }
        }

        public override string ToString()
        {
            return string.Format(description);
        }
    }

    public class Rootobject
    {
        public Special[] special { get; set; }
    }
}
