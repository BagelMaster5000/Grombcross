using System.ComponentModel;

namespace Grombcross.Models {
    public class Block : INotifyPropertyChanged {
        public enum BlockState { EMPTY, FILLED, X, QUESTION }
        public BlockState State { get; set; }
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
