using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.View.Background
{
    public class Scrolling : Background
    {
        public Scrolling() { }
        public void Update()
        {
            rect.X -= 3;
        }
    }
}
