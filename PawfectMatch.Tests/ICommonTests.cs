using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.Tests
{
    public interface ICommonTests
    {
        [Fact]
        public Task InsertAsync();
        [Fact]
        public Task UpdateAsync();
        [Fact]
        public Task SearchByIdAsync();
        [Fact]
        public Task ExistAsync();
        [Fact]
        public Task ListAsync();
        [Fact]
        public Task SaveAsync();
        [Fact]
        public Task DeleteAsync();

    }
}
