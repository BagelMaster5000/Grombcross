using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models {
    public class HintLine : INotifyPropertyChanged {
        public string LineString { get; set; }
        public List<int> HintNumbers = new List<int>();
        public bool LineFulfilled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
