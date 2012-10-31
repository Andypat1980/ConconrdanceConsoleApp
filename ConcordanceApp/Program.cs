﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConcordanceApp
{
    //class to hold the word count and line occurrences for each word
    public class WordData
    {
        public int WordCount { get; set; }
        public List<int> LineOccurrences { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //SortedDictionary to store concordance word in sorted order
            SortedDictionary<string, WordData> concordance = new SortedDictionary<string, WordData>();

            //string variable to fetch original text from input.txt file and display on output screen
            string originalInputText ;
            using (System.IO.StreamReader reader = new System.IO.StreamReader("input.txt"))
            {
                int currentLine = 0;
                originalInputText = reader.ReadToEnd().ToString();

                //Need to replace \r\n with blanck space from originalInputText
                string inputText = originalInputText.Replace("\r\n", " ");

                //Getting Sentences from Text using Regex split
                List<String> Sentences = Regex.Split(inputText, @"(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-Z])").ToList();
                

                if (Sentences != null && Sentences.Count > 0)
                {
                    //loop through each sentence 
                    foreach (string sentence in Sentences)
                    {
                        string[] words = sentence.Split(new string[] { ", ", ". ", " ", ",", " \"", "\" ", "? ", "! ", "?", "!" }, System.StringSplitOptions.RemoveEmptyEntries);
                        currentLine++;

                        foreach (string word in words)
                        {
                            if (!concordance.ContainsKey(word))
                            {
                                concordance.Add(word, new WordData() { WordCount = 0, LineOccurrences = new System.Collections.Generic.List<int>() });
                            }

                            concordance[word].WordCount++;
                            concordance[word].LineOccurrences.Add(currentLine);
                        }
                    }
                }
            }

            //Display Original Input text fetched from .txt file
            Console.Write("Input Text: \n " + originalInputText + "\n\n");

            //Display the Output
            foreach (string word in concordance.Keys)
            {
                Console.Write("{0}\t\t\t {1} : ", word, concordance[word].WordCount.ToString());
                Console.Write(concordance[word].LineOccurrences[0].ToString());

                for (int i = 1; i < concordance[word].LineOccurrences.Count; i++)
                {
                    Console.Write(", {0}", concordance[word].LineOccurrences[i].ToString());
                }

                Console.WriteLine();
            }

            System.Console.ReadKey();
        }
    }
}
