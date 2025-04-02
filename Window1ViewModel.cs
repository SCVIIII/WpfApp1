using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace WpfApp1
{
    public partial class Window1ViewModel : ObservableObject
    {

        private ObservableCollection<Person> _people;

        public ObservableCollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);  // SetProperty 自动触发通知
        }

        public Window1ViewModel()
        {
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


    }

    public class Person
    {
        public string Name { get; set; }
        public double Pe { get; set; }
    }
  
    
}
