﻿using Android.Views;

namespace FindTheTreasure;

public class BluetoothLEPermissions : Permissions.BasePlatformPermission
{
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions
    {
        get
        {
            if(DeviceInfo.Current.VersionString.Equals("11"))
            {
                return new List<(string androidPermission, bool isRuntime)>
            {
                (Android.Manifest.Permission.Bluetooth, true),
                (Android.Manifest.Permission.AccessFineLocation, true),
                (Android.Manifest.Permission.AccessCoarseLocation, true),

            }.ToArray();
            };
            return new List<(string androidPermission, bool isRuntime)>
            {
                //(Android.Manifest.Permission.BluetoothAdmin, true),
                (Android.Manifest.Permission.BluetoothScan, true),
                (Android.Manifest.Permission.BluetoothConnect, true),
                (Android.Manifest.Permission.AccessFineLocation, true),
                //(Android.Manifest.Permission.AccessBackgroundLocation, true),
            }.ToArray();
        }
    }    
}