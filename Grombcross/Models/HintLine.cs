using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models {
    public class HintLine : INotifyPropertyChanged {
        public string HintNumbersString { get; set; } = "";
        public List<int> HintNumbers = new List<int>();

        private bool _lineFulfilled;
        public bool LineFulfilled {
            get { return _lineFulfilled; }
            set {
                _lineFulfilled = value;
                OnPropertyChanged(nameof(LineFulfilled));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
