using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using System.Collections.Generic;
using Com.Nguyenhoanglam.Imagepicker.Model;
using AndroidX.AppCompat.Widget;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Com.Nguyenhoanglam.Imagepicker.UI.Imagepicker;
using Kotlin.Jvm.Functions;
using Java.Lang;
using System.Linq;

namespace DotnetAndroidImagePickerQs
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IFunction1
    {
        private readonly List<Image> images = new();
        private ImageAdapter imageAdapter;

        private SwitchCompat folderModeSwitch;
        private SwitchCompat singleSelectModeSwitch;
        private SwitchCompat cameraModeSwitch;
        private SwitchCompat showCameraSwitch;
        private SwitchCompat showSelectAllSwitch;
        private SwitchCompat showUnselectAllSwitch;
        private SwitchCompat showNumberIndicatorSwitch;
        private SwitchCompat enableImageTransitionSwitch;
        private Button launchPickerButton;
        private Button launchFragmentButton;
        private RecyclerView recyclerView;

        private ImagePickerLauncher launcher;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitializeComponents();

            imageAdapter = new ImageAdapter(this);
            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(imageAdapter);

            launcher = ImagePickerKt.RegisterImagePicker(this, null, this);

            launchFragmentButton.Click += LaunchFragmentButton_Click;
            launchPickerButton.Click += LaunchPickerButton_Click;
        }

        private void LaunchPickerButton_Click(object sender, System.EventArgs e)
        {
            bool folderMode = folderModeSwitch.Checked;
            bool singleSelectMode = singleSelectModeSwitch.Checked;
            bool cameraMode = cameraModeSwitch.Checked;
            bool showCamera = showCameraSwitch.Checked;
            bool selectAllEnabled = showSelectAllSwitch.Checked;
            bool unselectAllEnabled = showUnselectAllSwitch.Checked;
            bool showNumberIndicator = showNumberIndicatorSwitch.Checked;
            bool enableImageTransition = enableImageTransitionSwitch.Checked;

            ImagePickerConfig config = new()
            {
                CameraMode = cameraMode,
                SingleSelectMode = singleSelectMode,
                FolderMode = folderMode,
                ShowCamera = showCamera,
                SelectAllEnabled = selectAllEnabled,
                UnselectAllEnabled = unselectAllEnabled,
                ImageTransitionEnabled = enableImageTransition,
                SelectedIndicatorType = showNumberIndicator ? IndicatorType.Number : IndicatorType.CheckMark,
                LimitSize = 100,
                RootDirectory = RootDirectory.Dcim,
                SubDirectory = "Image Picker",
                FolderGridCount = new GridCount(2, 4),
                ImageGridCount = new GridCount(3, 5),
                SelectedImages = images,
                CustomColor = new CustomColor()
                {
                    Background = "#000000",
                    StatusBar = "#000000",
                    Toolbar = "#212121",
                    ToolbarTitle = "#FFFFFF",
                    ToolbarIcon = "#FFFFFF",
                    DoneButtonTitle = "#FFFFFF",
                    SnackBarBackground = "#323232",
                    SnackBarMessage = "#FFFFFF",
                    SnackBarButtonTitle = "#4CAF50",
                    LoadingIndicator = "#757575",
                    SelectedImageIndicator = "#1976D2"
                },
                CustomMessage = new CustomMessage()
                {
                    ReachLimitSize = "You can only select up to 10 images.",
                    CameraError = "Unable to open camera.",
                    NoCamera = "YouResource.Device has no camera.",
                    NoImage = "No image found.",
                    NoPhotoAccessPermission = "Please allow permission to access photos and media.",
                    NoCameraPermission = "Please allow permission to access camera."
                },
                CustomDrawable = new CustomDrawable()
                {
                    BackIcon = Resource.Drawable.ic_back,
                    CameraIcon = Resource.Drawable.ic_camera,
                    SelectAllIcon = Resource.Drawable.ic_select_all,
                    UnselectAllIcon = Resource.Drawable.ic_unselect_all,
                    LoadingImagePlaceholder = Resource.Drawable.img_loading_placeholder
                }
            };

            launcher.Launch(config);
        }

        private void LaunchFragmentButton_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void InitializeComponents()
        {
            folderModeSwitch = FindViewById<SwitchCompat>(Resource.Id.folderModeSwitch); ;
            singleSelectModeSwitch = FindViewById<SwitchCompat>(Resource.Id.singleSelectModeSwitch); ;
            cameraModeSwitch = FindViewById<SwitchCompat>(Resource.Id.cameraModeSwitch); ;
            showCameraSwitch = FindViewById<SwitchCompat>(Resource.Id.showCameraSwitch); ;
            showSelectAllSwitch = FindViewById<SwitchCompat>(Resource.Id.showSelectAllSwitch); ;
            showUnselectAllSwitch = FindViewById<SwitchCompat>(Resource.Id.showUnselectAllSwitch); ;
            showNumberIndicatorSwitch = FindViewById<SwitchCompat>(Resource.Id.showNumberIndicatorSwitch); ;
            enableImageTransitionSwitch = FindViewById<SwitchCompat>(Resource.Id.enableImageTransitionSwitch); ;
            launchPickerButton = FindViewById<Button>(Resource.Id.launchPickerButton); ;
            launchFragmentButton = FindViewById<Button>(Resource.Id.launchFragmentButton); ;
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView); ;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public Object Invoke(Object p0)
        {
            if (p0 is not Java.Util.ArrayList images) return p0;

            this.images.Clear();
            this.images.AddRange(images.ToEnumerable<Image>());
            imageAdapter.SetImages(this.images);

            return imageAdapter;
        }
    }
}
