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
    public class ScenarioConsent
    {
        [Fact(DisplayName = "User อนุมัติการเข้าถึงข้อมูลได้")]
        public async Task UserApproveInfo()
        {
            var sut = new Consent();
            var res = await sut.UserApproveInfo();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "User ปฏิเสธการเข้าถึงข้อมูลได้")]
        public async Task UserRejectInfo()
        {
            var sut = new Consent();
            var res = await sut.UserRejectInfo();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager อนุมัติการเข้าถึงข้อมูลได้")]
        public async Task ManagerApproveInfo()
        {
            var sut = new Consent();
            var res = await sut.ManagerApproveInfo();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager ปฏิเสธการเข้าถึงข้อมูลได้")]
        public async Task ManagerRejectInfo()
        {
            var sut = new Consent();
            var res = await sut.ManagerRejectInfo();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager อนุมัติการระงับบัญชีได้")]
        public async Task ManagerApproveSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerApproveSuspendAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager ปฏิเสธการระงับบัญชีได้")]
        public async Task ManagerRejectSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerRejectSuspendAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager อนุมัติการยกเลิกการระงับบัญชีได้")]
        public async Task ManagerApproveCancelSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerApproveCancelSuspendAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "Manager ปฏิเสธการยกเลิกการระงับบัญชีได้")]
        public async Task ManagerRejectCancelSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerRejectCancelSuspendAccount();
            res.Should().Be(true);
        }
    }
}
