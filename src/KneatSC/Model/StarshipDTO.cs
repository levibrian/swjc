namespace KneatSC.Model
{
    public class StarshipDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string HyperdriveRating { get; set; }
        public string MGLT { get; set; }
        public string StarshipClass { get; set; }
        public string Url { get; set; }

        public decimal JumpCount { get; set; }
    }
}
