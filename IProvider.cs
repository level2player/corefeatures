using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace corefeatures
{
    public interface IMarketProvider
    {
        //Order interface
        void Order();

        void CostCalculation<T>(T t) where T : Investmenter;
    }
    public class HKMarketProvider : IMarketProvider
    {
        public void CostCalculation<T>(T t)where T : Investmenter
        {
            if(t is GeneralInvestmenter)
            {
                t.Cost(200);
            }
            else if(t is VIPInvestmenter)
            {

                 t.Cost(100);
            }
        }

        //接口的隐式实现
        public void Order()
        {
            Console.WriteLine("Hk Order");
        }

        public List<string> GetTradeLog(int Investmenter,DateTime StartTime=default(DateTime),DateTime? EndTime=null,int? rows=100)
        {
            Console.WriteLine(string.Format("GetTradeLog Request,Investmenter={0},StartTime={1},EndTime={2},rows={3}",Investmenter,StartTime,EndTime,rows));
            return null;
        }

        public Task<List<string>> GetTradeLogAsync(int Investmenter,DateTime StartTime=default(DateTime),DateTime? EndTime=null,int? rows=100)
        {
           return  Task.Run<List<string>>(()=>
           {
               Thread.Sleep(1000);
               Console.WriteLine(string.Format("GetTradeLog Request async,Investmenter={0},StartTime={1},EndTime={2},rows={3}",Investmenter,StartTime,EndTime,rows));
               var reulst=new List<string>();
               return reulst;
           });
        }
    }
    
    public class SZMarketProvider : IMarketProvider
    {
        public void CostCalculation<T>(T t) where T : Investmenter
        {
            if(t is GeneralInvestmenter)
            {
                t.Cost(100);
            }
            else if(t is VIPInvestmenter)
            {

                 t.Cost(50);
            }
        }

        //接口的显示实现
        void IMarketProvider.Order()
        {
            Console.WriteLine("SZ Order");
        }
    }


    public interface Investmenter
    {
        void GetName();

        void Cost(double Money);
    }

    public class GeneralInvestmenter : Investmenter
    {
        public void GetName()
        {
            Console.WriteLine("My name is Jie,I am a GeneralInvestmenter.");
        }

        public void Cost(double Money)
        {
            Console.WriteLine("GeneralInvestmenter Cost Money:"+Money);
        }


    }
    public class VIPInvestmenter : Investmenter
    {
        public void GetName()
        {
            Console.WriteLine("My name is quick,I am a VIPInvestmenter.");
        }

        public void Cost(double Money)
        {
             Console.WriteLine("VIPInvestmenter Cost Money:"+Money);
        }


    }


    public class HKTradeDay : IEnumerable
    {
        string[] m_Days = {  "Mon", "Tue", "Wed", "Thr", "Fri" };
        public IEnumerator GetEnumerator()
        {
            for(Int32 i=0;i<m_Days.Length;i++)
            {
                yield return m_Days[i];
            }
        }
    }

}