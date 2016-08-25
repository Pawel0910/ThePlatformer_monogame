using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.SpriteBase.Animation
{
    /// <summary>
    /// This class calls event(method) on specified frame in animation. 
    /// </summary>
    interface IEventizerAnimation
    {
        /// <summary>
        /// Starts event on specified frame.
        /// </summary>
        /// <param name="currentFrame"></param>
        void runEvent(int currentFrame);
    }
}
