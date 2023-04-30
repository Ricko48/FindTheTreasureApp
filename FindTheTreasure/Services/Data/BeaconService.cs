using FindTheTreasure.Models;

namespace FindTheTreasure.Services.Data
{
    public class BeaconService
    {
        private readonly List<BeaconModel> knownBeacons = new List<BeaconModel>
        {
            new BeaconModel
            { 
                MAC = "0C:F3:EE:B8:DD:0A",
                Name = "EMBeacon54828",
                SerialNumber = "5350336951052"
            },
        };

        public List<BeaconModel> GetAll()
        {
            //simulates a DB call
            return knownBeacons;
        }
    }
}
