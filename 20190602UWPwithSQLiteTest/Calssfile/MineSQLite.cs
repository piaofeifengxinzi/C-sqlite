using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace _20190602UWPwithSQLiteTest.Calssfile
{
    class MineSQLite
    {
        //初始化数据库函数,需要在APP.xaml初始化时就调用
        public static void InitializaDatabase()
        {
            //这一句算是初始化连接sqlite，还有，不知道为啥，sqlite的这里要这样用，和函数一样？？？，好奇怪
            using (SqliteConnection mineConnection = new SqliteConnection("Filename=Test.db"))
            {
                //打开连接，不知道为啥要实现这一句
                mineConnection.Open();
                String createTableCommand = "create table if not exists minetesttable" 
                    + "(id int primary key,"
                    + "name varchar(10) not null,"
                    + "age int not null,"
                    + "sex int not null)";
                SqliteCommand sqliteCommand = new SqliteCommand(createTableCommand, mineConnection);
                //加上这一句命令才会真正的执行，所有的都是这样，只不过形式不同，有的要新建读或写对象
                sqliteCommand.ExecuteReader();
            }
        }

        public static void AddData(People people)
        {
            //每个函数好像都要使用这个，有点麻烦的
            using (SqliteConnection mineConnection = new SqliteConnection("Filename=Test.db"))
            {
                mineConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand();
                sqliteCommand.Connection = mineConnection;
                sqliteCommand.CommandText = "insert into minetesttable(id, name, age, sex) values(@id, @name, @age, @sex);";
                sqliteCommand.Parameters.AddWithValue("@id", people.id);
                sqliteCommand.Parameters.AddWithValue("@name", people.name);
                sqliteCommand.Parameters.AddWithValue("@age", people.age);
                sqliteCommand.Parameters.AddWithValue("@sex", people.sex);
                //一定要加这一句，应该是最终向数据库写入数据，免得傻傻的一遍遍调试，血淋淋的教训啊
                sqliteCommand.ExecuteReader();
                mineConnection.Close();
            }
        }

        public static List<People> GetData()
        {
            List<People> peoples = new List<People>();
            //同样的，先使用固定的语法连接到数据库
            using (SqliteConnection mineConnection = new SqliteConnection("Filename=Test.db"))
            {
                //固定的打开方式
                mineConnection.Open();
                String selectCommand = "select *from minetesttable";
                SqliteCommand sqliteCommand = new SqliteCommand(selectCommand, mineConnection);
                //读取缓冲区的数据吧，应该是这样的
                SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
                while(sqliteDataReader.Read())
                {
                    People people = new People
                    {
                        id = sqliteDataReader.GetInt32(0)
                        ,
                        name = sqliteDataReader.GetString(1)
                        ,
                        age = sqliteDataReader.GetInt32(2)
                        ,
                        sex = sqliteDataReader.GetInt32(3)
                    };
                    peoples.Add(people);
                }
                mineConnection.Close();
            }
            return peoples;
        }

        public static void deleteData()
        {
            //不论使用什么数据库都要先连接到数据库
            using (SqliteConnection mineConnection = new SqliteConnection("Filename=Test.db"))
            {
                mineConnection.Open();
                string commandString = "delete from minetesttable";
                SqliteCommand sqliteCommand = new SqliteCommand(commandString, mineConnection);
                //只有加上这一句，命令才会真正的执行，是个坑
                sqliteCommand.ExecuteReader();
                mineConnection.Close();
            }
        }

    }
}
