using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer
{
    static class PlayerAnimationLists
    {
        static List<String> idlePlayer;
        static List<String> setIdleList()
        {
            idlePlayer = new List<string>();
           // idlePlayer.Add();

            return idlePlayer;
        }
        public static List<String> drawMenuStart()
        {
            List<String> lista1 = new List<String>();
            
            return lista1;
        }
        public static List<String> getTestList()
        {
            List<String> list = new List<String>();
            list.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002);
            list.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0003);
            list.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0004);
            list.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0005);
            return list;
        }
    }
}
