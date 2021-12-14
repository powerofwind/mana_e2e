using Microsoft.Playwright;
using System;
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

        public async Task<string> CheckDialogMessageInAlertBrowser()
        {
            var browser = await BeforeScenario();
            page = await browser.NewPageAsync();
            await page.GotoAsync("https://letcode.in/alert");
            var dialogMessage = string.Empty;
            page.Dialog += async (_, dialog) =>
            {
                dialogMessage = dialog.Message;
                await dialog.DismissAsync(); //close dialog
                //await dialog.AcceptAsync(); //confirm dialog
            };
            await page.ClickAsync("text=Simple Alert"); //dialog ok
            //await page.ClickAsync("text=Confirm Alert"); //dialog Yes No
            return dialogMessage;
        }

        public async Task<string> InputTextInAlertBrowser()
        {
            var browser = await BeforeScenario();
            page = await browser.NewPageAsync();
            await page.GotoAsync("https://letcode.in/alert");
            //await page.GotoAsync("http://localhost:8101/#/wallet-topup-ppay"); //เทสกับมานะ
            var dialogMessage = string.Empty;
            page.Dialog += async (_, dialog) =>
            {
                dialogMessage = dialog.Message;
                await dialog.AcceptAsync("123"); //ใส่ input ใน dialog แล้วกดตกลง
            };
            await page.ClickAsync("text=Prompt Alert");
            //await page.ClickAsync("input[name=\"ion-input-1\"]"); //เทสกับมานะ
            var text = await page.InnerTextAsync("text=Prompt AlertYour name is: 123 >> div");
            return text;
        }
    }
}
