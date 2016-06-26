using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;
using ZXing.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CNodeUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Scanning : Page
    {
        private readonly DisplayRequest _displayRequest = new DisplayRequest();

        private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();

        private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();

        private MediaCapture _mediaCapture;

        private bool _externalCamera = false;

        private bool _mirroringPreview;

        private bool _isPreviewing = true;

        private static readonly Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");

        public Scanning()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    e.Handled = true;
                }
            };
            Init();
        }

        private async Task Init()
        {
            try
            {
                _displayRequest.RequestActive();
                //_displayOrientation = _displayInformation.CurrentOrientation;

                _mediaCapture = new MediaCapture();
                var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                {
                    VideoDeviceId = devices[0].Id
                }; // 0 => front, 1 => back

                await _mediaCapture.InitializeAsync(settings);

                VideoEncodingProperties resolutionMax = null;
                int max = 0;
                var resolutions = _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

                for (var i = 0; i < resolutions.Count; i++)
                {
                    VideoEncodingProperties res = (VideoEncodingProperties)resolutions[i];
                    if (res.Width * res.Height > max)
                    {
                        max = (int)(res.Width * res.Height);
                        resolutionMax = res;
                    }
                }

                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
                capturePreview.Source = _mediaCapture;
                //RegisterOrientationEventHandlers();
                _mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                await _mediaCapture.StartPreviewAsync();
                _orientationSensor.OrientationChanged += (s, arg) =>
                {
                    switch (arg.Orientation)
                    {
                        case SimpleOrientation.Rotated90DegreesCounterclockwise:
                            _mediaCapture.SetPreviewRotation(VideoRotation.None);
                            break;
                        case SimpleOrientation.Rotated180DegreesCounterclockwise:
                        case SimpleOrientation.Rotated270DegreesCounterclockwise:
                            _mediaCapture.SetPreviewRotation(VideoRotation.Clockwise180Degrees);
                            break;
                        default:
                            _mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                            break;
                    }
                };

                TimerCallback callBack = new TimerCallback(async (o) =>
                {

                    ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
                    // create storage file in local app storage
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    "temp.jpg",
                    CreationCollisionOption.GenerateUniqueName);
                    // take photo
                    await _mediaCapture.CapturePhotoToStorageFileAsync(imgFormat, file);
                    // Get photo as a BitmapImage
                    //BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
                    WriteableBitmap bitmap = new WriteableBitmap(1920, 1080);
                    using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                    {
                        await bitmap.SetSourceAsync(fileStream);
                    }
                    //bmpImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;


                    IBarcodeReader reader = new BarcodeReader()
                    {
                        Options = new DecodingOptions()
                        {
                            PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
                        }
                    };
                    var result = reader.Decode(bitmap);
                    await new MessageDialog(result?.Text ?? "什么都没找到").ShowAsync();
                    //timer.Change(4000, Timeout.Infinite);
                });
                var timer = new Timer(callBack, null, TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(6));
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
                //await _dialogService.ShowMessage(ex.ToString(), "error");
            }
        }
    }
}
