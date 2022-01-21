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
        public async Task<string> ReportIssue()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nrptdtl-create");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/report-create");
            await page.WaitForTimeoutAsync(2000);
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
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }
        }


        // ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
        public async Task<string> WithdrawPPaySuccess()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return "Fail";
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawPPayByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476419455827%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=pprora0910167715"), WithdrawPPayByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return "Fail";
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return "Fail";
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string WithdrawAmountComfirmPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22wallet-withdraw-bankaccount-confirm%22,%22triggerName%22:%22Button1%22%7D";
            var AmountSubmitComfirmResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountComfirmPPayApi);
            if (!AmountSubmitComfirmResponse.Ok)
            {
                return "Fail";
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.WaitForTimeoutAsync(6000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;


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


        ////// ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้ 
        ////public async Task<string> WithdrawBankingSuccess()
        ////{
        ////    var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

        ////    if (!isInitSuccess)
        ////    {
        ////        return "Can not InitPage";
        ////    }

        ////    await page.GotoAsync("http://localhost:8100/#/financial-menu");
        ////    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        ////    var dialogMessage = string.Empty;

        ////    const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
        ////    var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
        ////    if (!WithdrawResponse.Ok)
        ////    {
        ////        return "Fail";
        ////    }

        ////    await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
        ////    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        ////    const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476860532877%22%7D";
        ////    var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bank0123456789123"), WithdrawBankByIDApi);
        ////    if (!WalletWithdrawResponse.Ok)
        ////    {
        ////        return "Fail";
        ////    }

        ////    await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
        ////    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        ////    page.Dialog += page_Dialog3_EventHandler;
        ////    await page.ClickAsync("input[name=\"ion-input-1\"]");

        ////    const string WithdrawAmountbankApi = "https://localhost:44364/mcontent/Submit/";
        ////    var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountbankApi);
        ////    if (!AmountSubmitResponse.Ok)
        ////    {
        ////        return "Fail";
        ////    }

        ////    await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
        ////    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        ////    const string WithdrawAmountComfirmBankingApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22wallet-withdraw-bankaccount-confirm%22,%22triggerName%22:%22Button1%22%7D";
        ////    var AmountSubmitComfirmResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountComfirmBankingApi);
        ////    if (!AmountSubmitComfirmResponse.Ok)
        ////    {
        ////        return "Fail";
        ////    }

        ////    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        ////    page.Dialog += page_Dialog2_EventHandler;
        ////    await page.WaitForTimeoutAsync(2000);
        ////    page.Dialog += page_Dialog5_EventHandler;
        ////    await page.WaitForTimeoutAsync(6000);

        ////    var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
        ////    return result.status;

        ////    void page_Dialog3_EventHandler(object sender, IDialog dialog)
        ////    {
        ////        dialog.AcceptAsync("1.00");
        ////        page.Dialog -= page_Dialog3_EventHandler;
        ////    }

        ////    void page_Dialog2_EventHandler(object sender, IDialog dialog)
        ////    {
        ////        dialog.AcceptAsync();
        ////        page.Dialog -= page_Dialog2_EventHandler;
        ////    }
        ////    void page_Dialog5_EventHandler(object sender, IDialog dialog)
        ////    {
        ////        dialogMessage = dialog.Message;
        ////        dialog.DismissAsync();
        ////        page.Dialog -= page_Dialog2_EventHandler;
        ////    }
        ////}

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

        

        // ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
        public async Task<string> TopUpPPay()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.ClickAsync("text=0910167715");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

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
        public async Task<string> TopUpCreateQR()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            var dialogMessage = string.Empty;
            await page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img");
            await page.WaitForTimeoutAsync(1000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.ClickAsync("text=สร้างคิวอาร์โค้ดเพื่อเติมเงิน");
            await page.WaitForTimeoutAsync(1000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-create");
            await page.WaitForTimeoutAsync(1000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

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
        public async Task<string> TopUpbanking()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.ClickAsync("text=0123456789123");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

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

        // ส่งคำขอ KYC basic ได้ ยังไม่เสร็จ
        public async Task<string> SendRequestKYCBasic()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/kyc/basic/visit/nkycbsc-180056522489857");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/kyc-agreement");
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/kyc-basic-create");
            //await page.ClickAsync("input[name=\"ion-input-0\"]");
            //await page.FillAsync("input[name=\"ion-input-0\"]", "เตชะพงศ์");
            //await page.ClickAsync("input[name=\"ion-input-1\"]");
            //await page.FillAsync("input[name=\"ion-input-1\"]", "ขำคม");
            //await page.ClickAsync("button");
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
            //await page.ClickAsync("text=Done");
            //await page.ClickAsync("input[name=\"ion-input-2\"]");
            //await page.FillAsync("input[name=\"ion-input-2\"]", "1349900417320");
            //await page.ClickAsync("input[name=\"ion-input-3\"]");
            //await page.FillAsync("input[name=\"ion-input-3\"]", "ME1213211235");
            //await page.ClickAsync("text=ระบุที่อยู่ตามบัตร ปชช.");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/kyc-add-address");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("input[name=\"ion-input-4\"]");
            await page.FillAsync("input[name=\"ion-input-4\"]", "9/1");
            await page.ClickAsync("input[name=\"ion-input-5\"]");
            await page.FillAsync("input[name=\"ion-input-5\"]", "ในเมือง");
            await page.ClickAsync("input[name=\"ion-input-6\"]");
            await page.FillAsync("input[name=\"ion-input-6\"]", "เมือง");
            await page.ClickAsync("input[name=\"ion-input-7\"]");
            await page.FillAsync("input[name=\"ion-input-7\"]", "อุบลราชธานี");
            await page.ClickAsync("input[name=\"ion-input-8\"]");
            await page.FillAsync("input[name=\"ion-input-8\"]", "34000");
            await page.ClickAsync("input[name=\"ion-input-9\"]");
            await page.FillAsync("input[name=\"ion-input-9\"]", "0632130558");
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/kyc-basic-create");

            return "Success";
        }
    

    }
}
