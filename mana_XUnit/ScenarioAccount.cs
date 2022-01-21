using FluentAssertions;
using manaTest;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class ScenarioAccount
    {
        [Fact(DisplayName = "สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้")]
        public async Task AddPPayAccountByPID()
        {
            var sut = new Account();
            var res = await sut.AddPPayAccountByPID();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้")]
        public async Task AddPPayAccountByPhoneNumber()
        {
            var sut = new Account();
            var res = await sut.AddPPayAccountByPhoneNumber();
            res.Should().Be(true);
        }



        [Fact(DisplayName = "สร้างการผูกบัญชีธนาคารได้")]
        public async Task AddBankingAccount()
        {
            var sut = new Account();
            var res = await sut.AddBankingAccount();
            res.Should().Be(true);
        }
    }
}
