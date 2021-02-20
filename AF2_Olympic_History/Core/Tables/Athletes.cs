
namespace AF2_Olympic_History.Data.Tables
{
    class Athletes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Team { get; set; }
        public string NOC { get; set; }

        public override string ToString()
        {
            return Id.ToString() + "," + Name + "," + Sex + "," + Age.ToString() + "," + Height.ToString() + "," + Weight.ToString() + "," + Team + "," + NOC;
        }
    }
}
