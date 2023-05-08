using FindTheTreasure.Services.Beacons;
using FindTheTreasure.Services.Game;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace FindTheTreasure.Pages.Games.InGame.FoundBeacons
{
    public class FoundBeaconsViewModel
    {
        private readonly BeaconsService _beaconService;
        private readonly GameService _gameService;

        public FoundBeaconsModel BeaconsModel { get; set; } = new();
        public FoundBeaconsViewModel(BeaconsService beaconService, GameService gameService)
        {
            _beaconService = beaconService;
            _gameService = gameService;
        }

        public void Refresh(Microsoft.Maui.Controls.Maps.Map map)
        {
            if (!_gameService.IsInGame())
            {
                SetMapToCurrentLocation(map);
                return;
            }

            BeaconsModel.Beacons = _beaconService.GetFoundBeaconsAsync().Result;

            SetPinsToMap(map);
            SetNewRegion(map);
        }

        private void SetPinsToMap(Microsoft.Maui.Controls.Maps.Map map)
        {
            map.Pins.Clear();

            foreach (var beacon in BeaconsModel.Beacons)
            {
                var pin = new Pin
                {
                    Label = beacon.Name,
                    Location = new Location(beacon.Latitude, beacon.Longitude),
                    Type = PinType.Place
                };

                map.Pins.Add(pin);
            }
        }

        private void SetNewRegion(Microsoft.Maui.Controls.Maps.Map map)
        {
            var locations = map.Pins.Select(x => x.Location).Append(Geolocation.Default.GetLastKnownLocationAsync().Result);
            var center = GetPinsCenter(locations);
            var radius = GetPinsRegionRadius(locations, center);
            var newRegion = MapSpan.FromCenterAndRadius(center, radius);
            map.MoveToRegion(newRegion);
        }

        private Distance GetPinsRegionRadius(IEnumerable<Location> locations, Location center)
        {
            double maxDistance = 0;

            foreach (var pinPosition in locations)
            {
                var distance = Location.CalculateDistance(center, pinPosition, DistanceUnits.Miles);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return Distance.FromMiles(maxDistance);
        }

        private Location GetPinsCenter(IEnumerable<Location> locations)
        {
            double totalLat = 0, totalLon = 0;

            foreach (var pinPosition in locations)
            {
                totalLat += pinPosition.Latitude;
                totalLon += pinPosition.Longitude;
            }

            var centerLat = totalLat / locations.Count();
            var centerLon = totalLon / locations.Count();

            return new Location(centerLat, centerLon);
        }

        private void SetMapToCurrentLocation(Microsoft.Maui.Controls.Maps.Map map)
        {
            var location = Geolocation.Default.GetLastKnownLocationAsync().Result;
            var currentLocationRegion = new MapSpan(location, 0.01, 0.01);
            map.MoveToRegion(currentLocationRegion);
        }
    }
}
