using CNodeUwp.Common;
using CNodeUwp.Models.Topic.Version1;
using CNodeUwp.Services.Common.Version1;
using CNodeUwp.Services.Topic.Version1;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;

namespace CNodeUwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; set; }

        private IDialogService _dialogService { get; set; }

        private TopicTabType TabType { get; set; } = TopicTabType.All;

        private PaginatedCollection<TopicResponse> _topics;
        public PaginatedCollection<TopicResponse> Topics
        {
            get { return _topics; }
            set
            {
                Set(nameof(Topics), ref _topics, value);
            }
        }

        private bool _isLoadingCompleted;
        public bool IsLoadingCompleted
        {
            get { return _isLoadingCompleted; }
            set
            {
                Set(nameof(IsLoadingCompleted), ref _isLoadingCompleted, value);
            }
        }

        private MediaCapture _captureSource;
        public MediaCapture CaptureSource
        {
            get { return _captureSource; }
            set
            {
                Set(nameof(CaptureSource), ref _captureSource, value);
            }
        }

        public RelayCommand<string> FilterCommand
        {
            get
            {
                return new RelayCommand<string>((tab) =>
                           {
                               TopicTabType tabType = TopicTabType.All;
                               if (Enum.TryParse(tab, out tabType))
                               {
                                   TabType = tabType;
                                   GetTopics();
                               }

                           });
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    GetTopics();
                });
            }
        }

        public RelayCommand ScanCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(Consts.SCANNING_PAGE_KEY);
                    //try
                    //{
                    //    var captureMgr = new MediaCapture();
                    //    var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                    //    MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                    //    {
                    //        VideoDeviceId = devices[0].Id
                    //    }; // 0 => front, 1 => back

                    //    await captureMgr.InitializeAsync(settings);

                    //    VideoEncodingProperties resolutionMax = null;
                    //    int max = 0;
                    //    var resolutions = captureMgr.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

                    //    for (var i = 0; i < resolutions.Count; i++)
                    //    {
                    //        VideoEncodingProperties res = (VideoEncodingProperties)resolutions[i];
                    //        if (res.Width * res.Height > max)
                    //        {
                    //            max = (int)(res.Width * res.Height);
                    //            resolutionMax = res;
                    //        }
                    //    }

                    //    await captureMgr.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
                    //    CaptureSource = captureMgr;
                    //    await captureMgr.StartPreviewAsync();

                    //    TimerCallback callBack = new TimerCallback(async (o) =>
                    //    {

                    //        ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
                    //        // create storage file in local app storage
                    //        StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    //        "temp.jpg",
                    //        CreationCollisionOption.GenerateUniqueName);
                    //        // take photo
                    //        await captureMgr.CapturePhotoToStorageFileAsync(imgFormat, file);
                    //        // Get photo as a BitmapImage
                    //        //BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
                    //        WriteableBitmap bitmap = new WriteableBitmap(1920, 1080);
                    //        using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                    //        {
                    //            await bitmap.SetSourceAsync(fileStream);
                    //        }
                    //        //bmpImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;


                    //        IBarcodeReader reader = new BarcodeReader()
                    //        {
                    //            Options = new DecodingOptions()
                    //            {
                    //                PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
                    //            }
                    //        };
                    //        var result = reader.Decode(bitmap);

                    //        if (result != null)
                    //        {
                    //            await _dialogService.ShowMessage(result.Text, "success");

                    //        }

                    //        //timer.Change(4000, Timeout.Infinite);
                    //    });
                    //    var timer = new Timer(callBack, null, 4000, Timeout.Infinite);
                    //}
                    //catch (Exception ex)
                    //{
                    //    // This can happen if access to the camera has been revoked.
                    //    await _dialogService.ShowMessage(ex.ToString(), "error");
                    //}



                    //try
                    //{
                    //    CameraCaptureUI captureUI = new CameraCaptureUI();
                    //    captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
                    //    captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

                    //    StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                    //    if (photo == null)
                    //    {
                    //        // User cancelled photo capture
                    //        return;
                    //    }
                    //    IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.ReadWrite);
                    //    WriteableBitmap bitmap = new WriteableBitmap(1920, 1080);
                    //    await bitmap.SetSourceAsync(stream);
                    //    IBarcodeReader reader = new BarcodeReader()
                    //    {
                    //        Options = new DecodingOptions()
                    //        {
                    //            PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
                    //        }
                    //    };
                    //    var result = reader.Decode(bitmap);
                    //    if (result != null)
                    //    {
                    //        string type = result.BarcodeFormat.ToString();
                    //        string content = result.Text;
                    //        await _dialogService.ShowMessage(content, type);
                    //    }
                    //    await _dialogService.ShowMessage("nothing", "warning");
                    //}
                    //catch (UnauthorizedAccessException ex)
                    //{
                    //    // This can happen if access to the camera has been revoked.
                    //    await _dialogService.ShowMessage(ex.ToString(), "error");
                    //}
                    //catch (Exception ex)
                    //{
                    //    // This can happen if access to the camera has been revoked.
                    //    await _dialogService.ShowMessage(ex.ToString(), "error");
                    //}


                    //IBarcodeReader reader = new BarcodeReader();
                    //// load a bitmap
                    //var barcodeBitmap = (Bitmap)Bitmap.LoadFrom("C:\\sample-barcode-image.png");
                    //// detect and decode the barcode inside the bitmap
                    //var result = reader.Decode(barcodeBitmap);
                    //// do something with the result
                    //if (result != null)
                    //{
                    //    string type = result.BarcodeFormat.ToString();
                    //    string content = result.Text;
                    //}
                });
            }
        }

        public RelayCommand FeedbackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(Consts.FEEDBACK_PAGE_KEY, this);
                });
            }
        }

        public RelayCommand<ItemClickEventArgs> GoToDetailCommand
        {
            get
            {
                return new RelayCommand<ItemClickEventArgs>((args) =>
                {
                    var topicResponse = args.ClickedItem as TopicResponse;
                    _navigationService.NavigateTo(Consts.TOPIC_DETAIL_PAGE_KEY, topicResponse.Id);
                });
            }
        }

        public MainPageViewModel(
            INavigationService navigationService
            , IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            GetTopics();
        }

        private void GetTopics()
        {
            Topics = new PaginatedCollection<TopicResponse>((c) =>
            {
                return TopicService.GetTopicListAsync(new TopicPageRequest()
                {
                    Page = c,
                    Tab = TabType,
                });
            });
        }
    }
}
