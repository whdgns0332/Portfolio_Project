using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7
{
    public static class MapArrays
    {
        // 0 = 빈 공간,   1 = 벽,   2 = 공,   3 = 목적지 위의 공,   4 = 목적지,   5 = 플레이어

        public static int[,] map1 = new int[5, 5]
        {
            {1, 1, 1, 1, 1 },
            {1, 0, 2, 4, 1 },
            {1, 0, 0, 0, 1 },
            {1, 5, 0, 0, 1 },
            {1, 1, 1, 1, 1 }
        };

        public static int[,] map2 = new int[6, 7]
        {
            {1, 1, 1, 1, 1, 1, 1 },
            {1, 0, 0, 1, 0, 4, 1 },
            {1, 0, 2, 1, 0, 4, 1 },
            {1, 0, 2, 0, 0, 0, 1 },
            {1, 5, 0, 1, 0, 0, 1 },
            {1, 1, 1, 1, 1, 1, 1 }
        };


        public static int[,] map3 = new int[10, 10]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {1, 5, 0, 1, 0, 0, 0, 0, 0, 1 },
            {1, 0, 2, 1, 0, 2, 0, 0, 0, 1 },
            {1, 0, 2, 0, 0, 2, 2, 0, 0, 1 },
            {1, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
            {1, 1, 1, 1, 0, 0, 1, 0, 1, 1 },
            {1, 4, 4, 0, 0, 0, 1, 0, 0, 1 },
            {1, 4, 4, 0, 0, 0, 1, 2, 0, 1 },
            {1, 4, 4, 0, 0, 0, 1, 0, 0, 1 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        public static List<int[,]> MapList = new List<int[,]>{ map1, map2, map3 };
    }
}
