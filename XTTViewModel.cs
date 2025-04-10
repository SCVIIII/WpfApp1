using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1;
using WpfApp1.Dto;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using static System.Net.Mime.MediaTypeNames;
using IFOXSQLiteCodes01;

namespace WpfApp1
{
    public partial  class XTTViewModel : ObservableObject
    {

        //combox的数据源
        //单相三相
        [ObservableProperty]
        private List<string> _itemsDansanxiang;
        //套管类型 地上JDG,地下SC
        [ObservableProperty]
        private List<string> _itemsTaoguan;
        //导体类型 电线、电缆、消防
        [ObservableProperty]
        private List<string> _itemsCableType;

        //测试回路dto
        private ObservableCollection<DatagridHuiluDto> _datadridhuilus;
        public ObservableCollection<DatagridHuiluDto> DatagridHuilu
        {
            get => _datadridhuilus;
            set => SetProperty(ref _datadridhuilus, value);  // SetProperty 自动触发通知
        }

        public  XTTViewModel()
        {
            ItemsCableType = PUBData.PUBCreateDatas.Cre_CableType_Dianxian_And_Dianlan();
            ItemsDansanxiang = PUBData.PUBCreateDatas.Cre_CableType_Danxiang_And_Sanxiang();
            ItemsTaoguan = PUBData.PUBCreateDatas.Cre_Taoguan_Type();

            // 初始化命令，使用 RelayCommand<DatagridHuiluDto> 来传递参数
            OnButtonClick = new RelayCommand<DatagridHuiluDto>(Test2); // 初始化命令
            DatagridHuilu = new ObservableCollection<DatagridHuiluDto>
            {
                new DatagridHuiluDto { IdGuihao = "1AL1", IdHuilu = "N1",Pe=1 ,Izd = 16, PurposeType = "照明",Cos=0.9,VE="30mA", CableType="WDZ-BYJ",L123="L123", Phase="1P", TaoguanType="SC",Fushe="MR/WC/CC"},
                new DatagridHuiluDto { IdGuihao = "1AL1", IdHuilu = "m1",Pe=1 ,Izd = 25, PurposeType = "插座", CableType="BTLY",L123="L123"},
                new DatagridHuiluDto { IdGuihao = "1AL1", IdHuilu = "N1", Pe = 1, Izd = 30, PurposeType = "照明", CableType = "BTLY", L123 = "L123" },
                new DatagridHuiluDto { IdGuihao = "1AL4", IdHuilu = "N1", Pe = 1, Izd = 25, PurposeType = "插座", CableType = "BTLY", L123 = "L123" },
            };

            var test01 = new DatagridHuiluDto { IdGuihao = "3AP1", IdHuilu = "N1", Pe = 1, Izd = 25, PurposeType = "照明", Cos = 0.9, VE = "30mA", CableType = "WDZ-BYJ", L123 = "L123", Phase = "单相", TaoguanType = "SC" };
            var test02 = IFOXSQLiteCodes01.Query.SQLQueryCable.SQL_Query_Cable01(25);
            test01.Fushe = test02.Byj380 + test02.Scbyj380;
            DatagridHuilu.Add(test01);

            #region 2025年4月10日新增：回路分析与处理函数，涉及两个项目的class调整，错误极多





            #endregion


        }


        public RelayCommand<DatagridHuiluDto> OnButtonClick { get; }

        private void Test2(DatagridHuiluDto item)
        {
            item.IQueryCable(); // 调用查询方法
            if (item.IsChecked)
            {
                MessageBox.Show($"勾选框已选中，第三列内容: {item.IdGuihao}");
            }
            else
            {
                int index = DatagridHuilu.IndexOf(item) + 1;
                MessageBox.Show($"勾选框未选中，行号: {index}");
            }
        }

    }

    public partial class DatagridHuiluDto : XTTHuiluDto
    {
        private bool _isChecked = false;
        private string _buttonText = "按钮";

        public bool IsChecked
        {
            get => _isChecked;

            //set => SetProperty(ref _isChecked, value);  // 自动触发PropertyChanged
            set
            {
                if (SetProperty(ref _isChecked, value))  // 确保 SetProperty 正常触发通知
                {
                    MessageBox.Show($"勾选框状态已更改: {value}");
                    // 你可以在这里执行其他逻辑，例如确保属性更新后重新同步界面
                }
            }
        }



        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);  // 自动触发PropertyChanged
        }

    }


}




