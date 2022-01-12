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








        // ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้
        // ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
        // สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้


        // ส่งคำขอ KYC basic ได้
        // ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ
        // ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
        // ถอนเงินออกขากร้าน Business เข้ากระเป๋าเงิน Mana ได้
        // ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้     
        // ถองเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ

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


// consent case
// User ปฏิเสธการเข้าถึงข้อมูลได้
// Manager อนุมัติการเข้าถึงข้อมูลได้
// Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
// Manager ปฏิเสธการเข้าถึงข้อมูลได้
// Manager อนุมัติการระงับบัญชีได้
// Manager ปฏิเสธการระงับบัญชีได้
// User อนุมัติการเข้าถึงข้อมูลได้
// Manager อนุมัติการยกเลิกการระงับบัญชีได้




