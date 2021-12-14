using FluentAssertions;
using manaTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class UnitTest1
    {
        [Fact(DisplayName = "เช็ค Text ใน Alert Browser")]
        public async Task Test1()
        {
            var sut = new SetUpProject();
            var res = await sut.CheckDialogMessageInAlertBrowser();
            ////ClickAsync("text=Simple Alert")
            res.Should().Be("Hey! Welcome to LetCode");

            ////ClickAsync("text=Confirm Alert")
            //res.Should().Be("Are you happy with LetCode?");
        }

        [Fact(DisplayName = "กรอก input ใน Alert Browser")]
        public async Task Test2()
        {
            var sut = new SetUpProject();
            var res = await sut.InputTextInAlertBrowser();
            res.Should().Be("Your name is: 123");
        }
    }
}
