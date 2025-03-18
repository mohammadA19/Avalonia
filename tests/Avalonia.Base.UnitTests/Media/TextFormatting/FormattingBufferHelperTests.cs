using System;
using System.Collections.Generic;
using System.Reflection;
using Avalonia.Media.TextFormatting;
using Avalonia.Utilities;
using Xunit;

namespace Avalonia.Base.UnitTests.Media.TextFormatting
{
    public class FormattingBufferHelperTests
    {
        public static TheoryData<int32> SmallSizes => new() { 1, 500, 10_000, 125_000 };
        public static TheoryData<int32> LargeSizes => new() { 500_000, 1_000_000 };

        [Theory]
        [MemberData(nameof(SmallSizes))]
        public void Should_Keep_Small_Buffer_List(int32 itemCount)
        {
            var capacity = FillAndClearList(itemCount);

            Assert.True(capacity >= itemCount);
        }

        [Theory]
        [MemberData(nameof(LargeSizes))]
        public void Should_Reset_Large_Buffer_List(int32 itemCount)
        {
            var capacity = FillAndClearList(itemCount);

            Assert.Equal(0, capacity);
        }

        private static int32 FillAndClearList(int32 itemCount)
        {
            var list = new List<int32>();

            for (var i = 0; i < itemCount; ++i)
            {
                list.Add(i);
            }

            FormattingBufferHelper.ClearThenResetIfTooLarge(list);

            return list.Capacity;
        }

        [Theory]
        [MemberData(nameof(SmallSizes))]
        public void Should_Keep_Small_Buffer_ArrayBuilder(int32 itemCount)
        {
            var capacity = FillAndClearArrayBuilder(itemCount);

            Assert.True(capacity >= itemCount);
        }

        [Theory]
        [MemberData(nameof(LargeSizes))]
        public void Should_Reset_Large_Buffer_ArrayBuilder(int32 itemCount)
        {
            var capacity = FillAndClearArrayBuilder(itemCount);

            Assert.Equal(0, capacity);
        }

        private static int32 FillAndClearArrayBuilder(int32 itemCount)
        {
            var arrayBuilder = new ArrayBuilder<int32>();

            for (var i = 0; i < itemCount; ++i)
            {
                arrayBuilder.AddItem(i);
            }

            FormattingBufferHelper.ClearThenResetIfTooLarge(ref arrayBuilder);

            return arrayBuilder.Capacity;
        }

        [Theory]
        [MemberData(nameof(SmallSizes))]
        public void Should_Keep_Small_Buffer_Stack(int32 itemCount)
        {
            var capacity = FillAndClearStack(itemCount);

            Assert.True(capacity >= itemCount);
        }

        [Theory]
        [MemberData(nameof(LargeSizes))]
        public void Should_Reset_Large_Buffer_Stack(int32 itemCount)
        {
            var capacity = FillAndClearStack(itemCount);

            Assert.Equal(0, capacity);
        }

        private static int32 FillAndClearStack(int32 itemCount)
        {
            var stack = new Stack<int32>();

            for (var i = 0; i < itemCount; ++i)
            {
                stack.Push(i);
            }

            FormattingBufferHelper.ClearThenResetIfTooLarge(stack);

            var array = (Array) stack.GetType()
                .GetField("_array", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(stack)!;

            return array.Length;
        }

        [Theory]
        [MemberData(nameof(SmallSizes))]
        public void Should_Keep_Small_Buffer_Dictionary(int32 itemCount)
        {
            var capacity = FillAndClearDictionary(itemCount);

            Assert.True(capacity >= itemCount);
        }

        [Theory]
        [MemberData(nameof(LargeSizes))]
        public void Should_Reset_Large_Buffer_Dictionary(int32 itemCount)
        {
            var capacity = FillAndClearDictionary(itemCount);

            Assert.True(capacity <= 3); // dictionary trims to the nearest prime starting with 3
        }

        private static int32 FillAndClearDictionary(int32 itemCount)
        {
            var dictionary = new Dictionary<int32, int32>();

            for (var i = 0; i < itemCount; ++i)
            {
                dictionary.Add(i, i);
            }

            FormattingBufferHelper.ClearThenResetIfTooLarge(ref dictionary);

            var array = (Array) dictionary.GetType()
                .GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(dictionary)!;

            return array.Length;
        }
    }
}
