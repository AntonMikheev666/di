﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using TagCloud.Interfaces;
using Moq;

namespace TagCloud.Implementations
{
    public class WordProcessor: IWordProcessor
    {
        private readonly IEnumerable<string> badWords;
        private readonly HashSet<string> borringWordsType = new HashSet<string> { "ADVPRO", "APRO", "CONJ", "PART", "PR", "SPRO" };

        public WordProcessor(IEnumerable<string> badWords)
        {
            this.badWords = badWords.Select(w => w.ToLower());
        }

        public IDictionary<string, int> GetFrequencyDictionary(string filePath)
        {
            return MysteamShell.Analyze(filePath)
                .Where(s => !borringWordsType.Any(t => s.Contains(t)))
                .Select(s => s.Substring(0, s.IndexOf("{")).ToLower())
                .Except(badWords.Select(w => w.ToLower()))
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}