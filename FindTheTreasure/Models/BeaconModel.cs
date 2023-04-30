namespace FindTheTreasure.Models
{
    public class BeaconModel
    {
        /// <summary>
        /// Hardware info, always known
        /// </summary>
        public string MAC { get; set; }
        
        /// <summary>
        /// Custom info from DB
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Custom info from DB
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Custom info from DB
        /// </summary>
        public string SerialNumber { get; set; }

        public DateTime Discovered { get; set; }
    }
}
