using FluentAssertions;
using manaTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class UnitTest1
    {
        [Fact(DisplayName = "เข้าหน้า Ticket ได้")]
        public async Task Test1()
        {
            var sut = new SetUpProject();
            sut.ManaMcontent();

            var res = await sut.ManaMcontent();
            res.Should().Be("");
        }
    }
}
