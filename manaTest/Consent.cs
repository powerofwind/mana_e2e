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
    public class Consent
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

        // User อนุมัติการเข้าถึงข้อมูลได้
        public async Task<bool> UserApproveInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";

            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // User ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<bool> UserRejectInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager อนุมัติการเข้าถึงข้อมูลได้
        public async Task<bool> ManagerApproveInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<bool> ManagerRejectInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager อนุมัติการระงับบัญชีได้
        public async Task<bool> ManagerApproveSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager ปฏิเสธการระงับบัญชีได้
        public async Task<bool> ManagerRejectSuspendAccount()
        {

            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager อนุมัติการยกเลิกการระงับบัญชีได้
        public async Task<bool> ManagerApproveCancelSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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

        // Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
        public async Task<bool> ManagerRejectCancelSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
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
