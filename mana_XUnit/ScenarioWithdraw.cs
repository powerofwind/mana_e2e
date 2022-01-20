using FluentAssertions;
using manaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class ScenarioWithdraw
    {

        [Fact(DisplayName = "ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้")]
        public async Task WithdrawPPaySuccess()
        {
            var sut = new Withdraw();
            var res = await sut.WithdrawPPaySuccess();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้")]
        public async Task WithdrawBankingSuccess()
        {
            var sut = new Withdraw();
            var res = await sut.WithdrawBankingSuccess();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ")]
        public async Task NotWithdrawPPayMoneyNotEnough()
        {
            var sut = new Withdraw();
            var res = await sut.NotWithdrawPPayMoneyNotEnough();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ")]
        public async Task NotWithdrawBankingMoneyNotEnough()
        {
            var sut = new Withdraw();
            var res = await sut.NotWithdrawBankingMoneyNotEnough();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้")]
        public async Task CannotWithdrawPPayNeverTopup()
        {
            var sut = new Withdraw();
            var res = await sut.CannotWithdrawPPayNeverTopup();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้")]
        public async Task CannotWithdrawBankingNeverTopup()
        {
            var sut = new Withdraw();
            var res = await sut.CannotWithdrawBankingNeverTopup();
            res.Should().Be(true);
        }

    }
}
