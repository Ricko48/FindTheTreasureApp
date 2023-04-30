using Android.Locations;

namespace FindTheTreasure.Services.GPS
{
    public class AndroidGPSFeatureService
    {
        private readonly LocationManager androidLocationManager;
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
    }
}
