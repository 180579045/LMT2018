using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UICore.Controls.Metro;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// 对象树专用控件，解析后，保存了当前基站所有的对象树内容信息，方便及时搜索;
    /// </summary>
    class ObjTree_MetroExpander : MetroExpander, I_SCMT_BelongUserControl
    {
        /// <summary>
        /// 保存了基站所有对象树的信息;
        /// </summary>
        public List<ObjNode> m_RootNode { get; set; }

        /// <summary>
        /// 保存搜索结果;
        /// </summary>
        private ObservableCollection<ObjNode> ret { get; set; }

        public ObjTree_MetroExpander()
        {
            ret = new ObservableCollection<ObjNode>();
        }

        /// <summary>
        /// 查找对象树中匹配的信息
        /// </summary>
        /// <param name="findlist"></param>
        /// <param name="search_content"></param>
        /// <returns></returns>
        private ObservableCollection<ObjNode> FindObjByName(List<ObjNode> findlist, string search_content)
        {
            foreach (ObjNode iter in findlist)
            {
                // 检索时，不区分大小写
                string ObjName = iter.ObjName.ToLower();
                if (ObjName.Contains(search_content.ToLower()))
                {
                    ObjNode temp = iter;
                    ret.Add(temp);
                }
                if ((iter.SubObj_Lsit == null))
                {
                    continue;
                }

                FindObjByName(iter.SubObj_Lsit, search_content);
            }

            return ret;
        }

        /// <summary>
        /// 搜索更新附属控件的显示内容;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateBelongControl(object sender, SearchButtonEventArgs e)
        {
            Console.WriteLine("Receive the SearchText Notify Event" + e.SearchContent);
            this.SubExpender.Children.Clear();

            if (string.IsNullOrEmpty((e as SearchButtonEventArgs).SearchContent))
            {
                return;
            }
            
            // 带有"'"是表明用户正在用中文输入法输入中，不处理
            if (e.SearchContent.Contains("'"))
            {
                return;
            }

            ret.Clear();
            this.Dispatcher.BeginInvoke(new Action(()=> {
                ret.Clear();
                ret = this.FindObjByName(m_RootNode, e.SearchContent);
                Console.WriteLine("检索结果个数:" + ret.Count);
                FindFinishUpdateSubExpander();
            }));
            
        }

        private void FindFinishUpdateSubExpander()
        {
            this.SubExpender.Children.Clear();
            int count = 0;     // 计数，超过50个就不显示了，没有意义

            if ((ret != null) && (ret.Count != 0))
            {
                
                foreach (ObjNode iter in ret)
                {
                    count++;
                    
                    // 初始化对应的内容,并加入到容器中;
                    var subitems = new MetroExpander
                    {
                        Header = iter.ObjName,
                        obj_type = iter
                    };

                    subitems.Click += iter.ClickObjNode;
                    this.SubExpender.Children.Add(subitems);

                    if(count == 50)
                    {
                        return;
                    }
                }

            }

            return;
        }
    }
}
