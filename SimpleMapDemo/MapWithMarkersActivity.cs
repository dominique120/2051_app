using System;

using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Graphics;

namespace SimpleMapDemo
{
    public class HideAndShowKeyboard
    {
         //Shows the soft keyboard
        public void showSoftKeyboard(Android.App.Activity activity, View view)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            view.RequestFocus();
            inputMethodManager.ShowSoftInput(view, 0);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);//personal line added
        }

         // Hides the soft keyboard
        public void hideSoftKeyboard(Android.App.Activity activity)
        {
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }

    [Activity(Label = "@string/activity_label_mapwithmarkers")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback
    {
        static readonly LatLng start_point = new LatLng(-12.072237, -76.914285);

        static readonly LatLng end_point_1_RF = new LatLng(-12.082285, -76.935592);
        static readonly LatLng end_point_2_JP = new LatLng(-12.071533, -76.956535);
        static readonly LatLng end_point_3_GL = new LatLng(-12.075992, -76.958327);

        Android.Widget.Button animateToLocationButton;
        GoogleMap googleMap;


        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;
            AddMarkersToMap();
            //animateToLocationButton.Click += AnimateToPasschendaele;

            PolylineOptions RF_polyline = new PolylineOptions()
                                .Add(new LatLng(-12.082553, -76.934275))
                                .Add(new LatLng(-12.082270, -76.935533))
                                .Add(new LatLng(-12.082287, -76.935816))
                                .Add(new LatLng(-12.084463, -76.938913))
                                ;
            

            PolylineOptions GL_polyline = new PolylineOptions()
                                .Add(new LatLng(-12.075163, -76.957738))
                                .Add(new LatLng(-12.075981, -76.958245))
                                .Add(new LatLng(-12.075745, -76.959329))
                                .Add(new LatLng(-12.075750, -76.960557))
                                ;
            

            PolylineOptions JP_polyline = new PolylineOptions()
                                .Add(new LatLng(-12.070950, -76.955305))
                                .Add(new LatLng(-12.071475, -76.956480))
                                .Add(new LatLng(-12.071868, -76.957333))
                                .Add(new LatLng(-12.073699, -76.961399))
                                ;

            RF_polyline.InvokeColor(Color.Orange);
            JP_polyline.InvokeColor(Color.Red);
            GL_polyline.InvokeColor(Color.Green);

            RF_polyline.InvokeWidth(35.0f);
            JP_polyline.InvokeWidth(35.0f);
            GL_polyline.InvokeWidth(35.0f);



            map.AddPolyline(JP_polyline);
            map.AddPolyline(RF_polyline);
            map.AddPolyline(GL_polyline);
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            var mapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            animateToLocationButton = FindViewById<Android.Widget.Button>(Resource.Id.animateButton);
            animateToLocationButton.Click += AnimateToPasschendaele;

        }

        


        Random pos_rnd = new Random();
        int prevnum = -1;
        int pos_i;

        void AnimateToPasschendaele(object sender, EventArgs e)
        {
            
            var builder = CameraPosition.InvokeBuilder();
            HideAndShowKeyboard hideAndShowKeyboard = new HideAndShowKeyboard();

            do
            {
                pos_i = pos_rnd.Next(1,4);
            } while (pos_i == prevnum);

            prevnum = pos_i;


            if (pos_i == 1)
            {
                hideAndShowKeyboard.hideSoftKeyboard(this);

                builder.Target(end_point_1_RF);
                builder.Zoom(18);
                builder.Bearing(250);
                builder.Tilt(70);
                var cameraPosition = builder.Build();

                googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));

                var loc_marker = new MarkerOptions();
                loc_marker.SetPosition(end_point_1_RF)
                                    .SetSnippet("En promedio 5 minutos ahorrados")
                                   .SetTitle("Utilize la Av. Raul Ferrero");
                googleMap.AddMarker(loc_marker);

                Marker marker = googleMap.AddMarker(loc_marker);
                marker.ShowInfoWindow();
            } 
            else if (pos_i == 2)
            {
                hideAndShowKeyboard.hideSoftKeyboard(this);

                builder.Target(end_point_2_JP);
                builder.Zoom(18);
                builder.Bearing(270);
                builder.Tilt(70);
                var cameraPosition = builder.Build();

                googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));

                var loc_marker = new MarkerOptions();
                loc_marker.SetPosition(end_point_2_JP)
                                    .SetSnippet("En promedio 2 minutos ahorrados")
                                   .SetTitle("Utilize la Av. Javier Prado");
                googleMap.AddMarker(loc_marker);

                Marker marker = googleMap.AddMarker(loc_marker);
                marker.ShowInfoWindow();
            }
            else if (pos_i == 3)
            {
                hideAndShowKeyboard.hideSoftKeyboard(this);

                builder.Target(end_point_3_GL);
                builder.Zoom(18);
                builder.Bearing(210);
                builder.Tilt(70);
                var cameraPosition = builder.Build();

                googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));

                var loc_marker = new MarkerOptions();
                loc_marker.SetPosition(end_point_3_GL)
                                    .SetSnippet("En promedio 7 minutos ahorrados")
                                   .SetTitle("Utilize la Av. Golf los Incas");
                googleMap.AddMarker(loc_marker);

                Marker marker = googleMap.AddMarker(loc_marker);
                marker.ShowInfoWindow();
            }
        }

        void AddMarkersToMap()
        {
            var my_location = new MarkerOptions();
            my_location.SetPosition(start_point)
                      .SetTitle("Mi Posicion Actual")
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(my_location);

            Marker marker = googleMap.AddMarker(my_location);
            marker.ShowInfoWindow();

            // We create an instance of CameraUpdate, and move the map to it.
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(start_point, 15);
            googleMap.MoveCamera(cameraUpdate);
        }
    }
}