using Android.Locations;
using Android.Net.Wifi.Rtt;
using FindTheTreasure.Models;

namespace FindTheTreasure.Services.GPS
{
    public class AndroidGPSFeatureService
    {
        private readonly LocationManager androidLocationManager;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public AndroidGPSFeatureService(LocationManager androidLocationManager)
        {
            this.androidLocationManager = androidLocationManager;
        }

        public bool IsGPSOn()
        {
            //available providers are "gps", "network", "passive" (the other ones than GPS can be used to determine e.g. user's city on an e-shop)
            bool providerEnabled = androidLocationManager.IsProviderEnabled(LocationManager.GpsProvider); //?
            //bool locationEnabled = androidLocationManager.IsLocationEnabled; //returns true when GPS provider returns true and we want location from GPS. 
            return providerEnabled;
        }

        public async Task<LocationModel> GetCachedLocation()
        {
            try
            {
                Microsoft.Maui.Devices.Sensors.Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                    return new LocationModel
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };           
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }

        public async Task<LocationModel> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Microsoft.Maui.Devices.Sensors.Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    return new LocationModel
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
            return null;
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
