using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Graph
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ColorRGB thiscolor;
        Color clr;
        bool editing = false;
        public MainWindow()
        {
            InitializeComponent();
            thiscolor = new ColorRGB();
            thiscolor.Red = 0;
            thiscolor.Green = 0;
            thiscolor.Blue = 0;
            


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.Strokes.Clear();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string pathFile = @"C:\Users\Костя\Desktop\Photo\Photo.gif";
            MemoryStream ms = new MemoryStream();

            FileStream file = new FileStream(pathFile, FileMode.Create);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas1.Width, (int)inkCanvas1.Height, 96, 96, PixelFormats.Default);
            rtb.Render(inkCanvas1);

            GifBitmapEncoder gifEnc = new GifBitmapEncoder();
            gifEnc.Frames.Add(BitmapFrame.Create(rtb));
            gifEnc.Save(ms);
            ms.Close();
            MessageBox.Show("Файл сохранен, " + pathFile);
        }

        private void Blue_Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            thiscolor.Red = Convert.ToByte(Red_Color.Value);
            thiscolor.Blue = Convert.ToByte(Blue_Color.Value);
            thiscolor.Green = Convert.ToByte(Green_Color.Value);
            clr = Color.FromRgb(thiscolor.Red, thiscolor.Blue, thiscolor.Green);
            this.inkCanvas1.DefaultDrawingAttributes.Color = clr;
            Label1.Background = new SolidColorBrush(clr);

        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            
            if (!editing)
            {
                inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
                editing = true;
            }
            else 
            {
                inkCanvas1.EditingMode = InkCanvasEditingMode.None;
                inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
                editing = false;
            }

        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            var send = sender as Button;
            if (send != null) inkCanvas1.DefaultDrawingAttributes.Color = ((SolidColorBrush)(send.Background)).Color;


        }
    }
    class ColorRGB
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }


}
