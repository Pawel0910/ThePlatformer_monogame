using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.SpriteBase.Animation.Events
{
    public class EventizerAnimationImpl : IEventizerAnimation
    {
        private int eventFrame;
        private String methodToInvokeName;
        private Object spriteObject;
        public EventizerAnimationImpl(int eventFrame, String methodToInvokeName, Object sprite)
        {
            this.eventFrame = eventFrame;
            this.methodToInvokeName = methodToInvokeName;
            this.spriteObject = sprite;
        }

        public void runEvent(int currentFrame)
        {
            if (currentFrame == eventFrame)
            {
                String objectName = spriteObject.GetType().FullName;
                Type type = Type.GetType(objectName);
                MethodInfo method = type.GetMethod(methodToInvokeName);
                method.Invoke(spriteObject, null);
            }
        }
        public int EventFrame
        {
            get { return eventFrame; }
        }
    }
}
