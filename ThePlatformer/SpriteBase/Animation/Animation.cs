using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.SpriteBase.Animation
 {  /// <summary>
      /// Loads images and create a animiation by a specified params.
      /// </summary>
    public class Animation
    {/// <summary>
     /// <paramref name="maxFrames"/> Maximum of possible frames on one animation
     /// </summary>
        private static readonly int maxFrames = 25;
        private readonly String[] textureNames;
        private int framesAmount { get; set; }
        private int currentFrame = 1;
        private int delayBeetwenFrames { get; set; }
        private int elapsedTime;
        private Dictionary<String, List<Texture2D>> textureDict;
        private List<Texture2D> currentAnimation;

        /// <param name="framesAmount">Amount of frames to show. Important: frames are counted from index = 1</param>
        /// <param name="textureName">Name of texture to be loaded.</param>
        /// <param name="delayBeetwenFrames">Delay which occured beetwen following frames</param>
        public Animation(int delayBeetwenFrames = 200, params String[] textureNames)
        {
            textureDict = new Dictionary<String, List<Texture2D>>();
            currentAnimation = new List<Texture2D>();

            this.textureNames = textureNames;
            this.delayBeetwenFrames = delayBeetwenFrames;
        }
        public void LoadConent(ContentManager content)
        {
            foreach (String textureName in textureNames)
            {
                List<Texture2D> textureList = new List<Texture2D>();
                for (int i = 0; i < maxFrames; i++)
                {
                    try
                    {
                        textureList.Add(content.Load<Texture2D>(textureName + (i + 1)));
                    }
                    catch (ContentLoadException ex)
                    {
                        break;
                    }
                }
                textureDict.Add(textureName, textureList);
            }

        }

        public void setCurrentAnimation(String textureName)
        {
            currentAnimation = textureDict[textureName];
            elapsedTime = delayBeetwenFrames + 1;
        }
        /// <summary>
        /// Returns null if the frame has not change.
        /// </summary>
        /// <returns></returns>
        public Texture2D proceedAnimation(GameTime gameTime)
        {
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > delayBeetwenFrames && currentAnimation.Count > 0)
            {
                elapsedTime = 0;
                if (currentFrame < currentAnimation.Count)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame = 1;
                }
                return currentAnimation[currentFrame - 1];

            }
            return null;
        }
    }
}
