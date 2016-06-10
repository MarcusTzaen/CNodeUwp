using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CNodeUwp.UserControls
{
    public sealed partial class TextWithImages : UserControl
    {
        private static bool IsSetTopProperty = false;
        private static bool IsSetExcellentProperty = false;
        public bool IsTop
        {
            get { return (bool)GetValue(IsTopProperty); }
            set { SetValue(IsTopProperty, value); }
        }

        public static readonly DependencyProperty IsTopProperty = DependencyProperty.Register(
              nameof(IsTop),
              typeof(bool),
              typeof(TextWithImages),
              new PropertyMetadata(false, new PropertyChangedCallback((d, e) =>
              {
                  //if (e.OldValue != e.NewValue && !IsSetTopProperty)
                  //{
                  //    TextWithImages twi = d as TextWithImages;
                  //    bool s = (bool)e.NewValue;
                  //    int margin = 0;
                  //    if (s)
                  //    {
                  //        margin = 24;
                  //        twi.imgTopIcon.Visibility = Visibility.Visible;
                  //    }
                  //    else
                  //    {
                  //        margin = -24;
                  //    }

                  //    var excellentIconMargin = twi.imgExcellentIcon.Margin;
                  //    excellentIconMargin.Left += margin;
                  //    twi.imgExcellentIcon.Margin = excellentIconMargin;

                  //    var titleMargin = twi.tbTitle.Margin;
                  //    titleMargin.Left += margin;
                  //    twi.tbTitle.Margin = titleMargin;
                  //    IsSetTopProperty = true;
                  //}
              }))
            );

        public bool IsExcellent
        {
            get { return (bool)GetValue(IsExcellentProperty); }
            set { SetValue(IsExcellentProperty, value); }
        }

        public static readonly DependencyProperty IsExcellentProperty = DependencyProperty.Register(
              nameof(IsExcellent),
              typeof(bool),
              typeof(TextWithImages),
              new PropertyMetadata(false, new PropertyChangedCallback((d, e) =>
              {
                  //if (e.OldValue != e.NewValue && !IsSetExcellentProperty)
                  //{
                  //    TextWithImages twi = d as TextWithImages;
                  //    bool s = (bool)e.NewValue;
                  //    int margin = 0;
                  //    if (s)
                  //    {
                  //        margin = 24;
                  //        twi.imgExcellentIcon.Visibility = Visibility.Visible;
                  //    }
                  //    else
                  //    {
                  //        margin = -24;
                  //    }

                  //    var titleMargin = twi.tbTitle.Margin;
                  //    titleMargin.Left += margin;
                  //    twi.tbTitle.Margin = titleMargin;
                  //    IsSetExcellentProperty = true;
                  //}
              }))
            );


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
              nameof(Title),
              typeof(string),
              typeof(TextWithImages),
              new PropertyMetadata(null, new PropertyChangedCallback((d, e) =>
              {
                  TextWithImages twi = d as TextWithImages;
                  twi.tbTitle.Text = (string)e.NewValue;
              }))
            );

        public TextWithImages()
        {
            this.InitializeComponent();
        }
    }
}
