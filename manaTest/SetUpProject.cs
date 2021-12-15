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

        public async Task<bool> ManaMcontent()
        {
            var browser = await BeforeScenario();
            var res = await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/profile/addresses");
            await page.WaitForTimeoutAsync(2000);
            if (!res.Ok)
            {
                return false;
            }
            await page.GotoAsync("http://localhost:8100/#/user-profile-address");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("text=กทม123 ทวีวัฒนา ทวีวัฒนา กรุงเทพมหานคร 101700632130913 >> p");
            await page.WaitForTimeoutAsync(10000);

            return true;
        }
    }
}
