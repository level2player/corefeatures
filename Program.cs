using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using corefeatures;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CSharpFeature1();
            CSharpFeature2();
            CSharpFeature3();
            CSharpFeature4();
            CSharpFeature5();
            CSharpFeature6();
            CSharpFeature7();
            Console.ReadKey();
        }

        #region C#1.0 接口隐式和显式实现
        private static void CSharpFeature1()
        {
            //隐式接口实现
            IMarketProvider marketProvider=new HKMarketProvider(); 
            HKMarketProvider hkMarketProvider=new HKMarketProvider();
            marketProvider.Order();
            hkMarketProvider.Order();

            //因为显式接口约束,SZMarketProvider无法直接方法接口方法,只能通过接口访问
            //SZMarketProvider szMarketProvider=new SZMarketProvider();
            //szMarketProvider.Order();
            IMarketProvider IszMarketProvider=new SZMarketProvider();
            IszMarketProvider.Order();
        }
        #endregion

        #region C#2.0 匿名方法、泛型、协变、迭代器
        
        private static void CSharpFeature2()
        {
            //匿名方法
            Task.Run(()=>{Console.WriteLine("Lambda的方式调用匿名方法");});
            Task.Run(delegate(){Console.WriteLine("委托的方式调用匿名方法");});

            //泛型方法
            HKMarketProvider hkMarketProvider=new HKMarketProvider();
            SZMarketProvider szMarketProvider=new SZMarketProvider();
            VIPInvestmenter vipInvestmenter=new VIPInvestmenter();
            GeneralInvestmenter generalInvestmenter=new GeneralInvestmenter();
            hkMarketProvider.CostCalculation<VIPInvestmenter>(vipInvestmenter);
            szMarketProvider.CostCalculation<VIPInvestmenter>(vipInvestmenter);
            hkMarketProvider.CostCalculation<GeneralInvestmenter>(generalInvestmenter);
            szMarketProvider.CostCalculation<GeneralInvestmenter>(generalInvestmenter);

            //协变
            List<VIPInvestmenter> vipInvestmenterList=new List<VIPInvestmenter>();
            vipInvestmenterList.Add(new VIPInvestmenter());
            IEnumerable<Investmenter> investmenters = vipInvestmenterList;
            foreach(Investmenter investmenter in investmenters)
            {
                investmenter.GetName();
            }
            //逆变省略........

            //迭代器
            HKTradeDay hKTradeDay=new HKTradeDay();
            foreach(string day in hKTradeDay)
            {
                Console.WriteLine("hk market trade day is "+day);
            }

        }
        #endregion

        #region C#3.0 匿名类型、扩展方法、查询表达式和Linq
        private static void CSharpFeature3()
        {
            //匿名类型
            var anonyint=1;
            anonyint++;
            var anonArray = new[] { new { name = "apple", diam = 4 },
             new { name = "grape", diam = 1 }}; 

            //扩展方法
            Console.WriteLine("abcdefg length="+"abcdefg".GetStrLenth());

            //查询表达式和Linq
            var studentArray = new[] { 
                new { name = "jack", score =77},
                new { name = "Davis", score = 59 },
                new { name = "Williams", score = 98 },
                new { name = "Smith", score = 82 },
                new { name = "Johnson", score = 74 },
                new { name = "Brown", score = 67 },
                new { name = "Jones", score = 52 },
                new { name = "John", score = 90 }}; 

            var studentScores=from student in studentArray 
                where student.score>=80 orderby 
                student.score descending select string.Format("name={0},score={1}",student.name,student.score);
            foreach (var s in studentScores)
            {
                Console.WriteLine(s);
            }
           Console.WriteLine("AverageScore="+studentArray.Average(x=>x.score));
        }
        #endregion


        #region C#4.0 可选参数、动态类型
        private static void CSharpFeature4()
        {
            //可选参数
            HKMarketProvider hkMarketProvider=new HKMarketProvider();
            hkMarketProvider.GetTradeLog(10021);
            hkMarketProvider.GetTradeLog(10022,default(DateTime),DateTime.Now,9999999);

            //动态类型
            dynamic dynamichkMarketProvider=new HKMarketProvider();
            dynamichkMarketProvider?.Order();
        }
        #endregion

        
        #region C#5.0 async、await异步编程模型
        private async static void CSharpFeature5()
        {
          HKMarketProvider hkMarketProviderxx=new HKMarketProvider();
          await hkMarketProviderxx.GetTradeLogAsync(9009);
        }
        #endregion

        #region C#6.0 nameof、空合并运算符、字符串插值
        private  static void CSharpFeature6()
        {
            //nameof
            HKMarketProvider hkMarketProvider=new HKMarketProvider();
            try
            {
            hkMarketProvider=null;
            if(hkMarketProvider==null)throw new ArgumentException(nameof(hkMarketProvider));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("ArgumentException:"+ex.Message);
            }
            //空合并运算符
            if(hkMarketProvider!=null)
            {
                hkMarketProvider.Order();
            }
            hkMarketProvider?.Order();
            
            //字符串插值
            string tmpstr="6.0";
            Console.WriteLine($"this is C#{tmpstr} new feature....");

        }
        #endregion

        #region C#7.0 Out变量、表达式体成员、switch匹配任何类型、多返回参数
        private  static void CSharpFeature7()
        {
          //Out变量
          if(double.TryParse("123.9999",out var parseValue))
          {
              Console.WriteLine($"double TryParse very sucessful value:{parseValue}");
          }
          //表达式体成员
          Utils.ConsoleStar(99);
          //switch匹配任何类型
          IMarketProvider marketProvider=new HKMarketProvider();
          switch(marketProvider)
          {
            case HKMarketProvider hkmarket:
                hkmarket.Order();
                break;
            case SZMarketProvider szmarket:
                //..........
                break;
            case null:
                throw new ArgumentException(nameof(marketProvider));
          }

         //多返回参数
         var results= Utils.TryParseNumber("123.00");
         Console.WriteLine($"results.Item1={results.Item1},results.Item2={results.Item2}");
        }
        
        #endregion

    }
}
