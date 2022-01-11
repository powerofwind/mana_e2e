using mana_Test.Models;
using Microsoft.Playwright;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class SetUpProject
    {
        private static IPage page;
        private static IBrowser browser;
        private static IPlaywright playwright;

        public async Task<IBrowser> BeforeScenario()
        {
            playwright ??= await Playwright.CreateAsync();
            if (null == browser)
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 1000,
                });
            }
            return browser;
        }

        public async Task<bool> ManaMcontent(string url)
        {
            var browser = await BeforeScenario();
            page = await browser.NewPageAsync();
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

        //public async Task<string> TopUpPPay()
        //{
        //    var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
        //    if (!isInitSuccess)
        //    {
        //        return "Can not InitPage";
        //    }
        //    await page.WaitForTimeoutAsync(2000);
        //    await page.GotoAsync("http://localhost:8100/#/financial-menu");
        //    var dialogMessage = string.Empty;
        //    page.Dialog += page_Dialog1_EventHandler;
        //    await page.ClickAsync("ion-col:has-text(\"เติมเงิน\")");
        //    await page.WaitForTimeoutAsync(2000);

        //    await page.GotoAsync("http://localhost:8100/#/wallet-topup");
        //    await page.WaitForTimeoutAsync(2000);
        //    page.Dialog += page_Dialog2_EventHandler;
        //    await page.ClickAsync("text=devmasterpp");
        //    await page.WaitForTimeoutAsync(2000);

        //    await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
        //    await page.WaitForTimeoutAsync(2000);
        //    page.Dialog += page_Dialog3_EventHandler;
        //    await page.ClickAsync("input[name=\"ion-input-1\"]");
        //    await page.WaitForTimeoutAsync(2000);
        //    await page.ClickAsync("button");
        //    await page.WaitForTimeoutAsync(2000);

        //    await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay-confirm");
        //    await page.WaitForTimeoutAsync(2000);
        //    page.Dialog += page_Dialog4_EventHandler;
        //    page.Dialog += page_Dialog5_EventHandler;
        //    await page.ClickAsync("button");
        //    await page.WaitForTimeoutAsync(5000);

        //    void page_Dialog1_EventHandler(object sender, IDialog dialog)
        //    {
        //        dialog.DismissAsync();
        //        page.Dialog -= page_Dialog1_EventHandler;
        //    }
        //    void page_Dialog2_EventHandler(object sender, IDialog dialog)
        //    {
        //        dialog.DismissAsync();
        //        page.Dialog -= page_Dialog2_EventHandler;
        //    }
        //    void page_Dialog3_EventHandler(object sender, IDialog dialog)
        //    {
        //        dialog.AcceptAsync("25.00");
        //        page.Dialog -= page_Dialog3_EventHandler;
        //    }
        //    void page_Dialog4_EventHandler(object sender, IDialog dialog)
        //    {
        //        dialog.DismissAsync();
        //        page.Dialog -= page_Dialog4_EventHandler;
        //    }
        //    void page_Dialog5_EventHandler(object sender, IDialog dialog)
        //    {
        //        dialogMessage = dialog.Message;
        //        dialog.DismissAsync();
        //        page.Dialog -= page_Dialog4_EventHandler;
        //    }

        //    var result = JsonSerializer.Deserialize<TopUp>(dialogMessage);
        //    return result.Status;
        //}

        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้
        public async Task<bool> AddPPayAccountByPhoneNumber()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.ClickAsync("text=พร้อมเพย์");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เบอร์โทรศัพท์\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=OK >> button");
            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForTimeoutAsync(2000);
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

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.ClickAsync("text=พร้อมเพย์");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เลขบัตรประชาชน\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715123");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=OK >> button");
            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            var a = await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForTimeoutAsync(2000);
            return true;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }

        // สร้างร้านสำหรับ Business ได้
        public async Task<string> CreateBusinessShop()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-create$shop");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/merchant-create");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "testE2Eshop889900");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("button");
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


        // สร้างการผูกบัญชีธนาคารได้
        public async Task<bool> AddBankingAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.ClickAsync("text=บัญชีธนาคาร");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/account-create-bankaccount");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "8888888");
            await page.ClickAsync("text=กรุงไทย");
            await page.GotoAsync("http://localhost:8100/#/account-bank-select");
            await page.ClickAsync("ion-item:nth-child(6) .qr");
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/account-create-bankaccount");

            return true;

            //await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            //await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            //await page.ClickAsync("button:has-text(\"เลขบัตรประชาชน\")");
            //await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715123");
            //await page.WaitForTimeoutAsync(2000);
            //await page.ClickAsync("text=OK >> button");
            //await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            //await page.ClickAsync("text=OK >> button");
            //await page.WaitForTimeoutAsync(2000);
            //page.Dialog += page_Dialog3_EventHandler;
            //await page.WaitForTimeoutAsync(2000);
            //page.Dialog += page_Dialog3_EventHandler;
            //var a = await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            //await page.WaitForTimeoutAsync(2000);

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }
    }
}
