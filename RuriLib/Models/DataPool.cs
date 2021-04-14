﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RuriLib.ViewModels;

namespace RuriLib.Models
{
    /// <summary>
    /// The pool where data lines are taken from.
    /// </summary>
    public class DataPool : ViewModelBase
    {
        /// <summary>The IEnumerable of all available data lines.</summary>
        public IEnumerable<string> List { get; set; }

        public List<string[]> Sublists { get; set; }

        /// <summary>The total number of lines.</summary>
        public long Size { get; set; }

        /// <summary>
        /// Creates a DataPool given an IEnumerable and counts the amount of lines.
        /// </summary>
        /// <param name="list">The IEnumerable to pick lines from</param>
        /// <param name="subLists">The subLists 'list'</param>
        public DataPool(IEnumerable<string> list, List<string[]> subLists = null,
            bool globalRemoveDup = false, bool wordlistRemoveDup = false)
        {
            if (globalRemoveDup || wordlistRemoveDup) List = list.Distinct();
            else List = list;
            Size = List.Count();
            Sublists = subLists;
        }

        /// <summary>
        /// Creates a DataPool by loading lines from a file.
        /// </summary>
        /// <param name="fileName">The name of the file to load data lines from</param>
        public DataPool(string fileName)
        {
            List = File.ReadLines(fileName);
            Size = List.Count();
        }

        /// <summary>
        /// Creates a DataPool by generating all the possible combinations of a string.
        /// </summary>
        /// <param name="charSet">The allowed character set (one after the other like in the string "abcdef")</param>
        /// <param name="length">The length of the output combinations</param>
        public DataPool(string charSet, int length)
        {
            List = charSet.Select(x => x.ToString());
            for (int i = 0; i < length; i++)
                List = List.SelectMany(x => charSet, (x, y) => x + y);
            Size = (int)Math.Pow(charSet.Length, length);
        }

        public void RemoveDuplicate()
        {
            try
            {
                if (List?.Count() > 0)
                {
                    List = List.Distinct();
                }
            }
            catch { }
        }

    }
}
