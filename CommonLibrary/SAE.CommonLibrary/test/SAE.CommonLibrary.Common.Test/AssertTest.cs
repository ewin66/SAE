using System;
using Xunit;
using assert = SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.Common.Check;
using SAE.Test.Infrastructure;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.Common.Test
{
    public class AssertTest:BaseTest
    {
        public AssertTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void IsNullOrWhiteSpace()
        {
            string str = null;
            assert.Assert.Build(str)
                         .IsNullOrWhiteSpace();
            str = "...";
            var ex= Xunit.Assert.Throws<SAEException>(()=>
            {
                assert.Assert.Build(str)
                      .IsNullOrWhiteSpace();
            });
            this.Show(ex.Message);
        }

        [Fact]
        public void NotNullOrWhiteSpace()
        {
            string str = "...";
            assert.Assert.Build(str)
                         .NotNullOrWhiteSpace();
            str = null;
            var ex = Xunit.Assert.Throws<SAEException>(() =>
            {
                assert.Assert.Build(str)
                   .NotNullOrWhiteSpace();
            });
            this.Show(ex.Message);
        }

        [Fact]
        public void True()
        {
            bool bl = true;
            assert.Assert.Build(bl)
                         .True();
            bl = false;
            var ex = Xunit.Assert.Throws<SAEException>(() =>
            {
                assert.Assert.Build(bl)
                  .True();
            });
            this.Show(ex.Message);
        }

        [Fact]
        public void False()
        {
            bool bl = false;
            assert.Assert.Build(bl)
                         .False();
            bl = true;
            var ex = Xunit.Assert.Throws<SAEException>(() =>
            {
                assert.Assert.Build(bl)
                  .False();
            });
            this.Show(ex.Message);
        }


        [Fact]
        public void Null()
        {
            object @object = null;
            assert.Assert.Build(@object)
                         .Null();
            @object = new { Age = 10 };
            var ex = Xunit.Assert.Throws<SAEException>(() => {
                assert.Assert.Build(@object)
                  .Null();
            });
            this.Show(ex.Message);
        }


        [Fact]
        public void NotNull()
        {
            object @object = new { Age = 10 };
            assert.Assert.Build(@object)
                         .NotNull();
            @object = null;
            var ex = Xunit.Assert.ThrowsAny<SAEException>(() =>
            {
                assert.Assert.Build(@object)
                  .NotNull();
            });
            this.Show(ex.Message);
        }
    }
}
