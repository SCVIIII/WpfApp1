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
using WpfApp1;
using WpfApp1.Dto;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    public partial  class XTTViewModel : ObservableObject
    {
        //测试回路dto
        private ObservableCollection<DatagridHuiluDto> _datadridhuilus;
        public ObservableCollection<DatagridHuiluDto> DatagridHuilu
        {
            get => _datadridhuilus;
            set => SetProperty(ref _datadridhuilus, value);  // SetProperty 自动触发通知
        }

        public XTTViewModel()
        {

            // 初始化命令，使用 RelayCommand<DatagridHuiluDto> 来传递参数
            OnButtonClick = new RelayCommand<DatagridHuiluDto>(Test2); // 初始化命令

            DatagridHuilu = new ObservableCollection<DatagridHuiluDto>
            {
                new DatagridHuiluDto { IdGuihao = "1AL1", IdHuilu = "N1",Pe=1 ,In = 30, Purpose = "照明", CableType="BTLY",CableCSA=2.5,L123="L123"},
                new DatagridHuiluDto { IdGuihao = "1AL2", IdHuilu = "N1",Pe=1 ,In = 25, Purpose = "插座", CableType="BTLY",CableCSA=2.5,L123="L123"},
                new DatagridHuiluDto { IdGuihao = "1AL3", IdHuilu = "N1", Pe = 1, In = 30, Purpose = "照明", CableType = "BTLY", CableCSA = 2.5, L123 = "L123" },
                new DatagridHuiluDto { IdGuihao = "1AL4", IdHuilu = "N1", Pe = 1, In = 25, Purpose = "插座", CableType = "BTLY", CableCSA = 2.5, L123 = "L123" },
            };
        }


        public RelayCommand<DatagridHuiluDto> OnButtonClick { get; }

        private void Test2(DatagridHuiluDto item)
        {
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




