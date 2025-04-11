using CommunityToolkit.Mvvm.ComponentModel;
using IFOXSQLiteCodes01.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Dto;
using WpfApp1.PUBData;

namespace WpfApp1.Dto
{
    internal class TestDtos
    {
    }

    public partial class XTTHuiluDto : ObservableObject
    {

        //[ObservableProperty]
        //手动添加的属性
        //负荷计算
        private double _pe = 0;
        private double _kx = 1.0;
        private double _cos = 0.9;
        private double _ijs = 0;

        //[ObservableProperty] private bool _isChecked=false; //是否选中

        //MVVM自动生成的属性
        [ObservableProperty] private string _idGuihao = string.Empty; //所属的配电箱名称
        [ObservableProperty] private string _idHuilu = string.Empty; //回路编号
        [ObservableProperty] private double _izd;                      //整定电流

        [ObservableProperty] private string _cableType = string.Empty;
        [ObservableProperty] private string _cable = string.Empty;
        [ObservableProperty] private string _cableRuslt = string.Empty;  //最终的导体表达


        //回路信息
        [ObservableProperty] private string _purposeType = string.Empty; // 回路功能
        [ObservableProperty] private string _purposeName = string.Empty; // 回路用途
        [ObservableProperty] private string _purposeAC = string.Empty; // 风机功能功率
        [ObservableProperty] private string _blkName = string.Empty; // 所需插入的块名
        [ObservableProperty] private string _blkVis = string.Empty;  // 所需插入块的可见性

        [ObservableProperty] private string _circuitBreaker_Brand = string.Empty;     //微断型号/塑壳品牌
        [ObservableProperty] private string _circuitBreaker_Result = string.Empty;    //微断的结果/塑壳的为空
        [ObservableProperty] private string _circuitBreaker_Shell = string.Empty;  //塑壳壳架
        [ObservableProperty] private string _circuitBreaker_Type; //保护开关类型
        [ObservableProperty] private string _circuitBreaker_CD="C"; //微端曲线

        [ObservableProperty] private string _l123 = string.Empty; // 相序
        [ObservableProperty] private string _vE = string.Empty;   // 剩余电流保护器
        //[ObservableProperty] private string _phase = string.Empty;  //单相/三相

        [ObservableProperty] private bool _isVaild = false;       // 此回路是否满足校验条件
        [ObservableProperty] private string _errorInfos = string.Empty;       // 存放错误信息


        [ObservableProperty] private string _taoguanType = string.Empty;  //套管类型JDG/SG
        [ObservableProperty] private int _taoguanCSA = 0;  // 套管截面
        [ObservableProperty] private string _fushe = "SC";

        //以下为预留字段
        [ObservableProperty] private string _info1 = string.Empty;
        [ObservableProperty] private string _info2 = string.Empty;
        [ObservableProperty] private string _info3 = string.Empty;
        [ObservableProperty] private string _info4 = string.Empty;
        [ObservableProperty] private string _info5 = string.Empty;


        /// <summary>
        /// 功率
        /// </summary>
        public double Pe
        {
            get => _pe;
            set
            {
                if (SetProperty(ref _pe, Math.Round(value, 1))) // 限定为一位小数
                {
                    OnPropertyChanged(nameof(Ijs));
                }
            }
        }

        public double Kx
        {
            get => _kx;
            set
            {
                if (SetProperty(ref _kx, Math.Round(value, 1))) // 限定为一位小数
                {
                    OnPropertyChanged(nameof(Ijs));

                }
            }
        }
        public double Cos
        {
            get => _cos;
            set
            {
                if (SetProperty(ref _cos, Math.Round(value, 2))) // 限定为一位小数
                {
                    OnPropertyChanged(nameof(Ijs));
                }
            }
        }

        /// <summary>
        /// 计算电流（只读属性）
        /// </summary>
        public double Ijs => Math.Round(Pe * Kx / Cos / 0.38 / 1.732, 1); // Ijs 计算为一位小数

        /// <summary>
        /// 查询电缆截面积与保护套管
        /// 此方法依赖引用项目SQLiteCodes01才能运行
        /// </summary>
        /// <param name="Izd"></param>
        public void IQuery_Cable()
        {
            //静态类
            var list_CircuitBreaker_Type_220 = PUBData.PUBCreateDatas.Cre_CircuitBreaker_Type_220();
            var list_CircuitBreaker_Type_380 = PUBData.PUBCreateDatas.Cre_CircuitBreaker_Type_380();
            var list_cableType_dianlan = PUBData.PUBCreateDatas.CreateListCableType_Dianlan();
            var list_calbeType_dianxian = PUBData.PUBCreateDatas.CreateListCableType_Dianxian();


            var res = new SQLCableQueryClass01();
            // 1-1判断是电缆or电线
            // 1-1-1电缆
            if (list_cableType_dianlan.Contains(CableType))
            {
                //1-2判断是单相or三相
                //1-2-1单相
                if (list_CircuitBreaker_Type_220.Contains(CircuitBreaker_Type))
                {
                    //查询结果
                    res = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(Izd);
                    if (res == null) return;
                    //导体截面
                    if (!string.IsNullOrEmpty(res.Yjy220)) { Cable = res.Yjy220; } else { Cable = string.Empty; }
                    //套管截面
                    int TaoguanCSAQuery;
                    var TaoguanSQLQueryResult = res.Scyjy220;
                    if (int.TryParse(res.Scyjy220, out TaoguanCSAQuery))
                    {
                        TaoguanCSA = TaoguanCSAQuery;
                    }
                }
                //1-2-2三相
                else if (list_CircuitBreaker_Type_380.Contains(CircuitBreaker_Type))
                {
                    //查询结果
                    res = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(Izd);
                    if (res == null) return;
                    //导体截面
                    if (!string.IsNullOrEmpty(res.Yjy380)) { Cable = res.Yjy380; } else { Cable = string.Empty; }
                    //套管截面
                    int TaoguanCSAQuery;
                    var TaoguanSQLQueryResult = res.Scyjy380;
                    if (int.TryParse(res.Scyjy380, out TaoguanCSAQuery))
                    {
                        TaoguanCSA = TaoguanCSAQuery;
                    }
                }
            } //end of if (CableType == "电缆")

            // 1-1-2电线
            else if (list_calbeType_dianxian.Contains(CableType))
            {
                //1-2判断是单相or三相
                //1-2-1单相
                if (list_CircuitBreaker_Type_220.Contains(CircuitBreaker_Type))
                {
                    //查询结果
                    res = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(Izd);
                    if (res == null) return;
                    //导体截面
                    if (!string.IsNullOrEmpty(res.Byj220)) { Cable = res.Byj220; } else { Cable = string.Empty; }
                    //套管截面
                    int TaoguanCSAQuery;
                    var TaoguanSQLQueryResult = res.Scbyj220;
                    if (int.TryParse(res.Scbyj220, out TaoguanCSAQuery))
                    {
                        TaoguanCSA = TaoguanCSAQuery;
                    }
                }
                //1-2-2三相
                else if (list_CircuitBreaker_Type_380.Contains(CircuitBreaker_Type))
                {
                    //查询结果
                    res = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(Izd);
                    if (res == null) return;
                    //导体截面
                    if (!string.IsNullOrEmpty(res.Byj380)) { Cable = res.Byj380; } else { Cable = string.Empty; }
                    //套管截面
                    int TaoguanCSAQuery;
                    var TaoguanSQLQueryResult = res.Scbyj380;
                    if (int.TryParse(res.Scbyj380, out TaoguanCSAQuery))
                    {
                        TaoguanCSA = TaoguanCSAQuery;
                    }
                }
            } //end of else if(Cable == "电线)


        }

        /// <summary>
        /// 根据回路编号,设置默认的回路功能、功率
        /// </summary>
        public void Methos_CalDefalutPe()
        {
            //从标准数据中获取回路功能
            var list_purpose =PUBCreateDatas.Cre_Huilu_Purpose();
            var list_cable = PUBCreateDatas.Cre_CableType_Dianxian_And_Dianlan();
            //"WDZ-BYJ", "WDZ-YJY", "WDZN-BYJ", "BTLY", "WDZN-YJY", "WDZN-KYJY", "WDZN-RYJS"

            //普通照明回路
            if (IdHuilu.StartsWith("m") && !IdHuilu.StartsWith("mk"))
            {
                PurposeType = list_purpose[0];
                Pe = 0.3;
                Izd = 10;
                CircuitBreaker_Type = "微断1P";
                CableType = list_cable[0];
            }
            //普通插座回路
            else if (IdHuilu.StartsWith("c"))
            {
                PurposeType = list_purpose[1];
                Pe = 0.5;
                Izd = 16;
                CircuitBreaker_Type = "RCB0-2P";
                CableType = list_cable[0];

            }
            //智能照明回路
            else if (IdHuilu.StartsWith("mk"))
            {
                PurposeType = list_purpose[2];
                Pe = 0.3;
                Izd = 10;
                CircuitBreaker_Type = "微断1P";
                CableType = list_cable[0];

            }
            //空调插座回路
            else if (IdHuilu.StartsWith("k"))
            {
                PurposeType = list_purpose[3];
                Pe = 2;
                Izd = 20;
                CircuitBreaker_Type = "RCB0-2P";
                CircuitBreaker_CD = "D";
                CableType = list_cable[0];

            }
            //子配电箱回路
            else if (IdHuilu.StartsWith("N"))
            {
                PurposeType = list_purpose[4];
                Pe = 3;
                Izd = 25;
                CircuitBreaker_Type = "微断2P";
                CableType = list_cable[1];
            }
            //风机控制箱、风机回路待补充

        } //end of Methos_CalDefalutPe()


        /// <summary>
        /// 从SQLite中查询断路器的型号
        /// </summary>
        public void IQuery_CircuitBreaker()
        {
            var list_CircuitBreaker_Type_220 = PUBCreateDatas.Cre_CircuitBreaker_Type_220();
            var list_CircuitBreaker_Type_380 = PUBCreateDatas.Cre_CircuitBreaker_Type_380();
            
            var res = new SQL_CircuitBreaker_QueryClass01();

            // 断路器类型如下：var list = new List<string>
            //{
            //    "微断1P","微断2P","微断3P",
            //    "RCB0-2P", "RCB0-4P",
            //    "塑壳3P" ,"塑壳3P/MA","塑壳3P/MX+OF"
            //};
            // 1判断是单相还是三相
            // 单相回路
            if (list_CircuitBreaker_Type_220.Contains(CircuitBreaker_Type))
            {
                //2-判断开关的类型
                //单相普通MCCB 
                if(CircuitBreaker_Type== "微断1P" || CircuitBreaker_Type== "微断2P")
                {
                    res = IFOXSQLiteCodes01.Query.SQLQueryCircuitBreaker.SQL_Query_CircuitBreaker01(CircuitBreaker_Brand, Izd);
                    if (res == null) return;
                    //微断器壳架
                    if (!string.IsNullOrEmpty(res.Mccb_shell)) 
                    { 
                        CircuitBreaker_Shell = res.Mccb_shell;
                        
                        //1P的微断
                        if (CircuitBreaker_Type == "微断1P")
                        { CircuitBreaker_Result = CircuitBreaker_Shell + CircuitBreaker_CD + Izd + "/" + "1P"; }

                        //2P的微断
                        if (CircuitBreaker_Type == "微断2P")
                        { CircuitBreaker_Result = CircuitBreaker_Shell + CircuitBreaker_CD + Izd + "/" + "2P"; }
                    } 
                    else { CircuitBreaker_Shell = string.Empty; }

                }
                //单相RCB0
                else if (CircuitBreaker_Type == "RCB0-2P")
                {
                    res = IFOXSQLiteCodes01.Query.SQLQueryCircuitBreaker.SQL_Query_CircuitBreaker01(CircuitBreaker_Brand, Izd);
                    if (res == null) return;
                    //微断器壳架
                    if (!string.IsNullOrEmpty(res.Mccb_shell)) 
                    { 
                        CircuitBreaker_Shell = res.Mccb_shell;
                        CircuitBreaker_Result = CircuitBreaker_Shell + CircuitBreaker_CD + Izd + "/" + "2P" + res.Rcb0_suf;
                    }
                    else { CircuitBreaker_Shell = string.Empty; }

                    
                }
            } //end of if (list_CircuitBreaker_Type_220.Contains(CircuitBreaker_Type))

            //三相的回路
            else if (list_CircuitBreaker_Type_380.Contains(CircuitBreaker_Type))
            {
                //2-判断开关的类型
                //三相普通MCCB 
                if (CircuitBreaker_Type == "微断3P")
                {
                    res = IFOXSQLiteCodes01.Query.SQLQueryCircuitBreaker.SQL_Query_CircuitBreaker01(CircuitBreaker_Brand, Izd);
                    if (res == null) return;
                    //微断器壳架
                    if (!string.IsNullOrEmpty(res.Mccb_shell)) 
                    { 
                        CircuitBreaker_Shell = res.Mccb_shell;
                        CircuitBreaker_Result = CircuitBreaker_Shell + CircuitBreaker_CD + Izd + "/" + "3P";
                    }
                    else { CircuitBreaker_Shell = string.Empty; }
                }

                //三相RCB0
                else if (CircuitBreaker_Type == "RCB0-4P")
                {
                    res = IFOXSQLiteCodes01.Query.SQLQueryCircuitBreaker.SQL_Query_CircuitBreaker01(CircuitBreaker_Brand, Izd);
                    if (res == null) return;
                    //微断器壳架
                    if (!string.IsNullOrEmpty(res.Mccb_shell)) 
                    { 
                        CircuitBreaker_Shell = res.Mccb_shell;
                        CircuitBreaker_Result = CircuitBreaker_Shell + CircuitBreaker_CD + Izd + "/" + "4P" + res.Rcb0_suf;
                    }
                    else { CircuitBreaker_Shell = string.Empty; CircuitBreaker_Result = null;  }

                }
                // "塑壳3P" ,"塑壳3P/MA","塑壳3P/MX+OF"
                else if (CircuitBreaker_Type == "塑壳3P" || CircuitBreaker_Type == "塑壳3P/MA" || CircuitBreaker_Type == "塑壳3P/MX+OF")
                {
                    res = IFOXSQLiteCodes01.Query.SQLQueryCircuitBreaker.SQL_Query_CircuitBreaker01(CircuitBreaker_Brand, Izd);
                    if (res == null) return;
                    //微断器壳架
                    if (!string.IsNullOrEmpty(res.Mcb_shell))
                    {
                        CircuitBreaker_Shell = res.Mcb_shell;
                        //塑壳的表达
                        CircuitBreaker_Result = CircuitBreaker_Shell + "/" + Izd + "/" + "3P";

                        //继续为MA/切非回路添加附件
                        if (CircuitBreaker_Type == "塑壳3P/MA" && !string.IsNullOrEmpty(res.Mcb_ma))
                        {
                            CircuitBreaker_Result = CircuitBreaker_Result + "/" + res.Mcb_ma;
                        }
                        else if (CircuitBreaker_Type == "塑壳3P/MX+OF" && !string.IsNullOrEmpty(res.Mcb_fas))
                        {
                            CircuitBreaker_Result = CircuitBreaker_Result + "/" + res.Mcb_fas;
                        }
                    }
                    else { CircuitBreaker_Shell = string.Empty; }

                }



            }
        }
    }

    /// <summary>
    /// 定义类:存放筛选,排序,且设置好信息后的配电箱回路信息
    /// 供后续插入函数使用
    /// </summary>
    public class XTTHuiluList_Dto : ObservableObject
    {
        private string _name;
        private List<XTTHuiluDto> _listHuilus;

        /// <summary>
        /// 所属配电箱的名称
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public List<XTTHuiluDto> ListHuilus
        {
            get => _listHuilus;
            set => SetProperty(ref _listHuilus, value);
        }
    }
}






