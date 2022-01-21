using FluentAssertions;
using manaTest;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class ScenarioTopup
    {
        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้")]
        public async Task TopUpPPay()
        {
            var sut = new Topup();
            var res = await sut.TopUpPPay();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้")]
        public async Task TopUpCreateQR()
        {
            var sut = new Topup();
            var res = await sut.TopUpCreateQR();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้")]
        public async Task TopUpbanking()
        {
            var sut = new Topup();
            var res = await sut.TopUpbanking();
            res.Should().Be(true);
        }

    }
}
