namespace NZWalks.API.Models
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }    
        public double Lat { get; set; }
        public double LongReg { get; set; }
        public long Population { get; set; }

        public IEnumerable<Walk> Walks { get; set; }


    }
}
