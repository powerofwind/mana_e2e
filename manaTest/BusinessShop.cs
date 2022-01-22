using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mana_Test.Models;
using manaTest.Helpers;
using Microsoft.Playwright;
using System.Text.Json;

namespace manaTest
{
    public class BusinessShop
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


        // สร้างร้านสำหรับ Business ได้
        public async Task<bool> CreateBusinessShop()
        {           
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-create$shop");
            if (!isInitSuccess)
            {
                return false;
            }
  
            await page.GotoAsync("http://localhost:8100/#/merchant-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "testE2Eshop889900");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("button");
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

        // ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้
        public async Task<bool> withdrawBusinessShop()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("text=ถอนเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountBusinessApi = "https://localhost:44364/mcontent/Submit/";
            var WithdrawAmountBusinessApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), WithdrawAmountBusinessApi);
            if (!WithdrawAmountBusinessApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");

            await page.WaitForTimeoutAsync(6000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("1.00");
                page.Dialog -= page_Dialog3_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
            void page_Dialog5_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }


        // สร้าง QR ร้าน Business ได้
        public async Task<bool> CreatQRBusiness()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("text=คิวอาร์รับเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-qr-receive-money");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;
        }
    }
}
