using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;
namespace _20190602UWPwithSQLiteTest.Calssfile
{
    // 参考文档 https://www.cnblogs.com/waku/p/10547300.html
    // 这里的文档最终返回的时json格式的数据，还要搞一个接送格式解析器
    class FakerData
    {
        private static Faker<People> faker = new Faker<People>("zh_CN");
        public static string fakername()
        {
            faker.RuleFor(p => p.id, f => f.Random.Number(0,10000))
                .RuleFor(p => p.name, f => f.Name.FullName())
                .RuleFor(p => p.age, f => f.Random.Number(15, 60))
                .RuleFor(p => p.sex, f => f.Random.Number(0, 1));
            var data = faker.Generate();
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
        }
    }
}
