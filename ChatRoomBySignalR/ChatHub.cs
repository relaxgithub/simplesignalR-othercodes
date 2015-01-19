using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatRoomBySignalR
{
    [HubName("RelaxChatHub")]
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.sendMessage(name, message + "  " + HttpContext.Current.Request.QueryString["version"]);
        }

        [HubMethodName("showmessage")]
        public void ShowMessage(string name, string message)
        {
            Clients.Others.showMessage(new Message() { Name = name, MessageInfo = message });
        }

        /// <summary>
        /// the method in server that has a return value
        /// </summary>
        /// <returns></returns>
        [HubMethodName("returnmethod")]
        public IEnumerable<Message> GetAllList()
        {
            Message[] am = { 
                    new Message(){Name="a",MessageInfo="b"},
                    new Message(){Name="c",MessageInfo="d"},
                    new Message(){Name="e",MessageInfo="f"}
                };

            return am;
        }

        [HubMethodName("normaltype")]
        public Message GetSingleType()
        {
            return new Message() { Value = "调用服务端HUB中具有普通返回类型的函数!" };
        }

        [HubMethodName("onlineNews")]
        public IEnumerable<NewsInfo> NewsInfoList()
        {
            NewsInfo[] news_List = { 
                        new NewsInfo(){ Title="中央深改组会议首提全面深化公安改革",Url="http://news.163.com/14/1231/02/AEOR6Q0B00014AED.html"},
                        new NewsInfo(){Title="印尼捞起部分飞机残骸 3具尸体手拉手",Url="http://news.163.com/special/inakejishilian/"},
                        new NewsInfo(){Title="赵本山现身上海拒回应传闻 与人谈笑风生",Url="http://news.163.com/14/1231/02/AEOS67QK00014Q4P.html"}
                    };
            
            return news_List;
        }

        [HubMethodName("realList")]
        public void RealInfoList()
        {
            while (true)
            {
                NewsInfo[] news_List = { 
                        new NewsInfo(){ Title="中央深改组会议首提全面深化公安改革",Url="http://news.163.com/14/1231/02/AEOR6Q0B00014AED.html"},
                        new NewsInfo(){Title="印尼捞起部分飞机残骸 3具尸体手拉手",Url="http://news.163.com/special/inakejishilian/"},
                        new NewsInfo(){Title="赵本山现身上海拒回应传闻 与人谈笑风生",Url="http://news.163.com/14/1231/02/AEOS67QK00014Q4P.html"}
                    };

                Clients.All.push_NewsList(news_List);
                Thread.Sleep(5000);
            }
            
        }
    }

    public class Message
    {
        public string Name { get; set; }
        public string MessageInfo { get; set; }
        public string Value { get; set; }
    }

    public class NewsInfo
    {
        public string Url { get; set; }
        public string Title { get; set; }
    }
}