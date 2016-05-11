using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 颜色列表
    /// </summary>
    public class ColorList
    {
        /// <summary>
        /// 颜色库
        /// </summary>
        private static string[] _list = new string[]
        { 
            "24,113,182",
            "89,80,171",
            "211,73,50",
            "20,168,130",
            "192,44,103",
            "160,82,20",
            "15,146,155",
            "30,60,136",
            "118,95,85",
            "225,46,80",
            "142,52,165",
            "30,150,65",
            "23,76,201",
            "230,76,20",
            "246,67,75",
            "20,155,222",
            "220,160,0",
            "120,138,26",
            "0,190,145",
            "114,136,160",
            "124,205,124",
            "188,238,104",
            "205,198,115",
            "238,238,209",
            "255,185,15",
            "205,149,12",
            "255,211,155",
            "255,231,186",
            "255,165,79"
        };

        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="num"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string GetColor(int num, float f)
        {
            return string.Format("rgba({0},{1})", _list[num], f);
        }
    }
}
