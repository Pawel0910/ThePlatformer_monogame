using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThePlatformer.SpriteBase.Animation
{
    /// <summary>
    /// Loads all images found by prefix and create a animiation by a specified params.
    /// </summary>
    public interface IAnimation
    {
        void LoadConent(ContentManager content);

        /// <summary>
        /// Set event on specified frame.
        /// </summary>
        /// <param name="textureName">Name of given animation.</param>
        /// <param name="frame">Frame on which the event is to occur.</param>
        /// <param name="eventName">Method name which be called.</param>
        void setEventOnAnimation(String textureName, String eventName, int frame);
        void setDelayBeetwenAnim(String animationName, int delay);

        void setCurrentAnimation(String textureName);

        /// <summary>
        /// Returns null if the frame has not change.
        /// </summary>
        /// <returns></returns>
        Texture2D changeTextureOnAnimation(GameTime gameTime);
         bool frameEnded { get; set; }
        bool getAnimationStatus(string animationName);

    }

}
