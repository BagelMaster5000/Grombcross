using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Exceptions {
    public class InvalidPuzzlePathException : Exception {
        public InvalidPuzzlePathException() { }

        public InvalidPuzzlePathException(string message) : base(message) { }
    }
}
