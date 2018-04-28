/*************************************************************************************
* CLR版本：        $ $
* 类 名 称：       $RedisOperation$
* 机器名称：       $machinename$
* 命名空间：       $SCMT_json.Interface$
* 文 件 名：       $SendUIToJsonInterface.cs$
* 创建时间：       $2018.04.08$
* 作    者：       $luanyibo$
* 说   明 ：
*     UI与json模块的交互消息格式。
* 修改时间     修 改 人    修改内容：
* 2018.04.08   栾义博      创建文件并实现类  UIToJsonInterface
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Support;
//using ServiceStack.Redis.Support;
using System.Threading;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.DataAnnotations;
using Newtonsoft.Json.Linq;

using SCMT_json.JSONDataMgr;


namespace SCMT_json.Interface.Redis
{
    /// <summary>
    /// 
    /// </summary>
    class RedisOperation
    {
        private RedisManagerPool redisManger;
        /// <summary>
        /// 获取一个Redis Client 
        /// </summary>
        /// <param name="redisConf">数据库的连接地址</param>
        /// <returns>redis 数据库的连接句柄</returns>
        public IRedisClient ConnectionRedis(string redisConf)
        {
            redisManger = new RedisManagerPool(redisConf);//redisConf= "127.0.0.1:6379");
            var redis = redisManger.GetClient();//获取一个Redis Client 
            return redis;
        }

        /// <summary>
        /// 清空数据库缓存，慎用
        /// </summary>
        /// <param name="redisClient"></param>
        public void deleteAllRecordsInRedis(IRedisClient redisClient)
        {
            redisClient.FlushAll();
        }

        public void saveInfoIntoRedis(IRedisClient redisClient, Todo toDo)
        {
            var redisTodos = redisClient.As<Todo>();
            redisClient.Store(toDo);

            /*
            var newTodo = new Todo                                          //实例化一个Todo类  
            {
                Id = redisTodos.GetNextSequence(),
                Content = "Learn Redis",
                Order = 1,
            };*/
        }

        public IList<Todo> getAllInfoFromRedis(IRedisClient redisClient)
        {
            var redisTodos = redisClient.As<Todo>();
            var redisClientAll = redisTodos.GetAll();
            return redisClientAll;
        }

        public void test()
        {
            IRedisClient redisClient = ConnectionRedis("127.0.0.1:6379");
            //var redisManger = ConnectionRedis("127.0.0.1:6379");
            //var redis = redisManger.GetClient();                           //获取一个Redis Client  
            //redis.Save();                                    
            //redis.FlushAll();                                              //清空数据库缓存，慎用
            deleteAllRecordsInRedis(redisClient);

            //var redisClientAll = redisClient.GetAll();
            //"1===:{0}".Print(redisClientAll.Dump());

            //Console.ReadLine();
            //var redisTodos = redisClient.As<Todo>();
            var newTodo = new Todo                                          //实例化一个Todo类  
            {
                Id = 1,
                Content = "Learn Redis",
                Order = 1,
            };
            var newTodo2 = new Todo                                          //实例化一个Todo类  
            {
                Id = 2,
                Content = "Learn Redis2",
                Order = 2,
            };
            //redisTodos.Store(newTodo);
            //redisClient.Store(newTodo);
            //redisClient.Store(newTodo2);
            saveInfoIntoRedis(redisClient, newTodo);
            saveInfoIntoRedis(redisClient, newTodo2);

            //Todo saveTodo = redisTodos.GetById(newTodo.Id);
            var redisClientAll = getAllInfoFromRedis(redisClient);//redisTodos.GetAll();
            Console.WriteLine("==========");
            Console.WriteLine(redisClientAll.Dump());

        }
        public void test2(IRedisClient redisClient)
        {
            var redisTodos = redisClient.As<Todo>();
            var newTodo = new Todo                                          //实例化一个Todo类  
            {
                Id = redisTodos.GetNextSequence(),
                Content = "Learn Redis",
                Order = 1,
            };
            redisTodos.Store(newTodo);
            //redis.Store(newTodo);//把newTodo实例保存到数据库中    增
            //redisTodos.Store(newTodo);     //把newTodo实例保存到数据库中    增

            Todo saveTodo = redisTodos.GetById(newTodo.Id);               //根据Id查询        查  
            "Saved Todo: {0}".Print(saveTodo.Dump());

            saveTodo.Done = true;                                         //改  
            //redisTodos.Store(saveTodo);
            ///redis.Store(saveTodo);

            var updateTodo = redisTodos.GetById(newTodo.Id);            //查  
            "Updated Todo: {0}".Print(updateTodo.Dump());

            redisTodos.DeleteById(newTodo.Id);                           //删除 

            var remainingTodos = redisTodos.GetAll();
            "No more Todos:".Print(remainingTodos.Dump());

            Console.ReadLine();
        }

        public void saveJsonFileIntoRedis(string filepath)
        {
            filepath = "D:\\C#\\SCMT\\mib123.json";
            JsonFile jsonMibFile = new JsonFile();
            string filestr = jsonMibFile.ReadFileToEnd(filepath);
            JObject jo = JObject.Parse(filestr);
            JArray mibJArray = JArray.Parse(jo["tableList"].ToString());
            string nameMib;
            string oid;
            string childNameMib;
            string childOid;
            foreach (var ss in mibJArray)
            {
                nameMib = (string)ss["nameMib"];
                oid = (string)ss["oid"];
                Console.WriteLine("======({0}:{1})=======", nameMib, oid);
                foreach (var sss in ss["childList"])
                {
                    childNameMib = (string)sss["childNameMib"];
                    childOid = (string)sss["childOid"];
                    Console.WriteLine("*({0}:{1})*", childNameMib,  childOid);
                }  
            }
            


            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            Console.WriteLine(values[2].Dump());
            string test2 = values[2];
            JObject jo2 = JObject.Parse(test2);


            //string test1 = (string)jo["tableList"];
            //Console.WriteLine(test1);// DVD read/writer
            //string firstDrive = (string)jo["tableList"][0];
            //Console.WriteLine(firstDrive);// DVD read/writer  



        }
    }

    class Todo
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public bool Done { get; set; }
    }
    public class Car
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Make Make { get; set; }
        public Model Model { get; set; }
    }

    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Model
    {
        public int Id { get; set; }
        public Make Make { get; set; }
        public string Name { get; set; }
    }

}


