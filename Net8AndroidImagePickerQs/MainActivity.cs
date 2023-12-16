using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using Com.Nguyenhoanglam.Imagepicker.Model;
using Com.Nguyenhoanglam.Imagepicker.UI.Imagepicker;
using Kotlin.Jvm.Functions;

namespace DotnetAndroidImagePickerQs
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IFunction0, IFunction1
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
            SetContentView(Resource.Layout.activity_main);
            InitializeComponents();

            imageAdapter = new ImageAdapter(this);
            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(imageAdapter);

            launcher = ImagePickerKt.RegisterImagePicker(this, this, this);

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

        Java.Lang.Object IFunction1.Invoke(Java.Lang.Object p0)
        {
            if (p0 is null)
            {
                return p0;
            }
            var images = p0.JavaCast<Java.Util.ArrayList>(); ;

            this.images.Clear();
            this.images.AddRange(images.ToEnumerable<Image>());
            imageAdapter.SetImages(this.images);

            return imageAdapter;
        }

        Java.Lang.Object? IFunction0.Invoke()
        {
            return this;
        }
    }
}