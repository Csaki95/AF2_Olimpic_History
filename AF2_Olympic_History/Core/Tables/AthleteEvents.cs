
namespace AF2_Olympic_History.Data.Tables
{
    class AthleteEvents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Season { get; set; }
        public string City { get; set; }
        public string Sport { get; set; }
        public string Event { get; set; }
        public int Medal { get; set; }
        
        public override string ToString()
        {
            return Id.ToString() + "," + Name + "," + Year.ToString() + "," + Season + "," + City + "," + Sport + "," + Event + "," + Medal.ToString();
        }
    }
}
