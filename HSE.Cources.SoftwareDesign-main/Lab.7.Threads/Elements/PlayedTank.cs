using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._7.Threads.Elements
{
    class PlayedTank:Tank
    {

        #region Initialize

        public PlayedTank(
            Point createdPosition
        )
            : base(
                   createdPosition,
                   new Bitmap("Resources/tankunusual.png"), 
                   -2
                  )
        { }

        #endregion
    }
}
