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
using AutoMapper;
using static MaterialDesignThemes.Wpf.Theme;
using System.ComponentModel;
using System.Windows.Data;

namespace WpfApp1
{
    public partial  class XTTViewModel : ObservableObject
    {

        #region combox的数据源
        //combox的数据源
        //单相三相
        [ObservableProperty]
        private List<string> _items_CircuitBreaker_Type;
        //套管类型 地上JDG,地下SC
        [ObservableProperty]
        private List<string> _itemsTaoguan;
        //导体类型 电线、电缆、消防
        [ObservableProperty]
        private List<string> _itemsCableType;
        //存放筛选处理后的配电箱及对应回路信息
        [ObservableProperty]
        private List<string> _items_Huilu_Purpose;
        [ObservableProperty]
        private ObservableCollection<GroupedHuilu> _groupedHuilus;

        #endregion

        private ObservableCollection<XTTHuiluList_Dto> _list_xTTHuilus;
        public ObservableCollection<XTTHuiluList_Dto> List_xTTHuilus
        {
            get => _list_xTTHuilus;
            set => SetProperty(ref _list_xTTHuilus, value);
        }



        //测试回路dto
        private ObservableCollection<DatagridHuiluDto> _datadridhuilus;
        public ObservableCollection<DatagridHuiluDto> DatagridHuilu
        {
            get => _datadridhuilus;
            set => SetProperty(ref _datadridhuilus, value);  // SetProperty 自动触发通知
        }


        //public bool? IsAllItems1Selected
        //{
        //    get
        //    {
        //        var selected = DatagridHuilu.Select(item => item.IsChecked).Distinct().ToList();
        //        return selected.Count == 1 ? selected.Single() : (bool?)null;
        //    }
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            SelectAll(value.Value, models);
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //private static void SelectAll(bool select, GroupedHuilu models)
        //{
        //    foreach (var model in models)
        //    {
        //        model.Huilus = select;
        //    }
        //}

        public  XTTViewModel()
        {
            ItemsCableType =   PUBData.PUBCreateDatas.Cre_CableType_Dianxian_And_Dianlan();
            Items_CircuitBreaker_Type = PUBData.PUBCreateDatas.Cre_CircuitBreaker_Type();
            ItemsTaoguan = PUBData.PUBCreateDatas.Cre_Taoguan_Type();
            Items_Huilu_Purpose = PUBData.PUBCreateDatas.Cre_Huilu_Purpose();

            // 初始化命令，使用 RelayCommand<DatagridHuiluDto> 来传递参数
            OnButtonClick = new RelayCommand<DatagridHuiluDto>(Test2); // 初始化命令
            
            #region 2025年4月10日新增：回路分析与处理函数，涉及两个项目的class调整，错误极多

            var XTTHuilus = new List<XTTHuiluDto>()
            { 
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "m1", TaoguanType="SC",CircuitBreaker_Brand="schneider" },
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "m2", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "c1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "c2", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "N1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL1", IdHuilu = "k1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL2", IdHuilu = "c1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL2", IdHuilu = "c2", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL3", IdHuilu = "N1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
                new XTTHuiluDto {IdGuihao = "5AL3", IdHuilu = "k1", TaoguanType="SC",CircuitBreaker_Brand="schneider"},
            };

            //为回路添加默认值
            foreach (var item in XTTHuilus)
            {
                item.Methos_CalDefalutPe();
                item.IQuery_Cable(); // 调用查询方法
                item.IQuery_CircuitBreaker(); // 调用查询方法  //品牌选项待添加
            }

            //测试用
            // 在ViewModel构造函数中配置AutoMapper
            var config1 = new MapperConfiguration(cfg =>
            {
                // 如果需要从XTTHuiluDto到DatagridHuiluDto的映射
                cfg.CreateMap<XTTHuiluDto, DatagridHuiluDto>()
                   .ForMember(dest => dest.IsChecked, opt => opt.Ignore())
                   .ForMember(dest => dest.ButtonText, opt => opt.Ignore());
            });

            var mapper = config1.CreateMapper();


            //automapper用于 XTTHuiluDto与DatagridHuiluDto之间的转换
            //引入automapper
            // 配置映射
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<XTTHuiluDto, DatagridHuiluDto>();
            });
            var mapper1 = config.CreateMapper();

            //XTTHuiluDto source = new XTTHuiluDto();
            List<DatagridHuiluDto> target = mapper1.Map<List<DatagridHuiluDto>>(XTTHuilus);

            // 创建一个新的 ObservableCollection 并将 target 中的元素添加进去
            var newCollection = new ObservableCollection<DatagridHuiluDto>(target);

            // 将新的集合赋值给 DatagridHuilu 属性
            DatagridHuilu = newCollection;

            //MessageBox.Show("方向1  automapper赋值结束");

            // 配置映射
            var config2 = new MapperConfiguration(cfg2 =>
            {
                cfg2.CreateMap<DatagridHuiluDto,XTTHuiluDto >();
            });
            var mapper2 = config2.CreateMapper();


            //XTTHuiluDto source = new XTTHuiluDto();
            List<XTTHuiluDto> target2 = mapper1.Map<List<XTTHuiluDto>>(DatagridHuilu);

            //MessageBox.Show("方向2  automapper赋值结束");



            #endregion


            
            GroupedHuilus = new ObservableCollection<GroupedHuilu>(
            XTTHuilus.GroupBy(x => x.IdGuihao)
                     .Select(g => new GroupedHuilu
                     {
                         IdGuihao = g.Key,
                         Huilus = new ObservableCollection<DatagridHuiluDto>(mapper.Map<ObservableCollection<DatagridHuiluDto>>(g))
                     })
        );

            if(true)
            {

            }

        }


        public RelayCommand<DatagridHuiluDto> OnButtonClick { get; }

        private void Test2(DatagridHuiluDto item)
        {
            item.Methos_CalDefalutPe();
            item.IQuery_Cable(); // 调用查询方法
            item.IQuery_CircuitBreaker(); // 调用查询方法
            if (item.IsChecked)
            {
                //MessageBox.Show($"勾选框已选中，第三列内容: {item.IdGuihao}");
            }
            else
            {
                int index = DatagridHuilu.IndexOf(item) + 1;
                //MessageBox.Show($"勾选框未选中，行号: {index}");
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

    public class GroupedHuilu
    {
        public string IdGuihao { get; set; }
        public ObservableCollection<DatagridHuiluDto> Huilus { get; set; }
    }

    public class GroupedHuilu2 : XTTHuiluDto
    {
        public string IdGuihao { get; set; }
        public ObservableCollection<DatagridHuiluDto> Huilus { get; set; }
    }

}




