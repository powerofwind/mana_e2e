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

        //[Fact(DisplayName = "เช็ค Text ใน Alert Browser")]
        //public async Task Test2()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.CheckDialogMessageInAlertBrowser();
        //    ////ClickAsync("text=Simple Alert")
        //    res.Should().Be("Hey! Welcome to LetCode");

        //    ////ClickAsync("text=Confirm Alert")
        //    //res.Should().Be("Are you happy with LetCode?");
        //}

        //[Fact(DisplayName = "กรอก input ใน Alert Browser")]
        //public async Task Test3()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.InputTextInAlertBrowser();
        //    res.Should().Be("Your name is: 123");
        //}

        [Fact(DisplayName = "สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้")]
        public async Task AddPPayAccountByPID()
        {
            var sut = new SetUpProject();
            var res = await sut.AddPPayAccountByPID();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้")]
        public async Task AddPPayAccountByPhoneNumber()
        {
            var sut = new SetUpProject();
            var res = await sut.AddPPayAccountByPhoneNumber();
            res.Should().Be(true);
        }


        [Fact(DisplayName = "สร้างร้านสำหรับ Business ได้")]
        public async Task CreateBusinessShop()
        {
            var sut = new SetUpProject();
            var res = await sut.CreateBusinessShop();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "สร้างการผูกบัญชีธนาคารได้")]
        public async Task AddBankingAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.AddBankingAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "แจ้งปัญหาไปยังทีม Support ได้")]
        public async Task ReportIssue()
        {
            var sut = new SetUpProject();
            var res = await sut.ReportIssue();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "สร้าง QR ร้าน Business ได้")]
        public async Task CreatQRBusiness()
        {
            var sut = new SetUpProject();
            var res = await sut.CreatQRBusiness();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้")]
        public async Task CannotWithdrawPPayNeverTopup()
        {
            var sut = new SetUpProject();
            var res = await sut.CannotWithdrawPPayNeverTopup();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้")]
        public async Task CannotWithdrawBankingNeverTopup()
        {
            var sut = new SetUpProject();
            var res = await sut.CannotWithdrawBankingNeverTopup();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้")]
        public async Task TopUpPPay()
        {
            var sut = new SetUpProject();
            var res = await sut.TopUpPPay();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้")]
        public async Task TopUpCreateQR()
        {
            var sut = new SetUpProject();
            var res = await sut.TopUpCreateQR();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้")]
        public async Task TopUpbanking()
        {
            var sut = new SetUpProject();
            var res = await sut.TopUpbanking();
            res.Should().Be("Success");
        }


        [Fact(DisplayName = "ส่งคำขอ KYC basic ได้")]
        public async Task SendRequestKYCBasic()
        {        
            var sut = new SetUpProject();
            var res = await sut.SendRequestKYCBasic();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "User อนุมัติการเข้าถึงข้อมูลได้")]
        public async Task UserApproveInfo()
        {
            var sut = new SetUpProject();
            var res = await sut.UserApproveInfo();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "User ปฏิเสธการเข้าถึงข้อมูลได้")]
        public async Task UserRejectInfo()
        {
            var sut = new SetUpProject();
            var res = await sut.UserRejectInfo();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager อนุมัติการเข้าถึงข้อมูลได้")]
        public async Task ManagerApproveInfo()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerApproveInfo();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager ปฏิเสธการเข้าถึงข้อมูลได้")]
        public async Task ManagerRejectInfo()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerRejectInfo();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager อนุมัติการระงับบัญชีได้")]
        public async Task ManagerApproveSuspendAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerApproveSuspendAccount();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager ปฏิเสธการระงับบัญชีได้")]
        public async Task ManagerRejectSuspendAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerRejectSuspendAccount();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager อนุมัติการยกเลิกการระงับบัญชีได้")]
        public async Task ManagerApproveCancelSuspendAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerApproveCancelSuspendAccount();
            res.Should().Be("Success");
        }

        [Fact(DisplayName = "Manager ปฏิเสธการยกเลิกการระงับบัญชีได้")]
        public async Task ManagerRejectCancelSuspendAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.ManagerRejectCancelSuspendAccount();
            res.Should().Be("Success");
        }

        //[Fact(DisplayName = "ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้")]
        //public async Task withdrawBusinessShop()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.withdrawBusinessShop();
        //    res.Should().Be("Success");
        //}

        [Fact(DisplayName = "ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้")]
        public async Task WithdrawPPaySuccess()
        {
            var sut = new SetUpProject();
            var res = await sut.WithdrawPPaySuccess();
            res.Should().Be("Fail");
        }

        [Fact(DisplayName = "ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้")]
        public async Task WithdrawBankingSuccess()
        {
            var sut = new SetUpProject();
            var res = await sut.WithdrawBankingSuccess();
            res.Should().Be("Fail");
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ")]
        public async Task NotWithdrawPPayMoneyNotEnough()
        {
            var sut = new SetUpProject();
            var res = await sut.NotWithdrawPPayMoneyNotEnough();
            res.Should().Be("Fail");
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ")]
        public async Task NotWithdrawBankingMoneyNotEnough()
        {
            var sut = new SetUpProject();
            var res = await sut.NotWithdrawBankingMoneyNotEnough();
            res.Should().Be("Fail");
        }

        // ส่งคำขอ KYC basic ได้
    }
}

//done case
// สร้างการผูกบัญชีธนาคารได้
// สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้
// สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้
// สร้างร้านสำหรับ Business ได้
// แจ้งปัญหาไปยังทีม Support ได้
// สร้าง QR ร้าน Business ได้
// ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้
// ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้
// ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
// สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้
// ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้
// ถอนเงินออกขากร้าน Business เข้ากระเป๋าเงิน Mana ได้
// User อนุมัติการเข้าถึงข้อมูลได้
// User ปฏิเสธการเข้าถึงข้อมูลได้
// Manager อนุมัติการเข้าถึงข้อมูลได้
// Manager ปฏิเสธการเข้าถึงข้อมูลได้
// Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
// Manager อนุมัติการยกเลิกการระงับบัญชีได้
// Manager อนุมัติการระงับบัญชีได้
// Manager ปฏิเสธการระงับบัญชีได้
// ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
// ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้ 
// ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ
// ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ










