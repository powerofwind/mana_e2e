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
    public class Topup
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





        // ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
        public async Task<bool> TopUpPPay()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=0910167715");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupPPayApi = "https://localhost:44364/mcontent/Submit/";
            var TopupPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupPPayApi);
            if (!TopupPPayApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
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
                dialog.AcceptAsync("25.00");
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

        // สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้
        public async Task<bool> TopUpCreateQR()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=สร้างคิวอาร์โค้ดเพื่อเติมเงิน");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupBankApi = "https://localhost:44364/mcontent/Submit/";
            var TopupBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupBankApi);
            if (!TopupBankApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
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
                dialog.AcceptAsync("20.00");
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

        // ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้
        public async Task<bool> TopUpbanking()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=0123456789123");


            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupBankApi = "https://localhost:44364/mcontent/Submit/";
            var TopupBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupBankApi);
            if (!TopupBankApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
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
                dialog.AcceptAsync("30.00");
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
    }
}
