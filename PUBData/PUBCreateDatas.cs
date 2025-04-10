using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.PUBData
{
    //public class PUBCreateDatas
    //{
    //    /// <summary>
    //    /// 以下类型的导体为电线
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<string> CreateListCableType_Dianxian()
    //    {
    //        List<string> list = new List<string> { "WDZ-BYJ", "WDZN-BYJ", "WDZN-RYJS", "电线" };
    //        return list;
    //    }

    //    /// <summary>
    //    /// 以下类型的导体为电缆
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<string> CreateListCableType_Dianlan()
    //    {
    //        List<string> list = new List<string> { "WDZ-YJY", "BTLY", "WDZN-YJY", "WDZN-KYJY", "电缆" };
    //        return list;
    //    }

    //    /// <summary>
    //    /// 以下类型的相位为单相
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<string> CreateListPhase_Danxiang()
    //    {
    //        List<string> list = new List<string> { "1P", "2P", "RCB0-2P", "单相" };
    //        return list;
    //    }

    //    /// <summary>
    //    /// 以下类型的相位为三相
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<string> CreateListPhase_Sanxiang()
    //    {
    //        List<string> list = new List<string> { "3P", "4P", "RCB0-4P", "三相" };
    //        return list;
    //    }


    //}


    public static class PUBCreateDatas
    {
        /// <summary>
        /// 创建回路类型
        /// 服务对象：XTTViewModel及Dto
        /// 序列：照明、插座、智能照明、空调插座、A型应急照明、子配电箱、风机控制箱、风机回路
        /// </summary>
        /// <returns></returns>
        public static List<string> Cre_Huilu_Purpose()
        {
            var list = new List<string> { "照明m", "插座c","智能照明mk","空调插座","A型应急照明","子配电箱N","风机控制箱AC","风机回路AC" };
            return list;
        }


        /// <summary>
        /// 创建保护套管类型
        /// 服务对象：XTTViewModel
        /// </summary>
        /// <returns></returns>
        public static List<string> Cre_Taoguan_Type()
        {
            var list = new List<string> { "JDG", "SC" };
            return list;
        }

        /// <summary>
        /// 所有的导体类型
        /// 服务对象：XTTViewModel
        /// </summary>
        /// <returns></returns>
        public static List<string> Cre_CableType_Dianxian_And_Dianlan()
        {
            var list = new List<string> { "WDZ-BYJ", "WDZ-YJY", "WDZN-BYJ", "BTLY", "WDZN-YJY", "WDZN-KYJY", "WDZN-RYJS" };
            return list;
        }

        /// <summary>
        /// 以下类型的导体为电线
        /// 服务对象：Dto
        /// </summary>
        /// <returns></returns>
        public static  List<string> CreateListCableType_Dianxian()
        {
            List<string> list = new List<string> { "WDZ-BYJ", "WDZN-BYJ", "WDZN-RYJS"};
            return list;
        }

        /// <summary>
        /// 以下类型的导体为电缆
        /// </summary>
        /// <returns></returns>
        public static  List<string> CreateListCableType_Dianlan()
        {
            List<string> list = new List<string> { "WDZ-YJY", "BTLY", "WDZN-YJY", "WDZN-KYJY" };
            return list;
        }

        /// <summary>
        /// 全部开关类型对应的相位
        /// 服务对象：XTTViewModel
        /// </summary>
        /// <returns></returns>
        public static List<string> Cre_CableType_Danxiang_And_Sanxiang()
        {
            var list = new List<string> { "1P", "2P", "RCB0-2P", "3P", "4P", "RCB0-4P" };
            return list;
        }

        /// <summary>
        /// 以下类型的相位为单相
        /// </summary>
        /// <returns></returns>
        public static  List<string> CreateListPhase_Danxiang()
        {
            List<string> list = new List<string> { "1P", "2P", "RCB0-2P" };
            return list;
        }

        /// <summary>
        /// 以下类型的相位为三相
        /// </summary>
        /// <returns></returns>
        public static  List<string> CreateListPhase_Sanxiang()
        {
            List<string> list = new List<string> { "3P", "4P", "RCB0-4P" };
            return list;
        }


    }
}
