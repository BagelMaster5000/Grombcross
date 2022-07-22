using System.ComponentModel;

namespace Grombcross.Models {
    public class Block : INotifyPropertyChanged {
        public enum BlockState { EMPTY, FILLED, MARKED }
        public BlockState State { get; set; }

        public void FillBlock() => State = BlockState.FILLED;
        public void MarkBlock() => State = BlockState.MARKED;
        public void ClearBlock() => State = BlockState.EMPTY;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
