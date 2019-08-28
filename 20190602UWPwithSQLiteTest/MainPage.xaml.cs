using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using _20190602UWPwithSQLiteTest.Calssfile;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace _20190602UWPwithSQLiteTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int i = 1;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            while(i<20)
            {
                People people = new People { id = i, name = "张金龙", age = 20, sex = 1 };
                MineSQLite.AddData(people);
                ++i;
            }
            finalresult.Text = "添加完成";
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            /*List<People> peoples = new List<People>();
            finalresult.Text = "1";
            peoples = MineSQLite.GetData();
            finalresult.Text = "2";
            String result = "";
            foreach(People x in peoples)
            {
                result = result + "工号：" + x.id + "姓名：" + x.name + "年龄：" + x.age + "性别：" + x.sex + "\n";
            }
            finalresult.Text = result;
            */
            string data = "";
            
            int i = 0;
            while (i < 20)
            {
                //自己写的faker类的返回的一个json字符串
                data = FakerData.fakername();
                //JsonReader jsonReader = new JsonReader(new StringReader(data));
                //不要像上面那样用，直接反序列化相应的对象就可以了，要结合faker类的RuleFor用
                People people = JsonConvert.DeserializeObject<People>(data);
                MineSQLite.AddData(people);
                ++i;
            }
            System.Diagnostics.Debug.WriteLine(data);
            //ole.WriteLine("Json格式字符串:");
            finalresult.Text = data;
        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            MineSQLite.deleteData();
            System.Diagnostics.Debug.WriteLine("删除数据成功");
        }
    }
}
