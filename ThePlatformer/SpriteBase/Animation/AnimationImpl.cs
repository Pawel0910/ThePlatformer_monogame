﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.SpriteBase.Animation.Events;

namespace ThePlatformer.SpriteBase.Animation
{
    public class AnimationImpl : IAnimation
    {    /// <summary>
         /// <paramref name="maxFrames"/> Maximum of possible frames on one animation
         /// </summary>
        private static readonly int maxFrames = 25;
        private readonly String[] textureNames;
        private int currentFrame = 1;
        private int elapsedTime;
        public Dictionary<String, List<Texture2D>> textureDict { get; set; }
        public Dictionary<String, List<EventizerAnimationImpl>> events { get; set; }
        public Dictionary<String, int?> delays;
        public Dictionary<String, bool> animationStatus { get; set; }
        private List<Texture2D> currentAnimation;
        private String currentAnimationName;
        private Object spriteObject;
        public bool frameEnded { get; set; }

        private int framesAmount { get; set; }
        private int delayBeetwenFrames { get; set; }

        public AnimationImpl(int delayBeetwenFrames = 200, Object sprite = null, params String[] textureNames)
        {
            textureDict = new Dictionary<String, List<Texture2D>>();
            events = new Dictionary<string, List<EventizerAnimationImpl>>();
            delays = new Dictionary<string, int?>();
            animationStatus = new Dictionary<string, bool>();
            currentAnimation = new List<Texture2D>();

            this.spriteObject = sprite;
            this.textureNames = textureNames;
            this.delayBeetwenFrames = delayBeetwenFrames;
        }
        public AnimationImpl(int delayBeetwenFrames =200,Object sprite = null)
        {
            textureDict = new Dictionary<String, List<Texture2D>>();
            events = new Dictionary<string, List<EventizerAnimationImpl>>();
            delays = new Dictionary<string, int?>();
            animationStatus = new Dictionary<string, bool>();
            currentAnimation = new List<Texture2D>();

            this.spriteObject = sprite;
            this.delayBeetwenFrames = delayBeetwenFrames;
        }
        public void setFromDifferentAnimation(IAnimation differentAnim)
        {
            textureDict = differentAnim.textureDict;
            animationStatus = differentAnim.animationStatus;
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
                animationStatus.Add(textureName, false);
            }
        }
        public bool getAnimationStatus(string animationName)
        {
            return animationStatus[animationName];
        }
        public void setDelayBeetwenAnim(String animationName, int delay)
        {
            delays.Add(animationName, delay);
        }
        public void setEventOnAnimation(String textureName, String eventName, int frame)
        {
            EventizerAnimationImpl eventAnim = new EventizerAnimationImpl(frame, eventName, spriteObject);
            List<EventizerAnimationImpl> eventList;


            if (!events.ContainsKey(textureName))
            {
                eventList = new List<EventizerAnimationImpl>();
                eventList.Add(eventAnim);
                events.Add(textureName, eventList);
            }
            else
            {
                eventList = events[textureName];
                if (!isEventOnSameFrame(frame, eventList))
                {
                    eventList.Add(eventAnim);
                    events[textureName] = eventList;
                }
            }
        }
        /// <summary>
        /// Checks if event on the same frame does not repeat.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private bool isEventOnSameFrame(int frame, List<EventizerAnimationImpl> eventList)
        {
            foreach (var myEvent in eventList)
            {
                if (myEvent.EventFrame == frame)
                {
                    return true;
                }
            }
            return false;
        }
        public void setCurrentAnimation(String textureName)
        {
            if(currentAnimation != textureDict[textureName])
            {
                currentAnimation = textureDict[textureName];
                currentAnimationName = textureDict.FirstOrDefault(x => x.Value == currentAnimation).Key;
                elapsedTime = delayBeetwenFrames + 1;
                animationStatus[currentAnimationName] = false;
            }
        }
        public Texture2D changeTextureOnAnimation(GameTime gameTime)
        {
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
           
            if (elapsedTime > setDifferentDelay() && currentAnimation.Count > 0)
            {
                elapsedTime = 0;
                setFrameNumber();
                if (events.ContainsKey(currentAnimationName))
                {
                     foreach (var myEvent in events[currentAnimationName])
                    {
                        myEvent.runEvent(currentFrame);
                    }
                }
                checkIfAnimationEnded();
                return currentAnimation[currentFrame - 1];

            }
            return null;
        }
        private void checkIfAnimationEnded()
        {
            if (currentFrame == currentAnimation.Count)
            {
                animationStatus[currentAnimationName] = true;
            }
            else
            {
                animationStatus[currentAnimationName] = false;
            }

        }
        private void setFrameNumber()
        {
            if (currentFrame < currentAnimation.Count)
            {
               currentFrame++;
            }
            else
            {
                currentFrame = 1;
            }
        }
        private int setDifferentDelay()
        {
            if (currentAnimationName != null && delays.ContainsKey(currentAnimationName) && delays[currentAnimationName]!=null)
            {
               return (int)delays[currentAnimationName];
            }
            return delayBeetwenFrames;
        }
    }
}
