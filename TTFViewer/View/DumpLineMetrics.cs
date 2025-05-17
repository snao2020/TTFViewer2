using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TTFViewer.View
{
    public class DumpLineMetrics : INotifyPropertyChanged
    {
        public event EventHandler MetricsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static string BridgeString { get; } = "-";

        double _PositionWidth;
        public double PositionWidth
        {
            get => _PositionWidth;
            set
            {
                if (value != _PositionWidth)
                {
                    _PositionWidth = value;
                    RaisePropertyChanged("PositionWidth");
                }
            }
        }

        double _HexWidth;
        public double HexWidth
        {
            get => _HexWidth;
            set
            {
                if (value != _HexWidth)
                {
                    _HexWidth = value;
                    RaisePropertyChanged("HexWidth");
                }
            }
        }

        double _BridgeWidth;
        public double BridgeWidth
        {
            get => _BridgeWidth;
            set
            {
                if(value != _BridgeWidth)
                {
                    _BridgeWidth = value;
                    RaisePropertyChanged("BridgeWidth");
                }
            }
        }


        double _AsciiWidth;
        public double AsciiWidth
        {
            get => _AsciiWidth;
            set
            {
                if (value != _AsciiWidth)
                {
                    _AsciiWidth = value;
                    RaisePropertyChanged("AsciiWidth");
                }
            }
        }


        double _Height;
        public double Height
        {
            get => _Height;
            set
            {
                if (value != _Height)
                {
                    _Height = value;
                    RaisePropertyChanged("Height");
                }
            }
        }


        public void Load(Control control)
        {
            DependencyPropertyDescriptor.FromProperty(Control.FontFamilyProperty, typeof(Control))
                                        .AddValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontStyleProperty, typeof(Control))
                                        .AddValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontWeightProperty, typeof(Control))
                                        .AddValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontSizeProperty, typeof(Control))
                                        .AddValueChanged(control, OnFontChanged);

            OnFontChanged(control, EventArgs.Empty);
        }

        public void Unload(Control control)
        {
            DependencyPropertyDescriptor.FromProperty(Control.FontSizeProperty, typeof(Control))
                                        .RemoveValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontWeightProperty, typeof(Control))
                                        .RemoveValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontStyleProperty, typeof(Control))
                                        .RemoveValueChanged(control, OnFontChanged);
            DependencyPropertyDescriptor.FromProperty(Control.FontFamilyProperty, typeof(Control))
                                        .RemoveValueChanged(control, OnFontChanged);

        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            if (sender is Control control)
            {           
                double height = 0.0;
                char hexChar = ' ';
                double w = 0;
                for (int i = 0; i < 16; i++)
                {
                    string str = i.ToString("X");
                    Size size = MeasureTextSize(control, str);
                    if (size.Width > w)
                    {
                        hexChar = str[0];
                        w = size.Width;
                    }
                    height = Math.Max(height, size.Height);
                }

                PositionWidth = MeasureTextSize(control, new string(hexChar, 8) + ":").Width;
                HexWidth = MeasureTextSize(control, new string(hexChar, 2)).Width;

                BridgeWidth = MeasureTextSize(control, BridgeString).Width;

                double asciiWidth = 0;
                for (int i = 0x20; i < 0x7f; i++)
                {
                    Size size = MeasureTextSize(control, ((Char)i).ToString());
                    asciiWidth = Math.Max(asciiWidth, size.Width);
                    height = Math.Max(height, size.Height);
                }
                AsciiWidth = asciiWidth;
                Height = height;
                //LineHeight = height + 2;
                //LineWidth = Position + 2 + 12 * 2 + 16 * (Hex + 2) + 16 * (Ascii + 2);
                MetricsChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        static Size MeasureTextSize(Control control, string text)
        {
            Typeface typeFace = new Typeface(control.FontFamily,
                control.FontStyle, control.FontWeight, control.FontStretch);
            double pixelsPerDip = VisualTreeHelper.GetDpi(control).PixelsPerDip;

            FormattedText ft = new FormattedText(
                text, CultureInfo.CurrentCulture, control.FlowDirection,
                typeFace, control.FontSize, Brushes.Black, pixelsPerDip);

            return new Size(ft.Width, ft.Height);
        }
    }
}
