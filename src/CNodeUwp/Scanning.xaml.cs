using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;
using ZXing.Common;

namespace CNodeUwp
{
    public sealed partial class Scanning : Page
    {
        private readonly DisplayRequest _displayRequest = new DisplayRequest();

        private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();

        private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();

        private MediaCapture _mediaCapture;

        public static Size MaxSizeSupported = new Size(4000, 3000);

        private Result _result;

        private bool IsBusy;

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

                _mediaCapture = new MediaCapture();
                var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);

                if (cameraDevice == null)
                {
                    Debug.WriteLine("No camera device found!");
                    return;
                }
                var settings = new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Video,
                    MediaCategory = MediaCategory.Other,
                    AudioProcessing = Windows.Media.AudioProcessing.Default,
                    VideoDeviceId = cameraDevice.Id
                };
                await _mediaCapture.InitializeAsync(settings);
                VideoCapture.Source = _mediaCapture;
                _mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                await _mediaCapture.StartPreviewAsync();
                //var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                //MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                //{
                //    VideoDeviceId = devices[0].Id
                //}; // 0 => front, 1 => back

                //await _mediaCapture.InitializeAsync(settings);

                //VideoEncodingProperties resolutionMax = null;
                //int max = 0;
                //var resolutions = _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

                //for (var i = 0; i < resolutions.Count; i++)
                //{
                //    VideoEncodingProperties res = (VideoEncodingProperties)resolutions[i];
                //    if (res.Width * res.Height > max)
                //    {
                //        max = (int)(res.Width * res.Height);
                //        resolutionMax = res;
                //    }
                //}

                //await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
                //capturePreview.Source = _mediaCapture;
                ////RegisterOrientationEventHandlers();
                //_mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                //await _mediaCapture.StartPreviewAsync();
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

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.Tick += _timer_Tick;
                timer.Start();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void _timer_Tick(object sender, object e)
        {
            try
            {
                Debug.WriteLine(@"[INFO]开始扫描 -> " + DateTime.Now.ToString());
                if (!IsBusy)
                {
                    IsBusy = true;
                    IRandomAccessStream stream = new InMemoryRandomAccessStream();
                    await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);

                    var writeableBmp = await ReadBitmap(stream, ".jpg");

                    await Task.Factory.StartNew(async () => { await ScanBitmap(writeableBmp); });
                }
                IsBusy = false;
                await Task.Delay(50);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                IsBusy = false;
            }
        }

        /// <summary>  
        /// 解析二维码图片  
        /// </summary>  
        /// <param name="writeableBmp">图片</param>  
        /// <returns></returns>  
        private async Task ScanBitmap(WriteableBitmap writeableBmp)
        {
            try
            {
                var barcodeReader = new BarcodeReader()
                {
                    AutoRotate = true,
                    Options = new DecodingOptions
                    {
                        TryHarder = true,
                        PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
                    },
                };
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _result = barcodeReader.Decode(writeableBmp);
                });



                if (_result != null)
                {
                    Debug.WriteLine(@"[INFO]扫描到二维码:{result}   ->" + _result.Text);
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        tbkResult.Text = _result.Text;
                    });
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 读取照片流 转为WriteableBitmap给二维码解码器
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async static Task<WriteableBitmap> ReadBitmap(IRandomAccessStream fileStream, string type)
        {
            WriteableBitmap bitmap = null;
            try
            {
                Guid decoderId = DecoderIDFromFileExtension(type);

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream);
                BitmapTransform tf = new BitmapTransform();

                uint width = decoder.OrientedPixelWidth;
                uint height = decoder.OrientedPixelHeight;
                double dScale = 1;

                if (decoder.OrientedPixelWidth > MaxSizeSupported.Width || decoder.OrientedPixelHeight > MaxSizeSupported.Height)
                {
                    dScale = Math.Min(MaxSizeSupported.Width / decoder.OrientedPixelWidth, MaxSizeSupported.Height / decoder.OrientedPixelHeight);
                    width = (uint)(decoder.OrientedPixelWidth * dScale);
                    height = (uint)(decoder.OrientedPixelHeight * dScale);

                    tf.ScaledWidth = (uint)(decoder.PixelWidth * dScale);
                    tf.ScaledHeight = (uint)(decoder.PixelHeight * dScale);
                }


                bitmap = new WriteableBitmap((int)width, (int)height);

                PixelDataProvider dataprovider = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, tf,
                    ExifOrientationMode.RespectExifOrientation, ColorManagementMode.DoNotColorManage);
                byte[] pixels = dataprovider.DetachPixelData();

                Stream pixelStream2 = bitmap.PixelBuffer.AsStream();

                pixelStream2.Write(pixels, 0, pixels.Length);
            }
            catch
            {
            }

            return bitmap;
        }

        static Guid DecoderIDFromFileExtension(string strExtension)
        {
            Guid encoderId;
            switch (strExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    encoderId = BitmapDecoder.JpegDecoderId;
                    break;
                case ".bmp":
                    encoderId = BitmapDecoder.BmpDecoderId;
                    break;
                case ".png":
                default:
                    encoderId = BitmapDecoder.PngDecoderId;
                    break;
            }
            return encoderId;
        }

        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }
    }
}
