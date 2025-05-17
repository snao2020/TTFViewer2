using System;
using System.ComponentModel;

namespace TTFViewer.View
{
    public struct Range
    {
        public UInt32 Start { get; }
        public UInt32 End { get; }

        
        public Range(UInt32 start, UInt32 end)
        {
            Start = start;
            End = end;
        }

        public static Range Empty = new Range(0, 0);
    }
    
    
    public class SelectedRangeChangedEventArgs : EventArgs
    {
        public Range Range { get; }
        public bool Handled { get; set; }

        public SelectedRangeChangedEventArgs(Range range)
        {
            Range = range;
            Handled = false;
        }
    }


    public class SelectedRange : INotifyPropertyChanged
    {
        public static event EventHandler<SelectedRangeChangedEventArgs> SelectedRangeChanged;

        public static void RaiseSelectedRangeChanged(Range range)
        {
            SelectedRangeChanged?.Invoke(null, new SelectedRangeChangedEventArgs(range));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        Range _Range;
        public Range Range
        {
            get => _Range;
            set
            {
                if(!value.Equals(_Range))
                {
                    _Range = value;
                    RaisePropertyChanged("Range");
                }
            }
        }
    }
}
