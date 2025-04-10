using CommunityToolkit.Mvvm.ComponentModel;
using IFOXSQLiteCodes01.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //回路信息
        [ObservableProperty] private string _purposeType = string.Empty; // 回路功能
        [ObservableProperty] private string _purposeName = string.Empty; // 回路用途
        [ObservableProperty] private string _purposeAC = string.Empty; // 风机功能功率
        [ObservableProperty] private string _blkName = string.Empty; // 所需插入的块名
        [ObservableProperty] private string _blkVis = string.Empty;  // 所需插入块的可见性

        [ObservableProperty] private string _circuitBreaker_Brand = string.Empty;     //微断型号/塑壳额定电流
        [ObservableProperty] private string _circuitBreaker_Shell = string.Empty;  //塑壳壳架
        [ObservableProperty] private string _circuitBreaker_Type; //保护开关类型 0--微断 1--RCB0 2--塑壳  3--消防MA  4--负荷开关  5--ATS(4,5不应出现在小系统中)

        [ObservableProperty] private string _l123 = string.Empty; // 相序
        [ObservableProperty] private string _vE = string.Empty;   // 剩余电流保护器
        [ObservableProperty] private string _phase = string.Empty;  //单相/三相

        [ObservableProperty] private bool _isVaild = false;       // 此回路是否满足校验条件
        [ObservableProperty] private string _errorInfos = string.Empty;       // 存放错误信息


        [ObservableProperty] private string _taoguanType = string.Empty;  //套管类型JDG/SG
        [ObservableProperty] private int _taoguanCSA = 0;  // 套管截面
        [ObservableProperty] private string _fushe = string.Empty; 

        //以下为预留字段
        [ObservableProperty] private string _info1 = string.Empty;
        [ObservableProperty] private string _info2 = string.Empty;
        [ObservableProperty] private string _info3 = string.Empty;
        [ObservableProperty] private string _info4 = string.Empty;
        [ObservableProperty] private string _info5 = string.Empty;
        [ObservableProperty] private string _info6 = string.Empty;
        [ObservableProperty] private string _info7 = string.Empty;
        [ObservableProperty] private string _info8 = string.Empty;
        [ObservableProperty] private string _info9 = string.Empty;
        [ObservableProperty] private string _info10 = string.Empty;


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
        /// 查询电缆表
        /// 此方法依赖引用项目SQLiteCodes01才能运行
        /// </summary>
        /// <param name="Izd"></param>
        public void IQueryCable()
        {
            //var list_phase_danxiang= new List<string> { "1P", "2P","RCB0-2P","单相" };
            //var list_phase_sanxiang = new List<string> { "3P", "4P", "RCB0-4P","三相" };
            //var list_cableType_dianlan = new List<string> { "WDZ-YJY", "BTLY",  "WDZN-YJY", "WDZN-KYJY", "电缆" };
            //var list_calbeType_dianxian = new List<string> { "WDZ-BYJ", "WDZN-BYJ", "WDZN-RYJS", "电线" };

            //非静态类
            //var list_phase_danxiang = new PUBData.PUBCreateDatas().CreateListPhase_Danxiang();
            //var list_phase_sanxiang = new PUBData.PUBCreateDatas().CreateListPhase_Sanxiang();
            //var list_cableType_dianlan = new PUBData.PUBCreateDatas().CreateListCableType_Dianlan();
            //var list_calbeType_dianxian = new PUBData.PUBCreateDatas().CreateListCableType_Dianxian();

            //静态类
            var list_phase_danxiang =PUBData.PUBCreateDatas.CreateListPhase_Danxiang();
            var list_phase_sanxiang = PUBData.PUBCreateDatas.CreateListPhase_Sanxiang();
            var list_cableType_dianlan = PUBData.PUBCreateDatas.CreateListCableType_Dianlan();
            var list_calbeType_dianxian = PUBData.PUBCreateDatas.CreateListCableType_Dianxian();


            var res= new SQLCableQueryClass01();
            // 1-1判断是电缆or电线
            // 1-1-1电缆
            if (list_cableType_dianlan.Contains(CableType))
            {
                //1-2判断是单相or三相
                //1-2-1单相
                if(list_phase_danxiang.Contains(Phase) )
                {
                    //查询结果
                    res = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(Izd);
                    if (res ==  null) return;
                    //导体截面
                    if(!string.IsNullOrEmpty(res.Yjy220)) { Cable = res.Yjy220; } else { Cable = string.Empty; }
                    //套管截面
                    int TaoguanCSAQuery;
                    var TaoguanSQLQueryResult = res.Scyjy220;
                    if (int.TryParse(res.Scyjy220, out TaoguanCSAQuery))
                    {
                        TaoguanCSA = TaoguanCSAQuery;
                    }
                }
                //1-2-2三相
                else if (list_phase_sanxiang.Contains(Phase))
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
                if (list_phase_danxiang.Contains(Phase))
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
                else if (list_phase_sanxiang.Contains(Phase))
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
    }
}
