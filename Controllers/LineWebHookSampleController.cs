using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ITAXI_LINEBot.Models;
using Newtonsoft.Json;
using isRock.LineBot;
using isRock.LineBot.Conversation;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace ITAXI_LINEBot.Controllers
{
    public class LineWebHookSampleController : isRock.LineBot.LineWebHookControllerBase
    {
        const string channelAccessToken = "RSIPXCuOAO+Zh3ZyZctuu5KUnZNcehztUR8triGSJUQn4f+YGjmakQFN+X7pdwKqc0ylLlpURD6CMSOabGdlkwnhyuIt1Khuf6Zay7N3VANWpcF786eJzLRLotCDRarVPhz9rU/D5qAqiG4V9H2zdwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "Ud328c162347cac136ab9519026c833f0";

        [Route("api/LineWebHook")]
        [HttpPost]
        public IHttpActionResult POST()
        {

            try
            {
                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                //取得用戶詳細資訊
                var UserInfo = bot.GetUserInfo(AdminUserId);
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //var bot = new isRock.LineBot.Bot(this.channelAccessToken);
                //解析使用者傳給Bot的文字訊息
                string userCommandString = ReceivedMessage.events[0].message.text;
                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();


                if (ReceivedMessage.events.FirstOrDefault().type == "follow")
                {
                    var userInfo = GetUserInfo(ReceivedMessage.events.FirstOrDefault().source.userId);
                    ReplyMessage(LineEvent.replyToken, userInfo.displayName + "!!!歡迎加入ITAXI" + "🥰"
                        + "\n" + "需要服務歡迎點選下方按鈕，"
                        + "\n" + "ITAXI團隊在此祝福您有高興的車程" + "🥰");
                }

                if (LineEvent.message.text == "ITAXI")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\ITAXI.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "客服中心")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Customer_service.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "Q&A")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////乘客問題
                ////Q & A列表
                else if (LineEvent.message.text == "乘客問題")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Customer_Q&A.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////乘客問題
                ////Q&A問題答案
                else if (LineEvent.message.text == "如果找不到司機怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_1.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果司機一直都沒有到怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_2.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "想要取消訂單要到哪裡取消？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_3.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果我將個人物品遺留在車上怎麼辦?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_4.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果有關司機的意見能在那裡回饋?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_5.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "目前ITAXI的叫車範圍有哪些?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_6.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果想去其他地方可以自由選擇目的地嗎?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_7.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果點選立即訂單頁面一直跑到搭車紀錄怎麼辦?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_8.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果忘記密碼怎麼辦?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_9.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果上車時突然想改變下車地點、該如何處理?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_10.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "可以選擇司機嗎？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_11.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "請問我已經預約完成，但都沒收到系統訂單確認信怎麼辦?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_12.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "請問多久可以叫到車？都沒有司機接我的單怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_13.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////司機問題
                ////Q & A列表
                else if (LineEvent.message.text == "司機問題")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Driver_Q&A.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////司機問題
                ////Q&A問題答案
                else if (LineEvent.message.text == "如果乘客一直沒有出現怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_14.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果無法點選取消訂單或沒有取消按鈕可以按該怎麼辦?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_15.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                else if (LineEvent.message.text == "如果完成訂單後如果乘客沒有點選完成訂單、造成無法接下一單該怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_16.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////客服問題
                ////Q&A列表
                if (LineEvent.message.text == "客服問題")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\CusService_Q&A.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                ////客服問題
                ////Q&A問題答案
                if (LineEvent.message.text == "如果操作網頁上有問題怎麼辦？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_17.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "除了現金支付外還有其他付款方式嗎?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_18.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "搭車能夠索取發票嗎?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_19.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "如果網頁出現錯誤，404，ERROR，該如何處理?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_20.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "如果某些按鈕按了沒功能?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_21.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "加入ITaxi計程車隊以及處理開通程序可以請家人或朋友代辦嗎？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_22.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "是否有特殊行李規格與限制?")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_23.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "註冊時所提供的個人資料，會不會外洩？")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\Q&A_24.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }


                //聯絡客服
                if (LineEvent.message.text == "聯絡客服")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\ContactUS.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);
                }

                if (LineEvent.message.text == "查詢訂單紀錄")
                {
                    var Lesson_Flex = System.IO.File.ReadAllText(@"C:\inetpub\wwwroot\Chat\FlexMessage\ContactUS.txt");
                    this.ReplyMessageWithJSON(LineEvent.replyToken, Lesson_Flex);

                }

                if (LineEvent.message.text == "查詢乘客搭車紀錄")
                {
                    var responseMsg = "";
                    if (!ProcessCommand(LineEvent))
                        responseMsg = ProcessText(LineEvent);
                }

                if (LineEvent.message.text == "查詢司機載客紀錄")
                {
                    var responseMsg = "";
                    if (!ProcessCommand(LineEvent))
                        responseMsg = ProcessText(LineEvent);
                }


                return Ok();
            }
            catch (Exception ex)
            {
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //this.PushMessage("U0a333b03ae4ab842130dfd4bfbbbb2bd", "發生錯誤:\n" + ex.Message);
                return Ok();
            }

        }
        private bool ProcessCommand(isRock.LineBot.Event e)
        {
            var msg = e.message.text.Trim();

            if (msg == "查詢乘客搭車紀錄")
            {
                //今日花費
                var n = SeachOrder.GetCusOrd(e.source.userId);
                //回覆QuickReply
                isRock.LineBot.Bot b = new isRock.LineBot.Bot();
                isRock.LineBot.TextMessage TextMessage = new isRock.LineBot.TextMessage(
                    $"您今天總單數有 ${n}單");
                b.ReplyMessage(e.replyToken, TextMessage);
                return true;
            }


            if (msg == "查詢司機載客紀錄")
            {
                //本月花費
                var n = SeachOrder.GetDriOrd(e.source.userId);
                //回覆QuickReply
                isRock.LineBot.Bot b = new isRock.LineBot.Bot();
                isRock.LineBot.TextMessage TextMessage = new isRock.LineBot.TextMessage(
                    $"您今天總單數有 ${n}單");
                b.ReplyMessage(e.replyToken, TextMessage);
                return true;
            }

            return false;
        }
        private string ProcessText(isRock.LineBot.Event e)
        {
            const string HelpMsg = "嗨，您好。\n如果希望我幫您記帳，你可以直接輸入金額唷...\n\n例如:\n 180 \n 或\n 180 麥當勞\n然後再選取或輸入分類即可。";

            //取得User Id
            var UserId = e.source.userId;
            var msg = e.message.text;
            //如果沒有狀態
            if (string.IsNullOrEmpty(SeachOrder.GetState(UserId)))
            {
                //預期msg是金額數字
                //去除空白和不必要的字
                msg = msg.Replace("$", "").Replace("元", "").Trim();
                //全形轉半形
                //msg = Strings.StrConv(msg, VbStrConv.Narrow, 0);
                //如果當前沒有狀態，預期的是純數字
                //先以 ' ' 分割
                var item = msg.Split(' ');
                if (item.Length <= 0) return HelpMsg;
                float num = 0;
                string memo = "";
                if (float.TryParse(item[0], out num) == false)
                    return HelpMsg;
                if (item.Length == 2) memo = item[1].Trim();

                //如果資料沒問題, 保存起來
                Utility.SetState(UserId + "-Amount", num.ToString());
                Utility.SetState(UserId + "-Memo", memo);

                //回覆QuickReply
                isRock.LineBot.Bot b = new isRock.LineBot.Bot();
                isRock.LineBot.TextMessage TextMessage = new isRock.LineBot.TextMessage($"請選擇或直接輸入這筆金額'{num}'的記帳類別");

                
                //設定狀態
                Utility.SetState(UserId, "等待分類名稱");
                return "";
            }
            //如果有狀態，且為正等待分類名稱
            if (Utility.GetState(UserId) == "等待分類名稱")
            {
                //取回應該有的數字
                if (string.IsNullOrEmpty(Utility.GetState(UserId + "-Amount")))
                {
                    Utility.SetState(UserId, "");
                    return HelpMsg;
                }
                //取回數字
                float num = 0;
                if (float.TryParse(Utility.GetState(UserId + "-Amount"), out num) == false)
                    return HelpMsg;
                string memo = Utility.GetState(UserId + "-Memo");

                //有了數字num以及分類名稱
                var AccountType = msg.Trim();
                //清空狀態
                Utility.SetState(UserId, "");
                //紀錄
                if (Utility.SaveToDB(UserId, num, AccountType, memo))
                    return $"${num} 已記錄為 {AccountType}";
                else
                    return "記錄失敗";
            }
            return "";
        }
    }


}
