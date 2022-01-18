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
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
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
            await page.FillAsync("input[name=\"ion-input-0\"]", "testE2E");
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", "1234567890");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/account-confirm-bankaccount");
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            return true;

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }

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

        // สร้าง QR ร้าน Business ได้
        public async Task<bool> CreatQRBusiness()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");
            if (!isInitSuccess)
            {
                return false;
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.ClickAsync("text=คิวอาร์รับเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-qr-receive-money");
            // check หน้าเปิด Qr
            return true;
        }

        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้
        public async Task<bool> CannotWithdrawPPayNeverTopup()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");
            if (!isInitSuccess)
            {
                return false;
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> :nth-match(img, 3)");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("text=ppteste220632138965");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            return true;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                Console.WriteLine($"Dialog message: {dialog.Message}");
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
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

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> :nth-match(img, 3)");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            page.Dialog += page_Dialog2_EventHandler;
            await page.ClickAsync("text=bankteste224520342438");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            return true;

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                Console.WriteLine($"Dialog message: {dialog.Message}");
                dialog.DismissAsync();
                page.Dialog -= page_Dialog2_EventHandler;
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

            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("text=pprora0910167715");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);


            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;


            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("100.00");
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
        public async Task<string> WithdrawBankingSuccess()
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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=bank0123456789123");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;


            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("100.00");
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
        public async Task<string> NotWithdrawPPayMoneyNotEnough()
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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("text=pprora0910167715");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);


            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;


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
        public async Task<string> NotWithdrawBankingMoneyNotEnough()
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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("text=bank0123456789123");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(2000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
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
            await page.ClickAsync("text=เติมเงินMana WalletXTHB3,813.00ถอนเงิน >> img");
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

        // ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้
        public async Task<string> withdrawBusinessShop()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");
            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.ClickAsync("text=ถอนเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw-confirm");
            await page.WaitForTimeoutAsync(1000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(4000);
            page.Dialog += page_Dialog5_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(7000);

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

        // User อนุมัติการเข้าถึงข้อมูลได้
        public async Task<string> UserApproveInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // User ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<string> UserRejectInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager อนุมัติการเข้าถึงข้อมูลได้
        public async Task<string> ManagerApproveInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<string> ManagerRejectInfo()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager อนุมัติการระงับบัญชีได้
        public async Task<string> ManagerApproveSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager ปฏิเสธการระงับบัญชีได้
        public async Task<string> ManagerRejectSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager อนุมัติการยกเลิกการระงับบัญชีได้
        public async Task<string> ManagerApproveCancelSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")"); await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }

        // Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
        public async Task<string> ManagerRejectCancelSuspendAccount()
        {
            var isInitSuccess = await ManaMcontent("https://localhost:44364/dev/visit?url=test-home-feed");

            if (!isInitSuccess)
            {
                return "Can not InitPage";
            }

            var dialogMessage = string.Empty;
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")");
            await page.WaitForTimeoutAsync(2000);
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog2_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog1_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(4000);
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            return result.status;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            void page_Dialog2_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog2_EventHandler;
            }
        }


    }
}
