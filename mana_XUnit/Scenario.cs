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

      

        [Fact(DisplayName = "แจ้งปัญหาไปยังทีม Support ได้")]
        public async Task ReportIssue()
        {
            var sut = new SetUpProject();
            var res = await sut.ReportIssue();
            res.Should().Be(true);
        }

      
        [Fact(DisplayName = "ส่งคำขอ KYC basic ได้")]
        public async Task SendRequestKYCBasic()
        {
            var sut = new SetUpProject();
            var res = await sut.SendRequestKYCBasic();
            res.Should().Be(true);
        }
      

     


        //ส่งคำขอ KYC basic ได้(รอส่งข้อมูลข้ามหน้า)
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










