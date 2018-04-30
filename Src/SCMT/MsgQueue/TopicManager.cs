using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

namespace MsgQueue
{
    /// <summary>
    /// topic管理类
    /// </summary>
    public class TopicManager
    {
        public static TopicManager GetInstance()
        {
            return Singleton<TopicManager>.GetInstance();
        }

        public void AddTopic(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return;

            if (!_topicsList.Contains(topic))
            {
                _topicsList.Add(topic);
            }
        }

        /// <summary>
        /// 判断topic是否已经被订阅
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public bool HasSubscribed(string topic)
        {
            bool bHasSubscribed = false;

            if (!string.IsNullOrWhiteSpace(topic))
            {
                bHasSubscribed = _topicsList.Contains(topic);
            }

            return bHasSubscribed;
        }

        private List<string> _topicsList = new List<string>();
    }
}
