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
    public class Withdraw
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


        // ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
        public async Task<bool> WithdrawPPaySuccess()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawPPayByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476419455827%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=pprora0910167715"), WithdrawPPayByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
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


        // ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้ 
        public async Task<bool> WithdrawBankingSuccess()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476860532877%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bank0123456789123"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountbankApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountbankApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
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

        // ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ
        public async Task<bool> NotWithdrawPPayMoneyNotEnough()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawPPayByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476419455827%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=pprora0910167715"), WithdrawPPayByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string WithdrawAmountComfirmPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22wallet-withdraw-bankaccount-confirm%22,%22triggerName%22:%22Button1%22%7D";
            var AmountSubmitComfirmResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountComfirmPPayApi);
            if (!AmountSubmitComfirmResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(2000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);

            if (result.status == "Fail")
            {
                return true;
            }
            return false;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("4500.00");
                page.Dialog -= page_Dialog3_EventHandler;
            }

            void page_Dialog5_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog5_EventHandler;
            }
        }

        // ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ
        public async Task<bool> NotWithdrawBankingMoneyNotEnough()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476860532877%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bank0123456789123"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawAmountComfirmPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22wallet-withdraw-bankaccount-confirm%22,%22triggerName%22:%22Button1%22%7D";
            var AmountSubmitComfirmResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountComfirmPPayApi);
            if (!AmountSubmitComfirmResponse.Ok)
            {
                return false;
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);

            if (result.status == "Fail")
            {
                return true;
            }
            return false;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("4500.00");
                page.Dialog -= page_Dialog3_EventHandler;
            }

            void page_Dialog5_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog5_EventHandler;
            }
        }


        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้
        public async Task<bool> CannotWithdrawPPayNeverTopup()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fdlg%2Fconditional%2Finfo%2Fnwltwit-637777394584866804%3Faptx%3DGoToActivateExternalAccountFlow%26nextnp%3Dwallet%2Fdeposit%2Fppay%2Frequest%2Fnwltdep-637777394584866804%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ppteste220632138965"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            page.Dialog -= page_Dialog2_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }

        }

        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้
        public async Task<bool> CannotWithdrawBankingNeverTopup()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return false;
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fdlg%2Fconditional%2Finfo%2Fnwltwit-637777395161877564%3Faptx%3DGoToActivateExternalAccountFlow%26nextnp%3Dwallet%2Fdepositqr%2Facc%2Frequest%2Fnwltdep-637777395161877564%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bankteste224520342438"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return false;
            }

            page.Dialog -= page_Dialog2_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return true;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                Console.WriteLine($"Dialog message: {dialog.Message}");
                dialog.DismissAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

    }
}

