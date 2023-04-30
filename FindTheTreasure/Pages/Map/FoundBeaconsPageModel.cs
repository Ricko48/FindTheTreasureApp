using FindTheTreasure.Models;

namespace FindTheTreasure.Pages.Map
{
    public partial class FoundBeaconsPageModel
    {

        public FoundBeaconsModel FoundBeaconsModel { get; set; } = new()
        {
            Beacons = new()
            {
                new BeaconModel
                    { Discovered = DateTime.Now, MAC = "mac", Name = "name", SerialNumber = "123", Number = 1 }
            }
        };

        public FoundBeaconsPageModel()
        {

        }


    }
}
