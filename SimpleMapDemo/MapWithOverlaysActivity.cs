using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace SimpleMapDemo
{
    [Activity(Label = "@string/activity_label_mapwithoverlays")]
    public class MapWithOverlaysActivity : AppCompatActivity, IOnMapReadyCallback
    {
        static readonly LatLng InMaui = new LatLng(20.72110, -156.44776);
        static readonly LatLng LeaveFromHereToMaui = new LatLng(58.768410, -94.164963);

        static readonly LatLng[] LocationForCustomIconMarkers =
        {
            new LatLng(40.741773, -74.004986),
            new LatLng(41.051696, -73.545667),
            new LatLng(41.311197, -72.902646)
        };

        GoogleMap googleMap;
        MapFragment mapFragment;

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;
            googleMap.MyLocationEnabled = false;

          

            // Animate the move on the map so that it is showing the markers we added above.
            googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(LocationForCustomIconMarkers[1], 2));

            // Setup a handler for when the user clicks on a marker.
            
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MapWithOverlayLayout);

            mapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }


        protected override void OnPause()
        {
            // Pause the GPS - we won't have to worry about showing the 
            // location.
            googleMap.MyLocationEnabled = false;

            //googleMap.MarkerClick -= MapOnMarkerClick;
            googleMap.InfoWindowClick -= HandleInfoWindowClick;

            base.OnPause();
        }




        /// <summary>
        ///     Add three markers to the map.
        /// </summary>


        void HandleInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var circleOptions = new CircleOptions();
            circleOptions.InvokeCenter(InMaui);
            circleOptions.InvokeRadius(100.0);
        }



    }
}
