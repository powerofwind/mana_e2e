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

        //[Fact(DisplayName = "เติมเงิน ppay")]
        //public async Task Test4()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.TopUpPPay();
        //    res.Should().Be("Success");
        //}

        [Fact(DisplayName = "สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้")]
        public async Task AddPPayAccount()
        {
            var sut = new SetUpProject();
            var res = await sut.AddPPayAccount();
            res.Should().Be(true);
        }


        // สร้างร้านสำหรับ Business ได้
        // User ปฏิเสธการเข้าถึงข้อมูลได้
        // สร้างการผูกบัญชีธนาคารได้
        // Manager อนุมัติการเข้าถึงข้อมูลได้
        // ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้
        // ส่งคำขอ KYC basic ได้
        // Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
        // แจ้งปัญหาไปยังทีม Support ได้
        // ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
        // ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ
        // ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้
        // สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้
        // ถอนเงินออกขากร้าน Business เข้ากระเป๋าเงิน Mana ได้
        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้
        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้
        // สร้าง QR ร้าน Business ได้
        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้
        // ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้
        // Manager ปฏิเสธการเข้าถึงข้อมูลได้
        // ถองเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ
        // Manager อนุมัติการระงับบัญชีได้
        // Manager ปฏิเสธการระงับบัญชีได้
        // User อนุมัติการเข้าถึงข้อมูลได้
        // Manager อนุมัติการยกเลิกการระงับบัญชีได้

    }
}
