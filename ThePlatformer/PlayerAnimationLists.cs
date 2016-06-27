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
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu1);
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu2);
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu3);
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu4);
            return list;
        }
    }
}
