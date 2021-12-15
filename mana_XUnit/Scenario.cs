using FluentAssertions;
using manaTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace mana_XUnit
{
    public class Scenario
    {
        [Fact(DisplayName = "Go to address page")]
        public async Task Test1()
        {
            var sut = new SetUpProject();

            var res = await sut.ManaMcontent();
            res.Should().Be(true);
        }
    }
}
