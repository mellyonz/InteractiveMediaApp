using System;
namespace PerksDefualt
{
    public class Perks
    {
        public float level { get; set; }
        public float assignedLevel { get; set; }
        public float maxCardLevel { get; set; }
        public string[] names { get; set; }
        public string[] descriptions { get; set; }
        public string image { get; set; }



        public float[] Data
        {
            get { return new float[] { level, assignedLevel, maxCardLevel }; }
        }
        public string[][] Info
        {
            get { return new string[][] { names, descriptions }; }
        }


    }

    public class Rootobject
    {
        public Perks[] perks { get; set; }
    }
}
