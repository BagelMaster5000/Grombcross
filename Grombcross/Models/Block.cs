using System.ComponentModel;

namespace Grombcross.Models {
    public class Block : INotifyPropertyChanged {
        public enum BlockState { EMPTY, FILLED, X, QUESTION }
        private BlockState _state;
        public BlockState State {
            get { return _state; }
            private set {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
        public int Row;
        public int Column;

        public Block(int setRow, int setColumn) {
            Row = setRow;
            Column = setColumn;
        }

        public void FillBlock() => State = BlockState.FILLED;
        public void XBlock() => State = BlockState.X;
        public void QuestionBlock() => State = BlockState.QUESTION;
        public void ClearBlock() => State = BlockState.EMPTY;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
