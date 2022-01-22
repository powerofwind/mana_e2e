using mana_Test.Models;
using manaTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class SetUpProject
    {
        private static IPage page;
        //private static IBrowser browser;
        //private static IPlaywright playwright;

        //public async Task<IBrowser> BeforeScenario()
        //{
        //    playwright ??= await Playwright.CreateAsync();
        //    if (null == browser)
        //    {
        //        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //        {
        //            Headless = false,
        //            SlowMo = 1000,
        //        });
        //    }
        //    return browser;
        //}

        public async Task<bool> ManaMcontent(string url)
        {
            //var browser = await BeforeScenario();
            //page = await browser.NewPageAsync();
            page = await PageFactory.CreatePage().DoLogin();
            var res = await page.GotoAsync(url);
            await page.WaitForTimeoutAsync(2000);
            if (!res.Ok)
            {
                return false;
            }
            //await page.GotoAsync("http://localhost:8100/#/user-profile-address");
            //await page.WaitForTimeoutAsync(1000);
            //await page.ClickAsync("text=กทม123 ทวีวัฒนา ทวีวัฒนา กรุงเทพมหานคร 101700632130913 >> p");
            //await page.WaitForTimeoutAsync(10000);

            return true;
        }

        //public async Task<string> CheckDialogMessageInAlertBrowser()
        //{
        //    var browser = await BeforeScenario();
        //    page = await browser.NewPageAsync();
        //    await page.GotoAsync("https://letcode.in/alert");
        //    var dialogMessage = string.Empty;
        //    page.Dialog += async (_, dialog) =>
        //    {
        //        dialogMessage = dialog.Message;
        //        await dialog.DismissAsync(); //close dialog
        //        //await dialog.AcceptAsync(); //confirm dialog
        //    };
        //    await page.ClickAsync("text=Simple Alert"); //dialog ok
        //    //await page.ClickAsync("text=Confirm Alert"); //dialog Yes No
        //    return dialogMessage;
        //}

        //public async Task<string> InputTextInAlertBrowser()
        //{
        //    var browser = await BeforeScenario();
        //    page = await browser.NewPageAsync();
        //    await page.GotoAsync("https://letcode.in/alert");
        //    //await page.GotoAsync("http://localhost:8101/#/wallet-topup-ppay"); //เทสกับมานะ
        //    var dialogMessage = string.Empty;
        //    page.Dialog += async (_, dialog) =>
        //    {
        //        dialogMessage = dialog.Message;
        //        await dialog.AcceptAsync("123"); //ใส่ input ใน dialog แล้วกดตกลง
        //    };
        //    await page.ClickAsync("text=Prompt Alert");
        //    //await page.ClickAsync("input[name=\"ion-input-1\"]"); //เทสกับมานะ
        //    var text = await page.InnerTextAsync("text=Prompt AlertYour name is: 123 >> div");
        //    return text;
        //}


        // แจ้งปัญหาไปยังทีม Support ได้
        public async Task<bool> ReportIssue()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nrptdtl-create");
            await page.GotoAsync("http://localhost:8100/#/report-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("text=ประเภทของปัญหา, เลือกประเภท");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"การเงิน\")");
            await page.ClickAsync("button:has-text(\"OK\")");
            await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "test E2E แจ้งปัญหาการเงิน2");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "0632132623");
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.FillAsync("input[name=\"ion-input-1\"]", "tatae@gmail.com");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(5000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }
        }


        // ส่งคำขอ KYC basic
        public async Task<bool> SendRequestKYCBasic()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/kyc/basic/visit/nkycbsc-180056522489857");
            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/kyc-agreement");
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/kyc-basic-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "เตชะพงศ์");
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.FillAsync("input[name=\"ion-input-1\"]", "ขำคม");
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", "1349900417320");
            await page.ClickAsync("button");
            //// วันเกิด
            ////await page.ClickAsync("text=17");
            ////await page.ClickAsync("text=15");
            ////await page.ClickAsync("text=13");
            ////await page.ClickAsync("text=11");
            ////await page.ClickAsync("text=09");
            ////await page.ClickAsync("text=07");
            ////await page.ClickAsync("text=05");
            ////await page.ClickAsync("text=03");
            ////await page.ClickAsync("text=02");
            //////เดือนเกิด
            ////await page.ClickAsync(":nth-match(ion-picker-column:has-text(\"010203040506070809101112\"), 2)");
            ////await page.ClickAsync(":nth-match(:text(\"03\"), 2)");
            ////await page.ClickAsync(":nth-match(:text(\"04\"), 2)");
            ////await page.ClickAsync(":nth-match(:text(\"05\"), 2)");
            ////await page.ClickAsync(":nth-match(:text(\"06\"), 2)");
            //////ปีเกิด
            ////await page.ClickAsync("text=2020");
            ////await page.ClickAsync("text=2019");
            ////await page.ClickAsync("text=2018");
            ////await page.ClickAsync("text=2017");
            ////await page.ClickAsync("text=2016");
            ////await page.ClickAsync("text=2015");
            ////await page.ClickAsync("text=2014");
            ////await page.ClickAsync("text=2013");
            ////await page.ClickAsync("text=2012");
            ////await page.ClickAsync("text=2011");
            ////await page.ClickAsync("text=2010");
            ////await page.ClickAsync("text=2009");
            ////await page.ClickAsync("text=2008");
            ////await page.ClickAsync("text=2007");
            ////await page.ClickAsync("text=2006");
            ////await page.ClickAsync("text=2005");
            ////await page.ClickAsync("text=2004");
            ////await page.ClickAsync("text=2003");
            ////await page.ClickAsync("text=2002");
            ////await page.ClickAsync("text=2001");
            ////await page.ClickAsync("text=2000");
            ////await page.ClickAsync("text=1999");
            ////await page.ClickAsync("text=1998");
            ////await page.ClickAsync("text=1997");
            ////await page.ClickAsync("text=1996");
            ////await page.ClickAsync("text=1995");
            ////await page.ClickAsync("text=1994");
            ////await page.ClickAsync("text=1993");
            ////await page.ClickAsync("text=1992");
            ////await page.ClickAsync("text=1991");
            await page.ClickAsync("text=Done");
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", "1349900417320");
            await page.ClickAsync("input[name=\"ion-input-3\"]");
            await page.FillAsync("input[name=\"ion-input-3\"]", "ME1213211235");       
            await page.ClickAsync("text=ระบุที่อยู่ตามบัตร ปชช.");
            var page1 = await PageFactory.CreatePage().DoLogin();
            await page1.GotoAsync("http://localhost:8100/#/kyc-add-address");
            await page1.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page1.ClickAsync("input[name=\"ion-input-0\"]");
            await page1.FillAsync("input[name=\"ion-input-0\"]", "9/1");
            await page1.ClickAsync("input[name=\"ion-input-1\"]");
            await page1.FillAsync("input[name=\"ion-input-1\"]", "ในเมือง");
            await page1.ClickAsync("input[name=\"ion-input-2\"]");
            await page1.FillAsync("input[name=\"ion-input-2\"]", "เมือง");
            await page1.ClickAsync("input[name=\"ion-input-3\"]");
            await page1.FillAsync("input[name=\"ion-input-3\"]", "อุบลราชธานี");
            await page1.ClickAsync("input[name=\"ion-input-4\"]");
            await page1.FillAsync("input[name=\"ion-input-4\"]", "34000");
            await page1.ClickAsync("input[name=\"ion-input-5\"]");
            await page1.FillAsync("input[name=\"ion-input-5\"]", "0632130558");
            await page1.ClickAsync("button");

            await page.ClickAsync("text=ตรวจสอบเบอร์โทรศัพท์ของคุณ");
            var page2 = await PageFactory.CreatePage().DoLogin();
            await page2.GotoAsync("http://localhost:8100/#/kyc-tel-confirm");
            await page2.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page2.ClickAsync("input[name=\"ion-input-1\"]");
            await page2.FillAsync("input[name=\"ion-input-1\"]", "0910167715");
            await page2.ClickAsync("button");
            await page.ClickAsync("text=OK >> button");
            await page2.ClickAsync("button");

            const string CreateKYCApi = "https://localhost:44364/mcontent/Submit/";
            var CreateKYCApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateKYCApi);
            if (!CreateKYCApiResponse.Ok)
            {
                return false;
            }

            // app mana เข้าหน้านี้ยังไมได้
            await page.GotoAsync("http://localhost:8100/#/kyc-basic-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.PauseAsync();
            await page.ClickAsync("button");






            return true;
        }


    }
}
