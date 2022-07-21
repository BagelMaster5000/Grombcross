using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models
{
    public class Block
    {
        public enum BlockState { EMPTY, FILLED, MARKED }
        public BlockState State { get; set; }

        public void PlaceBlock() { } // TODO
        public void MarkBlock() { } // TODO
        public void ClearBlock() { } // TODO
    }
}
