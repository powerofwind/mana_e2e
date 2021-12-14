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
                page = await browser.NewPageAsync();
            }
            return browser;
        }

        public async Task<string> ManaMcontent()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("http://localhost:8100/#/user-profile-address");
            await page.ClickAsync("text=กทม123 ทวีวัฒนา ทวีวัฒนา กรุงเทพมหานคร 101700632130913 >> p");
            var ttt = string.Empty;
            page.Dialog += async (_, dialog) =>
            {
                Console.WriteLine(dialog.Message);
                ttt = dialog.Message;
                await dialog.DismissAsync();
            };
            await page.WaitForTimeoutAsync(5000);
            return ttt;
        }
    }
}
