using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
    public class DumpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public VirtualizingList<DumpItemViewModel> List { get; private set; }

        DumpModel Model;
        DumpItemGenerator DumpItemGenerator;

        public DumpViewModel(DumpModel model)
        {
            Model = model;
            DumpItemGenerator = new DumpItemGenerator(model, this);

            Load();
        }

        public bool IsSelected(UInt32 filePosition)
        {
            if ((filePosition & 0xFFFFFFF0) % 256 == 0 && (filePosition & 0x0F) % 2 == 0)
                return true;
            return false;
        }

        void Load()
        {
            List = null;

            List = new VirtualizingList<DumpItemViewModel>
            {
                ItemGenerator = DumpItemGenerator,
            };
        }

    }

    class DumpItemGenerator : VirtualizingList<DumpItemViewModel>.IItemGenerator
    {
        DumpViewModel DumpViewModel;
        DumpModel Model;
        int Count;
        //Range Selection;

        public DumpItemGenerator(DumpModel model, DumpViewModel vm)
        {
            DumpViewModel = vm;
            Model = model;

            UInt32 fileSize = Model.GetFileSize();
            UInt32 length = DumpItemViewModel.Length;
            Count = (int)((fileSize + length - 1) / length);
            //Selection = null;
        }

        public int GetCount()
        {
            return Count;
        }

        public DumpItemViewModel GetItem(int index)
        {
            UInt32 length = Model.GetFileSize();
            UInt32 start = (UInt32)index * DumpItemViewModel.Length;
            if(start < length)
            {
                UInt32 end = Math.Min(start + DumpItemViewModel.Length, length);
                return new DumpItemViewModel(DumpViewModel, start, end, Model);//, Selection);
            }
            return null;
        }

        public void SetItem(int index, DumpItemViewModel value)
        {
            throw new NotImplementedException();
        }
    }
}
