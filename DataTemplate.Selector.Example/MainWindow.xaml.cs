using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;

namespace DataTemplate.Selector.Example
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PathCollection.Add("Images/Sunset.jpg");
            PathCollection.Add("I'm not an image");
            PathCollection.Add("Images/Blue hills.jpg");
            PathCollection.Add("Images/Water lilies.jpg");
            PathCollection.Add("More string action");
            PathCollection.Add("Images/Winter.jpg");

            InitializeComponent();
        }

        public ObservableCollection<string> PathCollection { get; } = new ObservableCollection<string>();
    }

    public class ImgStringTemplateSelector : DataTemplateSelector
    {
        public System.Windows.DataTemplate ImageTemplate { get; set; }
        public System.Windows.DataTemplate StringTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item,
          DependencyObject container)
        {
            String path = (string)item;
            String ext = System.IO.Path.GetExtension(path);
            if (System.IO.File.Exists(path) && ext == ".jpg")
                return ImageTemplate;
            return StringTemplate;
        }
    }

    public class RelativeToAbsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
           object parameter, CultureInfo culture)
        {
            String relative = value as string;
            if (relative == null)
                return null;
            return System.IO.Path.GetFullPath(relative);
        }

        public object ConvertBack(object value, Type targetType,
           object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
