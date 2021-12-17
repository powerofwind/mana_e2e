using FluentAssertions;
using manaTest;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class Scenario
    {
        //[Fact(DisplayName = "Go to address page")]
        //public async Task Test1()
        //{
        //    var sut = new SetUpProject();

        //    var res = await sut.ManaMcontent();
        //    res.Should().Be(true);
        //}

        [Fact(DisplayName = "เช็ค Text ใน Alert Browser")]
        public async Task Test2()
        {
            var sut = new SetUpProject();
            var res = await sut.CheckDialogMessageInAlertBrowser();
            ////ClickAsync("text=Simple Alert")
            res.Should().Be("Hey! Welcome to LetCode");

            ////ClickAsync("text=Confirm Alert")
            //res.Should().Be("Are you happy with LetCode?");
        }

        [Fact(DisplayName = "กรอก input ใน Alert Browser")]
        public async Task Test3()
        {
            var sut = new SetUpProject();
            var res = await sut.InputTextInAlertBrowser();
            res.Should().Be("Your name is: 123");
        }

        [Fact(DisplayName = "เติมเงิน ppay")]
        public async Task Test4()
        {
            var sut = new SetUpProject();
            var res = await sut.TopUpPPay();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "สร้างบัญชี ppay")]
        public async Task AddPPayAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.AddPPayAccount();
            res.Should().Be("กระเป๋าที่เติมเข้า");
        }
        
    }
}
