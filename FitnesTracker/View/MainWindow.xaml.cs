using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnesTracker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //List<Models.UserActivity> userActivities = new List<Models.UserActivity>();

            //string path = Environment.CurrentDirectory + "/TestData";
            //string[] files = Directory.GetFiles(path);
            //List<string> jsonData = new List<string>();

            //for(int i = 0; i < files.Length; i++)
            //{
            //    jsonData.Add(File.ReadAllText(files[i]));
            //    userActivities.Add(JsonConvert.DeserializeObject<Models.UserActivity>(jsonData[i]));
            //}

            //string jsonData = File.ReadAllText(files[0]);

            //Models.UserActivity[] userActivity = JsonSerializer.Deserialize<Models.UserActivity[]>(jsonData);

            //foreach (var act in userActivity)
            //{
            //    Debug.WriteLine($"{act.Steps}");
            //}
            
        }
    }
}
