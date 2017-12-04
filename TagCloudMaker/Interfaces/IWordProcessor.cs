﻿using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordProcessor
    {
        IDictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words);
    }
}