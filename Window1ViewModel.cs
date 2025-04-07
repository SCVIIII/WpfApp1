using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

using System.Data.SQLite;
using System.Windows;
using WpfApp1.Dto;

namespace WpfApp1
{
    public  class Window1ViewModel : ObservableObject
    {
        //测试回路dto
        private ObservableCollection<DatagridHuiluDto> _datadridhuilus;
        public ObservableCollection<DatagridHuiluDto> DatagridHuilu
        {
            get => _datadridhuilus;
            set => SetProperty(ref _datadridhuilus, value);  // SetProperty 自动触发通知
        }


        private ObservableCollection<Person> _people;

        public ObservableCollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);  // SetProperty 自动触发通知
        }
        public RelayCommand TestCommand { get; }
        public Window1ViewModel()
        {
            //初始化Binding命令
            TestCommand = new RelayCommand(Test);  // 初始化命令
            People = new ObservableCollection<Person>
            {
                new Person { Name = "1AL1", Pe = 30 },
                new Person { Name = "1AL2", Pe = 25 },
                new Person { Name = "1AL3", Pe = 30 },
                new Person { Name = "1AL4", Pe = 25 },
                new Person { Name = "1AL5", Pe = 30 },
                new Person { Name = "1AL2", Pe = 25 },
                new Person { Name = "1AL1", Pe = 30 },
                new Person { Name = "1AL2", Pe = 25 },
                new Person { Name = "1AL1", Pe = 30 },
                new Person { Name = "1AL1", Pe = 30 },
                new Person { Name = "1AL2", Pe = 25 },
                new Person { Name = "1AL2", Pe = 25 },
            };

            


        }

        private void Test()
        {
            MessageBox.Show("Hello World!");
            //
            try
            {
                // 数据库连接字符串
                string connectionString = "Data Source=MyFirstDatabase0402.db;Version=3;";

                // 创建数据库文件（如果不存在）
                SQLiteConnection.CreateFile("MyFirstDatabase0402.db");

                // 打开数据库连接
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // 创建一个表
                    string createTableQuery = "CREATE TABLE IF NOT EXISTS People (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Pe REAL)";
                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // 插入一些示例数据
                    string insertQuery = "INSERT INTO People (Name, Pe) VALUES (@Name, @Pe)";
                    foreach (var person in People)
                    {
                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", person.Name);
                            insertCommand.Parameters.AddWithValue("@Pe", person.Pe);
                            insertCommand.ExecuteNonQuery();
                        }
                    }

                    // 查询数据
                    string selectQuery = "SELECT * FROM People";
                    using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                    {
                        using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Pe: {reader["Pe"]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库操作出错: {ex.Message}");
            }
            //
        }


    }

    public class Person
    {
        public string Name { get; set; }
        public double Pe { get; set; }
    }

   
    


}
