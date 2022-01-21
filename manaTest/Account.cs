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
    public class Account
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

        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้
        public async Task<bool> AddPPayAccountByPhoneNumber()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return false;
            }
   
            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=พร้อมเพย์");
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เบอร์โทรศัพท์\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715");

            const string CreateAccountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountPPayApi);
            if (!CreateAccountPPayApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateAccountPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-ppay%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountPPayApi);
            if (!ComfirmCreateAccountPPayApiResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }

        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้
        public async Task<bool> AddPPayAccountByPID()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=พร้อมเพย์");
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เลขบัตรประชาชน\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715123");

            const string CreateAccountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountPPayApi);
            if (!CreateAccountPPayApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateAccountPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-ppay%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountPPayApi);
            if (!ComfirmCreateAccountPPayApiResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }          
        }

        // สร้างการผูกบัญชีธนาคารได้
        public async Task<bool> AddBankingAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=บัญชีธนาคาร");
            await page.GotoAsync("http://localhost:8100/#/account-create-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "testE2E");
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", "1234567890");

            const string CreateAccountBankApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountBankApi);
            if (!CreateAccountBankApiResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateAccountbamkApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-bankaccount%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountbamkApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountbamkApi);
            if (!ComfirmCreateAccountbamkApiResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }
    }
}
