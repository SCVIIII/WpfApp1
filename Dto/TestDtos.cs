using CommunityToolkit.Mvvm.ComponentModel;
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
        //[ObservableProperty] private bool _isChecked=false; //是否选中

        //MVVM自动生成的属性
        [ObservableProperty] private string _idGuihao = string.Empty; //所属的配电箱名称
        [ObservableProperty] private string _idHuilu = string.Empty; //回路编号
        [ObservableProperty] private double _in;                      //整定电流

        [ObservableProperty] private string _cableType = string.Empty;
        [ObservableProperty] private string _cable = string.Empty;
        [ObservableProperty] private double _cableCSA = 2.5;



        //回路信息
        [ObservableProperty] private string _purpose = string.Empty; // 用途
        [ObservableProperty] private string _blkName = string.Empty; // 所需插入的块名
        [ObservableProperty] private string _blkVis = string.Empty;  // 所需插入块的可见性

        [ObservableProperty] private string _circuitBreaker_In = string.Empty;     //微断型号/塑壳额定电流
        [ObservableProperty] private string _circuitBreaker_Shell = string.Empty;  //塑壳壳架
        [ObservableProperty] private int _circuitBreaker_Mode = 0; //保护开关类型 0--微断 1--RCB0 2--塑壳  3--消防MA  4--负荷开关  5--ATS(4,5不应出现在小系统中)
        [ObservableProperty] private string _l123 = string.Empty; // 相序
        [ObservableProperty] private bool _isInsert = false;      // 此回路是否可以插入
        [ObservableProperty] private string _vE = string.Empty;   // 剩余电流保护器
        [ObservableProperty] private string _phase = string.Empty;  //单相/三相

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
            set => SetProperty(ref _pe, Math.Round(value, 1)); // 限定为一位小数
        }

        public double Kx
        {
            get => _kx;
            set => SetProperty(ref _kx, Math.Round(value, 1)); // 限定为一位小数
        }
        public double Cos
        {
            get => _cos;
            set => SetProperty(ref _cos, Math.Round(value, 1)); // 限定为一位小数
        }

        /// <summary>
        /// 计算电流（只读属性）
        /// </summary>
        public double Ijs => Math.Round(Pe * Kx / Cos / 0.38 / 1.732, 1); // Ijs 计算为一位小数


    }
}
