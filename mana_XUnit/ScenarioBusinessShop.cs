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
    public class ScenarioBusinessShop
    {
        [Fact(DisplayName = "สร้างร้านสำหรับ Business ได้")]
        public async Task CreateBusinessShop()
        {
            var sut = new BusinessShop();
            var res = await sut.CreateBusinessShop();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้าง QR ร้าน Business ได้")]
        public async Task CreatQRBusiness()
        {
            var sut = new BusinessShop();
            var res = await sut.CreatQRBusiness();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้")]
        public async Task withdrawBusinessShop()
        {
            var sut = new BusinessShop();
            var res = await sut.withdrawBusinessShop();
            res.Should().Be(true);
        }
    }
}
